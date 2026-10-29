using dag3;
using System;
using System.Collections.Generic;
using System.Text;

namespace dag3
{
    internal class Huffman
    {
        // Opgave 3 and 4: Build the Huffman tree
        // Return a Node or null
        public static Node? BygTræ(Dictionary<byte, int> frekvenser)
        {
            // Create a list to store all nodes
            var noder = new List<Node>();

            // Create a leaf node for each byte and its frequency
            foreach ((byte k, int v) in frekvenser)
            {
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
    }
}

