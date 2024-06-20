using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ATC_Vanguard.Vanguard
{
    public static class DataLoader
    {
        public static List<string> wordList { get; set; } = new List<string>();

        public static List<string> LoadWordsList()
        {
            string[] lines = File.ReadAllLines("ArtTheCrown/wordList.txt");

            return wordList = lines.ToList();
        }

        public static async Task SaveWordsList(List<string> words)
        {
            await File.WriteAllLinesAsync("ArtTheCrown/wordList.txt", words);
        }
    }
}
