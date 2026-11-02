
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
// Udskriver den genskabte tekst i konsollen (giver "Søren").
Console.WriteLine(tekst);
// Løber gennem hver byte i data og udskriver med komma
foreach (byte b in data)
    Console.Write($"{b},");
Console.WriteLine();
// Udskriver bytes i data adskilt af komma.
Console.WriteLine(string.Join(",",data));
Console.WriteLine();



//Opgave 1
// Kalder metoden, der udskriver længden af strengen og byte-arrayet for "æøå".
void BytesOgTegn()
{   // Strengen med de tre danske specialtegn.
    string str = "æøå";

    // Koder strengen til bytes (UTF-8).
    byte[] bStr = Encoding.UTF8.GetBytes(str);

    // Length på strengen tæller tegn (giver 3).
    Console.WriteLine($"String Length : {str.Length}");

    // Length på arrayet tæller bytes (giver 6), fordi æ, ø og å
    Console.WriteLine($"Byte Length :{bStr.Length}");

    //Samler bytes i bStr til én streng adskilt af komma.
    Console.WriteLine(string.Join(",", bStr));
    Console.WriteLine();

}
//opgave 2
BytesOgTegn();
// Koder strengen "Søren" til et byte-array efter UTF-8-reglerne.
byte[] data1 = Encoding.UTF8.GetBytes("Søren");
// "Komprimerer" data1 ved at kopiere bytes
byte[] kopi = Kompression.KopiKomprimer(data1);
// "Dekomprimerer" ved at kopiere bytes tilbage til et nyt array.
byte[] kopi1 = Kompression.KopiDekomprimer(kopi);
// Udskriver de kopierede bytes adskilt af komma.
Console.WriteLine($"Kopierer data:" + string.Join(",", kopi1));
Console.WriteLine();



//foreach (byte b in bStr) 
//opgave 3
byte[] a = { 1, 2, 3 };
    byte[] d = { 1, 3, 3 };

    Console.WriteLine(Kompression.ErEns(a, d));
    Console.WriteLine(a == d);
    Console.WriteLine();

//opgave 4
// Liste der indeholder alle testinput - hvert element er et byte-array
List<byte[]>testinput = new List<byte[]>();

// Test 1: Et tomt array 
testinput.Add(new byte[0]);
// Test 2:  ét tegn
testinput.Add(Encoding.UTF8.GetBytes("a"));   
testinput.Add(Encoding.UTF8.GetBytes("aaaaaaaaaa"));  // en tekst med kun ét forskelligt tegn,fx"aaaaaaaaaa"                                                     
testinput.Add(Encoding.UTF8.GetBytes("æøå"));         // en tekst med æ, ø og å
// Test 5: En længere tekst til at teste almindelig komprimering
testinput.Add(Encoding.UTF8.GetBytes("Det er en lang tekst, som bruges til at teste komprimering af data."));

byte[] alle = new byte[256];//arrayet skal have plads til 256 elementer
// Løkken kører 256 gange (i = 0 til 255)
for (int i = 0; i < 256; i++)
{
    alle[i] = (byte)i;//Værdien konverteres til typen byte og gemmes på plads i arrayet.  (værdi til byte gemmes i array)

}
// Tilføjer det samme array "alle" til listen 
testinput.Add(alle);

//Opretter et array med 100 pladser og fylder det med tilfældige bytes.
byte[] tilfældige = new byte[100];

// Opretter et Random-objekt til at lave tilfældige tal
Random rnd = new Random();

// Fylder hele arrayet med tilfældige byte-værdier
rnd.NextBytes(tilfældige);

// Tilføjer det tilfældige array til listen
testinput.Add(tilfældige);


// Opgave 5: Testprogram - gennemgår hvert testinput
foreach (byte[] input in testinput)
{// Komprimerer inputtet
    byte[] komprimeret = Kompression.KopiKomprimer(input);

    // Dekomprimerer igen
    byte[] gendannet = Kompression.KopiDekomprimer(komprimeret);

    // Sammenligner original og gendannet
    bool ok = Kompression.ErEns(input,gendannet);

    // Vælger tekst ud fra resultatet
    string resultat;
    if (ok) // Hvis original og gendannet er ens
    {
        resultat = "OK";
    }
    else // Hvis de er forskellige
    {
        resultat = "FEJL";
    }
    Console.WriteLine($"{resultat}");
}
Console.WriteLine();

// Opgave 6: Kalder TælFrekvenser på en tekst
byte[] data2 = Encoding.UTF8.GetBytes("aaaaaaaaab");        // Laver teksten om til bytes
int[] antal6 = Kompression.TælFrekvenser(data2);            // Bruger data2 (ikke data)
Console.WriteLine(antal6[97]);                              // Skal give 9
Console.WriteLine();


// Opgave 7: Kalder metoden med teksten "Rødgrød med fløde" omdannet til bytes
UdskrivFrekvenser(Encoding.UTF8.GetBytes("Rødgrød med fløde"));

// Udskriver frekvenstabellen for et byte-array
void UdskrivFrekvenser(byte[] data)
{   // Tæller hvor mange gange hver byte-værdi (0-255) forekommer
    int[] antal = Kompression.TælFrekvenser(data);

    // Løber gennem alle 256 mulige byte-værdier
    for (int b = 0; b < 256; b++)
    { 
        if (antal[b] > 0) // Kun byte-værdier der forekommer mindst én gang

        // Hvis værdien er et synligt tegn (mellem 32 og 126)
        if (b>=32 && b<=126)
        {   // Udskriver tallet, tegnet og antallet
            Console.WriteLine($"{b}'{(char)b}:{antal[b]}");
        }
        else // Ellers er det ikke et synligt tegn

        {    // Udskriver kun tallet og antallet
            Console.WriteLine($"{b}:{antal[b]}");
        }
    }
}
