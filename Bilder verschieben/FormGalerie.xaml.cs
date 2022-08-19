using System;
using System.Collections.Generic;
using System.IO;
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

namespace Bilder_verschieben
{

    public delegate void CreateEventHandler(string pfad);
    /// <summary>
    /// Interaktionslogik für FormGalerie.xaml
    /// </summary>
    public partial class FormGalerie : Window
    {
        public event CreateEventHandler OnCreate;
        public FormGalerie()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string wunschname = txtName.Text;

            if(wunschname.Length > 0)
            {
                //wir erstellen uns einen ordner in Meine Bilder
                string pfad = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                pfad = pfad + "/Gallery/" + wunschname;

                if (Directory.Exists(pfad) == false)
                {   //aus Mainwindow Liste der Bilder kopieren
                    //Callback
                    System.IO.Directory.CreateDirectory(pfad);
                    OnCreate?.Invoke(pfad);
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Der angegebene Ordner Exestiert bereits!", "Achtung!", MessageBoxButton.OK);
                    if (result == MessageBoxResult.OK)
                    {
                        return;
                    }
                }
                
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
