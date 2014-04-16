//PageSetup.xaml.cs
//compile with: /doc:PageSetup.html
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
    /// Interaction logic for PageSetup.xaml
    /// </summary>
    public partial class PageSetup : Window
    {
        /// <summary>
        /// Initialiserung der WPF Komponenten
        /// </summary>
        public PageSetup()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Property für den Speicherort und Namen des Word Dokuments
        /// </summary>
        public string Save
        {
            get { return SavePathChooser.Text; }
        }
        /// <summary>
        /// Property für den Speicherort der Vorlage
        /// </summary>
        public string TemplatePath
        {
            get { return TemplateChooser.Text; }
        }
        /// <summary>
        /// Button_Handler, um den Pfad des Word Dokuments anzugeben
        /// Windows Explorer Fenster öffnet sich
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.Filter = "Word-Dokument (*.docx)|*.docx";
            dlg.ShowDialog();
            if (dlg.FileName != "")
            {
                SavePathChooser.Text = dlg.FileName;
            }
        }
        /// <summary>
        /// Button_Handler, um den Pfad der Word Vorlage anzugeben
        /// Windows Explorer öffnet sich
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowseTemplate_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Word-Vorlage (*.dotx)|*.dotx";
            dlg.ShowDialog();
            TemplateChooser.Text = dlg.FileName;
        }
        /// <summary>
        /// Button Handler um die Eingaben zurück zu setzen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Button Handler, um die Eingaben zu bestätigen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EmptyTextBox();
                this.DialogResult = true;
                this.Close();
            }
            catch (EmptyTextBoxException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        /// <summary>
        /// Button Handler, um die Prozedur 'Word DOkument Speicherort' abzubrechen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Cancel_Click(object sender, RoutedEventArgs e)
        {
            SavePathChooser.Text = "";
        }
        /// <summary>
        /// Button Handler, um die Prozedur 'Word Vorlage Speicherort' abzubrechen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Template_Cancel_Click(object sender, RoutedEventArgs e)
        {
            TemplateChooser.Text = "";
        }
        /// <summary>
        /// Überprüft, ob alle Felder etwas beinhalten
        /// </summary>
        private void EmptyTextBox()
        {
            if (Save == "" || TemplatePath == "")
                throw new EmptyTextBoxException("Es darf kein Feld leer sein");
        }

    }
}
