using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace Calculator.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            InputNumberCommand = new RelayCommand(obj =>
            {
                Field += (string)obj;
            });
        }

        private string field;
        public string Field
        {
            get { return field; }
            set { field = value; OnPropertyChanged("Field"); }
        }

        public RelayCommand InputNumberCommand { get; set; }




        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
