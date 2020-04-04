using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class Program
    {
        static List<string> possibleWords = new List<string>() { "Words", "Formula", "Programming", "Bull Shit", "Method", "Random", "Avatar"};
        static void Main(string[] args)
        {
            Random random = new Random();
            string chosenWord = possibleWords[random.Next(0, possibleWords.Count)];
            List<char> guessedCharacters = new List<char>();
            bool canGuessMore = true;
            int maxWrongGuesses = 5;
            int wrongGuessesMade = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Guessed Letters: " + String.Join(", ", guessedCharacters));
                Console.WriteLine("You have " + (maxWrongGuesses - wrongGuessesMade) + " guesses remaining");
                Console.WriteLine(getActualOutputFromWord(chosenWord, guessedCharacters));
                Console.WriteLine("1. Guess a letter");
                Console.WriteLine("2. Solve the puzzle");
                ConsoleKeyInfo selectedOption = Console.ReadKey();

                switch (selectedOption.KeyChar)
                {
                    case '1':
                        Console.WriteLine("What is your guess?");
                        ConsoleKeyInfo guessChar = Console.ReadKey();
                        guessedCharacters.Add(char.ToLower(guessChar.KeyChar));
                        if (!chosenWord.ToLower().ToCharArray().Contains(char.ToLower(guessChar.KeyChar)))
                        {
                            wrongGuessesMade++;
                            if (wrongGuessesMade == maxWrongGuesses)
                            {
                                Console.WriteLine("Oh you dun fucked up. GG, scrub.");
                                canGuessMore = false;
                            }
                        }
                        if (getNumberOfNotGuessedLetters(word: chosenWord , guessedCharacters : guessedCharacters) == 0)
                        {
                            Console.Write("The word was: ");
                            Console.WriteLine(getActualOutputFromWord(chosenWord, guessedCharacters));
                            Console.WriteLine("WINNER WINNER CHICKEN DINNER");
                            canGuessMore = false;
                        }
                        break;
                    case '2':
                        Console.WriteLine("What do you think the word is?");
                        if (chosenWord.ToLower() == Console.ReadLine().ToLower())
                        {
                            Console.WriteLine("WINNER WINNER CHICKEN DINNER");
                            canGuessMore = false;
                        }
                        else 
                        {
                            Console.WriteLine("Lol you think you're hot shit. GG, Scrub.");
                            canGuessMore = false;
                            Console.ReadKey();
                        }
                        break;
                    default:
                        break;
                }
            } while (canGuessMore);

            Console.ReadKey();
        }

        static string getActualOutputFromWord(string word, List<char> guessedCharacters)
        {
            char[] output = new char[word.Length];
            char[] characters = word.ToCharArray();
            for (int i = 0; i < characters.Length; i++)
            {
                output[i] = characters[i] == ' ' ? ' ' : (guessedCharacters.Contains(char.ToLower(characters[i])) ? characters[i] : '_');
            }
            return String.Join(" ", output);
        }

        static int getNumberOfNotGuessedLetters(string word, List<char> guessedCharacters)
        {
            int output = 0;
            char[] characters = word.ToCharArray();
            for (int i = 0; i < characters.Length; i++)
            {
                output += characters[i] == ' ' ? 0 : (guessedCharacters.Contains(char.ToLower(characters[i])) ? 0 : 1);
            }
            return output;
        }

    }
}
