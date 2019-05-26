using System;
using System.Collections.Generic;
using System.Text;
using WordPuzzle.Models;

namespace WordPuzzle.Interfaces
{
    public interface INodeProcessor
    {
        bool ProcessNodes(List<Node> currentNodes, AppProperties props);
    }
}
