//Variabel.xaml.cs
//compile with: /doc:Variabel.html
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
    /// Interaction logic for Variabel.xaml
    /// </summary>
    public partial class Variabel : Window
    {
        private SortedList<string, string> variabel;
        /// <summary>
        /// Initalisiert die WPF Komponente
        /// </summary>
        /// <param name="list">Als Paramter wird die Liste mit allen gesetzten Verbindungs id übergeben</param>
        /// <param name="variabel">Eine Key Value Liste: Inhalt dieser Liste ist der Variablenname und der dazugehörende Typ</param>
        public Variabel(List<string> list, SortedList<string, string> variabel)
        {
            InitializeComponent();
            FillComboBox(list);
            this.variabel = variabel;
        }
        /// <summary>
        /// Propert für die Verbindungsid
        /// </summary>
        public string Id
        {
            get { return IdChooser.Text; }
        }
        /// <summary>
        /// Property für den Namen der Variable
        /// </summary>
        public string Identificator
        {
            get { return NameChooser.Text; }
        }
        /// <summary>
        /// Property für den Typ der Variable
        /// Falls int ausgewählt wurde wird der Typ auf int gesetzt ansonsten auf string
        /// </summary>
        public string Type
        {
            get
            {
                if (i.IsChecked == true)
                    return "int";
                else
                    return "string";
            }
        }
        /// <summary>
        /// Property für die Quelle der Variable
        /// Falls local ausgewählt wurde, wird die Quelle auf lokal gesetzt
        /// Ansonsten wir die Quelle auf sql gesetzt
        /// </summary>
        public string Source
        {
            get
            {
                if (local.IsChecked == true)
                    return "local";
                else
                    return "sql";
            }
        }
        /// <summary>
        /// Property für den gewünschten Variableninhalt
        /// Der Inhalt wird maskiert, um die Xml Syntax nicht zu verletzen
        /// </summary>
        public string Text
        {
            get { return SourceChooserText.Text; }
        }
        /// <summary>
        /// Schreibt alle vorhandenen Verbindungs-Id in eine ComboBox
        /// </summary>
        /// <param name="list"></param>
        private void FillComboBox(List<string> list)
        {
            foreach (string s in list)
            {
                // FontFamily.Source contains the font family name.
                IdChooser.Items.Add(s);
            }
        }
        /// <summary>
        /// Button Handler um die Eingaben zurückzusetzen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Button Handler um die Eingaben zu bestätigen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EmptyTextBox();
                VariableAlreadyExists();
                this.DialogResult = true;
                this.Close();
            }
            catch (EmptyTextBoxException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        /// <summary>
        /// Überprüft, ob alle Textfelder etwas beinhalten
        /// </summary>
        private void EmptyTextBox()
        {
            if (Id == "" && Source == "sql" || Identificator == "" || Text == "")
                throw new EmptyTextBoxException("Es darf kein Feld leer sein");
        }
        /// <summary>
        /// Lässt bei der Auswahl lokale Nummervariable nur Zahle zu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void int_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Source == "local" && Type == "int")
            {
                if (!char.IsDigit(e.Text, e.Text.Length - 1))
                    e.Handled = true;
            }
        }
        /// <summary>
        /// Überprüft, ob der Variablennamen schon gesetzt wurde
        /// </summary>
        private void VariableAlreadyExists()
        {
            if (variabel.Keys.Contains(Identificator))
                throw new EmptyTextBoxException("Variablenname schon vorhanden");
        }
    }
}
