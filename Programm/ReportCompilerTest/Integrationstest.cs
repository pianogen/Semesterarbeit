using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportCompiler;

namespace ReportCompilerTest
{
    /// <summary>
    /// Diese Testklasse testet die Zusammenarbeit aller Komponenten des Projekts ReportCompiler
    /// </summary>
    [TestClass]
    public class Integrationstest
    {
        private XDocument xml;
        /// <summary>
        /// Initialisiert ein XML Objekt
        /// </summary>
        [TestInitialize]
        public void InitializeXML()
        {
            xml = new XDocument(
                new XElement("rapport",
                    new XElement("connections"),
                    new XElement("variables"),
                    new XElement("maths"),
                    new XElement("document")
                )
            );
        }
        /// <summary>
        /// Fügt dem XML Objekt das Format Element hinzu
        /// </summary>
        public void AddElement()
        {
            xml.Root.Element("document").Add(
                new XElement("format")
            );
        }
        /// <summary>
        /// Fügt dem XML Objekt ein Connection Element hinzu
        /// </summary>
        public void Connection()
        {
            xml.Root.Element("connections").Add(
                new XElement("connection",
                    new XElement("id", "test"),
                    new XElement("type", "MSSQL"),
                    new XElement("server", "localhost\\SQLExpress"),
                    new XElement("database", "school_example"),
                    new XElement("user", "ReportingTool"),
                    new XElement("password", "F1rstC0mesF1rst")
                )
             );
        }
        /// <summary>
        /// Fügt dem XML Objekt ein Variabel Element und die Format Parameter hinzu
        /// </summary>
        public void Variables()
        {
            xml.Root.Element("document").Element("format").Add(
               new XElement("savepath", "C:\\Users\\gpiano\\Documents\\test\\integrationstest_Variable.docx"),
               new XElement("templatepath", "C:\\Users\\gpiano\\Documents\\test\\vorlage.dotx")
            );
            xml.Root.Element("variables").Add(
                new XElement("variable",
                    new XElement("id", ""),
                    new XElement("name", "test"),
                    new XElement("type", "int"),
                    new XElement("source", "local"),
                    new XElement("content", "3")
                )
            );
           
        }

        /// <summary>
        /// Fügt dem XML Objekt ein Mathematic Element und die Format Parameter hinzu
        /// </summary>
        public void Mathe()
        {
            xml.Root.Element("document").Element("format").Add(
               new XElement("savepath", "C:\\Users\\gpiano\\Documents\\test\\integrationstest_Mathe.docx"),
               new XElement("templatepath", "C:\\Users\\gpiano\\Documents\\test\\vorlage.dotx")
            );
            xml.Root.Element("maths").Add(
                new XElement("math",
                    new XElement("name", "test"),
                    new XElement("formula", "3*3")
                )
             );
        }
        /// <summary>
        /// Fügt dem XML Objekt die Format Paramter hinzu
        /// </summary>
        public void Format()
        {
            xml.Root.Element("document").Element("format").Add(
                new XElement("savepath", @"C:\Users\gpiano\Documents\test\integrationstest.docx"),
                new XElement("templatepath", @"C:\Users\gpiano\Documents\test\vorlage.dotx")
            );
        }
        /// <summary>
        /// Fügt dem XML Objekt ein Text Element und die Format Parameter hinzu
        /// </summary>
        public void Text()
        {
            xml.Root.Element("document").Add(
                new XElement("text",
                    new XElement("string", "test"),
                    new XElement("font", "Arial"),
                    new XElement("size", 13),
                    new XElement("paragraph", 10),
                    new XElement("style", 0),
                    new XElement("color", "Black")
                )
            );
            xml.Root.Element("document").Element("format").Add(
                new XElement("savepath", @"C:\Users\gpiano\Documents\test\integrationstest_text.docx"),
                new XElement("templatepath", @"C:\Users\gpiano\Documents\test\vorlage.dotx")
            );
        }

        /// <summary>
        /// Fügt dem XML Objekt ein Tabellen Element und die Format Parameter hinzu
        /// </summary>
        public void Tabelle()
        {
            xml.Root.Element("document").Add(
                new XElement("table",
                    new XElement("id", "test"),
                    new XElement("bold", "true"),
                    new XElement("background", "white"),
                    new XElement("border", "true"),
                    new XElement("sql", "Select * from dbo.test"),
                    new XElement("font", "Arial"),
                    new XElement("size", 13),
                    new XElement("paragraph", 10)
                )
             );

            xml.Root.Element("document").Element("format").Add(
               new XElement("savepath", @"C:\Users\gpiano\Documents\test\integrationstest_table.docx"),
               new XElement("templatepath", @"C:\Users\gpiano\Documents\test\vorlage.dotx")
           );
        }

        /// <summary>
        /// Überprüft, ob die ID des aufgenommenen Connection Element sich in der Liste der Verbindungen befindet
        /// </summary>
        [TestMethod()]
        public void ConnectionsTest()
        {
            InitializeXML();
            Connection();
            ReadXml test = new ReadXml(xml.ToString(), new ImportInWord());
            test.OpenXml();
            test.XML();
            Assert.IsTrue(test.ListOfConnections.Count(s => s.Id == "test") == 1);
        }
        /// <summary>
        /// Überprüft, ob der Name des aufgenommenen Variabel Element sich in der Variablenliste befindet
        /// </summary>
        [TestMethod()]
        public void VariablesTest()
        {
            InitializeXML();
            AddElement();
            Variables();
            ImportInWord doc = new ImportInWord();
            ReadXml test = new ReadXml(xml.ToString(), doc);
            test.OpenXml();
            test.XML();
            doc.Exit();
            Assert.IsTrue(test.ListOfVariables.Count(s => s.Name == "test") == 1);
        }
        /// <summary>
        /// Überprüft, ob der Name das erwartete Result des Mathe Elements sich in der Variablenliste befindet
        /// </summary>
        [TestMethod()]
        public void MatheTest()
        {
            InitializeXML();
            AddElement();
            Mathe();
            ImportInWord doc = new ImportInWord();
            ReadXml test = new ReadXml(xml.ToString(), doc);
            test.OpenXml();
            test.XML();
            doc.Exit();
            Assert.IsTrue(test.ListOfVariables.Exists(s => s.Number == 9));
        }
        /// <summary>
        /// Überprüft, ob ein Dokument erstellt wurde. Einstellungen müssen im Dokument selbst überprüft werden.
        /// </summary>
        [TestMethod()]
        public void FormatTest()
        {
            InitializeXML();
            AddElement();
            ImportInWord doc = new ImportInWord();
            Format();
            ReadXml test = new ReadXml(xml.ToString(), doc);
            test.OpenXml();
            test.XML();
            doc.Exit();
            Assert.IsTrue(File.Exists(@"C:\Users\gpiano\Documents\test\integrationstest.docx"));
        }
        /// <summary>
        /// Überprüft, ob ein Dokument erstellt wurde. Text muss im Dokument selbst überprüft werden.
        /// </summary>
        [TestMethod()]
        public void TextTest()
        {
            InitializeXML();
            AddElement();
            ImportInWord doc = new ImportInWord();
            Text();
            ReadXml test = new ReadXml(xml.ToString(), doc);
            test.OpenXml();
            test.XML();
            doc.Exit();
            Assert.IsTrue(File.Exists(@"C:\Users\gpiano\Documents\test\integrationstest_text.docx"));
        }
        /// <summary>
        /// Überprüft, ob ein Dokument erstellt wurde. Tabelle muss im Dokument selbst überprüft werden.
        /// </summary>
        [TestMethod()]
        public void TableTest()
        {
            InitializeXML();
            AddElement();
            Connection();
            ImportInWord doc = new ImportInWord();
            Tabelle();
            ReadXml test = new ReadXml(xml.ToString(), doc);
            test.OpenXml();
            test.XML();
            doc.Exit();
            Assert.IsTrue(File.Exists(@"C:\Users\gpiano\Documents\test\integrationstest_table.docx"));
        }
        /// <summary>
        /// Liest ein komplettes XML Dokument ein und überprüft ob Word Datei erstellt wurde. Inhalt muss selber überprüft werden.
        /// </summary>
        [TestMethod()]
        public void BigTest()
        {
            string file = @"C:\Users\gpiano\Documents\Test\Integrationstest.xml";
            ImportInWord doc = new ImportInWord();
            ReadXml test = new ReadXml(file, doc);
            test.CreateXml();
            test.XML();
            doc.Exit();
            Assert.IsTrue(File.Exists(file));
        }
    }
}
