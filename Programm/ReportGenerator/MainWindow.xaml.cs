//MainWindow.xaml.cs
//compile with: /doc:MainWindows.xml
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace ReportGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Speicher für den Pfad und Namen des XML Dokuments
        /// </summary>
        private string fileName;
        /// <summary>
        /// Speicher für ein Objekt der Klasse WriteXml, beinhaltet das gesamte XML Dokument
        /// </summary>
        private WriteXml xml;
        /// <summary>
        /// Speicher für die Liste aller gesetzten Verbindungen
        /// </summary>
        private List<string> id = new List<string>();

        private SortedList<string, string> variables = new SortedList<string, string>();

        /// <summary>
        /// Initalisiert die WPF Komponente
        /// Initalisiert ein WriteXml Objekt
        /// Aktualisiert die TextBox
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            xml = new WriteXml();
            InsertTextBox();
        }
        /// <summary>
        /// Property für die TextBox
        /// </summary>
        public string XmlBox
        {
            get { return Box.Text; }
        }
        /// <summary>
        /// DbConn Handler
        /// Initialisert eine neues Connection Objekt und öffnet eine neues Fenster
        /// Falls das Fenster ein OK zurückgibt, werden die eingegeben Paramter im XML Objekt geschrieben. 
        /// Die Textbox wird aktualisiert und die Verbindung wird der Verbindungsliste hinzugefügt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DBConn_Click(object sender, RoutedEventArgs e)
        {
            Connection subWindow = new Connection(id);
            subWindow.ShowDialog();
            if (subWindow.DialogResult ?? false)
            {
                xml.Connection(subWindow);
                InsertTextBox();
                AddToList(subWindow.Id);
                Tabelle.IsEnabled = true;
            }
       }
        /// <summary>
        /// Variable Button Handler
        /// Initialisert eine neues Variable Objekt und öffnet eine neues Fenster
        /// Falls das Fenster ein OK zurückgibt, werden die eingegeben Paramter im XML Objekt geschrieben. 
        /// Die Textbox wird aktualisiert und die gesetze Variable wird in der Textbox der Variablen aufgenommen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Varia_Click(object sender, RoutedEventArgs e)
        {
            Variabel subWindow = new Variabel(GetList(), variables);
            subWindow.ShowDialog();
            if (subWindow.DialogResult ?? false)
            {
                xml.Variables(subWindow);
                InsertTextBox();
                variables.Add(subWindow.Identificator, subWindow.Type);
                InsertVariables();
            }
        }
        /// <summary>
        /// Titel Button Handler
        /// Initialisert eine neues Titel Objekt und öffnet eine neues Fenster
        /// Falls das Fenster ein OK zurückgibt, wird überprüft, ob der manuelle Modus aktiviert ist
        /// Dies wird benötigt um den genauen Ort des XML Knotens im XML Objekt zu setzen.
        /// Zum Schluss werden die Paramter in das XML Objekt geschrieben
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Titel_Click(object sender, RoutedEventArgs e)
        {
            Titel subWindow = new Titel();
            subWindow.ShowDialog();
            if (subWindow.DialogResult ?? false)
            {
                if (EditButton.IsChecked == false)
                {
                    xml.Titel(subWindow);
                    InsertTextBox();
                }
                else
                {
                    string element = WriteXml.TitelManually(subWindow);
                    InsertTextBoxManually(element);
                }
            }
        }
        /// <summary>
        /// Tabelle Button Handler
        /// Initialisert eine neues Tabellen Objekt und öffnet eine neues Fenster
        /// Falls das Fenster ein OK zurückgibt, wird überprüft, ob der manuelle Modus aktiviert ist
        /// Dies wird benötigt um den genauen Ort des XML Knotens im XML Objekt zu setzen.
        /// Zum Schluss werden die Paramter in das XML Objekt geschrieben
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tabelle_Click(object sender, RoutedEventArgs e)
        {
            Tabelle subWindow = new Tabelle(GetList());
            subWindow.ShowDialog();
            if (subWindow.DialogResult ?? false)
            {
                if (EditButton.IsChecked == false)
                {
                    xml.Tabelle(subWindow);
                    InsertTextBox();
                }
                else
                {
                    string element = WriteXml.TabelleManually(subWindow);
                    InsertTextBoxManually(element);
                }
            }
        }
        /// <summary>
        /// Text Button Handler
        /// Initialisert eine neues Text Objekt und öffnet eine neues Fenster
        /// Falls das Fenster ein OK zurückgibt, wird überprüft, ob der manuelle Modus aktiviert ist
        /// Dies wird benötigt um den genauen Ort des XML Knotens im XML Objekt zu setzen.
        /// Zum Schluss werden die Paramter in das XML Objekt geschrieben
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Text_Click(object sender, RoutedEventArgs e)
        {
            Text subWindow = new Text();
            subWindow.ShowDialog();
            if (subWindow.DialogResult ?? false)
            {
                if (EditButton.IsChecked == false)
                {
                    xml.Text(subWindow);
                    InsertTextBox();
                }
                else
                {
                    string element = WriteXml.TextManually(subWindow);
                    InsertTextBoxManually(element);
                }
            }
        }
        /// <summary>
        /// Format Button Handler
        /// Initialisert eine neues Format Objekt und öffnet eine neues Fenster
        /// Falls das Fenster ein OK zurückgibt, werden die eingegebenen Parameter in das XML Objekt übernommen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Format_Click(object sender, RoutedEventArgs e)
        {
            PageSetup subWindow = new PageSetup();
            subWindow.ShowDialog();
            if (subWindow.DialogResult ?? false)
            {
                xml.Format(subWindow);
                InsertTextBox();
                Edit();
            }
        }
        /// <summary>
        /// Math Button Handler
        /// Initialisert eine neues Mathematic Objekt und öffnet eine neues Fenster
        /// Falls das Fenster ein OK zurückgibt, werden die eingegeben Parameter in das XML Objekt übernommen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Math_Click(object sender, RoutedEventArgs e)
        {
            Mathematic subWindow = new Mathematic(variables);
            subWindow.ShowDialog();
            if (subWindow.DialogResult ?? false)
            {
                xml.Mathe(subWindow);
                InsertTextBox();
            }
        }
        /// <summary>
        /// New Button Handler
        /// Setzt das gesamte XML Objekt sowie alle grafischen Anzeigen zurück
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void New_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to discard changes?", "Warnining", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                xml = new WriteXml();
                RemoveVariables();
                RemoveConnectionsId();
                InsertTextBox();
                fileName = null;
                Format.IsEnabled = true;
                Save.IsEnabled = false;
                Report.IsEnabled = false;
                Tabelle.IsEnabled = false;
            }
        }
        /// <summary>
        /// Open Button Handler
        /// Öffnet ein schon vorhandenes XML Dokument. 
        /// Um sicher zugehen, dass vorhin nicht irgendwelche Parameter eingegeben wurden, werden die Id sowie Variable Liste geleert und erst danach wieder gefüllt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            fileName = FileAccessor.Open();
            if (fileName != null)
            {
                try
                {
                    xml.Open(fileName);
                }
                catch (Exception exc)
                {
                    fileName = null;
                    MessageBoxResult ex = MessageBox.Show(exc.Message, "Fehler beim Öffnen der Datei");
                }
                try
                {
                    RemoveConnectionsId();
                    RemoveVariables();
                    InsertTextBox();
                    Edit();
                    Tabelle.IsEnabled = true;
                    id = xml.ReadConnections();
                    variables = xml.ReadVariables();
                    InsertVariables();
                }
                catch (Exception)
                {
                    fileName = null;
                    MessageBox.Show("Diese XML Datei kann nicht eingelesen werden", "Fehler");
                }
            }
        }
        /// <summary>
        /// Save Button Handler
        /// Ermöglicht das Speicher des XML Objekts in ein XML Dokument.
        /// Falls der Dateiname schon bekannt ist, wird die vorhandene Datei überschrieben, ansonsten wird nachgefragt, wo die Datei gespeichert werden soll.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EditButton.IsChecked == true)
                    xml.WriteFromString(Box.Text);
                FileAccessor.Save(ref fileName, xml);
            }
            catch (Exception exc)
            {
                MessageBoxResult ex = MessageBox.Show(exc.Message, "Fehler beim Speichern der Datei");
            }
        }
        /// <summary>
        /// Rapport Button Handler
        /// Erstellt direkt aus dem XML Objekt das Word Dokument
        /// Falls ein Fehler zurückgegeben wird, wird dieser auf als MessageBox aufgezeigt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rapport_Click(object sender, RoutedEventArgs e)
        {
            ReportCompiler.ImportInWord word = new ReportCompiler.ImportInWord();
            try
            {
                if (EditButton.IsChecked == true)
                {
                    RemoveConnectionsId();
                    RemoveVariables();
                    xml.WriteFromString(Box.Text);
                    id = xml.ReadConnections();
                    variables = xml.ReadVariables();
                    InsertVariables();
                }
                ReportCompiler.ReadXml file = new ReportCompiler.ReadXml(xml.getXML(), word);
                file.OpenXml();
                file.XML();
                word.Exit();
                MessageBox.Show("Dokument wurde erfolgreicht erstellt", "Rapportgeneriung erfolgreich abgeschlossen");
            }
            catch (ReportCompiler.OpenDocumentException ex)
            {
                MessageBoxResult result = MessageBox.Show("Fehler während der Laufzeit. Grund: " + ex.Message + "\n\nErstellte Datei wird gelöscht.\n\nProgramm mit Fehler beendet", "Fehler bei Rapportgenerierung");
                word.Quit();
            }
            catch (XmlException exc)
            {
                MessageBoxResult ex = MessageBox.Show(exc.Message, "Fehler in der Xml Datei");
            }
            
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show("Fehler während der Laufzeit. Grund: " + ex.Message + "\n\nDokument wurde nicht erstellt. Programm mit Fehler beendet", "Fehler bei Rapportgenerierung");
            }
        }
        /// <summary>
        /// Fügt der Verbindungsliste eine neue Verbindung hinzu
        /// </summary>
        /// <param name="value">Die Id der Verbindung</param>
        public void AddToList(string value)
        {
            id.Add(value);
        }
        /// <summary>
        /// Gibt die Verbindungsliste zurück
        /// </summary>
        /// <returns>Die Verbindungsliste</returns>
        public List<string> GetList()
        {
            return id;
        }
        /// <summary>
        /// Aktualisert den Inhalt der TextBox
        /// </summary>
        public void InsertTextBox()
        {
            try
            {
                Box.Text = xml.getXML();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Aktualisiert den Inhalt der TextBox im manuellen Modus
        /// </summary>
        /// <param name="value">Den XML Knoten als String</param>
        public void InsertTextBoxManually(string value)
        {
            Box.Text = Box.Text.Insert(Box.SelectionStart, value);
        }
        /// <summary>
        /// Fügt die einzelnen Variablen in die Variabletextbox ein
        /// </summary>
        /// <param name="name">Der Name der Variable</param>
        /// <param name="type">Der Typ der Variable</param>
        public void InsertVariables(string name, string type)
        {
            AllVariables.Text = AllVariables.Text.Insert(AllVariables.SelectionStart, name + "(" + type + ")" + Environment.NewLine);
        }

        /// <summary>
        /// Schaltet in den manuellen Modus
        /// Es kann in der TextBox direkt geschrieben werden
        /// Falls der manuelle Modus verlassen wird, wird der gesamte String in das Xml Objekt geschrieben.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (EditButton.IsChecked == true)
            {
                MessageBox.Show("Um < oder > als Text zu benutzen, verwenden Sie für < &lt; und für > &gt;","Warnung");
            }
            try
            {
                if (EditButton.IsChecked == false)
                {
                    RemoveConnectionsId();
                    RemoveVariables();
                    xml.WriteFromString(Box.Text);
                    id = xml.ReadConnections();
                    variables = xml.ReadVariables();
                    InsertVariables();
                }
            }
            catch (XmlException exc)
            {
                MessageBoxResult ex = MessageBox.Show(exc.Message, "Fehler in der Xml Datei");
            }
        }
        /// <summary>
        /// Setzt den Format Knopf auf deaktiviert und den Save Knopf auf aktiviert.
        /// Dies wird benötigt, da das Format nur einmal gesetzt werden soll und erst nach dem Setzen des Formats die Datei gespeichert werden darf.
        /// </summary>
        private void Edit()
        {
            Format.IsEnabled = false;
            Save.IsEnabled = true;
            Report.IsEnabled = true;
        }
        /// <summary>
        /// Leert die Verbindungsliste
        /// </summary>
        public void RemoveConnectionsId()
        {
            id.Clear();
        }
        /// <summary>
        /// Leert die Variablenliste
        /// </summary>
        public void RemoveVariables()
        {
            variables.Clear();
            AllVariables.Text = "";
        }
        /// <summary>
        /// Fügt der Liste eine weitere Variable hinzu
        /// </summary>
        public void InsertVariables()
        {
            AllVariables.Text = "";
            foreach (KeyValuePair<string, string> variable in variables)
                InsertVariables(variable.Key, variable.Value);
        }
    }
}
