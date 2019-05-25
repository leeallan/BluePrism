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


        public List<string> CharacterAtPosition(char item, int index, List<string> list)
        {
            return list.Where(x => x.ElementAt(index) == item).ToList();
        }

        public List<Node> GetWordsForRegex(string regex, List<string> list, string thisWord, string originalWord, Node parentNode)
        {
            Regex r = new Regex(regex, RegexOptions.IgnoreCase);
            list.Remove(thisWord);
            var matchedList = list
                    .Where(x => r.IsMatch(x))
                        .Select(x => new Node()
                        {
                            Word = x,
                            // IsGoal = endWord == x ? true : false,
                            ParentNode = parentNode
                        })
                        .ToList();

            SanitiseMatchedList(matchedList, originalWord);
            return matchedList;

        }

        private void SanitiseMatchedList(List<Node> list, string originalWord)
        {
            List<int> indices = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                byte mismatchCount = 0;
                for (int j = 0; j < list[i].Word.Length; j++)
                {
                    if (list[i].Word[j] != originalWord[j])
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
                list.Remove(list[index]);
            }


                        
        }
    }
}
