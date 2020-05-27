using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Calculator
{
    public class CalculatorViewModel : ViewModelBase
    {
        string currentEntry = "0";
        bool isNumDisplayed = false;
        double accumulatedNum = 0;
        string operatorPressed = "";

        public CalculatorViewModel()
        {
            ClearCommand = new Command(
                execute: () =>
                {
                    accumulatedNum = 0;
                    CurrentEntry = "0";
                    isNumDisplayed = false;
                    RefreshCanExecutes();
                });

            NumericCommand = new Command<string>(
                execute: (string parameter) =>
                {
                    if (isNumDisplayed || CurrentEntry == "0")
                        CurrentEntry = parameter;
                    else
                        CurrentEntry += parameter;

                    isNumDisplayed = false;
                    RefreshCanExecutes();
                },
                canExecute: (string parameter) =>
                {
                    if (parameter == "0" && operatorPressed == "/")
                    {
                        return !isNumDisplayed;
                    }
                    return isNumDisplayed || CurrentEntry.Length < 16;
                });

            DecimalPointCommand = new Command(
                execute: () =>
                {
                    if (isNumDisplayed)
                        CurrentEntry = "0.";
                    else
                        CurrentEntry += ".";

                    isNumDisplayed = false;
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return isNumDisplayed || !CurrentEntry.Contains(".");
                });

            OperatorCommand = new Command<string>(
                execute: (string parameter) =>
                {
                    double value = Double.Parse(CurrentEntry);
                    accumulatedNum = value;
                    isNumDisplayed = true;
                    operatorPressed = parameter;
                    RefreshCanExecutes();
                },
                canExecute: (string parameter) =>
                {
                    return !isNumDisplayed;
                });

            FactorialCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    if (value == 0)
                    {
                        CurrentEntry = "1";
                    }
                    else
                    {
                        double tmp = value;
                        for (int i = 1; i < value; i++)
                        {
                            tmp *= i;
                        }
                        CurrentEntry = tmp.ToString();
                    }
                    isNumDisplayed = false;
                    operatorPressed = "";
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    double checkValue = Double.Parse(CurrentEntry);
                    return !isNumDisplayed && (checkValue % 1 == 0) && (checkValue >= 0);
                });

            SquareCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    double square = value * value;
                    CurrentEntry = square.ToString();
                    isNumDisplayed = false;
                    operatorPressed = "";
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return !isNumDisplayed;
                });

            OneOverCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    double oneOver = 1/value;
                    CurrentEntry = oneOver.ToString();
                    isNumDisplayed = false;
                    operatorPressed = "";
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return !isNumDisplayed && (currentEntry != "0");
                });

            RootCommand = new Command<string>(
                execute: (string parameter) =>
                {
                    double value = Double.Parse(CurrentEntry);
                    double root = Double.Parse(parameter);
                    double res = Math.Pow(value, 1.0 / root);
                    CurrentEntry = res.ToString();
                    isNumDisplayed = false;
                    operatorPressed = "";
                    RefreshCanExecutes();
                },
                canExecute: (string parameter) =>
                {
                    if (parameter == "2")
                    {
                        double tmp = Double.Parse(currentEntry);
                        return !isNumDisplayed && tmp >= 0;
                    }
                    return !isNumDisplayed;
                });

            lnCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    double res = Math.Log(value);
                    CurrentEntry = res.ToString();
                    isNumDisplayed = false;
                    operatorPressed = "";
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    double tmp = Double.Parse(currentEntry);
                    return !isNumDisplayed && tmp > 0;
                });

            LogCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    double res = Math.Log10(value);
                    CurrentEntry = res.ToString();
                    isNumDisplayed = false;
                    operatorPressed = "";
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    double tmp = Double.Parse(currentEntry);
                    return !isNumDisplayed && tmp > 0;
                });

            EtoXCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    double res = Math.Pow(Math.E,value);
                    CurrentEntry = res.ToString();
                    isNumDisplayed = false;
                    operatorPressed = "";
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return !isNumDisplayed;
                });

            TrigCommand = new Command<string>(
                execute: (string parameter) =>
                {
                    double value = Double.Parse(CurrentEntry);
                    double res = 0;
                    if (parameter == "sin")
                    {
                        res = Math.Sin(value);
                    }
                    if (parameter == "cos")
                    {
                        res = Math.Cos(value);
                    }
                    if (parameter == "tan")
                    {
                        res = Math.Tan(value);
                    }
                    CurrentEntry = res.ToString();
                    isNumDisplayed = false;
                    operatorPressed = "";
                    RefreshCanExecutes();
                },
                canExecute: (string parameter) =>
                {
                    return !isNumDisplayed;
                });

            RandCommand = new Command<string>(
                execute: (string parameter) =>
                {
                    double value = Double.Parse(CurrentEntry);
                    Random random = new Random();
                    double res = random.NextDouble();
                    CurrentEntry = res.ToString();
                    isNumDisplayed = false;
                    operatorPressed = "";
                    RefreshCanExecutes();
                },
                canExecute: (string parameter) =>
                {
                    return !isNumDisplayed;
                });

            EqualsCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    if (operatorPressed == "+")
                    {
                        accumulatedNum += value;
                    }
                    if (operatorPressed == "-")
                    {
                        accumulatedNum -= value;
                    }
                    if (operatorPressed == "*")
                    {
                        accumulatedNum *= value;
                    }
                    if (operatorPressed == "/")
                    {
                        accumulatedNum /= value;
                    }
                    CurrentEntry = accumulatedNum.ToString();
                    isNumDisplayed = false;
                    operatorPressed = "";
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return !isNumDisplayed;
                });

            PlusMinusCommand = new Command(
                execute: () => {
                    double v = -Double.Parse(CurrentEntry);
                    CurrentEntry = v.ToString();
                    RefreshCanExecutes();
                });

            SecondCommand = new Command(
                execute: () => {
                    CurrentState = "2^x";
                    RefreshCanExecutes();
                });
            XtoXCommand = new Command<Button>(
                execute: (Button parameter) =>
                {
                    double value = Double.Parse(CurrentEntry);
                    double res = 1;
                    if (parameter.Text == "2^x")
                    {
                        res = Math.Pow(2, value);
                    }
                    if (parameter.Text == "10^x")
                    {
                        res = Math.Pow(10, value);
                    }
                    CurrentEntry = res.ToString();
                    isNumDisplayed = false;
                    operatorPressed = "";
                    RefreshCanExecutes();
                },
                canExecute: (Button parameter) =>
                {
                    return !isNumDisplayed;
                });
        }

        void RefreshCanExecutes()
        {
            ((Command)NumericCommand).ChangeCanExecute();
            ((Command)DecimalPointCommand).ChangeCanExecute();
            ((Command)OperatorCommand).ChangeCanExecute();
            ((Command)FactorialCommand).ChangeCanExecute();
            ((Command)SquareCommand).ChangeCanExecute();
            ((Command)OneOverCommand).ChangeCanExecute();
            ((Command)RootCommand).ChangeCanExecute();
            ((Command)lnCommand).ChangeCanExecute();
            ((Command)LogCommand).ChangeCanExecute();
            ((Command)EtoXCommand).ChangeCanExecute();
            ((Command)TrigCommand).ChangeCanExecute();
            ((Command)RandCommand).ChangeCanExecute();
            ((Command)ClearCommand).ChangeCanExecute();
            ((Command)PlusMinusCommand).ChangeCanExecute();
            ((Command)SecondCommand).ChangeCanExecute();
            ((Command)EqualsCommand).ChangeCanExecute();
            ((Command)XtoXCommand).ChangeCanExecute();

        }

        public string CurrentEntry
        {
            private set { SetProperty(ref currentEntry, value); }
            get { return currentEntry; }
        }


        public string CurrentState { private set; get; }
        public ICommand ClearCommand { private set; get; }

        public ICommand NumericCommand { private set; get; }
        public ICommand PlusMinusCommand { private set; get; }

        public ICommand DecimalPointCommand { private set; get; }

        public ICommand OperatorCommand { private set; get; }
        public ICommand FactorialCommand { private set; get; }
        public ICommand SquareCommand { private set; get; }
        public ICommand OneOverCommand { private set; get; }
        public ICommand RootCommand { private set; get; }
        public ICommand lnCommand { private set; get; }
        public ICommand LogCommand { private set; get; }
        public ICommand EtoXCommand { private set; get; }
        public ICommand TrigCommand { private set; get; }
        public ICommand RandCommand { private set; get; }
        public ICommand SecondCommand { private set; get; }
        public ICommand XtoXCommand { private set; get; }

        public ICommand EqualsCommand { private set; get; }


        public void SaveState(IDictionary<string, object> dictionary)
        {
            dictionary["CurrentEntry"] = CurrentEntry;
            dictionary["isNumDisplayed"] = isNumDisplayed;
            dictionary["accumulatedNum"] = accumulatedNum;
        }

        public void RestoreState(IDictionary<string, object> dictionary)
        {
            CurrentEntry = GetDictionaryEntry(dictionary, "CurrentEntry", "0");
            isNumDisplayed = GetDictionaryEntry(dictionary, "isNumDisplayed", false);
            accumulatedNum = GetDictionaryEntry(dictionary, "accumulatedNum", 0.0);

            RefreshCanExecutes();
        }

        public T GetDictionaryEntry<T>(IDictionary<string, object> dictionary,
                                        string key, T defaultValue)
        {
            if (dictionary.ContainsKey(key))
                return (T)dictionary[key];

            return defaultValue;
        }
    }
}
