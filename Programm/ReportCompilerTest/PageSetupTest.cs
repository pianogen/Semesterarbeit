using ReportCompiler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ReportCompilerTest
{
    
    
    /// <summary>
    /// Diese Testklasse testet die Klasse PageSetup des Projekts ReportCompiler
    ///</summary>
    [TestClass()]
    public class PageSetupTest
    {


        /// <summary>
        /// Testet das Verhalten der Property Save
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {
            PageSetup target = new PageSetup(); // TODO: Initialize to an appropriate value
            string expected = "Testpfad"; // TODO: Initialize to an appropriate value
            string actual;
            target.Save = expected;
            actual = target.Save;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testet das Verhalten der Property Template
        ///</summary>
        [TestMethod()]
        public void TemplateTest()
        {
            PageSetup target = new PageSetup(); // TODO: Initialize to an appropriate value
            string expected = "TestPfad"; // TODO: Initialize to an appropriate value
            string actual;
            target.Template = expected;
            actual = target.Template;
            Assert.AreEqual(expected, actual);
        }
    }
}
