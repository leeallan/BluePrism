using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WordPuzzle.Interfaces;
using WordPuzzle.Models;

namespace WordPuzzle
{
    public class WordFilter : IWordFilter
    {

        //public List<string> CharacterAtPosition(char item, int index, List<string> list)
        //{
        //    return list.Where(x => x.ElementAt(index) == item).ToList();
        //}

        public List<Node> GetWordsForRegex(string regex, List<string> list, string originalWord, Node parentNode)
        {
            Regex r = new Regex(regex, RegexOptions.IgnoreCase);

            list.Remove(parentNode.Word);

            var matchedList = list
                    .Where(x => r.IsMatch(x))
                        .Select(x => new Node()
                        {
                            Word = x,                            
                            ParentNode = parentNode
                        })
                        .ToList();

            SanitiseMatchedList(matchedList, originalWord);

            return matchedList;

        }


        /// <summary>
        /// Remove words that have n nuber of letters different to the startword, (Controlled by a threshold setting)
        /// </summary>
        /// <param name="list"></param>
        /// <param name="originalWord"></param>
        private void SanitiseMatchedList(List<Node> nodeList, string originalWord)
        {
            List<int> indices = new List<int>();

            for (int i = 0; i < nodeList.Count; i++)
            {
                byte mismatchCount = 0;
                for (int j = 0; j < nodeList[i].Word.Length; j++)
                {
                    if (nodeList[i].Word[j] != originalWord[j])
                    {
                        mismatchCount++;
                    }
                }
                if (mismatchCount > Constants.MismatchThreshold)
                {
                    indices.Add(i);
                }
            }

            var orderedist = indices.OrderByDescending(x => x).ToList();
            foreach (var index in orderedist)
            {
                nodeList.Remove(nodeList[index]);
            }


                        
        }
    }
}
