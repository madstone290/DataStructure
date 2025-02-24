using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.DataStructures
{
    public class UnionFind
    {
        private int _size;

        private int[] _unionSizes;

        private int[] _parents;

        private int _unionCount;

        public UnionFind(int size)
        {
            _size = size;
            _unionSizes = new int[size];
            _parents = new int[size];
            _unionCount = size;
            
            for (int i = 0; i < size; i++)
            {
                _unionSizes[i] = 1;
                _parents[i] = i;
            }
        }

        /// <summary>
        /// Find which uninon 'i' belongs to
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int Find(int i)
        {
            // if parent equals to child, then it the parent(child) is the root.
            int root = i;
            while(root != _parents[root])
            {
                root = _parents[root];
            }

            // Use path compression
            int current = i;
            while(current != root)
            {
                int next = _parents[current];
                _parents[current] = root;
                current = next;
            }

            return root;
        }

        public void Unify(int i1, int i2)
        {
            int root1 = Find(i1);
            int root2 = Find(i2);

            if (root1 == root2)
                return;

            if (root1 < root2)
            {
                _unionSizes[root1] += _unionSizes[root2];
                _parents[root2] = root1;
            }
            else
            {
                _parents[root2] += _unionSizes[root1];
                _parents[root1] = root2;
            }
            _unionCount--;
        }

        public bool Connected(int i1, int i2)
        {
            return Find(i1) == Find(i2);
        }

        public int UnionSize(int i)
        {
            return _unionSizes[Find(i)];
        }

        public int Size()
        {
            return _size;
        }

        public int UnionCount()
        {
            return _unionCount;
        }


    }
}
