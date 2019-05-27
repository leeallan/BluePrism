using System;
using System.Collections.Generic;
using System.Text;
using WordPuzzle.Events;

namespace WordPuzzle.Interfaces
{
    public interface IPuzzleProcessor
    {
        void Process(string startWord, string endWord);
        void OnPuzzleCompleted(object sender, PuzzleEventArgs e);
    }
}
