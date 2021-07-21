using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace Calculator.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        public RelayCommand ClickButtonCommand { get; set; }
        public RelayCommand ResultCommand { get; set; }
        public MainViewModel()
        {
            ClickButtonCommand = new RelayCommand(obj =>
            {
                UpField += (string)obj;
            });

            ResultCommand = new RelayCommand(obj =>
            {
                string сalculation = UpField.Replace('x', '*');

                NCalc.Expression exp = new NCalc.Expression(сalculation);

                UpField = exp.Evaluate().ToString();
            });
        }


        private string upField;
        public string UpField
        {
            get { return upField; }
            set
            {
                upField = value;

                OnPropertyChanged("UpField");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
