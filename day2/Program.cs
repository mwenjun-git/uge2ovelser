using dag2;
using System.Diagnostics;
using System.Text;

// Test binary search
BinærSøgning();

// Task 1: print the byte frequencies
void BinærSøgning()
{

    //Create a random number 
    Random r = new Random();
    //an array that holds 100 integers
    int[] hø = new int[100];
    //Loop 100 times
    for (int i = 0; i < hø.Length; i++)
    { // Pick a random integer from 0 to 499 (500 is excluded)
      // and store it in slot i
        hø[i] = r.Next(0, 500);
    }
    int nål = hø[0];
    hø.Sort();
    int index = dag2.Søg.BinSøg(hø, nål);
    Console.WriteLine($"nålen({nål}) findet på index{index}");
    Console.WriteLine();
}

//Opgave 1
UdskrivDictionary(Encoding.UTF8.GetBytes("Hej"));

void UdskrivDictionary(byte[] data)
{
    // Call the method in class Søg and store the result in tabel
    Dictionary<byte, int> tabel = Søg.TælFrekvenserDictionary(data);

    foreach (KeyValuePair<byte, int> par in tabel)
    {
        Console.WriteLine($"{par.Key}: {par.Value}");
        
    }
    Console.WriteLine();
}
//Opgave 2
UdskrivOrdtælling("hund kat hund fisk kat hund");
void UdskrivOrdtælling(string sætning)
{
    // Split the sentence into words at every space
    string[] ord = sætning.Split(' ');
    // Create the two empty lists
    List<string> ordList = new List<string>();
    List<int> antalList = new List<int>();
    // Fill both lists
    Søg.TælOrd(ord, ordList, antalList);
    for (int i = 0; i < ordList.Count; i++)
    {
        Console.WriteLine($"{ordList[i]}: {antalList[i]}");
    }
}
//Opgave 3
UdskrivOrdtællingDictionary("hund kat hund fisk kat hund");

void UdskrivOrdtællingDictionary(string sætning)
{
    // Split the sentence into words at every space
    string[] ord = sætning.Split(' ');

    // Count the words with the Dictionary version
    Dictionary<string, int> tabel = Søg.TælOrdDictionary(ord);

    // Print every word with its count
    foreach (KeyValuePair<string, int> par in tabel)
    {
        Console.WriteLine($"{par.Key}: {par.Value}");
    }
    Console.WriteLine();
}

//Opgave 4  measure how long the list version and the Dictionary version take
MålTider();
void MålTider()
{   // ... used to build the test words
    Random tilfældig = new Random();
    int[] størrelser = { 2000, 4000, 8000, 16000, 32000 };

    Console.WriteLine("Words | List (ms) | Dictionary (ms)");

    foreach (int n in størrelser)
    {
        // Make n words like "ord17" (about n/2 different ones)
        string[] ord = new string[n];
        for (int i = 0; i < n; i++)

            ord[i] = "ord" + tilfældig.Next(n / 2);

        // Time the list version
        Stopwatch sw = Stopwatch.StartNew();
        Søg.TælOrd(ord, new List<string>(), new List<int>());
        long tidListe = sw.ElapsedMilliseconds;

        // Time the dictionary version
        sw.Restart();
        Søg.TælOrdDictionary(ord);
        long tidDict = sw.ElapsedMilliseconds;

        Console.WriteLine($"{n} | {tidListe} | {tidDict}");
    }
}
//Opgave 6


