﻿using System;
using System.Collections.Generic;
using System.Text;
using WordPuzzle.Interfaces;
using WordPuzzle.Models;

namespace WordPuzzle
{
    public class NodeProcessor : INodeProcessor
    {
        private readonly IWordUtility _wordUtility;
        private readonly IWordFilter _wordFilter;
            
        public NodeProcessor(IWordFilter wordFilter,
            IWordUtility wordUtility)
        {
            _wordFilter = wordFilter;
            _wordUtility = wordUtility;

        }
        /// <summary>
        /// process current Node list and determine if end word reached
        /// </summary>
        /// <param name="currentNodes"></param>
        /// <returns>true if goal reached</returns>
        public bool ProcessNodes(List<Node> currentNodes, AppProperties props)
        {
            foreach (Node n in currentNodes)
            {
                if (n.Word == props.EndWord)
                    return true;

                var regex = _wordUtility.GetWordSearchRegex(n.Word, props.EndWord);
                n.ChildNodes = _wordFilter.GetWordsForRegex(regex, props.WordList, props.StartWord, n);

            }

            return false;
        }
    }
}
