using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WordPuzzle.Interfaces;
using WordPuzzle.Models;

namespace WordPuzzle
{
    public class FileUtility : IFileUtility
    {
        public List<string> FileToList(string path)
        {
            List<string> allStrings = new List<string>();
            List<string> all4LetterWords = new List<string>();

            using (StreamReader file = new StreamReader(path)) //TODO put in config
            {
                while (file.Peek() >= 0)
                {
                    allStrings.Add(file.ReadLine());
                }

                foreach (var s in allStrings)
                {
                    if (s.Length == Constants.WordLength)
                        all4LetterWords.Add(s.ToLower());
                }                
            }

            return all4LetterWords;
        }

        public void ResultsToFile(string path, List<string> words)
        {
            using (StreamWriter file = new StreamWriter(path))
            {
                foreach(var w in words)
                {
                    file.WriteLine(w);
                }                
            }
        }
    }
}
