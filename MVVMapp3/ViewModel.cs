using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace MVVMapp3
{
    

public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _firstName;
        private string _lastName;
        private DateTime _birthDate;
        private int _currentId = 1;
        public ICommand AddUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand OpenXmlCommand { get;  }
        public ICommand SaveXmlCommand { get; }
        public ObservableCollection<User> Users { get; set; }

        public ICommand UpdateXmlCommand { get; }
        private User _selectedUser;
        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                ((RelayCommand)DeleteUserCommand).RaiseCanExecuteChanged();
            }
        }
        public ViewModel()
        {
            Users = new ObservableCollection<User>(); // Users koleksiyonunu başlatıyoruz

            BirthDate = new DateTime(1999, 11, 30);

            AddUserCommand = new RelayCommand(AddUser, CanAddUser);
            DeleteUserCommand = new RelayCommand(DeleteUser, CanDeleteUser);
            SaveXmlCommand = new RelayCommand(SaveXml, CanSaveXml);
        }
        private void SaveXml(object parameter) 
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Xml dosyaları (*.xml) | *.xml | Tüm dosyalar (*.*)|*.*",
                Title = "Veriyi XML Dosyası Olarak Kaydet",
                DefaultExt = "xml"
            };

            bool? result = saveFileDialog.ShowDialog();
            if (result == true)
            {

                //Seçilen dosyanın yolunu al
                string filePath = saveFileDialog.FileName;
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<User>));

                    //Dosya akışını kullanarak xml Dosyasını oluşturuyoruz.
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        serializer.Serialize(writer, Users);
                    }
                    MessageBox.Show("Dosya başarıyla Kaydedildi!!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Dosya Kaydedilirken bir hata oluştu: {ex.Message}");
                }
            }
            }
        private bool CanSaveXml(object parameter)
        {

            //return Users != null && Users.Count > 0;
            return true;

        }
        private void AddUser(object parameter)
        {

            var user = new User()
            {
                Id = _currentId++,
                FirstName = FirstName,
                LastName = LastName,
                BirthDate = BirthDate
            };

            Users.Add(user);

            //// Alanları sıfırlayarak temizle
            FirstName = string.Empty;
            LastName = string.Empty;
            BirthDate = new DateTime(1999, 11, 30); // Varsayılan tarihe geri dön
            //((RelayCommand)AddUserCommand).RaiseCanExecuteChanged();
            //if (parameter is user)
            //{
            //    // User'ı koleksiyona ekle
            //    Users.Add(user);
            //}

        }
        private bool CanAddUser(object parameter)
        {
            //Tüm alanlar doluysa eklemeye izin ver
            return !string.IsNullOrWhiteSpace(FirstName) &&
                   !string.IsNullOrWhiteSpace(LastName) &&
                   BirthDate != default(DateTime);
            //return !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName) && BirthDate != null;

        }
        private void DeleteUser(object parameter)
        {
            var userToDelete = parameter as User;
            if (userToDelete != null)
            {
                Users.Remove(userToDelete);
                // ID'leri güncelle
                int id = 1;
                foreach (var user in Users)
                {
                    user.Id = id++;
                }
                // ID güncellemelerini yansıtmak için NotifyPropertyChanged çağırabiliriz
                OnPropertyChanged(nameof(Users));
            }
        }
        private bool CanDeleteUser(object parameter)
        {
            return parameter is User;
        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
                ((RelayCommand)AddUserCommand).RaiseCanExecuteChanged(); // Komutun durumunu güncelle

            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
                ((RelayCommand)AddUserCommand).RaiseCanExecuteChanged(); // Komutun durumunu güncelle

            }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
                 // Komutun durumunu güncelle

            }
        }


        //public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();














        
    }

}
