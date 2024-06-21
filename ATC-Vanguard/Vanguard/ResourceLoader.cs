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
    public static class ResourceLoader
    {
        public static List<string> wordList { get; set; } = new List<string>();
        private static bool loaded = false;
        public static List<string> GetWordsList()
        {
            if(loaded) return wordList;



            string[] lines = File.ReadAllLines("ArtTheCrown/wordList.txt");

            wordList = lines.ToList();
            loaded = true;
            return wordList;
            
        }

        public static async Task SaveWordsList(List<string> words)
        {
            await File.WriteAllLinesAsync("ArtTheCrown/wordList.txt", words);
        }
    }
}
