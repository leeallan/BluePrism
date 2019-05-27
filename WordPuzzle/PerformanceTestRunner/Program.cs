using System;
using WordPuzzle.Tests;

namespace PerformanceTestRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            RegexTests regexTests = new RegexTests();
            regexTests.LoadFile();
            regexTests.FindSpinChildrenRegex();
            regexTests.FindSpinChildrenNoRegex();
            
        }
    }
}
