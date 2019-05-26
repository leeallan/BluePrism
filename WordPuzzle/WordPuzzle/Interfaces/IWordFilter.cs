using System;
using System.Collections.Generic;
using System.Text;
using WordPuzzle.Models;

namespace WordPuzzle.Interfaces
{
    public interface IWordFilter
    {
        //List<string> CharacterAtPosition(char item, int index, List<string> list);
        List<Node> GetWordsForRegex(string regex, List<string> list, string originalWord, Node parentNode);
    }
}
