using ReportCompiler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace ReportCompilerTest
{


    /// <summary>
    /// Diese Testklasse testet die Klasse MSConnector aus dem Projekt ReportComipler
    ///</summary>
    [TestClass()]
    public class MyConnectorTest
    {

        /// <summary>
        /// Speicher für MSConnector Field
        /// </summary>
        private MyConnector connector;


        /// <summary>
        /// Setzen der Parameter für eine gültige Datenbankverbindung
        /// </summary>
        public void InitalizeConnection()
        {
            Connection connection = new Connection();
            connection.Server = "localhost";
            connection.Database = "school_example";
            connection.Username = "ReportingTool";
            connection.Password = "F1rstC0mesF1rst";
            connector = new MyConnector(connection);  
        }
        /// <summary>
        /// Setzen der Parameter für eine ungültige Datenbankverbindung (unbekannter Server)
        /// </summary>
        public void InitializeFailedConnection()
        {
            Connection connection = new Connection();
            connection.Server = "1.1.1.1";
            connection.Database = "school_example";
            connection.Username = "ReportingTool";
            connection.Password = "F1rstC0mesF1rst";
            connector = new MyConnector(connection);
        }
        /// <summary>
        /// Setzen der Parameter für eine ungültige Datenbankverbindung (unbekannte Datenbank)
        /// </summary>
        public void InitializeFailedDatabase()
        {
            Connection connection = new Connection();
            connection.Server = "localhost";
            connection.Database = "..";
            connection.Username = "ReportingTool";
            connection.Password = "F1rstC0mesF1rst";
            connector = new MyConnector(connection);
        }
        /// <summary>
        /// Setzen der Parameter für eine ungültige Datenbankverbindung (unbekannter Benutzername)
        /// </summary>
        public void InitializeFailedUserName()
        {
            Connection connection = new Connection();
            connection.Server = "localhost";
            connection.Database = "school_example";
            connection.Username = "..";
            connection.Password = "F1rstC0mesF1rst";
            connector = new MyConnector(connection);
        }

        /// <summary>
        /// Testet das Verhalten der Property ConnectionString
        /// </summary>
        public void ConnectionStringTest()
        {
            MyConnector target = new MyConnector(new Connection()); // TODO: Initialize to an appropriate value
            string expected = "test"; // TODO: Initialize to an appropriate value
            string actual;
            target.ConnectionString = expected;
            actual = target.ConnectionString;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Fragt einen einzelnen Wert auf der Datenbank ab, und überprüft ob der Inhalt nicht leer ist
        ///</summary>
        [TestMethod()]
        public void ResultTest()
        {
            InitalizeConnection(); // TODO: Initialize to an appropriate value
            connector.Connect();
            string text = "SELECT mit_id from mitarbeiter where mit_nachname='Stanik'";
            string variable = connector.Result(text);
            connector.Close();
            Assert.IsTrue(variable != "");
        }
        /// <summary>
        /// Fragt einen Wertbereich auf der Datenbank ab und überprüft ob der Inhalt nicht leer ist
        ///</summary>
        /// <summary>
        ///A test for loadTable
        ///</summary>
        [TestMethod()]
        public void loadTableTest()
        {
            InitalizeConnection();
            connector.Connect();
            string text = "SELECT mit_id from mitarbeiter where mit_nachname='Stanik'";
            DataTable variable = connector.loadTable(text);
            connector.Close();
            Assert.IsTrue(variable.ToString() != "");
        }
        /// <summary>
        /// Überprüft ob Datenbankverbindung erfolgreich war
        /// </summary>
        [TestMethod()]
        public void ConnectTest()
        {
            InitalizeConnection();
            connector.Connect();
            Assert.IsTrue(connector.ToString() != "");
            connector.Close();
        }

        /// <summary>
        /// Überprüft ob Datenbankverbindung fehlgeschlagen hat
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(MySql.Data.MySqlClient.MySqlException))]
        public void FailedConnection()
        {
            InitializeFailedConnection();
            connector.Connect();
        }
        /// <summary>
        /// Überprüft ob Datenbankverbindung fehlgeschlagen hat
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(MySql.Data.MySqlClient.MySqlException))]
        public void FailedDatabaseConnection()
        {
            InitializeFailedDatabase();
            connector.Connect();
        }
        /// <summary>
        /// Überprüft ob Datenbankverbindung fehlgeschlagen hat
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(MySql.Data.MySqlClient.MySqlException))]
        public void FailedUsernameorPW()
        {
            InitializeFailedUserName();
            connector.Connect();
        }
        /// <summary>
        /// Überprüft ob Datenbankverbindung fehlgeschlagen hat
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(MySql.Data.MySqlClient.MySqlException))]
        public void FailedQueryResult()
        {
            InitalizeConnection();
            connector.Connect();
            string text = "SELECT ...";
            connector.Result(text);
            connector.Close();
        }
        /// <summary>
        /// Überprüft ob Datenbankverbindung fehlgeschlagen hat
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(MySql.Data.MySqlClient.MySqlException))]
        public void FailedQueryLoadTable()
        {
            InitalizeConnection();
            connector.Connect();
            string text = "SELECT ...";
            connector.loadTable(text);
            connector.Close();
        }
    }
}
