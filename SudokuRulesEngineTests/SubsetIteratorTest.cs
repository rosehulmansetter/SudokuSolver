using NUnit.Framework;
using SudokuRulesEngine;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class SubsetIteratorTests
    {
        [Test]
        public void TestSubsetIterator()
        {
            SubsetIterator<int> it = new SubsetIterator<int>(2, new List<int> { 1, 6, 7, 9 });

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 1, 6 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 1, 7 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 1, 9 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 6, 7 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 6, 9 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 7, 9 }, it.Current);

            Assert.IsFalse(it.MoveNext());
        }

        [Test]
        public void TestBiggerSubsetIterator()
        {
            SubsetIterator<int> it = new SubsetIterator<int>(3, new List<int> { 1, 3, 4, 6, 7, 9 });

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 1, 3, 4 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 1, 3, 6 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 1, 3, 7 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 1, 3, 9 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 1, 4, 6 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 1, 4, 7 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 1, 4, 9 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 1, 6, 7 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 1, 6, 9 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 1, 7, 9 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 3, 4, 6 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 3, 4, 7 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 3, 4, 9 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 3, 6, 7 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 3, 6, 9 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 3, 7, 9 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 4, 6, 7 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 4, 6, 9 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 4, 7, 9 }, it.Current);

            Assert.IsTrue(it.MoveNext());
            AssertListEquality(new List<int> { 6, 7, 9 }, it.Current);

            Assert.IsFalse(it.MoveNext());
        }

        private void AssertListEquality(List<int> expected, List<int> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            CollectionAssert.AllItemsAreUnique(actual);

            foreach(int expectedValue in expected)
            {
                CollectionAssert.Contains(actual, expectedValue);
            }
        }
    }
}