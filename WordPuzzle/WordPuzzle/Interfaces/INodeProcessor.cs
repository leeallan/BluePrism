using System;
using System.Collections.Generic;
using System.Text;
using WordPuzzle.Events;
using WordPuzzle.Models;

namespace WordPuzzle.Interfaces
{
    public interface INodeProcessor
    {
        bool ProcessNodes(List<Node> currentNodes, AppProperties props);
        void AdvanceCurrentNodes(List<Node> currentNodes);

        event EventHandler<PuzzleEventArgs> OnComplete;
        void FireCompleteEvent(PuzzleEventArgs e);
               
        
    }
}
