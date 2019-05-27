using System;
using System.Collections.Generic;
using System.Text;
using WordPuzzle.Models;

namespace WordPuzzle.Interfaces
{
    public interface IWordUtility
    {      
        string GetWordSearchRegex(string startWord, string endWord);

    }
}
