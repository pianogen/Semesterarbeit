//Connections.cs
//compile with: /doc:Connection.html
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ReportGenerator
{
    /// <summary>
    /// Interaction logic for Connection.xaml
    /// </summary>
    public partial class Connection : Window
    {
        private List<string> id;

        /// <summary>
        /// Initialisiert die Komponenten der Verbindung und speichert alle bereits gesetzten Verbindungen in einer Variable ab.
        /// </summary>
        /// <param name="id">Liste aller bereits gesetzten Verbindungen</param>
        public Connection(List<string> id)
        {
            InitializeComponent();
            this.id = id;
        }

        /// <summary>
        /// Property für die Verbindungs-Id
        /// </summary>
        public string Id
        {
            get { return IdChooser.Text; }
        }
        /// <summary>
        /// Property für den Verbindugstyp, falls Microsoft SQL ausgewählt wurde wird MSSQL zurückgegeben
        /// Falls MySQL ausgewählt wurde, wird MySQL zurückgegeben
        /// </summary>
        public string Type
        {
            get
            {
                if (MSSQL.IsChecked == true)
                    return "MSSQL";
                else
                    return "MySQL";
            }
        }

        /// <summary>
        /// Property die IP Adresse des Servers
        /// </summary>
        public string ConnectionString
        {
            get { return ServerChooser.Text; }
        }
        /// <summary>
        /// Property für den Datenbanknamen
        /// </summary>
        public string Database
        {
            get { return DatabaseChooser.Text; }
        }
        /// <summary>
        /// Property für den Datenbankbenutzernamen
        /// </summary>
        public string User
        {
            get { return UserChooser.Text; }
        }
        /// <summary>
        /// Property für das Passwort des Datenbankbenutzers
        /// </summary>
        public string Password
        {
            get { return PasswordChooser.Text; }
        }
        /// <summary>
        /// Button_Handler um die Eingabe zu bestätigen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EmptyTextBox();
                ConnectionAlreadyExists();
                this.DialogResult = true;
                this.Close();
            }
            catch (EmptyTextBoxException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        /// <summary>
        /// Button_Handler um die Eingabe zurück zusetzen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Überprüft, ob alle Felder etwas beinhalten
        /// </summary>
        private void EmptyTextBox()
        {
            if (Id == "" || ConnectionString == "" || Database == "" || User == "" || Password == "")
                throw new EmptyTextBoxException("Es darf kein Feld leer sein");
        }
        /// <summary>
        /// Überprüft, ob die eingegebene ID schon vorhanden ist
        /// </summary>
        private void ConnectionAlreadyExists()
        {
            if (id.Contains(Id))
                throw new EmptyTextBoxException("ID von Datenbankverbindung existiert schon");
        }       
    }
}
