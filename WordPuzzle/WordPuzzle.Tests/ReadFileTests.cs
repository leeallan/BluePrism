using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace WordPuzzle.Tests
{
    public class ReadFileTests
    {
        public void QuickestFileReadTests()
        {
            long timeToReadAll;
            long timeToReadAllNoLinq;
            long timeToReadRequired;

            Console.WriteLine("Hello World!");
            List<string> str = new List<string>();
            List<string> all4LetterWords = new List<string>();
            using (StreamReader file = new StreamReader(@"C:\Users\lsall\Downloads\Blue Prism\words-english.txt")) //TODO put in config
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                while (file.Read() >= 0)
                {
                    str.Add(file.ReadLine());
                }

                all4LetterWords = str.Where(x => x.Length == 4).ToList();

                sw.Stop();
                timeToReadAll = sw.ElapsedMilliseconds;
            }

            using (StreamReader file = new StreamReader(@"C:\Users\lsall\Downloads\Blue Prism\words-english.txt")) //TODO put in config
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                while (file.Read() >= 0)
                {
                    str.Add(file.ReadLine());
                }

                foreach (var s in str)
                {
                    if (s.Length == 4)
                        all4LetterWords.Add(s);
                }

                sw.Stop();
                timeToReadAllNoLinq = sw.ElapsedMilliseconds;
            }

            using (StreamReader file = new StreamReader(@"C:\Users\lsall\Downloads\Blue Prism\words-english.txt")) //TODO put in config
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                while (file.Read() >= 0)
                {
                    string word = file.ReadLine();
                    if (word.Length == 4)
                        all4LetterWords.Add(word);
                }

                //all4LetterWords = str.Where(x => x.Length == 4).ToList();

                sw.Stop();
                timeToReadRequired = sw.ElapsedMilliseconds;
            }

            Console.WriteLine($"Time to read all words and filter: {timeToReadAll} ms");
            Console.WriteLine($"Time to read all words and filter no linq: {timeToReadAllNoLinq} ms");
            Console.WriteLine($"Time to read opnly required words: {timeToReadRequired} ms");
            Console.Read();
        }

    }
}
