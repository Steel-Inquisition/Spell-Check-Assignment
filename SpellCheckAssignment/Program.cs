// Spell Check Starter
// This start code creates two lists
// 1: dictionary: an array containing all of the words from "dictionary.txt"
// 2: aliceWords: an array containing all of the words from "AliceInWonderland.txt"

// Only thing I don't exactly like is using a dictionary like this, seems kind of inefficent but I can't figure anything else out. And the casting (int) was weird.


using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections.Generic;
class Program
{
    public static void Main(string[] args)
    {
        // Load data files into arrays
        String[] dictionary = System.IO.File.ReadAllLines(@"data-files/dictionary.txt");
        String aliceText = System.IO.File.ReadAllText(@"data-files/AliceInWonderLand.txt");
        String[] aliceWords = Regex.Split(aliceText, @"\s+");

        // Load the Actual Spell Check
        startSpellCheck(dictionary, aliceWords);
    }

    public static void startSpellCheck(String[] dictionary, String[] aliceWords)
    {
        // set up the loop
        bool runner = true;

        // set up the functions that can be chosen
        var searchSelector = new Dictionary<int, Delegate>();
        searchSelector[0] = new Func<string, String[], int>(LinearSearchString);
        searchSelector[1] = new Func<string, String[], int>(BinarySearchString);


        while (runner)
        {
            Stopwatch stopWatch = new Stopwatch();

            // Print out menu
            Console.WriteLine("<<< MENU >>> \n1. Spell Check a Word (linear) \n2. Spell Check A Word (binary) \n3. Spell Check Alice in Wonderland (linear) \n4. Spell Check Alice in Wonderland (binary) \n5. Exit");

            // Check which part of the menu is chosen
            string wordChecker = Console.ReadLine();

            switch (wordChecker)
            {
                case "1":
                    // Linear Dictionary Search
                    findWordByType(dictionary, stopWatch, searchSelector, 0);
                    break;
                case "2":
                    // Binary Dictionary Search
                    findWordByType(dictionary, stopWatch, searchSelector, 1);
                    break;
                case "3":
                    // Linear Alice Words Search
                    searchAliceWordByType(dictionary, aliceWords, stopWatch, searchSelector, 0);
                    break;
                case "4":
                    // Binary Alice Words Search
                    searchAliceWordByType(dictionary, aliceWords, stopWatch, searchSelector, 1);
                    break;
                case "5":
                    // Exit Function
                    Console.WriteLine("Exiting Function!");
                    runner = false;
                    break;
                default:
                    // If user types soemthing wrong
                    Console.WriteLine("Wrong Inputed");
                    break;
            }
        }
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


    static void findWordByType(String[] dictionary, Stopwatch stopWatch, Dictionary<int, Delegate> searchSelector, int selectSearchType)
    {

        // Get Input
        Console.WriteLine("Check Which Word?");
        string search = Console.ReadLine().ToLower();

        // Start Timer
        stopWatch.Start();

        // Get Position of the input in the dictionary and check if it was found
        // I don't like how I have to cast it as an (int)
        var gotPos = searchSelector[selectSearchType].DynamicInvoke(search, dictionary);
        checkIfFound((int)gotPos, search);

        // Stop Timer
        stopWatch.Stop();

        // Get time elapsed from starting timer to ending it
        TimeSpan ts = stopWatch.Elapsed;

        // Output how long it took
        Console.WriteLine(ts.ToString() + " time it took!");
    }

    static void searchAliceWordByType(String[] dictionary, String[] aliceWords, Stopwatch stopWatch, Dictionary<int, Delegate> searchSelector, int selectSearchType)
    {
        // Set up variable of how much not found
        int notFound = 0;

        // Start Timer
        stopWatch.Start();

        // Check every word in alice words
        for (int i = 0; i < aliceWords.Length; i++)
        {

            // Get the position (of it doesn't exist) of the word in Alice Words
            var gotPos = searchSelector[selectSearchType].DynamicInvoke(aliceWords[i], dictionary);

            // if the word doesn't exist, add not found by one
            // I don't like how I have to cast the variable to an (int) like this
            if ((int)gotPos == -1)
            {
                notFound++;
            }
        }

        // Stop Timer
        stopWatch.Stop();

        // Get time elapsed from starting timer to ending it
        TimeSpan ts = stopWatch.Elapsed;

        // Output how long it took and how many words were not found
        Console.WriteLine(ts.ToString() + " time it took!");
        Console.WriteLine($"There are {notFound} words not found in the dictionary!");
    }

    // This is what inspired me the most:
    // https://stackoverflow.com/questions/4233536/c-sharp-store-functions-in-a-dictionary
}



