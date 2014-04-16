//MSConnector.cs
//compile with: /doc:MSConnector.xml
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ReportCompiler
{
    /// <summary>
    /// Diese Klasse dient zur Verbindung 
    /// und Abfragen einer MSSQL Datenbank
    /// </summary>
    public class MSConnector : Connector
    {

        private SqlConnection conn;
        /// <summary>
        /// Speicher für ein Verbindungsobjekt 
        /// </summary>
        private Connection connection;
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
        /// <param name="connection">Als Parameter wird ein Connection Objekt übergeben</param>
        public MSConnector(Connection connection)
        {
            this.connection = connection;
            ConnectionString = "Data Source=" + connection.Server + ";Initial Catalog=" 
                + connection.Database + ";User ID=" + connection.Username 
                + ";Password=" + connection.Password;
        }
        /// <summary>
        /// Öffnet die Datenbankverbindung
        /// </summary>
        public void Connect()
        {
            conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
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
            catch (SqlException)
            {
                throw;
            }
        }

        /// <summary>
        /// Fragt Einzelwert in einer Microsoft SQL Datenbank ab (Variable)
        /// Bei einem Laufzeitfehler wird eine SQLException gefangen 
        /// und weiter geworfen
        /// </summary>
        /// <param name="text">Beinhaltet die Microsoft SQL Abfrage</param>
        /// <returns>Gibt das erhaltene Resultat zurück</returns>
        public string Result(string text)
        {
            string result = null;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand(text, conn);
                    SqlDataReader sqlReader = myCommand.ExecuteReader();
                    sqlReader.Read();
                    result = sqlReader[0].ToString();
                    conn.Close();
                }
                catch (SqlException)
                {
                    throw;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }
        /// <summary>
        /// Fragt eine Microsoft SQL Datenbank ab
        /// </summary>
        /// <param name="text">Beihnaltet die Microsoft SQL Abfrage</param>
        /// <returns>Gibt die erhaltene Tabelle zurück</returns>
        public DataTable loadTable(string text)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand(text, conn);
                    table.Load(myCommand.ExecuteReader());
                    conn.Close();
                }
                catch (SqlException)
                {
                    throw;
                }
            }
            return table;
        }
    }
}
