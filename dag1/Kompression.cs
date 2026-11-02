using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace dag1
{
    public class Kompression
    {

        //Opgave2 
        // "Komprimerer" data ved blot at kopiere hver byte
        // Metoden tager et array med rå data og returnerer en kopi af det.
        public static byte[] KopiKomprimer(byte[] råData)
        {
            // Opretter et nyt byte-array med samme længde som det oprindelige array.
            byte[] kopi = new byte[råData.Length];
            // Løber gennem alle pladser i det oprindelige array.
            for (int i = 0; i < råData.Length; i++)
            {
                //Byten kopieres på plads og sættes i det nye array.
                kopi[i] = råData[i];
            }
            return kopi;
        }

        // "Dekomprimerer" data ved blot at kopiere hver byte tilbage.
        // Da der ikke er komprimeret noget, er det samme fremgangsmåde som ovenfor.
        public static byte[] KopiDekomprimer(byte[] komprimeretData)
        {// Opretter et nyt byte-array med samme længde som de komprimerede data.
            byte[] kopi = new byte[komprimeretData.Length];

            // Løber gennem alle pladser i det komprimerede array.
            for (int i = 0; i < komprimeretData.Length; i++)
            {// Kopierer byten på plads i over i det nye array.
                kopi[i] = komprimeretData[i];
            }
            // Returnerer de dekomprimerede data
            return kopi;
        }
        // Opgave 3: Sammenlign arrays
        // Sammenligner to byte-arrays og returnerer true,
        // hvis de har samme længde og samme indhold.
        public static bool ErEns(byte[] a, byte[] b)
        {  // Hvis længderne er forskellige, kan arrays aldrig være ens.
            if (a.Length != b.Length)
            {
                return false;
            }
            // Løber gennem alle pladser og sammenligner element for element.
            for (int i = 0; i < a.Length; i++)
            { // Hvis bare én byte er forskellig, er arrays ikke ens.
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            // Længden er ens, og alle bytes er ens, så arrays har samme indhold.
            return true;
        }
        
        // Opgave 6: Tæller hvor mange gange hver byte-værdi (0-255) forekommer
        public static int[] TælFrekvenser(byte[] data)
        { // Opretter et array med 256 pladser, alle starter på 0
            int[] antal = new int[256];
            {    // Gennemgår hver byte i data
                foreach (byte b in data)
                { // Byte-værdien b bruges som plads, og tælleren på den plads øges med 1
                    antal[b]++;
                }
            }
            // Returnerer frekvenstabellen
            return antal;

        }
    }
}






    

