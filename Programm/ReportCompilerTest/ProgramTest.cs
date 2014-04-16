using ReportCompiler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ReportCompilerTest
{
    
    
    /// <summary>
    /// Die Klasse ProgramTest testet die Klasse Program von dem Projekt ReportCompiler
    ///</summary>
    [TestClass()]
    public class ProgramTest
    {
        /// <summary>
        /// Testet, ob die Datei gefunden wird
        ///</summary>
        [TestMethod()]
        public void OpenXMLTest()
        {
            string[] args = new string[1];
            args[0] = @"C:\Users\gpiano\Documents\test\semester2.xml";
            string path = Program.OpenXML(args);
            Assert.IsTrue(path != null);
        }
        /// <summary>
        /// Überprüft, ob Typ einer XML Datei richtig erkannt wird.
        /// </summary>
        [TestMethod()]
        public void FileTypeTest()
        {
            bool state = Program.FileType(@"C:\Users\gpiano\Documents\test\semester2.xml");
            Assert.IsTrue(state);
        }
        /// <summary>
        /// Überprüft, ob Typ einer nicht - XML Datei erkannt wird
        /// </summary>
        [TestMethod()]
        public void FileTypeTestFailed()
        {
            bool state = Program.FileType(@"C:\Users\gpiano\Documents\test\semester2.docx");
            Assert.IsFalse(state);
        }
    }
}
