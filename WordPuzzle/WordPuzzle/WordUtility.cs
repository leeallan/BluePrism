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
        StringBuilder _sb = new StringBuilder();
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
            _sb.Clear();




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

                regexParts[i] = regexStr;// $"{start}" startWord[_charIndices[i].Index] = "\\w";//.Replace(_charIndices[i].Char.ToString(), "\\w");
            }

            return string.Join('|', regexParts);

            //string regex = string.Join('|', _charIndices.Select(x => startWord.Replace(x.Char.ToString(),"\\w")));           
            //   string regex = string.Join('|', _charIndices.Select(x => startWord.Replace(x.Char.ToString(), "\\w")));
            // return regex;

        }

        private void AddToList(char _char, byte index)
        {
            _charIndices.Add(new CharIndex() { Char = _char, Index = index });
        }

    }
}


//_charIndices = new List<CharIndex>();
//            for (byte i = 0; i<Constants.WordLength; i++)
//            {
//                if (startWord[i] != endWord[i])
//                {
//                    AddToList(startWord[i], i);
//                }
//            }
//            _sb.Clear();

//            string[] regexParts = new string[_charIndices.Count];

//            for (int i = 0; i<_charIndices.Count; i ++)
//            {
//                regexParts[i] = startWord.Replace(_charIndices[i].Char.ToString(), "\\w");
//            }

//            return string.Join('|', regexParts);