using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WordPuzzle.Tests
{
    public class RegexTests
    {
        List<string> all4LetterWords = new List<string>();
        public void LoadFile()
        {
            using (StreamReader file = new StreamReader(@"C:\Users\lsall\Downloads\Blue Prism\words-english.txt"))
            {               
                while (file.Peek() >= 0)
                {
                    string word = file.ReadLine();
                    if (word.Length == 4)
                        all4LetterWords.Add(word.ToLower());
                }              
            }
        }

        public void FindSpinChildrenRegex()
        {
            string regex = "sp\\wn|spi\\w";

            Regex r = new Regex(regex, RegexOptions.IgnoreCase);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var matchedList = all4LetterWords.Where(x => r.IsMatch(x)).ToList();
            sw.Stop();

            Console.WriteLine($"Regex Time taken: {sw.Elapsed.ToString(@"mm\:ss\:fff")} - {string.Join(",", matchedList)} ");



        }

        public void FindSpinChildrenNoRegex()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var matchedList = all4LetterWords.Where(
                x =>  x[0] == 's' && x[1] == 'p' && x[2] == 'i' ||
                 x[0] == 's' && x[1] == 'p' && x[3] == 'n'
                 ) .ToList();
            sw.Stop();

            Console.WriteLine($"Non-Regex Time taken: {sw.Elapsed.ToString(@"mm\:ss\:fff")} - {string.Join(",", matchedList)} ");

        }
    }
}
