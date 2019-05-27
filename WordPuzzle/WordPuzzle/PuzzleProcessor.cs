using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WordPuzzle.Events;
using WordPuzzle.Interfaces;
using WordPuzzle.Models;

namespace WordPuzzle
{  
   
    public class PuzzleProcessor : IPuzzleProcessor
    {
        private readonly IFileUtility _fileUtility;
        private readonly INodeProcessor _nodeProcessor;
        public PuzzleProcessor(
            IFileUtility fileUtility,            
            INodeProcessor nodeProcessor)
        {
            _fileUtility = fileUtility;            
            _nodeProcessor = nodeProcessor;
        }

        List<Node> _currentNodes;
        bool _goalReached = false;
        List<string> _mainList;
        TimeSpan _timeTaken;

        public void Process(string startWord, string endWord)
        {

            //TODO Pass event into processor, to retracenodes, and output result
            //unit testing
            _mainList = _fileUtility.FileToList(@"C:\Users\lsall\Downloads\Blue Prism\words-english.txt");
            _mainList.Remove(startWord);
            
            AppProperties props = new AppProperties();
            props.EndWord =  endWord;
            props.StartWord = startWord;           
            props.WordList = _mainList;

            _currentNodes = new List<Node>();
            _currentNodes.Add(new Node() { IsStartNode = true, Word = startWord });
                        
            _nodeProcessor.OnComplete += OnPuzzleCompleted;
            string msg = "";

            Stopwatch sw = new Stopwatch();
            sw.Start();
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
                    //TODO: anything here?
                    var a = _currentNodes;
                    break;
                    //TODO success
                }              
            }

            sw.Stop();
            _timeTaken = sw.Elapsed;
            Console.WriteLine($"Puzzle complete in {_timeTaken.ToString(@"mm\:ss\:fff")}");
        }


        //TODO WHER TO PUT???
        //void OnPuzzleComplete(object sender, PuzzleEventArgs e)
        //{
                        
        //}

        public void OnPuzzleCompleted(object sender, PuzzleEventArgs e)
        {
            List<string> wordList = new List<string>();
            Node currentNode = e.Node;
           
            while (!currentNode.IsStartNode)
            {
                wordList.Add(currentNode.Word);
                currentNode = currentNode.ParentNode;
            }

            wordList.Add(currentNode.Word);

            wordList.Reverse();
           
            int wordNo = 1;
            foreach(var w in wordList)
            {
                Console.WriteLine($"{wordNo++}: {w}");

            }


            
        }
    }
}
