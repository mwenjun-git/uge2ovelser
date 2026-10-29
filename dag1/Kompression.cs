using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace dag1
{
    internal class Kompression
    {

        //Opgave2 
        public static byte[] KopiKomprimer(byte[] råData)
        {
            //et array af bytes
            byte[] kopi = new byte[råData.Length];
            for (int i = 0; i < råData.Length; i++)
            {
                kopi[i] = råData[i];
            }
            return kopi;
        }

        public static byte[] KopiDekomprimer(byte[] komprimeretData)
        {
            byte[] kopi = new byte[komprimeretData.Length];
            for (int i = 0; i < komprimeretData.Length; i++)
            {
                kopi[i] = komprimeretData[i];
            }
            return kopi;
        }
        public static bool ErEns(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static int[] TælFrekvenser(byte[] data)
        {
            int[] antal = new int[256];
            {
                foreach (byte b in data)
                {
                    antal[b]++;
                }
            }
            return antal;

        }
    }
}






    

