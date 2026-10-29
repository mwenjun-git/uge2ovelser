using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace dag2
{
    internal class Søg
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

        int[] TælFrekvenser(byte[] data)
        {
            int[] count = new int[256];
            foreach (byte b in data)
            { // use the byte value as the index, add 1
                count[b]++;
            }
            return count;
        }

        public static Dictionary<byte, int> TælFrekvenserDictionary(byte[] data)
        {  // 1. create the counter: key = byte value, value = how many times it appears
            var antal = new Dictionary<byte, int>();
            // Go through every byte in the input data
            foreach (byte b in data)
            {   // Check whether this byte has been seen before
                if (antal.ContainsKey(b))
                {// Seen before: add 1 to its count
                    antal[b]++;                                    // fix 4
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
        // The two lists are parallel: ord[i] has been seen antal[i] times.
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
        // Task 3: count how many times each word appears, using a Dictionary.
        // Key = the word, value = how many times it appears.
        public static Dictionary<string, int> TælOrdDictionary(string[] tekst)
        {
            // Create the empty counter
            var antal = new Dictionary<string, int>();

            // Go through every word in the input
            foreach (string ordet in tekst)
            {
                if (antal.ContainsKey(ordet))
                {
                    // Seen before: add 1 to its count
                    antal[ordet]++;
                }
                else
                {
                    // First time we see this word: start its count at 1
                    antal[ordet] = 1;
                }
            }

            // Return the finished word-frequency table
            return antal;
        }

    }
}





         

