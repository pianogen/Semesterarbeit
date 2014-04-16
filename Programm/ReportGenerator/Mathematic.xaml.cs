//Mathematic.xaml.cs
//compile with: /doc:Mathematic.html
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace ReportGenerator
{
    /// <summary>
    /// Interaction logic for Mathematic.xaml
    /// </summary>
    public partial class Mathematic : Window
    {
        SortedList<string, string> variables;
        /// <summary>
        /// Initialiserung der WPF Komponenten
        /// </summary>
        public Mathematic(SortedList<string,string> variables)
        {
            InitializeComponent();
            this.variables = variables;
        }
        /// <summary>
        /// Property für den Namen der algebraischer Formel
        /// </summary>
        public string Identificator
        {
            get { return NameChooser.Text; }
        }
        /// <summary>
        /// Property für die algebraische Formel
        /// </summary>
        public string Formula
        {
            get { return FormelChooser.Text; }
        }
        /// <summary>
        /// Button_Handler um die Eingabe zu bestätigen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendenButton_Click(object sender, RoutedEventArgs e)
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
        /// Button_Handler um die Eingabe zurück zusetzen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Überprüft, ob alle Felder etwas beinhalten
        /// </summary>
        private void EmptyTextBox()
        {
            if (Identificator == "" || Formula == "")
                throw new EmptyTextBoxException("Kein Feld darf leer sein");
        }

        private void VariableAlreadyExists()
        {
            if (variables.Keys.Contains(Identificator))
                throw new EmptyTextBoxException("Variablenname schon vorhanden");
        }
    }
}
