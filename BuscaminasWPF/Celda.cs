using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BuscaminasWPF
{
    public class Celda : INotifyPropertyChanged
    {
        int row;
        int column;
        string text;
        bool mina = false;
        Visibility showBomb = Visibility.Hidden;
        Visibility showFlag = Visibility.Hidden;
        Visibility showQuestion = Visibility.Hidden;
        

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
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

        public Visibility ShowBomb
        {
            get
            {
                return showBomb;
            }

            set
            {
                showBomb = value;
                RaisePropertyChanged("ShowBomb");
            }
        }

        public Visibility ShowFlag
        {
            get
            {
                return showFlag;
            }

            set
            {
                showFlag = value;
                RaisePropertyChanged("ShowFlag");
            }
        }

        public Visibility ShowQuestion
        {
            get
            {
                return showQuestion;
            }

            set
            {
                showQuestion = value;
                RaisePropertyChanged("ShowQuestion");
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
}
