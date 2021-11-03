using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.ScapegoatTree;
using NUnit.Framework;

namespace DataStructures.Tests.ScapegoatTree
{
    public class ScapegoatTreeImplementationTests
    {

        private readonly ScapegoatTreeImplementation<int> implementation = new();

        [Test]
        public void SearchWithRoot_TreeContainsLeftKey_ReturnsLeftNode()
        {
            var root = new Node<int>(1)
            {
                Left = new Node<int>(-1),
                Right = new Node<int>(2),
            };

            var result = implementation.SearchWithRoot(root, -1);

            Assert.AreEqual(root.Left, result);
        }

        [Test]
        public void SearchWithRoot_TreeContainsRightKey_ReturnsRightNode()
        {
            var root = new Node<int>(1)
            {
                Left = new Node<int>(-1),
                Right = new Node<int>(2),
            };

            var result = implementation.SearchWithRoot(root, 2);

            Assert.AreEqual(root.Right, result);
        }

        [Test]
        public void SearchWithRoot_TreeDoesNotContainLeftKey_ReturnsNull()
        {
            var root = new Node<int>(1)
            {
                Right = new Node<int>(2),
            };

            var result = implementation.SearchWithRoot(root, -1);

            Assert.IsNull(result);
        }

        [Test]
        public void SearchWithRoot_TreeDoesNotContainRightKey_ReturnsNull()
        {
            var root = new Node<int>(1)
            {
                Left = new Node<int>(-1),
            };

            var result = implementation.SearchWithRoot(root, 2);

            Assert.IsNull(result);
        }

        [Test]
        public void TryDeleteWithRoot_RootContainsLeftKey_LeftNodeIsDeleted()
        {
            var root = new Node<int>(1)
            {
                Left = new Node<int>(-1),
                Right = new Node<int>(2),
            };

            var result = implementation.TryDeleteWithRoot(root, -1);

            Assert.IsTrue(result);
            Assert.IsNull(root.Left);
        }

        [Test]
        public void TryDeleteWithRoot_RootDoesNotContainLeftKey_ReturnsFalse()
        {
            var root = new Node<int>(1)
            {
                Left = new Node<int>(-1),
                Right = new Node<int>(2),
            };

            var result = implementation.TryDeleteWithRoot(root, -2);

            Assert.IsFalse(result);
        }

        [Test]
        public void TryDeleteWithRoot_RootContainsRightKey_RightNodeIsDeleted()
        {
            var root = new Node<int>(1)
            {
                Left = new Node<int>(-1),
                Right = new Node<int>(2),
            };

            var result = implementation.TryDeleteWithRoot(root, 2);

            Assert.IsTrue(result);
            Assert.IsNull(root.Right);
        }

        [Test]
        public void TryDeleteWithRoot_RootDoesNotContainRightKey_ReturnsFalse()
        {
            var root = new Node<int>(1)
            {
                Left = new Node<int>(-1),
                Right = new Node<int>(2),
            };

            var result = implementation.TryDeleteWithRoot(root, 3);

            Assert.IsFalse(result);
        }

        [Test]
        public void TryDeleteWithRoot_LeftKeyHasOneLeftChild_ChildTakesParentsPlace()
        {
            var root = new Node<int>(1)
            {
                Left = new Node<int>(-1)
                {
                    Left = new Node<int>(-2),
                },
                Right = new Node<int>(2),
            };

            var result = implementation.TryDeleteWithRoot(root, -1);

            Assert.IsTrue(result);
            Assert.IsNotNull(root.Left);
            Assert.AreEqual(3, root.GetSize());
            Assert.AreEqual(-2, root.Left.Key);
            Assert.IsNull(root.Left.Left);
        }

        [Test]
        public void TryDeleteWithRoot_LeftKeyHasOneRightChild_ChildTakesParentsPlace()
        {
            var root = new Node<int>(1)
            {
                Left = new Node<int>(-1)
                {
                    Right = new Node<int>(0),
                },
                Right = new Node<int>(2),
            };

            var result = implementation.TryDeleteWithRoot(root, -1);

            Assert.IsTrue(result);
            Assert.IsNotNull(root.Left);
            Assert.AreEqual(3, root.GetSize());
            Assert.AreEqual(0, root.Left.Key);
            Assert.IsNull(root.Left.Right);
        }

        [Test]
        public void TryDeleteWithRoot_LeftKeyHasTwoChildren_LargestLeftChildTakesParentsPlace()
        {
            var root = new Node<int>(3)
            {
                Left = new Node<int>(1)
                {
                    Right = new Node<int>(2),
                    Left = new Node<int>(-1)
                    {
                        Right = new Node<int>(0),
                        Left = new Node<int>(-2),
                    },
                },
                Right = new Node<int>(4),
            };

            var result = implementation.TryDeleteWithRoot(root, 1);

            Assert.IsTrue(result);
            Assert.IsNotNull(root.Left);
            Assert.IsNotNull(root.Left.Left);
            Assert.AreEqual(6, root.GetSize());
            Assert.AreEqual(0, root.Left.Key);
        }

        [Test]
        public void TryDeleteWithRoot_RightKeyHasTwoChildren_LargestLeftChildTakesParentsPlace()
        {
            var root = new Node<int>(3)
            {
                Left = new Node<int>(1),
                Right = new Node<int>(10)
                {
                    Right = new Node<int>(11),
                    Left = new Node<int>(8)
                    {
                        Right = new Node<int>(9),
                        Left = new Node<int>(7),
                    },
                },
            };

            var result = implementation.TryDeleteWithRoot(root, 10);

            Assert.IsTrue(result);
            Assert.IsNotNull(root.Right);
            Assert.IsNotNull(root.Right.Left);
            Assert.AreEqual(6, root.GetSize());
            Assert.AreEqual(9, root.Right.Key);
        }

        [Test]
        public void TryInsertWithRoot_NewKeyIsSmaller_AddsKeyToTheLeft()
        {
            var root = new Node<int>(3);
            var node = new Node<int>(1);
            var path = new Stack<Node<int>>();

            var inserted = implementation.TryInsertWithRoot(root, node, path);

            Assert.IsTrue(inserted);
            Assert.IsNotNull(root.Left);
            Assert.AreEqual(1, path.Count);
        }

        [Test]
        public void TryInsertWithRoot_NewKeyIsBigger_AddsKeyToTheRight()
        {
            var root = new Node<int>(3);
            var node = new Node<int>(4);
            var path = new Stack<Node<int>>();

            var inserted = implementation.TryInsertWithRoot(root, node, path);

            Assert.IsTrue(inserted);
            Assert.IsNotNull(root.Right);
            Assert.AreEqual(1, path.Count);
        }

        [Test]
        public void TryInsertWithRoot_KeyIsDuplicate_ReturnsFalse()
        {
            var root = new Node<int>(3);
            var node = new Node<int>(3);
            var path = new Stack<Node<int>>();

            var inserted = implementation.TryInsertWithRoot(root, node, path);

            Assert.IsFalse(inserted);
            Assert.IsNull(root.Left);
            Assert.IsNull(root.Left);
            Assert.AreEqual(1, path.Count);
        }

        [Test]
        public void FindScapegoatInPath_PathIsEmpty_ThrowsException()
        {
            var path = new Stack<Node<int>>();

            Assert.Throws<ArgumentException>(() => implementation.FindScapegoatInPath(path, 0.5));
        }

        [Test]
        [TestCase(8, new[]{1,13,10,20,19,22,29}, 0.57, 8)]
        [TestCase(3, new[]{2,1,5,6,-1}, 0.5, 2)]
        public void FindScapegoatInPath_TreeIsUnbalanced_ReturnsScapegoat(int first, int[] nodes, double alpha, int expected)
        {
            var path = new Stack<Node<int>>();

            var root = new Node<int>(first);

            foreach (var item in nodes)
            {
                path.Clear();
                implementation.TryInsertWithRoot(root, new Node<int>(item), path);
            }

            var (_, scapegoat) = implementation.FindScapegoatInPath(path, alpha);

            Assert.AreEqual(expected, scapegoat.Key);
        }

        [Test]
        [TestCase(19, new[]{10,8,13,1,22,20,29}, 0.57)]
        [TestCase(3, new[]{2,1,5,6}, 0.5)]
        public void FindScapegoatInPath_TreeIsBalanced_ThrowsException(int first, int[] nodes, double alpha)
        {
            var path = new Stack<Node<int>>();

            var root = new Node<int>(first);

            foreach (var item in nodes)
            {
                path.Clear();
                implementation.TryInsertWithRoot(root, new Node<int>(item), path);
            }

            var ex = Assert.Throws<InvalidOperationException>(
                () => implementation.FindScapegoatInPath(path, alpha));

            Assert.AreEqual("Scapegoat node wasn't found. The tree should be unbalanced.", ex.Message);
        }

        [Test]
        [TestCase(8, new[] { 1, 13, 10, 20, 19, 22, 29 })]
        [TestCase(3, new[] { 2, 1, 5, 6, -1 })]
        public void FlattenTree_RootHasValues_FlattensInOrder(int first, int[] nodes)
        {
            var root = new Node<int>(first);

            foreach (var item in nodes)
            {
                implementation.TryInsertWithRoot(root, new Node<int>(item), new Stack<Node<int>>());
            }

            var expected = root.ToList();
            var actual = new List<Node<int>>();

            implementation.FlattenTree(root, actual);

            Assert.IsNotEmpty(actual);
            Assert.AreEqual(expected.Count, actual.Count);

            var keys = actual.Select(x => x.Key).ToList();

            Assert.AreEqual(expected, keys);
        }

        [Test]
        public void RebuildFlatTree_ValidFlatTree_RebuildsTree()
        {
            var expected = new Node<int>(3)
            {
                Left = new Node<int>(2)
                {
                    Left = new Node<int>(1)
                    {
                        Left = new Node<int>(-1),
                    },
                },
                Right = new Node<int>(5)
                {
                    Right = new Node<int>(6),
                },
            };


            var list = new List<Node<int>>
            {
                new(-1),
                new(1),
                new(2),
                new(3),
                new(5),
                new(6),
            };

            var tree = implementation.RebuildFromList(list, 0, list.Count - 1);

            Assert.AreEqual(list.Count, tree.GetSize());
            Assert.AreEqual(expected, tree);

        }

        [Test]
        public void RebuildFromList_StartIsInvalid_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => implementation.RebuildFromList(new List<Node<int>>(), 1, 0));
        }

        [Test]
        public void RebuildWithRoot_TreeIsUnbalanced_TreeStructureIsChanged()
        {
            var root = new Node<int>(3)
            {
                Left = new Node<int>(2),
                Right = new Node<int>(5),
            };

            root.Left.Left = new Node<int>(1);
            root.Right.Right = new Node<int>(6);
            root.Left.Left.Left = new Node<int>(-1);

            var result = implementation.RebuildWithRoot(root);

            Assert.IsTrue(result.IsAlphaWeightBalanced(0.5));
            Assert.IsTrue(result.Key == 3);
            Assert.IsNotNull(result.Left);
            Assert.AreEqual(1, result.Left!.Key);
        }
    }
}
