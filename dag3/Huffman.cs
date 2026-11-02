using dag3;
using System;
using System.Collections.Generic;
using System.Text;

namespace dag3
{
    public class Huffman
    {
        // Opgave 3 og 4: Bygger Huffman-træet
        // Returnerer en Node eller null
        public static Node? BygTræ(Dictionary<byte, int> frekvenser)
        {
            // Create a list to store all nodes
            var noder = new List<Node>();

            // Create a leaf node for each byte and its frequency
            foreach ((byte k, int v) in frekvenser)
            {
                //k er byte-værdien. v er frekvensen (antal gange, byte-værdien forekommer)
                var node = new Node(k, v);
                noder.Add(node);
            }

            // Return null if there is no data
            if (noder.Count == 0)
                return null;

            // The ID for the first internal node starts at 256
            int næsteId = 256;

            // Repeat until only one node remains
            while (noder.Count > 1)
            {
                // Sort by frequency, then by ID if frequencies are equal
                noder.Sort((a, b) => a.Frekvens != b.Frekvens
                    ? a.Frekvens.CompareTo(b.Frekvens)
                    : a.Id.CompareTo(b.Id));

                // Get the node with the lowest frequency
                Node mindst = noder[0];

                // Get the node with the second-lowest frequency
                Node næstMindst = noder[1];

                // Combine the two nodes into a new parent node
                Node samler = new Node(mindst, næstMindst, næsteId++);

                // Add the new parent node
                noder.Add(samler);

                // Remove the two original nodes
                noder.RemoveAt(0);
                noder.RemoveAt(0);
            }

            // Return the root node of the Huffman tree
            return noder[0];
        }

        // Opgave 5: Print the Huffman tree
        public static void UdskrivTræ(Node? node, int indryk = 0)
        {
            // Stop if the node is null
            if (node == null)
                return;

            string tekst;

            // Print the character and frequency for a leaf node
            if (node.ErBlad)
            {
                tekst = $"{(char)node.Værdi} ({node.Frekvens})";
            }
            else
            {
                // Print the ID and frequency for an internal node
                tekst = $"Node {node.Id} ({node.Frekvens})";
            }

            // Print the current node with indentation
            Console.WriteLine(new string(' ', indryk) + tekst);

            // Recursively print the left child
            UdskrivTræ(node.Venstre, indryk + 4);

            // Recursively print the right child
            UdskrivTræ(node.Højre, indryk + 4);
        }
        // Opgave 6: Generate Huffman codes recursively
        public static void DanKoder(Node? node, string sti, string[] koder)
        {
            // Stop if the node is null
            if (node == null)
                return;

            // If the node is a leaf, save its code
            if (node.ErBlad)
            {
                koder[node.Værdi] = sti;
                return;
            }

            // Go left and add 0 to the path
            DanKoder(node.Venstre, sti + "0", koder);

            // Go right and add 1 to the path
            DanKoder(node.Højre, sti + "1", koder);
        }
        // Opgave 7: Compress the input bytes
        public static string Komprimer(byte[] data, string[] koder)
        {
            // Create a StringBuilder to collect the codes
            StringBuilder sb = new StringBuilder();

            // Replace each byte with its Huffman code
            foreach (byte b in data)
            {
                sb.Append(koder[b]);
            }

            // Return the compressed string
            return sb.ToString();
        }
        // Opgave 8: Decompress the encoded string
        public static byte[] Dekomprimer(string komprimeret, Node? rod)
        {
            // Return an empty byte array if the tree is empty
            if (rod == null)
                return Array.Empty<byte>();

            // Store the decoded bytes
            List<byte> resultat = new List<byte>();

            // Start at the root
            Node? aktuel = rod;

            // Handle a tree containing only one leaf
            if (rod.ErBlad)
            {
                foreach (char bit in komprimeret)
                {
                    if (bit != '0')
                        throw new ArgumentException("Invalid Huffman code.");

                    resultat.Add(rod.Værdi);
                }

                return resultat.ToArray();
            }

            // Read the encoded string one character at a time
            foreach (char bit in komprimeret)
            {
                // Go left for 0
                if (bit == '0')
                {
                    aktuel = aktuel!.Venstre;
                }
                // Go right for 1
                else if (bit == '1')
                {
                    aktuel = aktuel!.Højre;
                }
                else
                {
                    throw new ArgumentException("Invalid character in encoded string.");
                }

                // Stop if the path does not exist
                if (aktuel == null)
                    throw new ArgumentException("Invalid Huffman code.");

                // If a leaf is reached, output its byte and restart at the root
                if (aktuel.ErBlad)
                {
                    resultat.Add(aktuel.Værdi);
                    aktuel = rod;
                }
            }

            // Return the decoded bytes
            return resultat.ToArray();
        }

        // Komprimerer data til et byte-array = header + Huffman-kodede bits
        public static byte[] KomprimerTilBytes(byte[] data)
        {
            // Tæller frekvenser i en int[256] (bruges til headeren)
            int[] freq = new int[256];
            foreach (byte b in data)
            {
                freq[b]++; // Byte-værdien bruges som plads
            }

            // Laver en Dictionary med kun de bytes, der forekommer
            var dict = new Dictionary<byte, int>();
            for (int i = 0; i < 256; i++)
            {
                if (freq[i] > 0)
                {
                    dict[(byte)i] = freq[i];
                }
            }

            // Bygger træet og danner koderne
            Node? rod = BygTræ(dict);
            string[] koder = new string[256];
            DanKoder(rod, "", koder);

            // Skriver hver kode som bits med BitSkriver
            var skriver = new dag4.BitSkriver();
            foreach (byte b in data)
            {
                skriver.SkrivKode(koder[b]);
            }
            byte[] bitData = skriver.TilArray(); // Fylder op med 0-bits

            // Laver headeren (længde + frekvenstabel)
            byte[] header = dag4.Header.LavHeader(data, freq);

            // Samler header og bit-data
            byte[] resultat = new byte[header.Length + bitData.Length];
            Array.Copy(header, 0, resultat, 0, header.Length);
            Array.Copy(bitData, 0, resultat, header.Length, bitData.Length);
            return resultat;
        }

        // Dekomprimerer et byte-array lavet af KomprimerTilBytes
        public static byte[] DekomprimerFraBytes(byte[] fil)
        {
            // Læser headeren: oprindelig længde og frekvenstabel
            int[] freq = new int[256];
            int længde = dag4.Header.LæsHeader(fil, freq);

            // Bygger præcis samme træ som kompressoren
            var dict = new Dictionary<byte, int>();
            for (int i = 0; i < 256; i++)
            {
                if (freq[i] > 0)
                {
                    dict[(byte)i] = freq[i];
                }
            }
            Node? rod = BygTræ(dict);

            byte[] resultat = new byte[længde];
            if (rod == null) return resultat; // Tom fil

            // Specialtilfælde: kun én slags byte
            if (rod.ErBlad)
            {
                Array.Fill(resultat, rod.Værdi);
                return resultat;
            }

            // Læser bits efter headeren (plads 1028)
            var læser = new dag4.BitLæser(fil, 4 + 256 * 4);
            for (int i = 0; i < længde; i++)
            {
                Node aktuel = rod;
                while (!aktuel.ErBlad)
                {
                    aktuel = læser.LæsBit() == 0 ? aktuel.Venstre! : aktuel.Højre!;
                }
                resultat[i] = aktuel.Værdi;
            }
            return resultat;
        }
    }
}

