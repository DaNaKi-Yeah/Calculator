using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        public RelayCommand ClearHistoryCommand { get; set; }
        public RelayCommand CopyCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public MainViewModel()
        {
            ClickButtonCommand = new RelayCommand(obj =>
            {
                UpdateStartIsNumber();

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
                    if (getButton == ")")
                    {
                        int isBalanceBrackets = 0;

                        for (int i = 0; i < UpField.Length; i++)
                        {
                            if (UpField[i].ToString() == "(")
                            {
                                isBalanceBrackets++;
                            }
                            else if (UpField[i].ToString() == ")")
                            {
                                isBalanceBrackets--;
                            }
                        }

                        if (isBalanceBrackets == 0)
                        {
                            return;
                        }
                    }

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
                            if (UpField[UpField.Length - 2].ToString() == "(" && (getButton == "+" || getButton == "-"))
                            {
                                UpField = UpField.Remove(UpField.Length - 1, 1) + (getButton);
                            }
                            else if (UpField[UpField.Length - 2].ToString() == "(" && (getButton == "x" || getButton == "/"))
                            {
                                UpField = UpField.Remove(UpField.Length - 1, 1);
                            }
                            else if (UpField[UpField.Length - 2].ToString() != "(")
                            {
                                UpField = UpField.Remove(UpField.Length - 1, 1) + (getButton);
                            }
                        }
                    }
                    else if (UpField[UpField.Length - 1].ToString() == "(" && gotCharIsSymbol == true)
                    {
                        if (getButton == "+" || getButton == "-")
                        {
                            UpField += getButton;
                        }
                    }
                    else if (UpField[UpField.Length - 1].ToString() == ")" && (getButton == "0." || gotCharIsSymbol == false) && getButton != ")")
                    {
                        UpField += $"x{getButton}";
                    }
                    else if (endCharIsSymbol == true && getButton == ")")
                    {
                        UpField = UpField.Remove(UpField.Length - 1, 1) + getButton;
                    }
                    else if (endCharIsSymbol == true && getButton == "(-" && (UpField[UpField.Length - 1].ToString() == "+" || UpField[UpField.Length - 1].ToString() == "-"))
                    {
                        for (int i = UpField.Length - 1; i >= 0; i--)
                        {
                            string getSymbol = UpField[i].ToString();

                            if (getSymbol == "-" && UpField[i - 1].ToString() == "(")
                            {
                                UpField = UpField.Remove(i - 1, 2);
                                return;
                            }
                        }

                        if (UpField[UpField.Length - 1].ToString() == "+")
                            UpField = UpField.Insert(UpField.Length, getButton);
                    }
                    else if (endCharIsSymbol == false || symbols[4] == UpField[UpField.Length - 1].ToString())
                    {
                        if (getButton == "0.")
                        {
                            getButton = ".";

                            if (UpField[UpField.Length - 1].ToString() == "(")
                            {
                                UpField += "0.";
                                return;
                            }

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
                        else if (getButton == "(")
                        {
                            if (UpField[UpField.Length - 1].ToString() == "(")
                            {
                                UpField += getButton;
                            }
                            else if (UpField[UpField.Length - 1].ToString() == ".")
                            {
                                UpField = UpField.Remove(UpField.Length - 1, 1) + "x(";
                            }
                            else
                            {
                                UpField += "x(";
                            }
                        }
                        else if (getButton == "(-")
                        {
                            if (UpField[UpField.Length - 1].ToString() == "(")
                            {
                                UpField += getButton;
                                return;
                            }

                            for (int i = UpField.Length - 1; i >= 0; i--)
                            {
                                string getSymbol = UpField[i].ToString();

                                if (getSymbol == "-" && UpField[i - 1].ToString() == "(")
                                {
                                    UpField = UpField.Remove(i - 1, 2);

                                    break;
                                }

                                bool isSymbol = false;

                                foreach (string symbol in symbols)
                                {
                                    if (getSymbol == symbol || getSymbol == "(")
                                    {
                                        isSymbol = true;
                                    }
                                }

                                if (isSymbol == true)
                                {
                                    UpField = UpField.Insert(i + 1, getButton);

                                    StartIsNumber = false;

                                    break;
                                }
                                else if (StartIsNumber == true)
                                {
                                    UpField = UpField.Insert(0, getButton);
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
                bool IsEqual = false;

                for (int i = 0; i < Operations.Count; i++)
                {
                    if (Operations[i].OpUpField == UpField)
                    {
                        IsEqual = true;
                    }
                }
                if (IsEqual == false)
                {
                    Operations.Add(new Operation() { OpUpField = UpField, OpDownField = DownField });
                }

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
                if (UpField.Length > 0)
                {
                    UpField = UpField.Remove(UpField.Length - 1);
                    DownField = "";
                }

                UpdateDownField();
            });
            ClearHistoryCommand = new RelayCommand(obj =>
            {
                Operations.Clear();
            });
            CopyCommand = new RelayCommand(obj =>
            {
                if (IsWorking == false)
                {
                    IsWorking = true;
                }
                else if (IsWorking == true)
                {
                    UpField += SelectedOperation?.OpDownField;

                    IsWorking = false;
                }
                UpdateDownField();
            });
            DeleteCommand = new RelayCommand(obj =>
            {
                Operations.Remove(SelectedOperation);
            });
        }

        public ObservableCollection<Operation> Operations { get; set; } = new ObservableCollection<Operation>();

        private Operation selectedOperation;
        public Operation SelectedOperation
        {
            get { return selectedOperation; }
            set { selectedOperation = value; OnPropertyChanged("SelectedOperation"); }
        }
        public Operation lastSelectedOperation { get; set; }


        static string[] symbols = new string[5] { "+", "-", "x", "÷", "." };

        public string upField = "";
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
        public bool IsWorking { get; set; } = false;

        private void UpdateDownField()
        {
            string getField = UpField;

            if (getField.Length == 0)
            {
                return;
            }

            int addBrackets = 0;

            for (int i = 0; i < getField.Length; i++)
            {
                if (getField[i].ToString() == "(")
                {
                    addBrackets++;

                    try
                    {
                        if (getField[i + 1].ToString() == "+")
                        {
                            getField = getField.Remove(i + 1, 1);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else if (getField[i].ToString() == ")")
                {
                    addBrackets--;
                }
            }

            bool endIsSymbol = false;

            foreach (string symbol in symbols)
            {
                if (getField[getField.Length - 1].ToString() == symbol)
                {
                    endIsSymbol = true;
                }
            }

            if (endIsSymbol == false)
            {
                string multiplied = string.Join("", Enumerable.Repeat(")", addBrackets).ToArray());

                getField = getField.Insert(getField.Length, multiplied);

                string сalculation = getField.Replace('x', '*').Replace('÷', '/');

                сalculation = сalculation.Replace(',', '.');

                NCalc.Expression exp = new NCalc.Expression(сalculation);

                try
                {
                    DownField = exp.Evaluate().ToString().Replace(',', '.');
                }
                catch (Exception ex)
                {
                    DownField = "Ошибка вычисления";
                }
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
