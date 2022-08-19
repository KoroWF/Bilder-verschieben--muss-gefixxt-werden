using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiboBilder
{
    public class Bild:INotifyPropertyChanged
    {
        public string Pfad { get; set; }

        private string _titel;
        public string Titel 
        { 
            get { return _titel; }


            set 
            {
                _titel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Titel"));      
            }
        
        }

        private string _beschreibung;
        public string Beschreibung 
        {
            get { return _beschreibung; }


            set
            {
                _beschreibung = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Beschreibung"));
            }


        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
