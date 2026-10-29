//Console.WriteLine("Hello, World!");
using dag1;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;
//// Konverterer strengen "Søren" til et byte-array efter UTF-8-reglerne og 
///gemmer resultatet i variablen data.
byte[] data = Encoding.UTF8.GetBytes("Søren");

///Konverterer bytes tilbage til en streng (UTF-8)
///gemmer resultatet i variablen tekst.
string tekst =Encoding.UTF8.GetString(data);
Console.WriteLine(tekst);
foreach (byte b in data)
    Console.Write($"{b},");
Console.WriteLine();
// Udskriver bytes i data adskilt af komma.
Console.WriteLine(string.Join(",",data));


//Opgave 1
BytesOgTegn();
byte[] data1 = Encoding.UTF8.GetBytes("Søren");
byte[] kopi = Kompression.KopiKomprimer(data1);
byte[] kopi1 = Kompression.KopiDekomprimer(kopi);
Console.WriteLine($"Kopierer data:" + string.Join(",", kopi1));

void BytesOgTegn()
{
    string str = "æøå";
    byte[] bStr = Encoding.UTF8.GetBytes(str);
    Console.WriteLine($"String Length : {str.Length}");
    Console.WriteLine($"Byte Length :{bStr.Length}");
    //Samler bytes i bStr til én streng adskilt af komma.
    Console.WriteLine(string.Join(",", bStr));
}

  //foreach (byte b in bStr) 
  //opgave 3
    byte[] a = { 1, 2, 3 };
    byte[] d = { 1, 3, 3 };

    Console.WriteLine(Kompression.ErEns(a, d));
    Console.WriteLine(a == d);
//opgave 4
List<byte[]>testinput = new List<byte[]>();
testinput.Add(new byte[0]);                           //et tomt array
testinput.Add(Encoding.UTF8.GetBytes("a"));           // ét tegn
testinput.Add(Encoding.UTF8.GetBytes("aaaaaaaaaa"));  // en tekst med kun ét forskelligt tegn,fx"aaaaaaaaaa"                                                     
testinput.Add(Encoding.UTF8.GetBytes("æøå"));         // en tekst med æ, ø og å
testinput.Add(Encoding.UTF8.GetBytes("Det er en lang tekst, som bruges til at teste komprimering af data."));
byte[] alle = new byte[256];//arrayet skal have plads til 256 elementer
for (int i = 0; i < 256; i++)
{
    alle[i] = (byte)i;//laver værdien i om til typen byte
}
testinput.Add(alle);
//Opretter et array med 100 pladser og fylder det med tilfældige bytes.
byte[] tilfældige = new byte[100];


Random rnd = new Random();
rnd.NextBytes(tilfældige);

testinput.Add(tilfældige);

//opgave 5
foreach (byte[] input in testinput)
{
    byte[] komprimeret = Kompression.KopiKomprimer(input);
    byte[] gendannet = Kompression.KopiDekomprimer(komprimeret);
    bool ok = Kompression.ErEns(input,gendannet);

    string resultat;
    if (ok)
    {
        resultat = "OK";
    }
    else
    {
        resultat = "FEJL";
    }
    Console.WriteLine($"{resultat}");
}

//opgave 7
//
UdskrivFrekvenser(Encoding.UTF8.GetBytes("Rødgrød med fløde"));
void UdskrivFrekvenser(byte[] data)
{
    int[] antal = Kompression.TælFrekvenser(data);
    for (int b = 0; b < 256; b++)
    {
        if(b>=32 && b<=126)
        {
            Console.WriteLine($"{b}'{(char)b}:{antal[b]}");
        }
        else
        {
            Console.WriteLine($"{b}:{antal[b]}");
        }
    }
}
