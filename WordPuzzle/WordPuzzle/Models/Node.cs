using System;
using System.Collections.Generic;
using System.Text;

namespace WordPuzzle.Models
{
    public class Node
    {
        public Node ParentNode { get; set; }
        public bool IsStartNode { get; set; }
        public string Word { get; set; }
        public List<Node> ChildNodes { get; set; }
    }
}



