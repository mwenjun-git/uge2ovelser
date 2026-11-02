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
    // Array er lettest at skrive (kun antal[b]++).
    // Dictionary bruger mindst plads, når der kun er få forskellige bytes,
    // fordi den kun gemmer de bytes, der faktisk forekommer (int[256] har altid 256 pladser).
    byte[] tekst = Encoding.UTF8.GetBytes("Rødgrød med fløde");   // Tekst til bytes

    int[] arr = Søg.TælFrekvenser(tekst);                   //array-version
    Dictionary<byte, int> dict = Søg.TælFrekvenserDictionary(tekst); // : Dictionary-version

    Console.WriteLine($"Array pladser: {arr.Length}");  // Altid 256
    Console.WriteLine($"Dictionary par: {dict.Count}"); // Kun bytes der forekommer
    Console.WriteLine();

   //Metoden UdskrivDictionary skal modtage en parameter af typen byte[], som har navnet data.
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
   // Kalder metoden med en sætning, hvor nogle ord gentages
   UdskrivOrdtælling("hund kat hund fisk kat hund");

   // Tæller ordene med to lister og udskriver resultatet
   void UdskrivOrdtælling(string sætning)
    {
        // Deler sætningen op i ord ved hvert mellemrum
        string[] ord = sætning.Split(' ');
        // Opretter to tomme lister: én til ordene og én til antallet
        List<string> ordList = new List<string>();
        List<int> antalList = new List<int>();
        // Fylder begge lister (plads i ordList passer til plads i antalList)
        Søg.TælOrd(ord, ordList, antalList);
        // Løber gennem listerne, én plads ad gangen
        for (int i = 0; i < ordList.Count; i++)

        {// Udskriver ordet og hvor mange gange det forekommer
            Console.WriteLine($"{ordList[i]}: {antalList[i]}");
        }
    }
    //Opgave 3
    // Tæller ordene i en sætning med en Dictionary og udskriver resultatet
    OrdtællingDictionary("hund kat hund fisk kat hund");

    void OrdtællingDictionary(string sætning)
    {
       // Deler sætningen op i ord ved hvert mellemrum
       string[] ord = sætning.Split(' ');

       // Tæller ordene med Dictionary-versionen (nøgle = ord, værdi = antal)
       Dictionary<string, int> tabel = Søg.TælOrdDictionary(ord);

      // Løber gennem alle par i Dictionary'en
      foreach (KeyValuePair<string, int> par in tabel)
      { // Udskriver ordet (nøglen) og hvor mange gange det forekommer (værdien)
        Console.WriteLine($"{par.Key}: {par.Value}");
      }
        Console.WriteLine();
    }

// Opgave 4: Måler hvor lang tid List-versionen og Dictionary-versionen tager
MålTider();
void MålTider()
{
    // Random-objekt til at lave tilfældige tal til ordene
    Random tilfældig = new Random();

    // De fem størrelser der skal testes (n fordobles hver gang)
    int[] størrelser = { 2000, 4000, 8000, 16000, 32000 };

    // Overskrift til tabellen
    Console.WriteLine("Ord    | Liste (ms) | Dictionary (ms)");

    foreach (int n in størrelser) // Gennemgår hver størrelse
    {
        // Opretter n ord, fx "ord17" (ca. n/2 forskellige ord)
        string[] ord = new string[n];
        for (int i = 0; i < n; i++)
        {
            ord[i] = "ord" + tilfældig.Next(n / 2); // Tilfældigt tal fra 0 til n/2 - 1
        }

        // Måler List-versionen
        Stopwatch sw = Stopwatch.StartNew(); // Starter stopuret
        Søg.TælOrd(ord, new List<string>(), new List<int>()); // Kører List-metoden
        sw.Stop(); // Stopper stopuret
        double tidListe = sw.Elapsed.TotalMilliseconds; // Tiden i millisekunder

        // Måler Dictionary-versionen
        sw.Restart(); // Nulstiller og starter stopuret igen
        Søg.TælOrdDictionary(ord); // Kører Dictionary-metoden
        sw.Stop(); // Stopper stopuret
        double tidDict = sw.Elapsed.TotalMilliseconds; // Tiden i millisekunder

        // Udskriver én række i tabellen
        Console.WriteLine($"{n,6} | {tidListe,10:F2} | {tidDict,15:F2}");
    }
    Console.WriteLine() ;
}


// Opgave 5: Forklaring
//
// Listen: Når antallet af ord fordobles, bliver tiden ca. 4 gange så lang
// (fx 8000 ord: 94 ms, 16000 ord: 429 ms).
// Det skyldes, at hvert ord skal søges igennem i listen med en løkke, 
// Ydre løkke: kører n gange (én gang for hvert ord)
// Indre løkke: søger i listen og sammenligner ét ord ad gangen
// så vi har en løkke inde i en løkke. Det passer til O(n²) (kvadratisk).
//
// Dictionary: Når antallet af ord fordobles, bliver tiden ca. 2 gange så lang
// (fx 16000 ord: 1,31 ms, 32000 ord: 2,18 ms).
// Et opslag i en Dictionary tager næsten lige lang tid uanset størrelsen (O(1)),
// og vi laver ét opslag pr. ord. Det passer til O(n) (lineær).

// Opgave 6: Kalder metoden, der måler selection sort mod Array.Sort
MålSortering();
// Opgave 6: Måler hvor lang tid selection sort og Array.Sort tager
void MålSortering()
{
    // Random-objekt til at lave tilfældige tal
    Random tilfældig = new Random();

    // De fire størrelser der skal testes (n fordobles hver gang)
    int[] størrelser = { 5000, 10000, 20000, 40000 };

    // Overskrift til tabellen
    Console.WriteLine("Antal  | Selection (ms) | Array.Sort (ms)");

    foreach (int n in størrelser) // Gennemgår hver størrelse
    {
        // Opretter et array med n tilfældige tal
        int[] original = new int[n];
        for (int i = 0; i < n; i++)
        {
            original[i] = tilfældig.Next(); // Tilfældigt positivt heltal
        }

        // Laver to kopier, så begge metoder sorterer de samme data
        // (sortering ændrer arrayet, så vi må ikke bruge det samme array to gange)
        int[] kopi1 = (int[])original.Clone(); // Kopi til selection sort
        int[] kopi2 = (int[])original.Clone(); // Kopi til Array.Sort

        // Måler selection sort
        Stopwatch sw = Stopwatch.StartNew(); // Starter stopuret
        Søg.SelectionSort(kopi1);            // Sorterer med selection sort
        sw.Stop();                           // Stopper stopuret
        double tidSelection = sw.Elapsed.TotalMilliseconds; // Tid i millisekunder

        // Måler Array.Sort
        sw.Restart();                        // Nulstiller og starter stopuret igen
        Array.Sort(kopi2);                   // Sorterer med den indbyggede metode
        sw.Stop();                           // Stopper stopuret
        double tidArraySort = sw.Elapsed.TotalMilliseconds; // Tid i millisekunder

        // Tjekker at selection sort giver samme resultat som Array.Sort
        // Tjekker at begge arrays er ens (kræver using System.Linq;)
        string resultat = kopi1.SequenceEqual(kopi2) ? "OK" : "FEJL";
       

        // Udskriver én række i tabellen
        Console.WriteLine($"{n,6} | {tidSelection,14:F2} | {tidArraySort,15:F2}  {resultat}");
    }
}   Console.WriteLine();

// Opgave 7: Kalder metoden med teksten "Rødgrød med fløde"
UdskrivTop10(Encoding.UTF8.GetBytes("Rødgrød med fløde")); // Laver teksten om til bytes og udskriver top 10
// Opgave 7: Udskriver de 10 hyppigste bytes i en tekst
void UdskrivTop10(byte[] data)
{
    // Tæller frekvenser med Dictionary-versionen
    Dictionary<byte, int> tabel = Søg.TælFrekvenserDictionary(data);

    // Opretter to parallelle arrays med plads til de bytes, der forekommer
    byte[] værdier = new byte[tabel.Count]; // Her gemmes byte-værdierne
    int[] antal = new int[tabel.Count];     // Her gemmes hvor mange gange hver forekommer

    int plads = 0; // Tæller hvilken plads vi er nået til i arrayene

    // Kopierer fra Dictionary til arrays
    foreach (KeyValuePair<byte, int> par in tabel)
    {
        værdier[plads] = par.Key;   // Gemmer byte-værdien (nøglen)
        antal[plads] = par.Value;   // Gemmer antallet (værdien)
        plads++;                    // Går videre til næste plads
    }

    // Sorterer så den hyppigste byte står først
    Søg.SorterEfterFrekvens(værdier, antal);

    // Udskriver højst 10 (der kan være færre end 10 forskellige bytes)
    int grænse = Math.Min(10, værdier.Length); // Vælger det mindste af 10 og antal bytes

    // Løber gennem de første "grænse" pladser
    for (int i = 0; i < grænse; i++)
    {
        // Hvis byten er et synligt tegn (32 til 126)
        if (værdier[i] >= 32 && værdier[i] <= 126)
        {
            // Udskriver tal, tegn og antal
            Console.WriteLine($"{værdier[i],3} '{(char)værdier[i]}' : {antal[i]}");
        }
        else // Ellers er det ikke et synligt tegn
        {
            // Udskriver kun tal og antal
            Console.WriteLine($"{værdier[i],3}     : {antal[i]}");
        }
    } // Slut på udskrivningsløkken
} // Slut på metoden

