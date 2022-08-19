using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BiboBilder;
using System.Collections.ObjectModel;
using Microsoft.WindowsAPICodePack.Dialogs;


namespace Bilder_verschieben
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //besser als List, weil Itemsource reagiert sofort auf Änderung

        ObservableCollection<Bild> liste = new ObservableCollection<Bild>();

        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            Anzeige.ItemsSource = liste;
        }

        private void Anzeige_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Wir nehmen das selektierte object als bild
            Bild wahl = Anzeige.SelectedItem as Bild;

            //wir übergeben an Fenster
            FormBild fenster = new FormBild(wahl);

            //was machen wir wenn auf update geklickt
            fenster.OnUpdate += Fenster_OnUpdate;

            //Fenster Öffnen
            fenster.ShowDialog();
        }

        private void Fenster_OnUpdate(string titel, string beschreibung)
        {
            Bild wahl = Anzeige.SelectedItem as Bild;
            wahl.Titel = titel;
            wahl.Beschreibung = beschreibung;
        }

        private void mnuBilder_Click(object sender, RoutedEventArgs e)
        {
            //Ich will bilder aus dem explorer auswählen
            OpenFileDialog dialog = new OpenFileDialog();
            //viele dateien sollen ausgewählt werden, daher:
            dialog.Multiselect = true;
            //uns soll es angezeigt werden.
            bool? geht = dialog.ShowDialog();
            if(geht == true)
            {
                //alle ausgewählten Bilder speichern (Pfade)
                string[] array = dialog.FileNames;
                foreach (string w in array)
                {
                    //name der Datei als Titel
                    string titel = System.IO.Path.GetFileName(w);
                    liste.Add(new Bild{ Pfad = w , Titel = titel});
                }
               // Anzeige.ItemsSource = null;
               // Anzeige.ItemsSource = liste;
            }
        }

        private void mnAnzeigen_Click(object sender, RoutedEventArgs e)
        {
            //alle Ordner für gallery anzeigen
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                string folder = dialog.FileName;

                liste.Clear();
                string[] bilder = System.IO.File.ReadAllLines(folder + "/inhalt.txt");
                foreach(string zeile in bilder)
                {
                    string[] wert = zeile.Split('|');
                    liste.Add(new Bild { Pfad = wert[0], Titel = wert[1], Beschreibung = wert[2] });
                }
                //string[] bilder = System.IO.Directory.GetFiles(folder);

                //liste.Clear();
                //foreach(string pfad in bilder)
                //{
                //wir prüfen die extension der Datei, nur wenn bild dann anzeigen
          //          string ex = System.IO.Path.GetExtension(pfad).ToLower(); ;
          //
            //        if (ex == ".png" || ex == ".jpg" || ex == ".bmp" || ex == ".jpeg" || ex == ".gif")
              //      {
                //        liste.Add(new Bild { Pfad = pfad });
                  //  }
             //   }
            }

        }

        private void mnErstellen_Click(object sender, RoutedEventArgs e)
        {

            FormGalerie fenster = new FormGalerie();

            fenster.OnCreate += Fenster_OnCreate;

            fenster.ShowDialog();
        }

        private void Fenster_OnCreate(string pfad)
        {
          
            //in dem Ordner eine textdatei anlegen
            System.IO.FileStream stream = System.IO.File.Create(pfad + "/inhalt.txt");

            System.IO.TextWriter write = new System.IO.StreamWriter(stream);

            //sobald das Fenster für Pfadangabe geschlossen wird wird hier der zielpfad übergeben
            foreach(Bild b in liste)
            {
                //Dateiname von Bild nehmen
                string dateiname = System.IO.Path.GetFileName(b.Pfad);
                string ziel = pfad + "/" + dateiname;
                System.IO.File.Copy(b.Pfad, pfad + "/" + dateiname);
                write.WriteLine(pfad + "/" + dateiname + "|" + b.Titel + "|" + b.Beschreibung);
          
            }
            liste.Clear();
            stream.Close();
        }
    }
}
