using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MVVMapp
{
    internal class ViewModel
    {
    }


public class ProfilViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Profil> profils;
        private Profil selectedProfil;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Profil> Profils
        {
            get { return profils; }
            set
            {
                profils = value;
                OnPropertyChanged(nameof(Profils));
            }
        }

        public Profil SelectedProfil
        {
            get { return selectedProfil; }
            set
            {
                selectedProfil = value;
                OnPropertyChanged("SelectedProfil");
            }
        }

        public ProfilViewModel()
        {
            Profils = new ObservableCollection<Profil>
        {
            new Profil { Id = 1, Name = "Alice" },
            new Profil { Id = 2, Name = "Bob" }
        };
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
