using System;
using System.Collections.Generic;
using System.Text;
using WordPuzzle.Events;
using WordPuzzle.Models;

namespace WordPuzzle.Interfaces
{
    public interface IPuzzleProcessor
    {
        void Process(AppProperties props);
        void OnPuzzleCompleted(object sender, PuzzleEventArgs e);
    }
}
