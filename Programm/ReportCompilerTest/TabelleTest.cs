using ReportCompiler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Collections.Generic;

namespace ReportCompilerTest
{
    
    
    /// <summary>
    /// Die Testklasse TabelleTest testet die Klasse Tabelle aus dem Projekt ReportCompiler
    ///</summary>
    [TestClass()]
    public class TabelleTest
    {
        Tabelle table;
        List<Connection> connections = new List<Connection>();
        /// <summary>
        /// Initialisiert die Datenbankverbindung
        /// </summary>
        public TabelleTest()
        {
            InitializeDBConnection();
        }

        /// <summary>
        /// Setzt Werte für die Datenbankverbindung ein
        /// </summary>
        public void InitializeDBConnection()
        {
            connections.Clear();
            Connection connection = new Connection();
            connection.Id = "mysql";
            connection.Type = "MySQL";
            connection.Server = "localhost";
            connection.Database = "school_example";
            connection.Username = "ReportingTool";
            connection.Password = "F1rstC0mesF1rst";
            connections.Add(connection);
        }
        /// <summary>
        /// Definiert eine korrekte SQL Abfrage
        /// </summary>
        public void InitializeSql()
        {
            table = new Tabelle();
            table.Id = "mysql";
            table.Border = true;
            table.BgColor = "White";
            table.Bold = true;
            table.Text = "SELECT * from mitarbeiter where mit_nachname='Stanik'";
        }

        /// <summary>
        /// Definiert eine falsche SQL Abfrage
        /// </summary>
        public void InitializeFailSql()
        {
            table = new Tabelle();
            table.Id = "mysql";
            table.Border = true;
            table.BgColor = "White";
            table.Bold = true;
            table.Text = "SELECT ..";
        }

        /// <summary>
        /// Definiert einen falsche DB Server
        /// </summary>
        public void InitializeFailDBConnection()
        {
            table = new Tabelle();
            table.Id = "tt";
            table.Border = true;
            table.BgColor = "White";
            table.Bold = true;
            table.Text = "SELECT * from mitarbeiter where mit_nachname='Stanik'";
        }

        /// <summary>
        /// Testet, ob eine Abfrage klappt, und ob der Inhalt der Tabelle nich leer ist
        ///</summary>
        [TestMethod()]
        public void getContentTest()
        {
            table = new Tabelle();// TODO: Initialize to an appropriate value
            InitializeSql();
            table.getContent(connections);
            Assert.IsTrue(table.Content != null);
        }
        /// <summary>
        /// Testet, ob eine falscher eine Exception wirft
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void getFailDBContentTest()
        {
            table = new Tabelle();// TODO: Initialize to an appropriate value
            InitializeFailDBConnection();
            table.getContent(connections);
        }
        /// <summary>
        /// Testet, ob eine falsche Abfrage eine Exception wirft
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(MySql.Data.MySqlClient.MySqlException))]
        public void getFailContentTest()
        {
            InitializeDBConnection();
            InitializeFailSql();
            table.getContent(connections);
        }
        /// <summary>
        /// Testet Verhalten von Property BgColorTest
        ///</summary>
        [TestMethod()]
        public void BgColorTest()
        {
            Tabelle target = new Tabelle(); // TODO: Initialize to an appropriate value
            string expected = "Blue"; // TODO: Initialize to an appropriate value
            string actual;
            target.BgColor = expected;
            actual = target.BgColor;
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Testet Verhalten von Property Size
        ///</summary>
        [TestMethod()]
        public void SizeTest()
        {
            Tabelle target = new Tabelle(); // TODO: Initialize to an appropriate value
            int expected = 10; // TODO: Initialize to an appropriate value
            int actual;
            target.Size = expected;
            actual = target.Size;
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Testet Verhalten von Property Paragraph
        ///</summary>
        [TestMethod()]
        public void ParagraphTest()
        {
            Tabelle target = new Tabelle(); // TODO: Initialize to an appropriate value
            int expected = 10; // TODO: Initialize to an appropriate value
            int actual;
            target.Paragraph = expected;
            actual = target.Paragraph;
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Testet Verhalten von Property Font
        ///</summary>
        [TestMethod()]
        public void FontTest()
        {
            Tabelle target = new Tabelle(); // TODO: Initialize to an appropriate value
            string expected = "Arial"; // TODO: Initialize to an appropriate value
            string actual;
            target.Font = expected;
            actual = target.Font;
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Testet Verhalten von Property Bold
        ///</summary>
        [TestMethod()]
        public void BoldTest()
        {
            Tabelle target = new Tabelle(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.Bold = expected;
            actual = target.Bold;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testet Verhalten von Property Border
        ///</summary>
        [TestMethod()]
        public void BorderTest()
        {
            Tabelle target = new Tabelle(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.Border = expected;
            actual = target.Border;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testet Verhalten von Property Content
        ///</summary>
        [TestMethod()]
        public void ContentTest()
        {
            Tabelle target = new Tabelle(); // TODO: Initialize to an appropriate value
            DataTable expected = new DataTable(); // TODO: Initialize to an appropriate value
            expected.Columns.Add("Test1", typeof(string));
            expected.Rows.Add("Test");
            DataTable actual;
            target.Content = expected;
            actual = target.Content;
        }

        /// <summary>
        /// Testet Verhalten von Property ID
        ///</summary>>
        [TestMethod()]
        public void IdTest()
        {
            Tabelle target = new Tabelle(); // TODO: Initialize to an appropriate value
            string expected = "test"; // TODO: Initialize to an appropriate value
            string actual;
            target.Id = expected;
            actual = target.Id;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testet Verhalten von Property Text
        ///</summary>
        [TestMethod()]
        public void TextTest()
        {
            Tabelle target = new Tabelle(); // TODO: Initialize to an appropriate value
            string expected = "Test"; // TODO: Initialize to an appropriate value
            string actual;
            target.Text = expected;
            actual = target.Text;
            Assert.AreEqual(expected, actual);
        }
    }
}
