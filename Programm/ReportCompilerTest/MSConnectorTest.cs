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
    public class MSConnectorTest
    {
        /// <summary>
        /// Speicher für MSConnector Field
        /// </summary>
        private MSConnector connector;


        /// <summary>
        /// Setzen der Parameter für eine gültige Datenbankverbindung
        /// </summary>
        public void InitalizeConnection()
        {
            Connection connection = new Connection();
            connection.Server = "localhost\\SQLExpress";
            connection.Database = "school_example";
            connection.Username = "ReportingTool";
            connection.Password = "F1rstC0mesF1rst";
            connector = new MSConnector(connection);
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
            connector = new MSConnector(connection);
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
            connector = new MSConnector(connection);
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
            connector = new MSConnector(connection);
        }
        /// <summary>
        /// Testet das Verhalten der Property ConnectionString
        /// </summary>
        [TestMethod()]
        public void ConnectionStringTest()
        {
            MSConnector target = new MSConnector(new Connection()); // TODO: Initialize to an appropriate value
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
            string text = "SELECT nachname from dbo.test where vorname='Annina'";
            string variable = connector.Result(text);
            connector.Close();
            Assert.IsTrue(variable != "");
        }

        /// <summary>
        /// Fragt einen Wertbereich auf der Datenbank ab und überprüft ob der Inhalt nicht leer ist
        ///</summary>
        [TestMethod()]
        public void loadTableTest()
        {
            InitalizeConnection();
            connector.Connect();
            string text = "SELECT * from dbo.test";
            DataTable variable = connector.loadTable(text);
            connector.Close();
            Assert.IsTrue(variable != null);
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
        [ExpectedException(typeof(System.Data.SqlClient.SqlException))]
        public void FailedConnection()
        {
            InitializeFailedConnection();
            connector.Connect();
        }
        /// <summary>
        /// Überprüft ob Datenbankverbindung fehlgeschlagen hat
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(System.Data.SqlClient.SqlException))]
        public void FailedDatabaseConnection()
        {
            InitializeFailedDatabase();
            connector.Connect();
        }
        /// <summary>
        /// Überprüft ob Datenbankverbindung fehlgeschlagen hat
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(System.Data.SqlClient.SqlException))]
        public void FailedUsernameorPW()
        {
            InitializeFailedUserName();
            connector.Connect();
        }
        /// <summary>
        /// Überprüft ob Datenbankverbindung fehlgeschlagen hat
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(System.Data.SqlClient.SqlException))]
        public void FailedQueryResult()
        {
            InitalizeConnection();
            connector.Connect();
            string text = "SELECT ...";
            connector.Result(text);
        }
        /// <summary>
        /// Überprüft ob Datenbankverbindung fehlgeschlagen hat
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(System.Data.SqlClient.SqlException))]
        public void FailedQueryLoadTable()
        {
            InitalizeConnection();
            connector.Connect();
            string text = "SELECT ...";
            connector.loadTable(text);
        }
    }
}
