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
        //public abstract void x();

        //Step 3 properties (read only)
        protected int rowIndex, colIndex;
        protected string text;
        protected string value;

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
        /*public Cell(int newRowIndex, int newcolIndex, string inText, string inValue)
        {
            rowIndex = newRowIndex;
            colIndex = newcolIndex;
            text = inText;
            value = inValue;
        }*/

        //getters
        public int GetRowIndex()
        {
            return rowIndex;
        }

        public int GetColIndex()
        {
            return colIndex;
        }

        public string GetText()
        {
            return text;
        }

        public string GetValue()
        {
            return value;
        }

        //setters
        public void SetRowIndex(int newRowIndex)
        {
            rowIndex = newRowIndex;
        }

        public void SetColIndex(int newColIndex)
        {
            colIndex = newColIndex;
        }

        public void SetText(string newText)
        {
            //if the text is being set to the same text, ignore
            if(text == newText)
            {
                return;
            }
            else
            {
                text = newText;
                PropertyChanged(this, new PropertyChangedEventArgs("Text"));
            }

        }

        //only the spreadsheet and cell class can change the value
        protected internal void SetValue(string newValue)
        {
            if (newValue == value)
            {
                return; //ignore
            }
            else
            {
                value = newValue;
                PropertyChanged(this, new PropertyChangedEventArgs("Value"));
            }
        }

    }

    //used in spreadsheet class to initialize custom cell parameters
    public class EditableCell : Cell
    {
        public EditableCell(int newRowIndex, int newcolIndex, string inText, string inValue)
        {
            rowIndex = newRowIndex;
            colIndex = newcolIndex;
            text = inText;
            value = inValue;
        }
    }
}
