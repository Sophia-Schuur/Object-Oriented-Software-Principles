using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CptS321
{
    public class Spreadsheet
    {
        private int RowCount;
        private int ColCount;
        private Cell[,] CellsArray; //2D array of cells, empty
        public event PropertyChangedEventHandler CellPropertyChanged;

        //contructor
        public Spreadsheet(int newNumRows, int newNumCols)
        {
            RowCount = newNumRows;
            ColCount = newNumCols;
            CellsArray = new Cell[newNumRows, newNumCols];
            for (int row = 0; row < newNumRows; row++)
            {
                for (int col = 0; col < newNumCols; col++)
                {
                    CellsArray[row, col] = new EditableCell(row, col, "", "");
                    CellsArray[row, col].PropertyChanged += PropertyChanged;
                }
            }
        }

        //return a cell at a particular given index
        public Cell GetCell(int rowIndex, int colIndex)
        {
            if (rowIndex > CellsArray.GetLength(0) || colIndex > CellsArray.GetLength(1))
            {
                return null;
            }
            else
            {
                return CellsArray[rowIndex, colIndex];
            }
        }

        //getters
        public int GetRowCount()
        {
            return RowCount;
        }

        public int GetColCount()
        {
            return ColCount;
        }

        public void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                if (!((Cell)sender).GetText().StartsWith("="))
                {
                    ((Cell)sender).SetValue(((Cell)sender).GetText());
                }
                else
                {
                    string equation = ((Cell)sender).GetText().Substring(1);
                    int column = Convert.ToInt16(equation[0]) - 'A';
                    int row = Convert.ToInt16(equation.Substring(1)) - 1;

                    Cell newCell = GetCell(row, column);
                    string value = newCell.GetValue();
                    ((Cell)sender).SetValue(value);
                }
            }
            CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Value"));
        }

        public void Demo()
        {
            int i = 0;
            Random rand = new Random();

            //randomize cells containing message
            for (i = 0; i < 50; i++)
            {
                int RandRowIndex = rand.Next(0, 49);
                int RandColIndex = rand.Next(0, 25);
                
                Cell newCell = GetCell(RandRowIndex, RandColIndex);
                newCell.SetText("EatPant");
                CellsArray[RandRowIndex, RandColIndex] = newCell;              
            }
            //set all B1 rows 
            for (i = 0; i < 50; i++)
            {
                CellsArray[i, 1].SetText("This is cell B" + (i + 1));
            }

            //set all rows in Ai to equal the value in Bi
            for (i = 0; i < 50; i++)
            {
                CellsArray[i, 0].SetText("=B" + (i + 1));
            }
        }
    }
}
