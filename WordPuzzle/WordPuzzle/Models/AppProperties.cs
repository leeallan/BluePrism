using System;
using System.Collections.Generic;
using System.Text;

namespace WordPuzzle.Models
{
    public class AppProperties
    {
        public List<string> WordList { get; set; }
        public string StartWord { get; set; }
        public string EndWord { get; set; }
        public string FilePath { get; set; }
        public string ResultPath { get; set; }

    }
}
