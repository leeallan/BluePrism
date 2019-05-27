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
      

        /// <summary>
        /// Get Regex to find words with 1 differnet character anywhere along the length of the start and end words, where the characters are different. eg spin spot, only last 2 characters differ
        /// </summary>
        /// <param name="startWord"></param>
        /// <param name="endWord"></param>
        /// <returns></returns>
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