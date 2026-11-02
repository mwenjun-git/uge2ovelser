
using System.Text;   // Så Encoding kan bruges
using System.Linq;   // Så SequenceEqual kan bruges (hvis den ikke allerede er med)
using dag4;          // Så Header, BitSkriver og BitLæser kan bruges


// Opgave 1: Tjekker bit-operationerne fra papiropgaven
byte x = 0b0110_0001; // Bogstavet 'a' (97), som i opgaven

// Viser et tal som tal, som tegn og som binært tal
void VisN(int n)
{
    // Udskriver tallet, tegnet (char) og den binære form
    Console.WriteLine($"Numerisk er n{n}, som er tegnet {(char)n}, binært {Convert.ToString(n, 2)}");
}

VisN(x);              // Udskriver x selv
int a = x << 1;       // Skubber alle bits én plads til venstre
VisN(a);              // Udskriver resultatet af x << 1

VisN(x >> 3);         // Skubber alle bits tre pladser til højre
VisN(x & 0b1111);     // Beholder kun de nederste 4 bits (AND)
VisN((x >> 5) & 1);   // Henter bit nummer 5 (skub ned, og behold kun den nederste)
VisN(x | 0b0000_0110); // Sætter bit 1 og 2 til 1 (OR)

// Modulus: viser resten ved division med 5
for (int i = 0; i < 25; i++)
{
    if (i % 5 == 0) // Hver gang i går op i 5
    {
        Console.Write(" "); // Skriver et mellemrum
        Console.WriteLine(); // Og starter en ny linje
    }
    Console.WriteLine($"{i} % 5 = {i % 5}"); // Udskriver i og resten
}

// Opgave 2: BitSkriver
var bs = new dag4.BitSkriver(); // Opretter en ny BitSkriver

// Første byte: 0000 1011
bs.SkrivBit(0);
bs.SkrivBit(0);
bs.SkrivBit(0);
bs.SkrivBit(0);
bs.SkrivBit(1);
bs.SkrivBit(0);
bs.SkrivBit(1);
bs.SkrivBit(1);

// Anden byte: 0000 1010
bs.SkrivBit(0);
bs.SkrivBit(0);
bs.SkrivBit(0);
bs.SkrivBit(0);
bs.SkrivBit(1);
bs.SkrivBit(0);
bs.SkrivBit(1);
bs.SkrivBit(0);

// Tredje byte: skriver en hel kode på én gang (mellemrum ignoreres)
bs.SkrivKode("0000 0010");

// To bits mere, så den sidste byte ikke er fyldt helt op
bs.SkrivBit(1);
bs.SkrivBit(0);
Console.WriteLine();

// Læser PaddedBits FØR TilArray(), ellers er antalBits allerede nulstillet
Console.WriteLine($"PaddedBits: {bs.PaddedBits}"); // Skal give 6

var bytes = bs.TilArray(); // Fylder sidste byte op med 0-bits og henter alle bytes

// Udskriver hver byte som tal
foreach (var b in bytes)
{
    Console.Write($"{b},"); // Skal give 11,10,2,128,
}
Console.WriteLine();
Console.WriteLine();

// Opgave 3: Skriver koden "101". Det skal give 10100000
var skriver = new dag4.BitSkriver(); // Opretter en ny BitSkriver
skriver.SkrivKode("101");            // Skriver bits 1, 0, 1

byte[] resultat = skriver.TilArray(); // Fylder op med 0-bits og henter bytes

// Udskriver hver byte som 8 binære cifre
foreach (byte b in resultat)
{
    Console.WriteLine(Convert.ToString(b, 2).PadLeft(8, '0')); // PadLeft sætter 0'er foran
}
Console.WriteLine();

// Opgave 4: BitLæs
// Testdata: tre bytes
byte[] data = { 202, 5, 12 };

// Opretter læseren, som starter ved byte nummer 0
BitLæser læser = new BitLæser(data, 0);

// 3 bytes x 8 bits = 24 bits der skal læses
for (int i = 0; i < 24; i++)
{
    Console.Write(læser.LæsBit()); // Læser én bit og udskriver den

    // Mellemrum efter hver 8. bit, så bytes er adskilt
    if (i % 8 == 7) // Er vi lige blevet færdige med en byte?
    {
        Console.Write(" "); // Så udskriver vi et mellemrum
    }
}
Console.WriteLine();

// Opgave 5: Tester headeren
byte[] tekst = Encoding.UTF8.GetBytes("Rødgrød med fløde");

// Tæller frekvenser: dag4 har ikke Kompression, så vi tæller selv
int[] freq = new int[256];
foreach (byte b in tekst)
{
    freq[b]++; // Byte-værdien bruges som plads
}

byte[] header = Header.LavHeader(tekst, freq);   // Skriver headeren
int[] freq2 = new int[256];                      // Tom tabel til at læse ind i
int længde = Header.LæsHeader(header, freq2);    // Læser headeren tilbage

Console.WriteLine($"Header-størrelse: {header.Length}"); // Skal give 1028
Console.WriteLine($"Længde: {længde}");                  // Skal give 20
Console.WriteLine(freq.SequenceEqual(freq2) ? "OK" : "FEJL"); // Skal give OK


