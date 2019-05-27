using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WordPuzzle.Interfaces;
using WordPuzzle.Models;

namespace WordPuzzle.UnitTests
{

   

    [TestClass]
    public class NodeProcessorTests
    {
        INodeProcessor _nodeProcessor;
        IWordFilter _wordFilter;
        IWordUtility _wordUtility;

        public NodeProcessorTests()
        {
            _wordUtility = new WordUtility();
            _wordFilter = new WordFilter();
            _nodeProcessor = new NodeProcessor(_wordFilter, _wordUtility);

        }

        [TestMethod]
        public void ProcessNodes_EndWordFound_EventFired()
        {
            bool completeEventRaised = false; ;
            _nodeProcessor.OnComplete += (o, e) => { completeEventRaised = true; };

            List<Node> nodes = new List<Node>();
            nodes.Add(new Node() { Word = "test" });
            nodes.Add(new Node() { Word = "cats" });

            AppProperties props = new AppProperties();
            props.StartWord = "rats";
            props.EndWord = "cats";
            props.WordList = new List<string>() { "test", "cats", "rats", "spin" };

            bool result = _nodeProcessor.ProcessNodes(nodes, props);

            Assert.IsTrue(result);
            Assert.IsTrue(completeEventRaised);

        }

        [TestMethod]
        public void ProcessNodes_EndWordNotFoundFound_EventNotFired()
        {
            bool completeEventRaised = false; ;
            _nodeProcessor.OnComplete += (o, e) => { completeEventRaised = true; };

            List<Node> nodes = new List<Node>();
            nodes.Add(new Node() { Word = "test" });
            nodes.Add(new Node() { Word = "cast" });

            AppProperties props = new AppProperties();
            props.StartWord = "rats";
            props.EndWord = "cats";
            props.WordList = new List<string>() { "test", "cats", "rats", "spin" };

            bool result = _nodeProcessor.ProcessNodes(nodes, props);

            Assert.IsFalse(result);
            Assert.IsFalse(completeEventRaised);

        }

        [TestMethod]
        public void AdvanceCurrentNodesToChildNodes()
        {
            List<Node> nodes = new List<Node>();

            Node curretnNode1= new Node() { Word = "test" };
            Node curretnNode2 = new Node() { Word = "cast" };

            Node childNode1 = new Node() { Word = "best" };
            Node childNode2 = new Node() { Word = "bats" };
            Node childNode3 = new Node() { Word = "ball" };
            Node childNode4 = new Node() { Word = "call" };

            curretnNode1.ChildNodes = new List<Node>() { childNode1, childNode2 };
            curretnNode2.ChildNodes = new List<Node>() { childNode3, childNode4 };

            nodes.Add(curretnNode1);
            nodes.Add(curretnNode2);
                        
            _nodeProcessor.AdvanceCurrentNodes(nodes);

            Assert.IsTrue(nodes.Count == 4);
            Assert.AreEqual(nodes[0], childNode1);
            Assert.AreEqual(nodes[1], childNode2);
            Assert.AreEqual(nodes[2], childNode3);
            Assert.AreEqual(nodes[3], childNode4);

        }
    }
}
