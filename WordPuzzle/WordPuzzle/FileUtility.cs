using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WordPuzzle.Interfaces;

namespace WordPuzzle
{
    public class FileUtility : IFileUtility
    {
        public List<string> FileToList(string path)
        {
            List<string> all4LetterWords = new List<string>();
            using (StreamReader file = new StreamReader(path)) //TODO put in config
            {                
                while (file.Peek() >= 0)
                {
                    string word = file.ReadLine();
                    if (word.Length == Constants.WordLength)
                        all4LetterWords.Add(word.ToLower());
                }             
            }

            return all4LetterWords;
        }
    }
}
