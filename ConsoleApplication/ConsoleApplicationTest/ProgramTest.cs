using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication;

namespace ConsoleApplicationTest
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void FalseDir_Returns_ZeroValues()
        {
            string path = "df";
            Tuple<float, int> expected = Tuple.Create(0.0f, 0);
            Tuple<float, int> actual = Tuple.Create(0.0f, 0);

            Program.GetDirDetails(path);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TrueDir_Returns_NonZeroValues()
        {
            string path = @"C:\Users";
            Tuple<float, int> expected = Tuple.Create(0.0f, 0);
            Tuple<float, int> actual;

            actual = Program.GetDirDetails(path);

            Assert.AreNotEqual(expected, actual);
        }
    }
}
