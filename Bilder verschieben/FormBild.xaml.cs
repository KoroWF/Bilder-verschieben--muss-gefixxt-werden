using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BiboBilder;

namespace Bilder_verschieben
{
    public delegate void UpdateEventHandler(string titel, string beschreibung);
    /// <summary>
    /// Interaktionslogik für FormBild.xaml
    /// </summary>
    public partial class FormBild : Window
    {
        public event UpdateEventHandler OnUpdate;
        public FormBild(Bild bild)
        {
            InitializeComponent();
            txtTitel.Text = bild.Titel;
            txtBesch.Text = bild.Beschreibung; 

            //image.Source hat Typ Imagesource als abstrakte Klasse
            //Bitmap image ist eine unterklasse für instanz bilden
            imgBild.Source = new BitmapImage(new Uri(bild.Pfad));

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string titel = txtTitel.Text;
            string besch = txtBesch.Text;

            if(titel.Length > 0 && besch.Length > 0)
            {
                //sende die daten an Hauptfenster
                //callback
                OnUpdate?.Invoke(titel, besch);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
