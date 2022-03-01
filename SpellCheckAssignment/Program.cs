// Spell Check Starter
// This start code creates two lists
// 1: dictionary: an array containing all of the words from "dictionary.txt"
// 2: aliceWords: an array containing all of the words from "AliceInWonderland.txt"

using System;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;
class Program
{
    private static System.Timers.Timer aTimer;

    public static void Main(string[] args)
    {
        // Load data files into arrays
        String[] dictionary = System.IO.File.ReadAllLines(@"data-files/dictionary.txt");
        String aliceText = System.IO.File.ReadAllText(@"data-files/AliceInWonderLand.txt");
        String[] aliceWords = Regex.Split(aliceText, @"\s+");

        // Asking
        printStringArray(dictionary, aliceWords);

        /*
        Console.WriteLine("***ALICE WORDS***");
        printStringArray(aliceWords, 0, 50);
        */
    }

    public static void printStringArray(String[] dictionary, String[] aliceWords)
    {
        bool runner = true;

        while (runner)
        {
            Stopwatch stopWatch = new Stopwatch();

            // Print out array elements at index values from start to stop 
            Console.WriteLine("<<< MENU >>> \n1. Spell Check a Word (linear) \n2. Spell Check A Word (binary) \n3. Spell Check Alice in Wonderland (linear) \n4. Spell Check Alice in Wonderland (binary) \n5. Exit");

            string wordChecker = Console.ReadLine();

            switch (wordChecker)
            {
                case "1":
                    Console.WriteLine("Check Which Word?");
                    string search = Console.ReadLine().ToLower();


                    int gotPos = LinearSearchString(search, dictionary);

                    stopWatch.Start();
                    checkIfFound(gotPos, search);
                    stopWatch.Stop();

                    TimeSpan ts = stopWatch.Elapsed;

                    Console.WriteLine(ts.ToString());
                    break;
                case "2":
                    Console.WriteLine("Check Which Word?");
                    search = Console.ReadLine().ToLower();


                    gotPos = BinarySearchString(search, dictionary);

                    stopWatch.Start();
                    checkIfFound(gotPos, search);
                    stopWatch.Stop();

                    ts = stopWatch.Elapsed;

                    Console.WriteLine(ts.ToString());
                    break;
                case "3":                    
                    for (int i = 0; i < dictionary.Length; i++)
                    {
                        gotPos = LinearSearchString(dictionary[i], aliceWords);
                    }


                    break;
                case "4":
                    Console.WriteLine("Check Which Word?");
                    search = Console.ReadLine().ToLower();
                    gotPos = BinarySearchString(search, aliceWords);
                    break;
                case "5":
                    Console.WriteLine("Exiting Function!");
                    runner = false;
                    break;
                default:
                    Console.WriteLine("Wrong Inputed");
                    break;
            }
        }



        /*
        for (int i = start; i < stop; i++)
        {
            Console.WriteLine(array[i]);
        }
        */
    }

    static int LinearSearchString(string search, String[] strings)
    {
        for (int pos = 0; pos < strings.Length; pos++)
        {
            if (strings[pos] == search)
            {
                return pos;
            }
        }

        return -1;
    }

    static int BinarySearchString(string search, String[] words)
    {
        int lowerIndex = 0;
        int upperIndex = words.Length - 1;

        while (lowerIndex <= upperIndex)
        {
            int middleIndex = (lowerIndex + upperIndex) / 2;

            if (search == words[middleIndex])
            {
                return middleIndex;

            }
            else if (words[middleIndex].CompareTo(search) > 0)
            {
                upperIndex = middleIndex - 1;

            }
            else
            {
                lowerIndex = middleIndex + 1;
            }
        }

        return -1;
    }


    static void checkIfFound(int gotPos, string search)
    {
        if (gotPos > -1)
        {
            Console.WriteLine($"{search} was found at position {gotPos}!");
        }
        else
        {
            Console.WriteLine($"{search} was not found!");
        }
    }
}



