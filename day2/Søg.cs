using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace dag2
{
    public class Søg
    {
        public static int BinSøg(int[] høstak, int nål)
        {
            int min = 0;
            int max = høstak.Length - 1;
            int midt = min + (max - min) / 2;
            // -1 is a common "not found" marker
            int index = -1;
            // Debugs
            int count = 0;
            while (min <= max)
            {
                count++;
                Console.WriteLine($"Round {count}: min={min}, max={max}, midt={midt}");
                // Found the needle: return its index
                if (høstak[midt] == nål)
                {
                    return midt;
                }
                // Middle value is too big: needle must be in the left half
                if (høstak[midt] > nål)
                {
                    max = midt - 1;
                }
                else
                {
                    min = midt + 1;
                }
                midt = min + (max - min) / 2;
            }
            return -1;
        }
        // Opgave1 
        // Bruger byte-værdien som indeks i arrayet og tæller 1 op.fx antal[65]---3
        // Byte-værdien bruges som indeks i arrayet,Selv hvis en byte-værdi aldrig forekommer, har den stadig en plads i arrayet, og værdien er 0.
        public static int[] TælFrekvenser(byte[] data)
        { // Opretter et array med 256 pladser, alle starter på 0
            int[] antal = new int[256];
            {    // Gennemgår hver byte i data
                foreach (byte b in data)
                { // use the byte value as the index, add 1
                    antal[b]++;
                }
            }
            // Returnerer frekvenstabellen
            return antal;

        }
    

        // Klassen skal være public static, så metoderne kan kaldes uden søg 
        public static Dictionary<byte, int> TælFrekvenserDictionary(byte[] data)
        {  // 1.Opretter en tom Dictionary: key = byte value, value = how many times it appears
            var antal = new Dictionary<byte, int>();
            // Gennemgår hver byte i data
            foreach (byte b in data)

            {    // Hvis byten allerede er set
                if (antal.ContainsKey(b))
                {// Seen before: add 1 to its count
                    antal[b]++;                                    
                }
                else
                {// First time we see this byte: start its count at 1
                    antal[b] = 1;
                }
            }
            // Return the finished frequency table
            return antal;
        }
        // Task 2: count how many times each word appears in the text.
        // The two lists are  ord[i] has been seen antal[i] times.
        public static void TælOrd(string[] tekst, List<string> ord, List<int> antal)
        {
            // Go through every word in the input
            foreach (string ordet in tekst)
            {
                // -1 means "not found yet"
                int index = -1;

                // Search the word list from start to end with a loop
                for (int i = 0; i < ord.Count; i++)
                {
                    if (ord[i] == ordet)
                    {
                        // Found the word: remember its position and stop searching
                        index = i;
                        break;
                    }
                }

                if (index == -1)
                {
                    // New word: add it to the word list and start its count at 1
                    ord.Add(ordet);
                    antal.Add(1);
                }
                else
                {
                    // Known word: add 1 to the count at the same position
                    antal[index]++;
                }
            }
        }
        // Opgave 3: Tæller hvor mange gange hvert ord forekommer, med en Dictionary
        // Key = the word, value = how many times it appears.
        // Opretter en tom Dictionary: nøgle = ord, værdi = antal
        public static Dictionary<string, int> TælOrdDictionary(string[] tekst)
        {
            // Opretter en tom Dictionary: nøgle = ord, værdi = antal
            var antal = new Dictionary<string, int>();

            // Gennemgår hvert ord
            foreach (string ordet in tekst)
            { // Hvis ordet allerede findes
                if (antal.ContainsKey(ordet))
                {
                    // Øger antallet med 1
                    antal[ordet]++;
                }
                else
                {
                    // Første gang: opretter ordet med værdien 1
                    antal[ordet] = 1;
                }
            }

            // Returnerer Dictionary'en
            return antal;
        }
        // Opgave 6: Selection sort - sorterer et int-array fra mindst til størst   int[] tal = { 8, 3, 6, 1, 5 };
        public static void SelectionSort(int[] tal)
        {
            // Ydre løkke: plads i er det sted, hvor det næste mindste tal skal stå
            // Vi stopper ved Length - 1, fordi det sidste tal automatisk står rigtigt
            for (int i = 0; i < tal.Length - 1; i++)
            {
                // Antager først, at det mindste tal står på plads i
                int minIndex = i;

                // Indre løkke: leder efter et mindre tal blandt de resterende
                for (int j = i + 1; j < tal.Length; j++)
                {
                    // Hvis tallet på plads j er mindre end det mindste vi har fundet indtil nu
                    if (tal[j] < tal[minIndex])
                    {
                        minIndex = j; // Gemmer pladsen på det nye mindste tal
                    }
                }

                // Bytter det mindste tal ind på plads i
                int temp = tal[i];          // Gemmer tallet på plads i midlertidigt
                tal[i] = tal[minIndex];     // Lægger det mindste tal på plads i
                tal[minIndex] = temp;       // Lægger det gamle tal, hvor det mindste stod
            }
        }
        // Opgave 7: Selection sort - sorterer efter frekvens, største først
        public static void SorterEfterFrekvens(byte[] værdier, int[] antal) // To parallelle arrays: byte-værdi og antal  65(A)---3
        {
            // Ydre løkke: Antager, at byte-værdien på plads i har det højeste antal
            for (int i = 0; i < antal.Length - 1; i++)
            {
                int maxIndex = i; // Antager først, at den hyppigste står på plads i

                // Indre løkke: leder efter en hyppigere byte blandt resten
                for (int j = i + 1; j < antal.Length; j++)
                {
                    // Hvis plads j har et STØRRE antal end det største fundet indtil nu
                    if (antal[j] > antal[maxIndex])
                    {
                        maxIndex = j; // Gemmer pladsen på det nye største antal
                    }
                } // Slut på indre løkke

                // Bytter antallene
                int tempAntal = antal[i];          // Gemmer antallet på plads i midlertidigt
                antal[i] = antal[maxIndex];        // Lægger det største antal på plads i
                antal[maxIndex] = tempAntal;       // Lægger det gamle antal, hvor det største stod

                // Bytter også byte-værdierne, så de følger med deres antal
                byte tempVærdi = værdier[i];       // Gemmer byte-værdien på plads i midlertidigt
                værdier[i] = værdier[maxIndex];    // Flytter den hyppigste byte til plads i
                værdier[maxIndex] = tempVærdi;     // Lægger den gamle byte, hvor den hyppigste stod
            } // Slut på ydre løkke
        } // Slut på metoden
    }
    
}





         

