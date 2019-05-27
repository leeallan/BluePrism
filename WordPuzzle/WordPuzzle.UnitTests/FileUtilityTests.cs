using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using WordPuzzle.Interfaces;

namespace WordPuzzle.UnitTests
{
    [TestClass]
    public class FileUtilityTests
    {

        IFileUtility _fileUtility;
        List<string> all4LetterWords;

        public FileUtilityTests()
        {
            _fileUtility = new FileUtility();
        }

        [TestMethod]
        public void LoadFile_All4LetterWords_ListNotNull()
        {
            
            all4LetterWords = _fileUtility.FileToList(@"C:\Users\lsall\Downloads\Blue Prism\words-english.txt");

            Assert.IsNotNull(all4LetterWords);


        }

        [TestMethod]
        public void LoadFile_All4LetterWords_ContainsOnly4LetterWords()
        {
            int count4LtterWords = 0;

            all4LetterWords = _fileUtility.FileToList(@"C:\Users\lsall\Downloads\Blue Prism\words-english.txt");

            foreach(var w in all4LetterWords)
            {
                if (w.Length == 4)
                    count4LtterWords++;
            }

            Assert.AreEqual(all4LetterWords.Count, all4LetterWords.Count);


        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void LoadFile_ThrowError_FileNotFound()
        {          
            all4LetterWords = _fileUtility.FileToList(@"C:\Users\lsall\Downloads\Blue Prism\words-englistxt");           
        }

        [TestMethod]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void LoadFile_ThrowError_DirectoryNotFound()
        {          
            all4LetterWords = _fileUtility.FileToList(@"C:\Users\lsall\Downloads\Blue\words-english.txt");           

        }
    }
}
