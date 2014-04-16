//x.cs
//compile with: /doc:x.xml
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace ReportCompiler
{
    /// <summary>
    /// Diese Klasse dient zur Verbindung 
    /// und Abfragen einer MySQL Datenbank
    /// </summary>
    public class MyConnector : Connector
    {
        /// <summary>
        /// Speicher für ein Verbindungsobjekt 
        /// </summary>
        private Connection connection;

        private MySqlConnection conn;
        /// <summary>
        /// Speicher für die Verbindungwerte
        /// </summary>
        public string ConnectionString
        {
            get;
            set;
        }
        /// <summary>
        /// Generiert die Werte der Verbindung 
        /// und somit den connectionString
        /// </summary>
        /// <param name="connection">Beinhaltet alle benötigten Werte für den Verbindungsaufbau</param>
        public MyConnector(Connection connection)
        {
            this.connection = connection;
            ConnectionString = "SERVER=" + connection.Server + ";" +
                            "DATABASE=" + connection.Database + ";" +
                            "UID=" + connection.Username + ";" +
                            "PASSWORD=" + connection.Password + ";";
        }
        /// <summary>
        /// Öffnet die Datenbankverbindung
        /// </summary>
        public void Connect()
        {
            conn = new MySqlConnection(ConnectionString);
            try
            {
                conn.Open();
            }
            catch (MySqlException)
            {
                throw;
            }
        }
        /// <summary>
        /// Schliesst die Datenbankverbindung
        /// </summary>
        public void Close()
        {
            try
            {
                conn.Close();
            }
            catch (MySqlException)
            {
                throw;
            }
        }
        /// <summary>
        /// Fragt Einzelwert in einer MySQL Datenbank ab (Variable)
        /// Bei einem Laufzeitfehler wird eine MySQLException gefangen 
        /// und weiter geworfen
        /// </summary>
        /// <param name="text">Beinhaltet die MySQL Abfrage</param>
        /// <returns>Gibt das erhaltene Resultat zurück</returns>
        /// 
        public string Result(string text)
        {
            string result = null;
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = text;
                MySqlDataReader sqlreader = command.ExecuteReader();
                sqlreader.Read();
                result = sqlreader.GetValue(0).ToString();
            }
            catch (MySqlException)
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// Fragt eine MySQL Datenbank ab
        /// </summary>
        /// <param name="text">Beihnaltet die MySQL Abfrage</param>
        /// <returns>Gibt die erhaltene Tabelle zurück</returns>
        public DataTable loadTable(string text)
        {
            DataTable table = new DataTable();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = text;
                table.Load(command.ExecuteReader());
            }
            catch (MySqlException)
            {
                throw;
            }
            return table;
        }

    }
}
