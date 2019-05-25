using System;
using System.Collections.Generic;
using System.Text;

namespace WordPuzzle.Interfaces
{
    public interface IFileUtility
    {
        List<string> FileToList(string path);
    }
}
