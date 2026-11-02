using System;
using System.Collections.Generic;
using System.Text;

namespace dag4
{
        // Klasse der skriver og læser headeren i den komprimerede fil
        public static class Header
        {
            // Laver headeren = oprindelig længde (4 bytes) + frekvenstabel (256 x 4 bytes)
            public static byte[] LavHeader(byte[] data, int[] frekvenser)
            {
                // Headeren er 4 + 256 * 4 = 1028 bytes lang
                byte[] header = new byte[4 + 256 * 4];

                // Laver den oprindelige længde om til 4 bytes
                byte[] længdeBytes = BitConverter.GetBytes(data.Length);

            // Kopierer de 4 bytes ind forrest i headeren (into the front of)(plads 0)
            Array.Copy(længdeBytes, 0, header, 0, 4);

                // Gennemgår alle 256 pladser i frekvenstabellen
                for (int i = 0; i < 256; i++)
                {
                    // Laver frekvensen for byte-værdi i om til 4 bytes
                    byte[] frekBytes = BitConverter.GetBytes(frekvenser[i]);

                    // Kopierer dem ind på plads 4 + i * 4 (efter længden)
                    Array.Copy(frekBytes, 0, header, 4 + i * 4, 4);
                }

                // Returnerer den færdige header
                return header;
            }

        // Læser headeren: fylder frekvenstabellen og returnerer den oprindelige længde
        // Indeholder hele den komprimerede fils data som en byte-array--byte[] fil
        // Bruges til at gemme de 256 frekvensværdier, der bliver læst fra filen--int[] frekvenser
        public static int LæsHeader(byte[] fil, int[] frekvenser)
            {
                // Læser de første 4 bytes som et heltal = oprindelig længde
                int længde = BitConverter.ToInt32(fil, 0);

                // Gennemgår alle 256 pladser i frekvenstabellen
                for (int i = 0; i < 256; i++)
                {
                    // Læser 4 bytes fra plads 4 + i * 4 og gemmer dem som frekvens
                    frekvenser[i] = BitConverter.ToInt32(fil, 4 + i * 4);
                }

                // Returnerer den oprindelige længde
                return længde;
            }
        }
    }


