using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WordPuzzle.Interfaces;
using WordPuzzle.Models;

namespace WordPuzzle
{
    public class WordUtility : IWordUtility
    {
        List<CharIndex> _charIndices;
       
        public CharIndex[] CharIndices(string startWord, string endWord)
        {
            _charIndices = new List<CharIndex>();
            for (byte i = 0; i < Constants.WordLength; i++)
            {
                AddToList(startWord[i], i);
                if (startWord[i] != endWord[i])
                {
                    AddToList(endWord[i], i);
                }
            }

            return _charIndices.ToArray();
        }

        public string GetWordSearchRegex(string startWord, string endWord)
        {            
            _charIndices = new List<CharIndex>();
            for (byte i = 0; i < Constants.WordLength; i++)
            {
                if (startWord[i] != endWord[i])
                {
                    AddToList(startWord[i], i);
                }
            }            

            string[] regexParts = new string[_charIndices.Count];

            for (int i = 0; i < _charIndices.Count; i++)
            {
                string regexStr = "";
                for(int index = 0; index < Constants.WordLength; index++ )
                {
                    if (index == _charIndices[i].Index)
                        regexStr += "\\w";
                    else
                        regexStr += startWord[index];
                }

                regexParts[i] = regexStr;
            }

            return string.Join('|', regexParts);
        }

        private void AddToList(char _char, byte index)
        {
            _charIndices.Add(new CharIndex() { Char = _char, Index = index });
        }
    }
}