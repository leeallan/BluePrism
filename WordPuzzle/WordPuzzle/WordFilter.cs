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
 
        /// <summary>
        /// Generate Nodes for words retrieved by the regex
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="list"></param>
        /// <param name="originalWord"></param>
        /// <param name="parentNode"></param>
        /// <returns></returns>
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

            SanitiseMatchedList(matchedList, originalWord, Constants.MismatchThreshold);

            return matchedList;

        }


        /// <summary>
        /// Remove words that have > n number of letters different to the startword, (Controlled by a threshold setting)
        /// </summary>
        /// <param name="list"></param>
        /// <param name="originalWord"></param>
        private void SanitiseMatchedList(List<Node> nodeList, string originalWord, byte mismatchThreshold)
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
                if (mismatchCount > mismatchThreshold)
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

        public void SanitiseMatchedListTestAccessor(List<Node> nodeList, string originalWord, byte mismatchThreshold)
        {
            SanitiseMatchedList(nodeList, originalWord, mismatchThreshold);
        }
    }
}
