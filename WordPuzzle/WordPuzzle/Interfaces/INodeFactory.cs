using System;
using System.Collections.Generic;
using System.Text;
using WordPuzzle.Models;

namespace WordPuzzle.Interfaces
{
    public interface INodeFactory
    {
        List<Node> GenerateNodes(List<Node> nodes, string startWord, string endWord);
    }
}
