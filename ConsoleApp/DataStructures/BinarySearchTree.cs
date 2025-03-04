using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.DataStructures
{
    public class BinarySearchTree<T>
        where T : IComparable<T>
    {
        private class Node
        {
            public T _data;
            public Node _left;
            public Node _right;

            public Node(T data, Node left, Node right)
            {
                _data = data;
                _left = left;
                _right = right;
            }

            public override string ToString()
            {
                string left = _left == null ? "null" : _left._data.ToString();
                string right = _right == null ? "null" : _right._data.ToString();
                return $"{_data}, left: {left}, right: {right}";
            }
        }

        /// <summary>
        /// The number of nodes in the tree
        /// </summary>
        private int _nodeCount = 0;

        /// <summary>
        /// The root node of the tree
        /// </summary>
        private Node root;

        public bool IsEmpty => _nodeCount == 0;
        public int Size => _nodeCount;

        public bool UseMinValueFinder { get; set; }

        /// <summary>
        /// Add a value to the binary search tree
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Add(T value)
        {
            // Check if the value already exists in the tree
            if (Contains(value))
            {
                return false;
            }

            // If the value does not exist, add it to the tree
            root = Add(root, value);
            _nodeCount++;
            return true;
        }


        /// <summary>
        /// Recursive add function
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private Node Add(Node node, T value)
        {
            // Base case: found a leaf node
            if (node == null)
            {
                node = new Node(value, null, null);
                return node;
            }

            // Add the value to the left subtree
            if (value.CompareTo(node._data) < 0)
            {
                node._left = Add(node._left, value);
            }
            // Add the value to the right subtree
            else
            {
                node._right = Add(node._right, value);
            }
            return node;
        }

        public bool Remove(T value)
        {
            // Check if the value exists in the tree
            if (!Contains(value))
            {
                return false;
            }
            root = Remove(root, value);
            _nodeCount--;
            return true;
        }

        private Node Remove(Node node, T value)
        {
            if (node == null)
            {
                return null;
            }

            int compareValue = value.CompareTo(node._data);
            // Dig into the left subtree, the value we're looking for is smaller
            if (compareValue < 0)
            {
                node._left = Remove(node._left, value);
                return node;
            }

            // Dig into the right subtree, the value we're looking for is larger
            if (compareValue > 0)
            {
                node._right = Remove(node._right, value);
                return node;
            }

            // Found the node we wish to remove
            // This is the case with only a right subtree or no subtree at all
            if (node._left == null)
            {
                Node rightChild = node._right;
                node._data = default;
                node = null;
                return rightChild;
            }
            // This is the case with only a left subtree or no subtree at all
            if (node._right == null)
            {
                Node leftChild = node._left;
                node._data = default;
                node = null;
                return leftChild;
            }

            // This is the case with two children
            // Find the leftmost node in the right subtree
            // or the rightmost node in the left subtree
            Node temp = UseMinValueFinder ? FindMin(node._right) : FindMax(node._left);
            node._data = temp._data;
            node._right = Remove(node._right, temp._data);
            return node;
        }

        public T FindMin()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The tree is empty");
            }
            return FindMin(root)._data;
        }

        private Node FindMin(Node node)
        {
            Node current = node;
            while (current._left != null)
            {
                current = current._left;
            }
            return current;
        }

        private Node FindMax(Node node)
        {
            Node current = node;
            while (current._right != null)
            {
                current = current._right;
            }
            return current;
        }

        public bool Contains(T value)
        {
            return Contains(root, value);
        }

        private bool Contains(Node node, T value)
        {
            if (node == null)
            {
                return false;
            }
            int compareValue = value.CompareTo(node._data);
            // Dig into the left subtree
            if (compareValue < 0)
            {
                return Contains(node._left, value);
            }
            // Dig into the right subtree
            if (compareValue > 0)
            {
                return Contains(node._right, value);
            }
            // Found the value
            return true;
        }

        public int Height()
        {
            return Height(root);
        }

        private int Height(Node node)
        {
            if (node == null)
            {
                return 0;
            }
            return Math.Max(Height(node._left), Height(node._right)) + 1;
        }

        public IEnumerable<T> PreOrderTraversal()
        {
            return PreOrderTraversal(root);
        }

        private IEnumerable<T> PreOrderTraversal(Node node)
        {
            if (node == null)
            {
                yield break;
            }
            yield return node._data;
            foreach (var left in PreOrderTraversal(node._left))
            {
                yield return left;
            }
            foreach (var right in PreOrderTraversal(node._right))
            {
                yield return right;
            }
        }

        public IEnumerable<T> InOrderTraversal()
        {
            List<Node> list = new List<Node>();
            InOrderTraversal(root, list);
            return list.Select(n => n._data);
        }

        private void InOrderTraversal(Node node, List<Node> list)
        {
            if (node == null)
            {
                return;
            }
            InOrderTraversal(node._left, list);
            list.Add(node);
            InOrderTraversal(node._right, list);
        }

        public bool HasLeftValue(T value)
        {
            Node node = FindNode(root, value);
            return node != null && node._left != null;
        }

        public bool HasRightValue(T value)
        {
            Node node = FindNode(root, value);
            return node != null && node._right != null;
        }

        public T LeftValue(T value)
        {
            Node node = FindNode(root, value);
            if (node == null || node._left == null)
            {
                throw new InvalidOperationException("The value does not have a left child");
            }

            return node._left._data;
        }

        public T RightValue(T value)
        {
            Node node = FindNode(root, value);
            if (node == null || node._left == null)
            {
                throw new InvalidOperationException("The value does not have a left child");
            }
            return node._right._data;
        }

        private Node FindNode(Node node, T value)
        {
            if (node == null)
            {
                return null;
            }
            if (node._data.Equals(value))
            {
                return node;
            }
            if (value.CompareTo(node._data) < 0)
            {
                return FindNode(node._left, value);
            }
            return FindNode(node._right, value);
        }
    }


}
