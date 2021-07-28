using Calculator.ViewModels;
using System;
using Calculator;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace Calculator.Models
{
    class Operation:INotifyPropertyChanged
    {
        private string opUpField;
        public string OpUpField
        {
            get { return opUpField; }
            set { opUpField = value; OnPropertyChanged("OpUpField"); }
        }

        private string opDownField;
        public string OpDownField
        {
            get { return opDownField; }
            set { opDownField = value; OnPropertyChanged("OpDownField"); }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
