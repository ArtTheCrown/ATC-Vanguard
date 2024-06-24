using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC_Vanguard.Vanguard.others
{
    public class FindWordsGameSystem
    {
        public string Result { get; set; }
        public List<string> ValidWords { get; set; }

        private static List<string> EnglishWords = new List<string>
        {
        };


        public FindWordsGameSystem(int level)
        {
            // Load Words list
            EnglishWords = ResourceLoader.GetWordsList(); 
            // Generate gibberish based on the level
            Result = GenerateGibberish(level);
            // Find valid English words in the gibberish
            ValidWords = FindValidWords(Result);
        }

        private string GenerateGibberish(int level)
        {
            Random random = new Random();
            List<string> wordsToInclude = new List<string>();
            int numOfWords = level * 9; // Number of valid words increases with the level

            // Randomly select words to include and randomize their case
            for (int i = 0; i < numOfWords; i++)
            {
                string word = RandomizeCase(EnglishWords[random.Next(EnglishWords.Count)], random);
                wordsToInclude.Add(word);
            }

            // Determine the length of the gibberish based on the level
            int length = 100 + (level - 1) * 500; // Level 1: 100, Level 2: 600, ..., Level 7: 3000

            // Construct the gibberish string
            StringBuilder gibberish = new StringBuilder();
            while (gibberish.Length < length)
            {
                gibberish.Append(GetRandomCharacter(random));
            }

            // Insert valid words at random positions in the gibberish string
            foreach (var word in wordsToInclude)
            {
                int position = random.Next(gibberish.Length);
                gibberish.Insert(position, $"{word}");
            }

            return gibberish.ToString();
        }

        private string RandomizeCase(string word, Random random)
        {
            StringBuilder randomized = new StringBuilder(word.Length);
            foreach (char c in word)
            {
                if (random.Next(2) == 0)
                {
                    randomized.Append(char.ToLower(c));
                }
                else
                {
                    randomized.Append(char.ToUpper(c));
                }
            }
            return randomized.ToString();
        }

        private char GetRandomCharacter(Random random)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return chars[random.Next(chars.Length)];
        }

        private List<string> FindValidWords(string gibberish)
        {
            HashSet<string> foundWordsSet = new HashSet<string>();
            List<string> foundWords = new List<string>();

            foreach (var word in EnglishWords)
            {
                if (gibberish.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (!foundWordsSet.Contains(word)) // Check if word is already in HashSet
                    {
                        foundWordsSet.Add(word); // Add to HashSet
                        foundWords.Add(word.ToLower());   // Add to List
                    }
                }
            }

            return foundWords;
        }

    }

    public class FindWordsScore
    {
        public List<FindWordsPlayer> players { get; set; } = new List<FindWordsPlayer>();

        public FindWordsPlayer GetPlayer(string name)
        {
            var player = players.FirstOrDefault(n => n.username == name);
            if (player != null)
            {
                return player;
            }
            else
            {
                players.Add(new FindWordsPlayer{ username = name });
                return GetPlayer(name);
            }
        }

        public FindWordsPlayer? GetWinner()
        {
            return players.OrderByDescending(e => e.correct).FirstOrDefault();
        }

        public FindWordsPlayer? GetLoser()
        {
            return players.OrderByDescending(e => e.correct).LastOrDefault();
        }
    }

    public class FindWordsPlayer
    {
        public string? username { get; set; }
        public int correct { get; set; } = 0;
        public int incorrect { get; set; } = 0;
    }
}

