using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WordPuzzle.Interfaces;

namespace WordPuzzle.UnitTests
{
    [TestClass]
    public class WordUtilityTests
    {
        IWordUtility _wordUtility;

        public WordUtilityTests()
        {
            _wordUtility = new WordUtility();
        }


        [TestMethod]
        public void GetRegex_TwoCharactesDiffer_2RegexSegments()
        {
            string expectedRegex = "sp\\wn|spi\\w";

            string actualRegex = _wordUtility.GetWordSearchRegex("spin", "spot");

            Assert.AreEqual(expectedRegex, actualRegex);
        }

        [TestMethod]
        public void GetRegex_ThreeCharactesDiffer_3RegexSegments()
        {
            string expectedRegex = "s\\win|sp\\wn|spi\\w";

            string actualRegex = _wordUtility.GetWordSearchRegex("spin", "soot");

            Assert.AreEqual(expectedRegex, actualRegex);
        }

    }
}
