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
        string _outputPath;
        TimeSpan _timeTaken;

        public void Process(AppProperties props)
        {
            props.WordList = _fileUtility.FileToList(props.FilePath);
            props.WordList.Remove(props.StartWord);

            _outputPath = props.ResultPath;
            _currentNodes = new List<Node>();
            _currentNodes.Add(new Node() { IsStartNode = true, Word = props.StartWord });
                        
            _nodeProcessor.OnComplete += OnPuzzleCompleted;         

            Stopwatch sw = new Stopwatch();
            sw.Start();

            while(!_goalReached)
            {
                if (_currentNodes.Count == 0)
                {                    
                    Console.WriteLine("No route was found :(");
                    break;
                }

                _goalReached = _nodeProcessor.ProcessNodes(_currentNodes, props); 

                if (!_goalReached)
                {
                    _nodeProcessor.AdvanceCurrentNodes(_currentNodes);                    
                }
                else
                {                                 
                    break;                  
                }              
            }

            sw.Stop();
            _timeTaken = sw.Elapsed;
            Console.WriteLine($"Puzzle complete in {_timeTaken.ToString(@"mm\:ss\:fff")}");
            Console.WriteLine("File out put to : " + props.ResultPath);
        }

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
            _fileUtility.ResultsToFile(_outputPath, wordList);




        }
    }
}
