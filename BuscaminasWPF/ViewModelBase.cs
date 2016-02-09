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
                    c.Mina = false;
                    lstCeldas.Add(c);
                }

            Random r = new Random();
            for(int i=0; i< NumMinas; i++)
            {
                int x = r.Next(0, lstCeldas.Count);
                if (lstCeldas[x].Mina)
                    i--;
                lstCeldas[x].Mina = true;
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
            if (c.Mina)
            {
                c.Text = "M";
                MessageBox.Show("Game Over");
            }
            else
            {
                int n = GetNumMinasAlrededorCelda(c.Row, c.Column);
                c.Text = n.ToString();
                if (n == 0)
                {
                    DespejarCeldasAlrededor(c.Row, c.Column);
                }
            }
        }

        private void DespejarCeldasAlrededor(int i, int j)
        {
            if (i >= 0 && i < numDificultad && j >= 0 && j < numDificultad)
            {
                DespejarCeldasAlrededor_Pos(i - 1, j - 1);
                DespejarCeldasAlrededor_Pos(i - 1, j);
                DespejarCeldasAlrededor_Pos(i - 1, j + 1);
                DespejarCeldasAlrededor_Pos(i, j - 1);
                DespejarCeldasAlrededor_Pos(i, j + 1);
                DespejarCeldasAlrededor_Pos(i + 1, j - 1);
                DespejarCeldasAlrededor_Pos(i + 1, j);
                DespejarCeldasAlrededor_Pos(i + 1, j + 1);
            }
        }

        private void DespejarCeldasAlrededor_Pos(int a, int b)
        {
            int n1 = GetNumMinasAlrededorCelda(a, b);

            if (a >= 0 && a < numDificultad && b >= 0 && b < numDificultad && (GetCelda(a, b).Text == string.Empty))
            {
                GetCelda(a, b).Text = n1.ToString();
                //dgvMinas[a, b].Value = n1;
                //dgvMinas[a, b] = new DataGridViewTextBoxCell();

                if (n1 == 0)
                {
                    DespejarCeldasAlrededor(a, b);
                }
            }


        }

        private int GetNumMinasAlrededorCelda(int i, int j)
        {
            bool b1 = ExisteMinaEnCelda(i - 1, j - 1);
            bool b2 = ExisteMinaEnCelda(i - 1, j);
            bool b3 = ExisteMinaEnCelda(i - 1, j + 1);
            bool b4 = ExisteMinaEnCelda(i, j - 1);
            bool b5 = ExisteMinaEnCelda(i, j + 1);
            bool b6 = ExisteMinaEnCelda(i + 1, j - 1);
            bool b7 = ExisteMinaEnCelda(i + 1, j);
            bool b8 = ExisteMinaEnCelda(i + 1, j + 1);

            int n = 0;
            if (b1) n++;
            if (b2) n++;
            if (b3) n++;
            if (b4) n++;
            if (b5) n++;
            if (b6) n++;
            if (b7) n++;
            if (b8) n++;
            return n;
        }


        private bool ExisteMinaEnCelda(int i, int j)
        {
            if (i >= 0 && i < numDificultad && j >= 0 && j < numDificultad)
                return GetCelda(i, j).Mina;
            return false;
        }

        private Celda GetCelda(int row, int column)
        {
            return celdas.Where(e => e.Row == row && e.Column == column).First();
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
        bool mina = false;

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

        public bool Mina
        {
            get
            {
                return mina;
            }

            set
            {
                mina = value;
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
