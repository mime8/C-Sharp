using System;
using System.Linq;
using DataStructures.ScapegoatTree;
using NUnit.Framework;

namespace DataStructures.Tests.ScapegoatTree
{
    public class ScapegoatTreeTests
    {
        [Test]
        public void Constructor_NoParameters_InstanceIsValid()
        {
            var tree = new ScapegoatTree<int>();

            Assert.IsNull(tree.Root);
            Assert.IsTrue(tree.Size == 0);
            Assert.IsTrue(tree.MaxSize == 0);
            Assert.AreEqual(0.5, tree.Alpha);
            Assert.IsNotNull(tree.Base);
            Assert.IsInstanceOf<ScapegoatTreeImplementation<int>>(tree.Base);
        }

        [Test]
        public void Constructor_AlphaParameter_InstanceIsValid()
        {
            var expected = 0.6;

            var tree = new ScapegoatTree<int>(expected);

            Assert.IsNull(tree.Root);
            Assert.IsTrue(tree.Size == 0);
            Assert.IsTrue(tree.MaxSize == 0);
            Assert.AreEqual(expected, tree.Alpha);
            Assert.IsNotNull(tree.Base);
            Assert.IsInstanceOf<ScapegoatTreeImplementation<int>>(tree.Base);
        }

        [Test]
        [TestCase(1.1)]
        [TestCase(0.4)]
        public void Constructor_AlphaParameterIsInvalid_ThrowsException(double alpha)
        {
            Assert.Throws<ArgumentException>(() => new ScapegoatTree<int>(alpha));
            Assert.Throws<ArgumentException>(() => new ScapegoatTree<int>(1, alpha));
            Assert.Throws<ArgumentException>(() => new ScapegoatTree<int>(1, alpha, new ScapegoatTreeTestImplementation<int>()));
        }

        [Test]
        public void Constructor_KeyParameter_InstanceIsValid()
        {
            var expected = 10;

            var tree = new ScapegoatTree<int>(expected);

            Assert.IsNotNull(tree.Root);
            Assert.IsTrue(tree.Root!.Key == expected);
            Assert.IsTrue(tree.Size == 1);
            Assert.IsTrue(tree.MaxSize == 1);
            Assert.AreEqual(0.5, tree.Alpha);
            Assert.IsNotNull(tree.Base);
            Assert.IsInstanceOf<ScapegoatTreeImplementation<int>>(tree.Base);
        }

        [Test]
        public void Constructor_ImplementationParameter_InstanceIsValid()
        {
            var expected = new ScapegoatTreeTestImplementation<int>();

            var tree = new ScapegoatTree<int>(expected);

            Assert.IsNull(tree.Root);
            Assert.IsTrue(tree.Size == 0);
            Assert.IsTrue(tree.MaxSize == 0);
            Assert.AreEqual(0.5, tree.Alpha);
            Assert.IsNotNull(tree.Base);
            Assert.IsInstanceOf<ScapegoatTreeTestImplementation<int>>(tree.Base);
        }

        [Test]
        public void Constructor_KeyAndAlphaParameters_InstanceIsValid()
        {
            var key = 10;
            var alpha = 0.8;

            var tree = new ScapegoatTree<int>(key, alpha);

            Assert.IsNotNull(tree.Root);
            Assert.IsTrue(tree.Size == 1);
            Assert.IsTrue(tree.MaxSize == 1);
            Assert.AreEqual(alpha, tree.Alpha);
            Assert.IsNotNull(tree.Base);
            Assert.IsInstanceOf<ScapegoatTreeImplementation<int>>(tree.Base);
        }

        [Test]
        public void Constructor_KeyAlphaAndImplementationParameters_InstanceIsValid()
        {
            var key = 10;
            var alpha = 0.8;
            var implementation = new ScapegoatTreeTestImplementation<int>();

            var tree = new ScapegoatTree<int>(key, alpha, implementation);

            Assert.IsNotNull(tree.Root);
            Assert.IsTrue(tree.Size == 1);
            Assert.IsTrue(tree.MaxSize == 1);
            Assert.AreEqual(alpha, tree.Alpha);
            Assert.IsNotNull(tree.Base);
            Assert.IsInstanceOf<ScapegoatTreeTestImplementation<int>>(tree.Base);
        }

        [Test]
        public void Constructor_NodeAlphaAndImplementationParameters_InstanceIsValid()
        {
            var node = new Node<int>(10, new Node<int>(11), new Node<int>(1));
            var alpha = 0.8;
            var implementation = new ScapegoatTreeTestImplementation<int>();

            var tree = new ScapegoatTree<int>(node, alpha, implementation);

            Assert.IsNotNull(tree.Root);
            Assert.IsTrue(tree.Size == 3);
            Assert.IsTrue(tree.MaxSize == 3);
            Assert.AreEqual(alpha, tree.Alpha);
            Assert.IsNotNull(tree.Base);
            Assert.IsInstanceOf<ScapegoatTreeTestImplementation<int>>(tree.Base);
        }

        [Test]
        public void IsAlphaWeightBalanced_RootIsNull_ReturnsTrue()
        {
            var tree = new ScapegoatTree<int>();

            var result = tree.IsAlphaWeightBalanced();

            Assert.IsTrue(result);
        }

        [Test]
        public void Search_RootIsNull_ReturnsNull()
        {
            var tree = new ScapegoatTree<int>();

            var result = tree.Search(1);

            Assert.IsNull(result);
        }

        [Test]
        public void Search_KeyIsPresent_ReturnsKey()
        {
            var tree = new ScapegoatTree<int>(key: 1);

            var result = tree.Search(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result!.Key);
        }

        [Test]
        public void Search_KeyIsNotPresent_ReturnsNull()
        {
            var tree = new ScapegoatTree<int>(key: 1);

            var result = tree.Search(2);

            Assert.IsNull(result);
        }

        [Test]
        public void Insert_RootIsNull_InsertsRoot()
        {
            var tree = new ScapegoatTree<int>();

            var inserted = tree.Insert(1);

            Assert.IsTrue(inserted);
            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(1, tree.Root!.Key);
            Assert.AreEqual(1, tree.Size);
            Assert.AreEqual(1, tree.MaxSize);
        }

        [Test]
        public void Delete_RootIsNull_ReturnsFalse()
        {
            var tree = new ScapegoatTree<int>();

            var deleted = tree.Delete(1);

            Assert.IsFalse(deleted);
        }

        [Test]
        public void Delete_KeyIsNotPresent_ReturnsFalse()
        {
            var tree = new ScapegoatTree<int>(1);

            var deleted = tree.Delete(2);

            Assert.IsFalse(deleted);
            Assert.AreEqual(1, tree.Size);
        }

        [Test]

        public void Insert_KeyIsPresent_ReturnsFalse()
        {
            var tree = new ScapegoatTree<int>(1);

            var inserted = tree.Insert(1);

            Assert.IsFalse(inserted);
            Assert.AreEqual(1, tree.Size);
            Assert.AreEqual(1, tree.MaxSize);
        }

        [Test]
        public void Remove_KeyIsPresent_RemovesKey()
        {
            var tree = new ScapegoatTree<int>(1);

            var inserted = tree.Insert(2);

            Assert.IsTrue(inserted);

            var deleted = tree.Delete(2);

            Assert.IsTrue(deleted);
            Assert.AreEqual(1, tree.Size);
        }

        [Test]
        public void Remove_KeyIsRootWithNoChildren_RemovesKey()
        {
            var tree = new ScapegoatTree<int>(1);

            var deleted = tree.Delete(1);

            Assert.IsTrue(deleted);
            Assert.IsNull(tree.Root);
            Assert.AreEqual(0, tree.Size);
            Assert.AreEqual(tree.Size, tree.MaxSize);
        }

        [Test]
        public void Remove_KeyIsRootWithOneLeftChild_RemovesKey()
        {
            var tree = new ScapegoatTree<int>(1);

            var inserted = tree.Insert(-1);

            Assert.IsTrue(inserted);

            var deleted = tree.Delete(1);

            Assert.IsTrue(deleted);
            Assert.AreEqual(1, tree.Size);
            Assert.AreEqual(tree.Size, tree.MaxSize);
        }

        [Test]
        public void Remove_KeyIsRootWithOneRightChild_RemovesKey()
        {
            var tree = new ScapegoatTree<int>(1);

            var inserted = tree.Insert(2);

            Assert.IsTrue(inserted);

            var deleted = tree.Delete(1);

            Assert.IsTrue(deleted);
            Assert.AreEqual(1, tree.Size);
            Assert.AreEqual(tree.Size, tree.MaxSize);
        }

        [Test]
        public void Remove_KeyIsRootWithTwoChildren_RemovesKey()
        {
            var tree = new ScapegoatTree<int>(1);

            var inserted = tree.Insert(-1);

            Assert.IsTrue(inserted);

            inserted = tree.Insert(2);

            Assert.IsTrue(inserted);

            var deleted = tree.Delete(1);

            Assert.IsTrue(deleted);
            Assert.AreEqual(2, tree.Size);
            Assert.AreEqual(tree.Size, tree.MaxSize);
        }

        [Test]
        public void Insert_KeyIsNotPresent_KeyIsInserted()
        {
            var tree = new ScapegoatTree<int>(1);

            var inserted = tree.Insert(2);

            Assert.IsTrue(inserted);
            Assert.AreEqual(2, tree.Size);
            Assert.AreEqual(2, tree.MaxSize);
        }

        [Test]
        [TestCase(3, new[]{2,5,1,6}, -1, 0.5)]
        public void Insert_TreeIsUnbalanced_RebuildsTree(int root, int[] keys, int candidate, double alpha)
        {
            var tree = new ScapegoatTree<int>(root, alpha);

            tree.TreeIsUnbalanced += FailTreeIsUnbalanced;

            foreach (var item in keys)
            {
                Assert.DoesNotThrow(() => tree.Insert(item));
            }

            tree.TreeIsUnbalanced -= FailTreeIsUnbalanced;
            tree.TreeIsUnbalanced += PassTreeIsUnbalanced;

            Assert.Throws<SuccessException>(() => tree.Insert(candidate));
        }

        [Test]
        [TestCase(20, new[]{10,30,5,11,29,40,50, 1, 12}, new[]{50,40,30,29}, 0.7)]
        public void Delete_TreeIsUnbalanced_BalancesTree(int root, int[] keys, int[] candidates, double alpha)
        {
            var tree = new ScapegoatTree<int>(root, alpha);

            tree.TreeIsUnbalanced += FailTreeIsUnbalanced;

            foreach (var item in keys)
            {
                Assert.DoesNotThrow(() => tree.Insert(item));
            }

            tree.TreeIsUnbalanced -= FailTreeIsUnbalanced;
            tree.TreeIsUnbalanced += PassTreeIsUnbalanced;

            Assert.Throws<SuccessException>(() =>
            {
                foreach (var item in candidates)
                {
                    tree.Delete(item);
                }
            });
        }

        [Test]
        [TestCase(3, new[]{2,5,1,6}, -1, 0.5)]
        [TestCase(3, new[]{2,5,1,6}, 7, 0.5)]
        public void Insert_TreeIsUnbalanced_BalancesTree(int root, int[] keys, int candidate, double alpha)
        {
            var tree = new ScapegoatTree<int>(root, alpha);

            tree.TreeIsUnbalanced += FailTreeIsUnbalanced;

            foreach (var item in keys)
            {
                Assert.DoesNotThrow(() => tree.Insert(item));
            }

            tree.TreeIsUnbalanced -= FailTreeIsUnbalanced;

            var inserted = tree.Insert(candidate);

            Assert.True(inserted);
            Assert.True(tree.Size == 6);
            Assert.True(tree.IsAlphaWeightBalanced());
        }

        [Test]
        public void Contains_RootIsNull_ReturnsFalse()
        {
            var tree = new ScapegoatTree<int>();

            Assert.IsFalse(tree.Contains(1));
        }

        [Test]
        public void Contains_RootHasKey_ReturnsTrue()
        {
            var tree = new ScapegoatTree<int>(1);

            Assert.IsTrue(tree.Contains(1));
        }

        [Test]
        public void Contains_TreeHasKey_ReturnsTrue()
        {
            var tree = new ScapegoatTree<int>(1);

            tree.Insert(2);

            Assert.IsTrue(tree.Contains(2));
        }

        [Test]
        public void Contains_TreeDoesNotContainKey_ReturnsFalse()
        {
            var tree = new ScapegoatTree<int>(1);

            tree.Insert(2);

            Assert.IsFalse(tree.Contains(-1));
        }

        [Test]
        public void Clear_TreeHasKeys_ClearsTree()
        {
            var tree = new ScapegoatTree<int>(1);

            tree.Clear();

            Assert.IsTrue(tree.Size == 0);
            Assert.IsTrue(tree.MaxSize == 0);
            Assert.IsNull(tree.Root);
        }

        [Test]
        public void Tune_AlphaIsValid_ChangesAlpha()
        {
            var expected = 0.7;

            var tree = new ScapegoatTree<int>();

            tree.Tune(expected);

            Assert.AreEqual(expected, tree.Alpha);
        }

        [Test]
        public void Tune_AlphaIsNotValid_ThrowsException()
        {
            var expected = 9.9;

            var tree = new ScapegoatTree<int>();

            Assert.Throws<ArgumentException>(() => tree.Tune(expected));
        }

        [Test]
        public void GetEnumerator_RootIsNull_ReturnsEmpty()
        {
            var tree = new ScapegoatTree<int>();

            Assert.AreEqual(Enumerable.Empty<int>(), tree.GetEnumerator());
        }

        [Test]
        public void GetEnumerator_RootIsNotNull_ReturnsRootEnumerator()
        {
            var tree = new ScapegoatTree<int>(1);

            Assert.AreNotEqual(Enumerable.Empty<int>(), tree.GetEnumerator());
        }

        [Test]
        public void IEnumeratorGetEnumerator_RootIsNull_ReturnsEmpty()
        {
            var tree = new ScapegoatTree<int>();

            Assert.AreEqual(Enumerable.Empty<int>(), (tree as System.Collections.IEnumerable)!.GetEnumerator());
        }

        [Test]
        public void IEnumeratorGetEnumerator_RootIsNotNull_ReturnsEmpty()
        {
            var tree = new ScapegoatTree<int>(1);

            Assert.AreNotEqual(Enumerable.Empty<int>(), (tree as System.Collections.IEnumerable)!.GetEnumerator());
        }

        [Test]
        public void Delete_TreeIsUnbalanced_MaxSizeEqualsSize()
        {
            var root = new Node<int>(3, new Node<int>(5), new Node<int>(2));
            //root.Left!.Left = new Node<int>(1);
            root.Right!.Right = new Node<int>(6);
            root.Right!.Right.Right = new Node<int>(7);
            root.Right!.Right.Right.Right = new Node<int>(9, new Node<int>(10), null);

            var tree = new ScapegoatTree<int>(root, 0.5);

            tree.TreeIsUnbalanced += PassTreeIsUnbalanced;

            Assert.Throws<SuccessException>(() =>
            {
                tree.Delete(3);
            });

            Assert.AreEqual(tree.Size, tree.MaxSize);
        }

        private static void FailTreeIsUnbalanced(object? sender, EventArgs? e)
        {
            Assert.Fail();
        }

        private static void PassTreeIsUnbalanced(object? sender, EventArgs? e)
        {
            Assert.Pass();
        }
    }
}
