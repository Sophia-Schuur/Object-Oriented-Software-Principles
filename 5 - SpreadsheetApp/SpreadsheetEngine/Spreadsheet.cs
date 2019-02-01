using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Xml.Linq;
using System.IO;
using System.Xml;

namespace CptS321
{
    public class Spreadsheet
    {
        //getters/setters (autoproperties)
        public int RowCount { get; set; }
        public int ColCount { get; set; }

        //fields
        public Cell[,] CellsArray;    //2D array of cells, empty        
        public event PropertyChangedEventHandler CellPropertyChanged;
        private Dictionary<Cell, List<Cell>> dependencies;  //holds a dictionary of depenendencies of any given cell
        private Dictionary<String, Cell> cells; //dictionary of all cells

        //constructor
        public Spreadsheet(int newNumRows, int newNumCols)
        {
            RowCount = newNumRows;
            ColCount = newNumCols;

            CellsArray = new Cell[newNumRows, newNumCols];
            dependencies = new Dictionary<Cell, List<Cell>>();
            cells = new Dictionary<string, Cell>();
            for (int row = 0; row < newNumRows; row++)
            {
                for (int col = 0; col < newNumCols; col++)
                {
                    CellsArray[row, col] = new EditableCell(row, col, Convert.ToChar(col + 65).ToString() + (row + 1).ToString());
                    cells[CellsArray[row, col].Name] = CellsArray[row, col];
                    CellsArray[row, col].PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);
                }
            }
        }

        //return cell given its name. Uses the dictionary to track keys (cell name)
        private Cell GetCell(string name)
        {
            try
            {
                return cells[name];
            }
            //key exists?
            catch
            {
                throw new KeyNotFoundException();
            }

        }

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Cell cell = cells["A1"];
            Cell cell = (Cell)sender;
            if (e.PropertyName == "Text")
            {
                RemoveDependency(cell);
                DetermineValue(cell);
            }
            if (e.PropertyName == "Value")
            {
                Cascade(cell);
            }
            CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Value"));
        }

        //checks for cascading dependencies
        private void Cascade(Cell cell)
        {
            foreach (Cell c in dependencies.Keys)
            {
                //if a dependency exists, redetermine the cell value
                if (dependencies[c].Contains(cell))
                {
                    DetermineValue(c);
                }
            }
        }

        //determine value of a cell
        private void DetermineValue(Cell cell)
        {
            //empty cell. Set value to 0
            if (cell.Text.Length == 0)
            {
                cell.Value = "";
            }

            //formula?
            else if (cell.Text.StartsWith("="))
            {
                if (cell.Text.Length > 1)
                {
                    EvaluateExpression(cell);
                }
            }

            //not a formula, just set the value to text
            else
            {
                cell.Value = cell.Text;
            }
        }

        //evaluate formula
        public void EvaluateExpression(Cell cell)
        {
            int flag = 0;
            int count = 0;  //number of variables
            //remove equal sign
            ExpTree tree = new ExpTree(cell.Text.Substring(1));
            try
            {
                //each variable name in tree
                foreach (string sCell in tree.GetVariables())
                {
                    count++;
                    Cell dep = GetCell(sCell);
                    //hi: value = hi, text = hi
                    //does the cell key exist in the dependency list?
                    if (!dependencies.ContainsKey(cell))
                    {
                        //add a new LIST of DEPENDENCY CELLS for THIS PARTICULAR CELL
                        dependencies.Add(cell, new List<Cell>());
                    }

                    //because we know each variable exists in the tree add them to the list of dependencies
                    dependencies[cell].Add(dep);

                    if (Double.TryParse(dep.Value, out double result))
                    {
                        tree.SetVar(sCell, result);
                    }

                    //if we cant convert the value to a string then the text contains anomalies.
                    //if the cell text contains something like "Hi+2" it is invalid, display "#REF!"
                    //HOWEVER we want to display the variable text if we write "=A1" where A1 holds just a character string (like hi)
                    else
                    {
                        //check if there are multiple variables
                        //obviously you can't add hi+world, so the variable count must always be 1.
                        if (count == 1)
                        {
                            //check if each char in the variable is not a digit
                            foreach (char c in dep.Value)
                            {
                                flag = 0;
                                if (!char.IsDigit(c))   //if no digits
                                {
                                    flag = 1;
                                }
                            }

                            //if all the chars are not digits, we need to make sure there is no arithmetic going on
                            //i.e cant add "hi+hello"
                            if (flag == 1)
                            {
                                char[] chars = { '+', '-', '*', '/', '(', ')' };
                                int x = cell.Text.IndexOfAny(chars);

                                if (x == -1) //not found chars
                                {
                                    cell.Value = dep.Value;
                                    return;
                                }
                            }
                        }

                        cell.Value = "#REF!";
                        //cell.Value = dep.Value;
                        return;
                    }
                }

                //convert value of tree to a string
                cell.Value = tree.Eval().ToString();
            }
            catch
            {
                throw;
            }
        }

        //return a cell at a particular given index
        public Cell GetCell(int rowIndex, int colIndex)
        {
            if (CellsArray[rowIndex, colIndex] != null)
            {
                return CellsArray[rowIndex, colIndex];
            }
            else
            {
                return null;
            }
        }


        //remove a dependency, will call only when a cell is edited 
        private void RemoveDependency(Cell cell)
        {
            if (dependencies.ContainsKey(cell))
                dependencies[cell].Clear();
            dependencies.Remove(cell);
        }

        //demo for part 1
        public void Demo()
        {
            int i = 0;
            Random rand = new Random();

            //randomize cells containing message
            for (i = 0; i < 50; i++)
            {
                int RandRowIndex = rand.Next(0, 50);
                int RandColIndex = rand.Next(2, 26);

                CellsArray[RandRowIndex, RandColIndex].Text = "HelloWorld";

            }
            //set all B1 rows 
            for (i = 0; i < 50; i++)
            {
                CellsArray[i, 1].Text = ("This is cell B" + (i + 1));
            }

            //set all rows in Ai to equal the value in Bi
            //no longer works, see EvaluateCell
            for (i = 0; i < 50; i++)
            {
                CellsArray[i, 0].Text = ("=B" + (i + 1));
            }
        }

        public void save(int newNumRows, int newNumCols, Stream s)
        {
            XDocument d = new XDocument();
            XElement e = new XElement(new XElement("Root"));
            Cell cell;
            for (int row = 0; row < newNumRows; row++)
            {
                for (int col = 0; col < newNumCols; col++)
                {
                    cell = CellsArray[row, col];
                    //cell at this location
                    if (cell.Text != null) //if the cell was not edited
                    {
                        e.Add(
                         new XElement("Cell", new XElement("Name", cell.Name),
                             new XElement("Text", cell.Text), new XElement("Value", cell.Value),
                                new XElement("Row", row), new XElement("Col", col)));
                    }
                }
            }
            d.Add(e);
            d.Save(s);
        }

        public void load(int newNumRows, int newNumCols, StreamReader s)
        {
            //var doc = XDocument.Load(s);

            //string result = (string)doc.Root.Element("Name");

            //s.BaseStream.Position = 0;
            //XmlReader reader = XmlReader.Create(s);
            //while(doc.Read())
            //{
            //    result = (string)doc.Root.Element("Name");

            //}


            Cell temp;
            int row = 0, col = 0;
            string str = "";

            var doc = XDocument.Load(s);
            foreach (XElement root in doc.Nodes())
            {
                foreach (XElement cell in root.Nodes())
                {
                    foreach (XElement val in cell.Nodes())
                    {
                        //if (val.Name.LocalName == "Name")
                        //{
                        //    //val.ToString();
                        //    ////r = Int32.Parse(val.Value);
                        //    //temp = GetCell(val);
                        //    var value = val.InnerText();

                        //}
                        if(val.Name.LocalName == "Row")
                        {
                            row = Int32.Parse(val.Value);
                        }
                        else if (val.Name.LocalName == "Col")
                        {
                            col = Int32.Parse(val.Value);
                        }
                        else if (val.Name.LocalName == "Text")
                        {
                            str = val.Value;
                        }
                    }
                    CellsArray[row, col].Text = str;
                }
            }

            //cell = CellsArray[row, col];




        }
    }
}
