using System;
using System.Collections.Generic;
using System.Text;
using WordPuzzle.Models;

namespace WordPuzzle.Events
{
    public class PuzzleEventArgs : EventArgs
    {
        public PuzzleEventArgs(Node node)
        {
            this.Node = node;
        }

        public Node Node { get; set; }

    }

}
