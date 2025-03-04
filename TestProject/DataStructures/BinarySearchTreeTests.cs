using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp.DataStructures;
using System.Collections.Generic;
using System.Linq;

namespace TestProject.DataStructures
{
    [TestClass]
    public class BinarySearchTreeTests
    {
        [TestMethod]
        public void TestAdd()
        {
            var bst = new BinarySearchTree<int>();
            Assert.IsTrue(bst.Add(5));
            Assert.IsTrue(bst.Add(3));
            Assert.IsTrue(bst.Add(7));
            Assert.IsFalse(bst.Add(5)); // Duplicate value
            Assert.AreEqual(3, bst.Size);
        }

        [TestMethod]
        [DataRow(new int[] { 80, 4, 2, 14, -1, 6, 15, 0, 9, 16, 8, 13, 12 }, 14)]
        public void TestRemove_UseMaxValueFinder(int[] values, int valueToRemove)
        {
            var bst = new BinarySearchTree<int>();
            bst.UseMinValueFinder = false;
            foreach (var value in values)
            {
                bst.Add(value);
            }
            Assert.IsTrue(bst.Remove(valueToRemove));
            Assert.IsFalse(bst.Remove(valueToRemove)); // Value not found
            Assert.IsFalse(bst.Contains(valueToRemove));
            Assert.AreEqual(values.Length - 1, bst.Size);
            Assert.AreEqual(13, bst.RightValue(4));
            Assert.AreEqual(6, bst.LeftValue(13));
            Assert.AreEqual(15, bst.RightValue(13));
        }

        [TestMethod]
        [DataRow(new int[] { 80, 4, 2, 14, -1, 6, 15, 0, 9, 16, 8, 13, 12 }, 14)]
        public void TestRemove_UseMinValueFinder(int[] values, int valueToRemove)
        {
            var bst = new BinarySearchTree<int>();
            bst.UseMinValueFinder = true;
            foreach (var value in values)
            {
                bst.Add(value);
            }
            Assert.IsTrue(bst.Remove(valueToRemove));
            Assert.IsFalse(bst.Remove(valueToRemove)); // Value not found
            Assert.IsFalse(bst.Contains(valueToRemove));
            Assert.AreEqual(values.Length - 1, bst.Size);
            Assert.AreEqual(15, bst.RightValue(4));
            Assert.AreEqual(6, bst.LeftValue(15));
            Assert.AreEqual(16, bst.RightValue(15));
        }

        [TestMethod]
        public void TestContains()
        {
            var bst = new BinarySearchTree<int>();
            bst.Add(5);
            bst.Add(3);
            bst.Add(7);
            Assert.IsTrue(bst.Contains(5));
            Assert.IsTrue(bst.Contains(3));
            Assert.IsTrue(bst.Contains(7));
            Assert.IsFalse(bst.Contains(4));
        }

        [TestMethod]
        public void TestHeight()
        {
            var bst = new BinarySearchTree<int>();
            Assert.AreEqual(0, bst.Height());
            bst.Add(5);
            Assert.AreEqual(1, bst.Height());
            bst.Add(3);
            bst.Add(7);
            Assert.AreEqual(2, bst.Height());
            bst.Add(2);
            bst.Add(4);
            Assert.AreEqual(3, bst.Height());
        }

        [TestMethod]
        [DataRow(new int[] { 11, 6, 15, 3, 8, 13, 17, 1, 5, 12, 14, 19 }, new int[] { 11, 6, 3, 1, 5, 8, 15, 13, 12, 14, 17, 19 })]
        public void TestPreOrderTraversal(int[] values, int[] expected)
        {
            var bst = new BinarySearchTree<int>();
            foreach (var value in values)
            {
                bst.Add(value);
            }
            var result = bst.PreOrderTraversal().ToList();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(new int[] { 11, 6, 15, 3, 8, 13, 17, 1, 5, 12, 14, 19 }, new int[] { 1, 3, 5, 6, 8, 11, 12, 13, 14, 15, 17, 19 })]
        public void TestInOrderTraversal(int[] values, int[] expected)
        {
            var bst = new BinarySearchTree<int>();
            foreach (var value in values)
            {
                bst.Add(value);
            }
            var result = bst.InOrderTraversal().ToList();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestIsEmpty()
        {
            var bst = new BinarySearchTree<int>();
            Assert.IsTrue(bst.IsEmpty);
            bst.Add(5);
            Assert.IsFalse(bst.IsEmpty);
        }

        [TestMethod]
        public void TestSize()
        {
            var bst = new BinarySearchTree<int>();
            Assert.AreEqual(0, bst.Size);
            bst.Add(5);
            Assert.AreEqual(1, bst.Size);
            bst.Add(3);
            bst.Add(7);
            Assert.AreEqual(3, bst.Size);
        }

        [TestMethod]
        [DataRow(new int[] { 11, 6, 15, 3, 8, 13, 17, 1, 5, 12, 14, 19 }, 1)]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6 }, 1)]
        [DataRow(new int[] { 6, 5, 4, 3, 2, 1 }, 1)]
        [DataRow(new int[] { 3, 2, 1, 4, 5, 6 }, 1)]
        public void TestFindMin(int[] values, int minValue)
        {
            var bst = new BinarySearchTree<int>();
            foreach (var value in values)
            {
                bst.Add(value);
            }
            Assert.AreEqual(minValue, bst.FindMin());
        }
    }
}