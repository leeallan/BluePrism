using System;
using System.Collections.Generic;
using System.Text;

namespace WordPuzzle
{
    public class Constants
    {
        public static byte WordLength = 4;
        ///A settign of 4 doesn't remove nodes, as it appears doing so, removes child words that are required to calculate path
        public static byte MismatchThreshold = 4;
    }
}
