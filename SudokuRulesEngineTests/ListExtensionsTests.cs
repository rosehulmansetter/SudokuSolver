using NUnit.Framework;
using System.Collections.Generic;
using SudokuRulesEngine.ExtensionMethods;
using FluentAssertions;

namespace SudokuRulesEngineTests
{
    [TestFixture]
    public class ListExtensionsTests
    {
        [Test]
        public void TestIsSolved()
        {
            List<char> list = new List<char>();
            list.IsSolved().Should().BeFalse();

            list.Add('c');
            list.IsSolved().Should().BeTrue();

            list.Add('f');
            list.IsSolved().Should().BeFalse();
        }
    }
}
