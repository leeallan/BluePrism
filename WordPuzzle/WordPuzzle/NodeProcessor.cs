﻿using System;
using System.Collections.Generic;
using System.Text;
using WordPuzzle.Events;
using WordPuzzle.Interfaces;
using WordPuzzle.Models;

namespace WordPuzzle
{
    public class NodeProcessor : INodeProcessor
    {
        private readonly IWordUtility _wordUtility;
        private readonly IWordFilter _wordFilter;

        public event EventHandler<PuzzleEventArgs> OnComplete;
          
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
                {
                    FireCompleteEvent(new PuzzleEventArgs(n));
                    return true;
                }

                var regex = _wordUtility.GetWordSearchRegex(n.Word, props.EndWord);
                n.ChildNodes = _wordFilter.GetWordsForRegex(regex, props, n);
            }

            return false;
        }

        /// <summary>
        /// Set the current nodes list to all the children of the current nodes
        /// </summary>
        /// <param name="currentNodes"></param>
        public void AdvanceCurrentNodes(List<Node> currentNodes)
        {
            List<Node> temp = new List<Node>();
            foreach (Node n in currentNodes)
            {
                temp.AddRange(n.ChildNodes);
            }
            currentNodes.Clear();
            currentNodes.AddRange(temp);
        }


        public void FireCompleteEvent(PuzzleEventArgs e)
        {
            {
                OnComplete?.Invoke(this, e);

            }
        }
            
    }
}