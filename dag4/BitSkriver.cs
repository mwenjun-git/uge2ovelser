using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace dag4

{

    // Klasse der samler enkelte bits til hele bytes
    public class BitSkriver
    {
        //list af bytes=List<byte>
        
        // Liste med de færdige bytes (8 bits hver)
        List<byte> bytes = new List<byte>();

        //(antal bit som er modtaget)
        // Den byte der er ved at blive fyldt op
        private int aktuelByte = 0;
        // Antal bits der er skrevet i den aktuelle byte (0-7)
        private int antalBits = 0;

        // Antal 0-bits der mangler for at fylde den aktuelle byte op til 8
        public int PaddedBits
        {
            get { return (8 - antalBits) % 8; } // % 8 gør at en tom byte giver 0 i stedet for 8
        }



        //SkiverBit(bit)
        //-skub aktuel byte til venstre
        //-sæt ny bit(ind i aktuel byte)
        //-nåe 8 bits er modtaget, put aktuel byte i listen, og start forfra med aktuel byte
        // Skriver én bit (0 eller 1)
        public void SkrivBit(int bit)
        {
            bit = bit & 0b1;                    // Sikrer at kun den nederste bit bruges
            aktuelByte = aktuelByte << 1;       // Skubber aktuel byte én plads til venstre
            aktuelByte = aktuelByte | bit;      // Sætter den nye bit nederst
            antalBits += 1;                     // Der er nu én bit mere i den aktuelle byte

            if (antalBits == 8)                 // Når der er 8 bits, er byten færdig
            {
                bytes.Add((byte)aktuelByte);    // Lægger den færdige byte i listen
                aktuelByte = 0;                 // Starter forfra med en tom byte
                antalBits = 0;                  // Nulstiller tælleren
            }
        }

        //SkrivKode(kodeString)
        //overføre en bit string f.eks "0101110" til skrive bit
        // resultatet bliver en byte med bit´sne fra strengen.
        // Skriver en kode som "101" bit for bit
        public void SkrivKode(string kode)
        {
            foreach (char c in kode)            // Gennemgår hvert tegn i strengen
            {
                if (c == '1')                   // Tegnet '1' giver bit 1
                {
                    SkrivBit(1);
                }
                else if (c == '0')              // Tegnet '0' giver bit 0
                {
                    SkrivBit(0);
                }
            }
        }



        //TilArray()
        //Konvertere listen til en byte[],men
        //først afsluttets evt aktuel byte


        // Fylder sidste byte op med 0-bits og returnerer alle bytes
        public byte[] TilArray()
        {
            if(antalBits>0)  // Hvis der er bits i den aktuelle byte, der ikke er fyldt helt op
            {
                while(antalBits>0)// Så længe den aktuelle byte ikke er tom
                {
                    SkrivBit(0); // Fylder op med 0-bits (nulstilles ved 8 bits)
                }
            
            }
            return bytes.ToArray<byte>(); // Konverterer listen til et byte-array
        }
    }
}
