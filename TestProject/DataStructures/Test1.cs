using ConsoleApp.DataStructures;

namespace TestProject.DataStructures
{
    [TestClass]
    public class UnionFindTests
    {
        [TestMethod]
        public void TestUnionFind_Initialization()
        {
            int size = 10;
            UnionFind uf = new UnionFind(size);

            Assert.AreEqual(size, uf.Size());
            Assert.AreEqual(size, uf.UnionCount());

            for (int i = 0; i < size; i++)
            {
                Assert.AreEqual(i, uf.Find(i));
                Assert.AreEqual(1, uf.UnionSize(i));
            }
        }

        [TestMethod]
        public void TestUnionFind_Unify()
        {
            UnionFind uf = new UnionFind(10);

            uf.Unify(1, 2);
            Assert.IsTrue(uf.Connected(1, 2));
            Assert.AreEqual(9, uf.UnionCount());

            uf.Unify(2, 3);
            Assert.IsTrue(uf.Connected(1, 3));
            Assert.AreEqual(8, uf.UnionCount());

            uf.Unify(4, 5);
            Assert.IsTrue(uf.Connected(4, 5));
            Assert.AreEqual(7, uf.UnionCount());

            uf.Unify(1, 5);
            Assert.IsTrue(uf.Connected(1, 4));
            Assert.AreEqual(6, uf.UnionCount());
        }

        [TestMethod]
        public void TestUnionFind_Connected()
        {
            UnionFind uf = new UnionFind(10);

            uf.Unify(1, 2);
            uf.Unify(2, 3);
            uf.Unify(4, 5);

            Assert.IsTrue(uf.Connected(1, 3));
            Assert.IsFalse(uf.Connected(1, 4));
            Assert.IsTrue(uf.Connected(4, 5));
        }

        [TestMethod]
        public void TestUnionFind_UnionSize()
        {
            UnionFind uf = new UnionFind(10);

            uf.Unify(1, 2);
            uf.Unify(2, 3);

            Assert.AreEqual(3, uf.UnionSize(uf.Find(1)));
            Assert.AreEqual(3, uf.UnionSize(uf.Find(2)));
            Assert.AreEqual(3, uf.UnionSize(uf.Find(3)));

            uf.Unify(4, 5);
            Assert.AreEqual(2, uf.UnionSize(uf.Find(4)));
            Assert.AreEqual(2, uf.UnionSize(uf.Find(5)));
        }

        [TestMethod]
        public void TestUnionFind_Size()
        {
            UnionFind uf = new UnionFind(10);
            uf.Unify(0, 1);
            uf.Unify(1, 2);
            uf.Unify(2, 3);
            uf.Unify(3, 4);
            uf.Unify(4, 5);
            uf.Unify(5, 6);
            uf.Unify(6, 7);
            uf.Unify(7, 8);
            uf.Unify(8, 9);

            Assert.AreEqual(10, uf.Size());
            Assert.AreEqual(1, uf.UnionCount());
            Assert.AreEqual(true, uf.Connected(0, 9));
            Assert.AreEqual(10, uf.UnionSize(0));
            Assert.AreEqual(10, uf.UnionSize(1));
            Assert.AreEqual(10, uf.UnionSize(2));
            Assert.AreEqual(10, uf.UnionSize(3));
            Assert.AreEqual(10, uf.UnionSize(4));
            Assert.AreEqual(10, uf.UnionSize(5));
            Assert.AreEqual(10, uf.UnionSize(6));
            Assert.AreEqual(10, uf.UnionSize(7));
            Assert.AreEqual(10, uf.UnionSize(8));
            Assert.AreEqual(10, uf.UnionSize(9));


        }
    }
}