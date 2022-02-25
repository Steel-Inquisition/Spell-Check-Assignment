// Spell Check Starter
// This start code creates two lists
// 1: dictionary: an array containing all of the words from "dictionary.txt"
// 2: aliceWords: an array containing all of the words from "AliceInWonderland.txt"

using System;
using System.Timers;
using System.Text.RegularExpressions;
class Program
{
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

            // Print out array elements at index values from start to stop 
            Console.WriteLine("<<<MENU >>> \n1. Spell Check a Word (linear) \n2. Spell Check A Word (binary) \n3. Spell Check Alice in Wonderland (linear) \n4. Spell Check Alice in Wonderland (binary) \n5. Exit");

            string wordChecker = Console.ReadLine();

            switch (wordChecker)
            {
                case "1":
                    Console.WriteLine("Check Which Word?");
                    string search = Console.ReadLine().ToLower();
                    int gotPos = LinearSearchString(search, dictionary);
                    checkIfFound(gotPos, search);
                    break;
                case "2":
                    Console.WriteLine("Check Which Word?");
                    search = Console.ReadLine().ToLower();
                    gotPos = BinarySearchString(search, dictionary);
                    checkIfFound(gotPos, search);
                    break;
                case "3":
                    Console.WriteLine("Check Which Word?");
                    search = Console.ReadLine().ToLower();
                    gotPos = LinearSearchString(search, aliceWords);
                    checkIfFound(gotPos, search);
                    break;
                case "4":
                    Console.WriteLine("Check Which Word?");
                    search = Console.ReadLine().ToLower();
                    gotPos = BinarySearchString(search, aliceWords);
                    checkIfFound(gotPos, search);
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
        double lowerIndex = 0;
        double upperIndex = words.Length - 1;

        while (lowerIndex <= upperIndex)
        {
            double middleIndex = Math.Floor((lowerIndex + upperIndex) / 2);

            if (search == words[(int)middleIndex])
            {
                return (int)middleIndex;

            }
            else if (words[(int)middleIndex].CompareTo(search) > 0)
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
            Console.WriteLine($"{search} was found!");
        }
        else
        {
            Console.WriteLine($"{search} was not found!");
        }
    }
}



