using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;
using System.Data;
using System.Collections;
using System.ComponentModel;

namespace BuscaminasWPF
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase()
        {
            _canExecute = true;
        }
        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => MyAction(), _canExecute));
            }
        }
        private bool _canExecute;
        public void MyAction()
        {
            List<Celda> lstCeldas = new List<Celda>();
            for (int i = 0; i < NumDificultad; i++)
                for (int j = 0; j < NumDificultad; j++) 
                {
                    Celda c = new Celda();
                    c.Row = i;
                    c.Column = j;
                    c.Text = "";
                    lstCeldas.Add(c);
                }

            Celdas = lstCeldas;
        }

        private ICommand _leftClickCommand;
        public ICommand LeftClickCommand
        {
            get
            {
                return _leftClickCommand ?? (_leftClickCommand = new CommandHandler_Celda((Celda c) => MyActionLeftClick(c), true));
            }
        }

        public void MyActionLeftClick(Celda c)
        {
            c.Text = c.Row +" "+ c.Column;
        }


        int numMinas = 10;

        public int NumMinas
        {
            get { return numMinas; }
            set { numMinas = value;
            RaisePropertyChanged("NumMinas");
            }
        }
        int numDificultad = 10;

        public int NumDificultad
        {
            get { return numDificultad; }
            set { numDificultad = value;
            RaisePropertyChanged("NumDificultad");
            }
        }

        List<Celda> celdas;

        public List<Celda> Celdas
        {
            get { return celdas; }
            set { celdas = value;
            RaisePropertyChanged("Celdas");
            }
        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler temp = PropertyChanged;
            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class Celda : INotifyPropertyChanged
    {
        int row;
        int column;
        string text;

        public string Text
        {
            get { return text; }
            set { text = value;
            RaisePropertyChanged("Text");
            }
        }
        
        public int Row
        {
            get { return row; }
            set { row = value; }
        }
        
        public int Column
        {
            get { return column; }
            set { column = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler temp = PropertyChanged;
            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class CommandHandler : ICommand
    {
        private Action _action;
        private bool _canExecute;
        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }
    }

    public class CommandHandler_Celda : ICommand
    {
        private Action<Celda> _action;
        private bool _canExecute;
        public CommandHandler_Celda(Action<Celda> action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action((Celda)parameter);
        }
    }
}
