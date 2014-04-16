using ReportCompiler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ReportCompilerTest
{
    
    
    /// <summary>
    /// Diese Testklasse teste die Klasse Mathematics aus dem Projekt ReportCompiler
    ///</summary>
    [TestClass()]
    public class MathematicsTest
    {
        /// <summary>
        /// Testet das Verhalten der Property Formula
        ///</summary>
        [TestMethod()]
        public void FormulaTest()
        {
            Mathematics target = new Mathematics(); // TODO: Initialize to an appropriate value
            string expected = "Test"; // TODO: Initialize to an appropriate value
            string actual;
            target.Formula = expected;
            actual = target.Formula;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testet das Verhalten der Property Name
        ///</summary>
        [TestMethod()]
        public void NameTest()
        {
            Mathematics target = new Mathematics(); // TODO: Initialize to an appropriate value
            string expected = "Test"; // TODO: Initialize to an appropriate value
            string actual;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Teste das Verhalten der Property Result
        ///</summary>
        [TestMethod()]
        public void ResultTest()
        {
            Mathematics target = new Mathematics(); // TODO: Initialize to an appropriate value
            Decimal expected = 1.45M; // TODO: Initialize to an appropriate value
            Decimal actual;
            target.Result = expected;
            actual = target.Result;
            Assert.AreEqual(expected, actual);
        }
    }
}
