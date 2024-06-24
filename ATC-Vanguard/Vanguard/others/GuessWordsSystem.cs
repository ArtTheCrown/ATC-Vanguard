using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml.Linq;

namespace ATC_Vanguard.Vanguard.others
{
    public class GuessWordsSystem
    {
        public string Word { get; set; }
        public string Result { get; set; }

        public GuessWordsSystem(int level) 
        {
            Word = GenerateWord(ResourceLoader.GetWordsList(), level);

            Result = GenerateResult(Word, level);
        }

        private string GenerateWord(List<string> words, int level)
        {
            if (words == null || words.Count == 0)
            {
                throw new ArgumentException("The words list must not be null or empty.");
            }

            Random random = new Random();
            int difficulty = random.Next(6, 9);

            string word = "";
            while (word.Length < difficulty)
            {
                word = words[random.Next(words.Count)];
            }

            return word;
        }

        private string GenerateResult(string word, int level)
        {
            int upLimit = 0;
            int downLimit = 0;
            switch (level)
            {
                case 1:
                    upLimit = word.Length - 1;
                    downLimit = word.Length - 3;
                    break;
                case 2:
                    upLimit = word.Length - 3;
                    downLimit = word.Length - 5;
                    break;
                case 3:
                    if (word.Length > 7)
                    {
                        upLimit = word.Length - 6;
                        downLimit = word.Length - 7;
                    }
                    else
                    {
                        upLimit = word.Length - 5;
                        downLimit = word.Length - 5;
                    }
                    break;
            }


            Random random = new Random();
            string replacement = "_";
            int replacementLength = replacement.Length;

            int untouchedCount = random.Next(downLimit, upLimit);
            int replaceCount = word.Length - untouchedCount;

            StringBuilder result = new StringBuilder(word);
            HashSet<int> replacedPositions = new HashSet<int>();

            for (int i = 0; i < replaceCount; i++)
            {
                int position;
                do
                {
                    position = random.Next(0, word.Length);
                }
                while (replacedPositions.Contains(position));

                // Ensure the replacement does not exceed the string bounds
                if (position + replacementLength <= word.Length)
                {
                    result.Remove(position, replacementLength).Insert(position, replacement);
                    replacedPositions.Add(position);
                }
            }

            return result.ToString();
        }
    }

    public class GuessWordsScore
    {
        public List<GuessWordsPlayer> players { get; set; } = new List<GuessWordsPlayer>();

        public GuessWordsPlayer GetPlayer(string name)
        {
            var player = players.FirstOrDefault(n => n.username == name);
            if (player != null)
            {
                return player;
            }
            else
            {
                players.Add(new GuessWordsPlayer { username = name });
                return GetPlayer(name);
            }
        }

        public string Score()
        {
            StringBuilder scoreStr = new StringBuilder();
            foreach (GuessWordsPlayer player in players)
            {
                string temp = $"**`{player.username}`**\n" +
                              $"score: `{player.correct}`\n" +
                              $"\n";
                scoreStr.Append(temp);
            }

            return scoreStr.ToString();
        }

        public GuessWordsPlayer? GetWinner()
        {
            return players.OrderByDescending(e => e.correct).FirstOrDefault();
        }

        public GuessWordsPlayer? GetLoser()
        {
            return players.OrderByDescending(e => e.correct).LastOrDefault();
        }
    }

    public class GuessWordsPlayer
    {
        public string? username { get; set; }
        public int correct { get; set; } = 0;
        public int incorrect { get; set; } = 0;
    }
}
