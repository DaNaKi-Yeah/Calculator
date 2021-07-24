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
        public RelayCommand ClearCommand { get; set; }
        public RelayCommand CutCommand { get; set; }
        public RelayCommand AddRoundBracketCommand { get; set; }
        public MainViewModel()
        {
            ClickButtonCommand = new RelayCommand(obj =>
            {
                string getButton = (string)obj;

                if (getButton == ".")
                {
                    getButton = "0.";
                }

                if (UpField.Length == 0)
                {
                    bool startIsSymbol = false;

                    foreach (string symbol in symbols)
                    {
                        if (getButton == symbol)
                        {
                            startIsSymbol = true;
                        }
                    }

                    if (!startIsSymbol)
                    {
                        UpField += getButton;
                    }
                }
                else
                {
                    bool endCharIsSymbol = false;
                    bool gotCharIsSymbol = false;

                    foreach (string symbol in symbols)
                    {
                        if (UpField[UpField.Length - 1].ToString() == symbol)
                        {
                            endCharIsSymbol = true;
                        }
                        if (getButton == symbol)
                        {
                            gotCharIsSymbol = true;
                        }
                    }

                    if (endCharIsSymbol == true && gotCharIsSymbol == true)
                    {
                        if (UpField[UpField.Length - 1].ToString() != getButton)
                        {
                            UpField = UpField.Remove(UpField.Length - 1, 1) + (getButton);
                        }
                    }
                    else if (endCharIsSymbol == false || symbols[4] == UpField[UpField.Length - 1].ToString())
                    {
                        if (getButton == "0.")
                        {
                            getButton = ".";

                            for (int i = UpField.Length - 1; i >= 0; i--)
                            {
                                string getSymbol = UpField[i].ToString();

                                if (getSymbol == ".")
                                {
                                    return;
                                    //не ставит точку
                                }

                                bool isSymbol = false;

                                foreach (string symbol in symbols)
                                {
                                    if (getSymbol == symbol)
                                    {
                                        isSymbol = true;
                                    }
                                }

                                if (isSymbol == true || StartIsNumber == true)
                                {
                                    UpField += getButton;

                                    StartIsNumber = false;

                                    break;
                                }
                            }
                        }
                        else
                        {
                            UpField += getButton;
                        }
                    }
                    else
                    {
                        UpField += getButton;
                    }
                }
                UpdateDownField();
            });
            ResultCommand = new RelayCommand(obj =>
            {
                UpField = DownField;

                DownField = "";

                UpdateStartIsNumber();
            });
            ClearCommand = new RelayCommand(obj =>
            {

                UpField = "";

                DownField = "";

                UpdateStartIsNumber();
            });
            CutCommand = new RelayCommand(obj =>
            {
                if (UpField.Length != 1)
                {
                    UpField = UpField.Remove(UpField.Length - 1);
                }
                else
                {
                    UpField = UpField.Remove(UpField.Length - 1);
                    DownField = "";
                }

                UpdateDownField();
            });
            AddRoundBracketCommand = new RelayCommand(obj =>
            {
                //UpField += "(";
            });
        }

        static string[] symbols = new string[5] { "+", "-", "x", "/", "." };

        private string upField = "";
        public string UpField
        {
            get { return upField; }
            set
            {
                upField = value;

                OnPropertyChanged("UpField");
            }
        }

        private string downField = "";
        public string DownField
        {
            get { return downField; }
            set { downField = value; OnPropertyChanged("DownField"); }
        }

        public bool StartIsNumber { get; set; } = true;

        private void UpdateDownField()
        {
            if (UpField.Length == 0)
            {
                return;
            }

            bool endIsSymbol = false;

            foreach (string symbol in symbols)
            {
                if (UpField[UpField.Length - 1].ToString() == symbol)
                {
                    endIsSymbol = true;
                }
            }

            if (endIsSymbol == false)
            {
                string сalculation = UpField.Replace('x', '*');

                сalculation = сalculation.Replace(',', '.');

                NCalc.Expression exp = new NCalc.Expression(сalculation);

                DownField = exp.Evaluate().ToString();
            }
        }
        private void UpdateStartIsNumber()
        {
            StartIsNumber = true;

            for (int i = 0; i < UpField.Length; i++)
            {
                string symbolText = UpField[i].ToString();

                foreach (string symbol in symbols)
                {
                    if (symbolText == symbol)
                    {
                        StartIsNumber = false;
                    }
                }
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
