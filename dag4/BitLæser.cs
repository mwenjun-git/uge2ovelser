using System;
using System.Collections.Generic;
using System.Text;

namespace dag4
{
    // Klasse der læser bits ud af et byte-array, i samme rækkefølge som BitSkriver skrev dem
    public class BitLæser
    {
        // Det byte-array vi læser fra
        private byte[] data;

        // Hvilken byte i arrayet vi er nået til
        private int byteIndex;

        // Hvilken bit i den aktuelle byte (0 = første bit der læses, 7 = sidste)
        private int bitIndex;

        // Konstruktør: husker data og hvor vi skal starte
        public BitLæser(byte[] data, int startindex)
        {
            this.data = data;       // Gemmer arrayet
            byteIndex = startindex; // Starter ved den angivne byte
            bitIndex = 0;           // Starter ved den første (øverste) bit
        }

        // Læser én bit (0 eller 1)
        public int LæsBit()
        {
            // Skubber den ønskede bit helt ned til plads 0 og maskerer resten væk
            // bitIndex 0 giver >> 7 (øverste bit), bitIndex 7 giver >> 0 (nederste bit)
            int bit = (data[byteIndex] >> (7 - bitIndex)) & 1;

            bitIndex++; // Går videre til næste bit

            if (bitIndex == 8) // Når alle 8 bits i byten er læst
            {
                bitIndex = 0;  // Starter forfra i næste byte
                byteIndex++;   // Går til næste byte
            }

            return bit; // Returnerer den læste bit
        }
    }
}