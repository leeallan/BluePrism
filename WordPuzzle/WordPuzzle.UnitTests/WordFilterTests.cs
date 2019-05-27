using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WordPuzzle.Interfaces;
using WordPuzzle.Models;

namespace WordPuzzle.UnitTests
{
    [TestClass]
    public class WordFilterTests
    {
        IWordFilter _wordFilter;
        WordFilter _wordFilterPrivate;
        public WordFilterTests()
        {
            _wordFilter = new WordFilter();
            _wordFilterPrivate = new WordFilter();
        }

        [TestMethod]
        public void GetWordsForRegex()
        {
            List<string> words = new List<string>() { "spin", "span", "spit", "cats", "boot" };

            List<Node> nodes = _wordFilter.GetWordsForRegex("sp\\wn|spi\\w", words, "spin", new Node() { Word = "spin" } );
            nodes.OrderBy(x => x.Word);

            Assert.IsTrue(nodes.Count == 2);
            Assert.AreEqual(nodes[0].Word, "span");
            Assert.AreEqual(nodes[1].Word, "spit");
        }


        [TestMethod]
        public void SanitiseFilteredNodes_removesWords_Threshold_2()
        {
            List<Node> nodeList = new List<Node>()
            {
                new Node(){Word="cast" },
                new Node(){Word="pest" },
                new Node(){Word="peel" },
                new Node(){Word="ruin" },
                new Node(){Word="ball" },
            };
            
            string originalWord = "best";
            _wordFilterPrivate.SanitiseMatchedListTestAccessor(nodeList, originalWord, 2);

            Assert.IsTrue(nodeList.Count == 2);
            Assert.AreEqual(nodeList[0].Word, "cast");
            Assert.AreEqual(nodeList[1].Word, "pest");         
           

        }

        [TestMethod]
        public void SanitiseFilteredNodes_removesWords_Threshold_3()
        {
            List<Node> nodeList = new List<Node>()
            {
                new Node(){Word="cast" },
                new Node(){Word="pest" },
                new Node(){Word="peel" },
                new Node(){Word="ruin" },
                new Node(){Word="ball" },
            };

            string originalWord = "best";
            _wordFilterPrivate.SanitiseMatchedListTestAccessor(nodeList, originalWord, 3);

            Assert.IsTrue(nodeList.Count == 4);
            Assert.AreEqual(nodeList[0].Word, "cast");
            Assert.AreEqual(nodeList[1].Word, "pest");
            Assert.AreEqual(nodeList[2].Word, "peel");
            Assert.AreEqual(nodeList[3].Word, "ball");

        }
    }
}
