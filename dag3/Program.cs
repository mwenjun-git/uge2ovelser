using dag3;
using System;
using System.Collections.Generic;
using System.Text;

// Opgave 2: Test the Node constructors
byte a = (byte)'a';
byte b = (byte)'b';

Node blad1 = new Node(a, 1);
Node blad2 = new Node(b, 1);
// Print the leaf nodes
Console.WriteLine(blad1);
Console.WriteLine($"{blad1.Værdi}, {blad1.Frekvens}, {blad1.Id}");
Console.WriteLine($"{blad2.Værdi}, {blad2.Frekvens}, {blad2.Id}");
// Create an internal node
int nodeID = 256; // bin 100000000
Node node1 = new Node(blad1, blad2, nodeID++);
// Print the internal node
Console.WriteLine(node1);
Console.WriteLine($"{node1.Værdi}, {node1.Frekvens}, {node1.Id}, {node1.Venstre.Id}, {node1.Højre.Id}");

// Opgave 1 / Test preparation: Count character frequencies
static Dictionary<byte, int> TælFrekvenser(byte[] data)
{
    Dictionary<byte, int> d = new Dictionary<byte, int>();
    foreach (byte b in data)
    {
        if (!d.ContainsKey(b))
        {
            d.Add(b, 0);
        }

        d[b]++;
    }
    return d;
}

// Opgave 1: Test with the text "abracadabra"
string tekst = "abracadabra";
// Convert the text into bytes
byte[] byteTekst = Encoding.UTF8.GetBytes(tekst);
// Count the frequencies
Dictionary<byte, int> frekvenser = TælFrekvenser(byteTekst);
// Print character frequencies
Console.WriteLine("\nCharacter frequencies:");

foreach (var par in frekvenser)
{
    Console.WriteLine($"{(char)par.Key}: {par.Value}");
}

// Opgave 3 and 4: Build the Huffman tree
Node? rootNode = Huffman.BygTræ(frekvenser);

// Opgave 5: Print the Huffman tree
Console.WriteLine("\nHuffman tree:");
Huffman.UdskrivTræ(rootNode);


// Opgave 6: Generate Huffman codes
string[] koder = new string[256];

Huffman.DanKoder(rootNode, "", koder);

// Print the codes
Console.WriteLine("\nHuffman codes:");

foreach (var par in frekvenser)
{
    Console.WriteLine($"{(char)par.Key}: {koder[par.Key]}");
}

// Opgave 7: Compress the text
string komprimeret = Huffman.Komprimer(byteTekst, koder);

Console.WriteLine("\nCompressed text:");
Console.WriteLine(komprimeret);
// Compare the original and compressed lengths
Console.WriteLine($"Original size: {byteTekst.Length * 8} bits");
Console.WriteLine($"Compressed size: {komprimeret.Length} bits");

// Opgave 8: Decompress the text
byte[] dekomprimeret = Huffman.Dekomprimer(komprimeret, rootNode);
// Convert the bytes back into text
string gendannetTekst = Encoding.UTF8.GetString(dekomprimeret);

Console.WriteLine("\nDecompressed text:");
Console.WriteLine(gendannetTekst);

// Opgave 9: Test whether decompression works
Console.WriteLine("\nTest result:");

if (tekst == gendannetTekst)
{
    Console.WriteLine("Success: The text matches!");
}
else
{
    Console.WriteLine("Error: The text does not match!");
}