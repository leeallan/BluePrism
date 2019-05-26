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

                _goalReached = _nodeProcessor.ProcessNodes(_currentNodes, props); // NodeProcessor(_currentNodes);

                if (!_goalReached)
                {
                    List<Node> temp = new List<Node>();
                    foreach (Node n in _currentNodes)
                    {
                        temp.AddRange(n.ChildNodes);
                    }
                    _currentNodes.Clear();
                    _currentNodes.AddRange(temp);
                    
                }
                else
                {
                    var a = _currentNodes;
                }              
            }


          
        }

        //bool NodeProcessor(List<Node> nodes)
        //{
        //    foreach (Node n in nodes)
        //    {  
        //        if (n.Word == _endWord)
        //            return true;
               
        //        var regex = _wordUtility.GetWordSearchRegex(n.Word, _endWord);
        //        n.ChildNodes = _wordFilter.GetWordsForRegex(regex, _mainList, _startWord, n);              
               
        //    }

        //    return false;
        //}
    }
}
