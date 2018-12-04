using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace /*SpreadsheetEngine*/ CptS321
{
    public abstract class Cell : INotifyPropertyChanged
    {
        private readonly int rowIndex;
        private readonly int colIndex;

        private string name;
        private string text;
        private string value;

        public int RowIndex
        {
            get
            {
                return rowIndex;
            }
        }

        public int ColumnIndex
        {
            get
            {
                return colIndex;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        //empty argument constructor - set everything 0
        public Cell()
        {
            rowIndex = 0;
            colIndex = 0;
            text = null;
            value = null;
        }
        //full argument constructor
        public Cell(int newRowIndex, int newcolIndex, string inText, string inValue)
        {
            rowIndex = newRowIndex;
            colIndex = newcolIndex;
            text = inText;
            value = inValue;
        }
        //half constructor
        public Cell(int row, int col, string name)
        {
            rowIndex = row;
            colIndex = col;
            this.name = name;
            text = null;
            value = null;            
        }

        public string Value
        {
            get
            {
                return value;
            }
            internal set
            {
                if(this.value == value)
                {
                    return;
                }
                this.value = value;
                OnPropertyChanged("Value"); 
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                if (text == value)
                {
                    return;
                }
                    
                text = value;
                OnPropertyChanged("Text");
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
    public class EditableCell : Cell
    {
        //Constructor
        public EditableCell(int row, int col, string name) : base(row, col, name) { }
    }
}
