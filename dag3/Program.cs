using dag3;
using System;
using System.Collections.Generic;
using System.IO.Compression; // Til GZipStream og CompressionLevel
using System.Linq;
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
;

// Tester at komprimering + dekomprimering giver samme data
byte[] data = System.Text.Encoding.UTF8.GetBytes("Rødgrød med fløde");
byte[] komp = Huffman.KomprimerTilBytes(data);
byte[] tilbage = Huffman.DekomprimerFraBytes(komp);

Console.WriteLine($"Før: {data.Length}, efter: {komp.Length}");
Console.WriteLine(data.SequenceEqual(tilbage) ? "OK" : "FEJL");

// Dag5
// Opgave 2 + 3: Måler Huffman og GZip på rigtige filer
string mappe = @"C:\Users\whbtz\Desktop\test\"; // Din virkende sti
string[] filer = { "Danske_bog.txt", "kodetest.cs", "billede.jpg" };

// Overskrift til tabellen
Console.WriteLine("\nFil | Original | Huffman | Huffman % | Huffman ms | GZip | GZip % | GZip ms | Test");

foreach (string navn in filer)
{
    byte[] original = File.ReadAllBytes(mappe + navn); // Læser filen

    var sw = System.Diagnostics.Stopwatch.StartNew(); // Starter stopur
    byte[] huff = Huffman.KomprimerTilBytes(original); // Huffman-komprimering (kun ÉN gang)
    sw.Stop();                                         // Stopper stopuret
    long huffMs = sw.ElapsedMilliseconds;              // Tid for Huffman

    // Dekomprimerer igen for at tjekke, at filen pakkes korrekt ud
    byte[] gendannet = Huffman.DekomprimerFraBytes(huff);
    string test = original.SequenceEqual(gendannet) ? "OK" : "FEJL";

    sw.Restart();                                      // Nulstiller og starter stopuret igen
    byte[] gz = GZipKomprimer(original);               // GZip-komprimering
    sw.Stop();
    long gzMs = sw.ElapsedMilliseconds;                // Tid for GZip

    double huffPct = 100.0 * huff.Length / original.Length; // Komprimeret / original
    double gzPct = 100.0 * gz.Length / original.Length;

    Console.WriteLine($"{navn} | {original.Length} | {huff.Length} | {huffPct:F1}% | {huffMs} | {gz.Length} | {gzPct:F1}% | {gzMs} | {test}");
}
//Dag5 Opgave3
// Komprimerer data med GZip og returnerer det komprimerede byte-array
static byte[] GZipKomprimer(byte[] data)
{
    using var ud = new MemoryStream(); // Hukommelses-strøm, der gemmer resultatet
    using (var gzip = new GZipStream(ud, CompressionLevel.Optimal)) // Opretter GZip-strømmen
    {
        gzip.Write(data, 0, data.Length); // Skriver data ind i GZip
    } // Strømmen skal lukkes HER, ellers er GZip ikke færdig med at skrive
    return ud.ToArray(); // Henter de komprimerede bytes
}
//Opgave4+5
/*1.Hvilken filtype komprimeres bedst?
Tekst. Bogen kommer ned på 60,0 % med Huffman og 37,5 % med GZip, fordi
bogstavernes hyppighed er meget ujævn (mellemrum og "e" er hyppige).

2. Hvorfor kan JPEG ikke komprimeres?
JPEG er allerede komprimeret. Bytes ligner tilfældige tal, alle 256 værdier
forekommer omtrent lige ofte, og der er ingen gentagelser. Huffman giver 99,8 %
og GZip 99,9 %.

3. Hvorfor er GZip bedre end Huffman på tekst?
Huffman ser kun på, hvor ofte hver enkelt byte forekommer. GZip bruger først
LZ77, som erstatter gentagne stykker med en henvisning, og Huffman-koder bagefter.

4. Hvorfor klarer kodetest.cs sig dårligt med Huffman (81,9 %)?
Filen er kun 5.478 bytes, og headeren er fast 1028 bytes (ca. 19 %). Uden
headeren er data ca. 63 %, næsten som bogen.

5. Hvilken er hurtigst?
GZip. På JPEG tog Huffman 737 ms mod 119 ms for GZip, fordi min kode behandler
bits via strenge, som er langsomt.*/