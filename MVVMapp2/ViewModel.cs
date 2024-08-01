using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MVVMapp2
{
    // ProfilViewModel, ViewModel olarak kullanılan bir sınıftır
    public class ViewModel : INotifyPropertyChanged
    {
        private string _name; // Özelliğin veri saklama alanı

        // Public özellik: UI ile veri bağlama için kullanılan ve değişikliklerde bildirim yapan özellik
        public string Name
        {
            get { return _name; } // Özelliğin get yöntemi `_name` değerini döndürür
            set
            {
                if (_name != value) // `_name` ile `value` (yeni değer) eşit değilse
                {
                    _name = value; // `_name` alanını yeni değer ile güncelle
                    OnPropertyChanged(nameof(Name)); // Özellik değiştiğinde PropertyChanged olayını tetikle
                    (SaveCommand as RelayCommand)?.RaiseCanExecuteChanged(); // Komutun çalıştırılabilirliğini güncelle
                }
            }
        }

        // Komut: UI'dan tetiklenen işlemleri gerçekleştiren komut
        public ICommand SaveCommand { get; }

        // Constructor: ViewModel'in başlatılması ve komutların oluşturulması
        public ViewModel()
        {
            SaveCommand = new RelayCommand(
                param => Save(), // Komut çalıştırıldığında çağrılacak yöntem
                param => CanSave() // Komutun çalıştırılabilirliğini kontrol eden yöntem
            );
        }

        // Save komutu: Butona tıklandığında çalıştırılır
        private void Save()
        {
            MessageBox.Show("Kaydedildi!"); // Kaydetme işlemini simüle eden MessageBox
        }

        // CanSave: Save komutunun çalıştırılabilirliğini belirler
        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Name); // `Name` özelliği boş değilse true döner
        }

        // PropertyChanged olayı: Özelliklerdeki değişiklikleri dinlemek için
        public event PropertyChangedEventHandler PropertyChanged;

        // OnPropertyChanged: Özellik değişikliklerini UI'ye bildiren metod
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // PropertyChanged olayını tetikler
        }
    }
}
