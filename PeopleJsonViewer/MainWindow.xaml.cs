using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace PeopleJsonViewer
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Rootobject jsonData;
        private Person currentPerson;
        private int index = 0;
        private string _name;
        private string _gender;
        private string _dob;
        private string _job;
        private string _phoneNumber;
        private string _email;
        private string _fullAddress;

        public string FName { get => _name; set { _name = value; OnPropertyChanged(nameof(FName)); } }
        public string Gender { get => _gender; set { _gender = value; OnPropertyChanged(nameof(Gender)); } }
        public string Dob { get => _dob; set { _dob = value; OnPropertyChanged(nameof(Dob)); } }
        public string Job { get => _job; set { _job = value; OnPropertyChanged(nameof(Job)); } }
        public string PhoneNumber { get => _phoneNumber; set { _phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); } }
        public string Email { get => _email; set { _email = value; OnPropertyChanged(nameof(Email)); } }
        public string FullAddress { get => _fullAddress; set { _fullAddress = value; OnPropertyChanged(nameof(FullAddress)); } }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

        }

        private void OpenFileDialog_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                string jsonContent = File.ReadAllText(selectedFilePath);
                jsonData = JsonConvert.DeserializeObject<Rootobject>(jsonContent);
                LoadPerson();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            index++;
            if (index >= jsonData.people.Length) index = jsonData.people.Length - 1;
            LoadPerson();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (index > 0) index--;
            LoadPerson();
        }

        private void LoadPerson()
        {
            currentPerson = jsonData.people[index];
            FName = $"Name: {currentPerson.name}";
            Gender = $"Gender: {currentPerson.gender}";
            Dob = $"Date of Birth: {currentPerson.dob}";
            Job = $"Job: {currentPerson.job}";
            PhoneNumber = $"Phone number: {currentPerson.phone_number}";
            Email = $"Email: {currentPerson.email}";
            FullAddress = $"Address:\n{currentPerson.full_address}";
            UpdateLayout();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class Rootobject
        {
            public Person[] people { get; set; }
        }

        public class Person
        {
            public string name { get; set; }
            public string gender { get; set; }
            public string dob { get; set; }
            public string job { get; set; }
            public string phone_number { get; set; }
            public string email { get; set; }
            public string full_address { get; set; }
        }
    }
}
