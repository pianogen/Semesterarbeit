using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace ReportGenerator
{
    /// <summary>
    /// Interaction logic for Table.xaml
    /// </summary>
    public partial class Tabelle : Window
    {
        /// <summary>
        /// Initialisert die WPF Komponente
        /// Ruft die Methoden FillComboBox und ChooseColor auf
        /// </summary>
        /// <param name="list">Als Paramter wird die Liste der Connections Id mitgegegben</param>
        public Tabelle(List<string> list)
        {
            InitializeComponent();
            ChooseFont();
            FillComboBox(list);
            ChooseColor();
        }
        /// <summary>
        /// Setzt alle gestetzten Connections Id in eine ComboBox
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
        /// Ruft alle möglichen Schriftarten von Windows auf und speichert si in eine ComboBox
        /// Standardwert ist Arial
        /// </summary>
        public void ChooseFont()
        {
            foreach (System.Windows.Media.FontFamily fontFamily in Fonts.SystemFontFamilies)
            {
                FontChooser.Items.Add(fontFamily.Source);
            }
            FontChooser.SelectedValue = "Arial";
        }
        /// <summary>
        /// Generiert eine List mit allen möglichen Windows Farben und füllt eine ComboBox damit
        /// Als Standardwert wird weiss gesetzt
        /// </summary>
        private void ChooseColor()
        {
            List<string> colors = new List<string>();
            colors.AddRange(System.Enum.GetNames(typeof(KnownColor)));
            colors.Sort();
            foreach (string color in colors)
            {
                ColorChooser.Items.Add(color);
            }
            ColorChooser.SelectedValue = "White";
        }
        /// <summary>
        /// Property für die Verbindungs ID
        /// </summary>
        public string Id
        {
            get { return IdChooser.Text;  }
        }
        /// <summary>
        /// Property für den Stilzustand des Tabellenkopfes
        /// </summary>d
        public bool Bold
        {
            get { return HeaderBold.IsEnabled; }

        }
        /// <summary>
        /// Property für die Hintergrundfarbe des Tabellenkopfes
        /// </summary>
        public string BgColor
        {
            get { return ColorChooser.Text; }
        }
        /// <summary>
        /// Property für die Randeigenschaft der Tabelle
        /// </summary>
        public bool Border
        {
            get { return BorderChooser.IsEnabled; }
        }
        /// <summary>
        /// Property für die SQL Abfrage
        /// Die Eingabe wird maskiert, um die Xml Syntax nicht zu verletzen
        /// </summary>
        public string Sql
        {
            get { return Value.Text; }

        }

        /// <summary>
        /// Property für den Schriftart des Texts
        /// </summary>
        public string Font
        {
            get { return FontChooser.Text; }
        }
        /// <summary>
        /// Property für die Textgrösse
        /// </summary>
        public string Size
        {
            get { return SizeChooser.Text; }
        }

        /// <summary>
        /// Property für den Absatz nach dem Text
        /// </summary>
        public string Paragraph
        {
            get { return ParagraphAfter.Text; }
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
                EmptyCheck();
                this.DialogResult = true;
                this.Close();
            }
            catch (EmptyTextBoxException exc)
            {
                MessageBox.Show(exc.Message);
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
        /// Überprüft, ob alle Textfelder etwas beinhalten
        /// </summary>
        private void EmptyCheck()
        {
            if (Id == "" || Sql == "")
            {
                throw new EmptyTextBoxException("Kein Feld darf leer sein");
            }
        }

        /// <summary>
        /// Lässt bei den Textfelder 'Size' und 'Paragraph' nur numerische Werte zu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void int_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }
    }
}
