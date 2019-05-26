using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordPuzzle.Interfaces;
using WordPuzzle.Models;

namespace WordPuzzle
{
    public interface IPuzzleProcessor
    {
        void Process(string startWord, string endWord);
    }

    public class PuzzleProcessor : IPuzzleProcessor
    {
        private readonly IFileUtility _fileUtility;
        private readonly IWordUtility _wordUtility;
        private readonly IWordFilter _wordFilter;
        private readonly INodeProcessor _nodeProcessor;
        public PuzzleProcessor(
            IFileUtility fileUtility,
            IWordUtility wordUtility,
            IWordFilter wordFilter,
            INodeProcessor nodeProcessor)
        {
            _fileUtility = fileUtility;
            _wordUtility = wordUtility;
            _wordFilter = wordFilter;
            _nodeProcessor = nodeProcessor;
        }

        List<Node> _currentNodes;
        bool _goalReached = false;
        string _endWord = "";
        string _startWord = "";
        List<string> _mainList;

        public void Process(string startWord, string endWord)
        {
            AppProperties props = new AppProperties();
            props.EndWord = _endWord = endWord;
            props.StartWord = _startWord = startWord;

            
           _mainList = _fileUtility.FileToList(@"C:\Users\lsall\Downloads\Blue Prism\words-english.txt");
           _mainList.Remove(startWord);

            props.WordList = _mainList;

          

            Node startNode = new Node() { IsStartNode = true, Word = startWord };
            _currentNodes = new List<Node>();
            _currentNodes.Add(startNode);
            string msg = "";
           

            while(!_goalReached)
            {
                if (_currentNodes.Count == 0)
                {
                    msg = "No route was found :(";
                    Console.WriteLine(msg);
                    break;
                }

                _goalReached = _nodeProcessor.ProcessNodes(_currentNodes, props); 

                if (!_goalReached)
                {
                    _nodeProcessor.AdvanceCurrentNodes(_currentNodes);                    
                }
                else
                {
                    var a = _currentNodes;
                }              
            }          
        }
    }
}
