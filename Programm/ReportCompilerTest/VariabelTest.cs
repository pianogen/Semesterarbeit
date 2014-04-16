using ReportCompiler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ReportCompilerTest
{
    
    
    /// <summary>
    /// Diese Testklasse testet die Klasse Variabel aus dem Projekt ReportCompiler
    ///</summary>
    [TestClass()]
    public class VariabelTest
    {
        Variabel variable;
        List<Connection> connections = new List<Connection>();

        /// <summary>
        /// Initialisert DB Verbindung
        /// </summary>
        public VariabelTest()
        {
            InitializeDBConnection();
        }

        /// <summary>
        /// Setzt richtige Werte für DB Verbindung
        /// </summary>
        public void InitializeDBConnection()
        {
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
        /// Setzt richtige SQL Abfrage ein
        /// </summary>
        public void InitializeSqlStringVariable()
        {
            variable = new Variabel();
            variable.Id = "mysql";
            variable.Type = "string";
            variable.Source = "sql";
            variable.Text = "SELECT mit_vorname from mitarbeiter where mit_nachname='Stanik'"; 
        }
        /// <summary>
        /// Initialisert Int Variable
        /// </summary>
        public void InitializeSqlIntVariable()
        {
            variable = new Variabel();
            variable.Id = "mysql";
            variable.Type = "int";
            variable.Source = "sql";
            variable.Text = "SELECT mit_id from mitarbeiter where mit_nachname='Stanik'";
        }
        /// <summary>
        /// Initialisiert fehlerhaft SQL Abfrage
        /// </summary>
        public void InitializeFailSqlVariable()
        {
            variable = new Variabel();
            variable.Id = "mysql";
            variable.Type = "int";
            variable.Source = "sql";
            variable.Text = "Select ...";
        }
       
        /// <summary>
        ///¨Überprüft, ob das Casten der Werte funktioniert
        ///</summary>
        [TestMethod()]
        public void ResultToDecTest()
        {
            Variabel target = new Variabel(); // TODO: Initialize to an appropriate value
            string text = "3.5"; // TODO: Initialize to an appropriate value
            Decimal expected = 3.5M; // TODO: Initialize to an appropriate value
            Decimal actual;
            actual = target.ResultToDec(text);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Überprüft, ob bei fehlerhaftem Casten eine Exception geworfen wird
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void ResultToDecFailTest()
        {
            Variabel target = new Variabel();
            string text = "abc";
            Decimal actual;
            actual = target.ResultToDec(text);
        }
        /// <summary>
        /// Überprüft ob lokale String Variablen funktionieren
        ///</summary>
        [TestMethod()]
        public void getContentLocalTest()
        {
            Variabel target = new Variabel(); // TODO: Initialize to an appropriate value
            target.Source = "local";
            target.Type = "string";
            target.Text = "Hallo";
            string expected = "Hallo";
            string actual;
            target.getContent(new List<Connection>());
            actual = target.Content;
            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Überprüft ob SQL String Variablen funktionieren
        ///</summary>
        [TestMethod()]
        public void getSqlContentTest()
        {
            InitializeSqlStringVariable(); // TODO: Initialize to an appropriate value
            variable.getContent(connections);
            Assert.IsTrue(variable.Content != "");
        }

        /// <summary>
        /// Überprüft ob SQL int Variablen funktionieren
        ///</summary>
        public void getSqlNumberTest()
        {
            InitializeSqlIntVariable(); // TODO: Initialize to an appropriate value
            variable.getContent(connections);
            Assert.IsTrue(variable.Number != 0M);
        }
        /// <summary>
        /// Testet, ob bei fehlerhaften SQL Abfrage exception geworfen wird
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(MySql.Data.MySqlClient.MySqlException))]
        public void FalseSQLSyntax()
        {
            InitializeFailSqlVariable();
            variable.getContent(connections);
        }
        /// <summary>
        /// Überprüft ob lokale int Variablen funktionieren
        ///</summary>
        
        [TestMethod()]
        public void getLocalContentIntTest()
        {
            Variabel target = new Variabel(); // TODO: Initialize to an appropriate value
            target.Type = "int";
            target.Text = "5";
            Decimal expected = 5M;
            decimal actual;
            target.getLocalContent();
            actual = target.Number;
            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        ///A test for Content
        ///</summary>
        [TestMethod()]
        public void ContentTest()
        {
            Variabel target = new Variabel(); // TODO: Initialize to an appropriate value
            string expected = "test"; // TODO: Initialize to an appropriate value
            string actual;
            target.Content = expected;
            actual = target.Content;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Id
        ///</summary>
        [TestMethod()]
        public void IdTest()
        {
            Variabel target = new Variabel(); // TODO: Initialize to an appropriate value
            string expected = "test"; // TODO: Initialize to an appropriate value
            string actual;
            target.Id = expected;
            actual = target.Id;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Name
        ///</summary>
        [TestMethod()]
        public void NameTest()
        {
            Variabel target = new Variabel(); // TODO: Initialize to an appropriate value
            string expected = "test"; // TODO: Initialize to an appropriate value
            string actual;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Number
        ///</summary>
        [TestMethod()]
        public void NumberTest()
        {
            Variabel target = new Variabel(); // TODO: Initialize to an appropriate value
            Decimal expected = 3.5M; // TODO: Initialize to an appropriate value
            Decimal actual;
            target.Number = expected;
            actual = target.Number;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Source
        ///</summary>
        [TestMethod()]
        public void SourceTest()
        {
            Variabel target = new Variabel(); // TODO: Initialize to an appropriate value
            string expected = "Test"; // TODO: Initialize to an appropriate value
            string actual;
            target.Source = expected;
            actual = target.Source;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Text
        ///</summary>
        [TestMethod()]
        public void TextTest()
        {
            Variabel target = new Variabel(); // TODO: Initialize to an appropriate value
            string expected = "Test"; // TODO: Initialize to an appropriate value
            string actual;
            target.Text = expected;
            actual = target.Text;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Type
        ///</summary>
        [TestMethod()]
        public void TypeTest()
        {
            Variabel target = new Variabel(); // TODO: Initialize to an appropriate value
            string expected = "test"; // TODO: Initialize to an appropriate value
            string actual;
            target.Type = expected;
            actual = target.Type;
            Assert.AreEqual(expected, actual);
        }
    }
}
