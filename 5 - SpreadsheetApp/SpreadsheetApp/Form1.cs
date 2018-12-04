/*Sophia Schuur
  CptS 321
  Start: 10/29/2018
  Current: 12/3/2018

  A mini excel-like spreadsheet app that can handle operator precedence, saving and loading via XML, and some error checking. */



using CptS321;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SpreadsheetApp
{
    public partial class Form1 : Form
    {
        private Spreadsheet spreadsheet;
        private int curRow = 0;
        private int curCol = 0;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            spreadsheet = new Spreadsheet(50, 26);

            //subscribe to events
            spreadsheet.CellPropertyChanged += CellPropertyChanged;
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            dataGridView1.CellBeginEdit += dataGridView1_CellBeginEdit;

            dataGridView1.CellEnter += CellEnter; 
            textBox1.KeyDown += OnKeyDownHandler;
            textBox1.Leave += LeaveHandler;
            
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            for (char c = 'A'; c <= 'Z'; c++)
            {
                dataGridView1.Columns.Add(c.ToString(), c.ToString());
            }

            for (int i = 1; i <= 50; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i - 1].HeaderCell.Value = i.ToString();
            }       
        }

        //handler when user selects cell. finds clicked cell
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int row = e.RowIndex;
            int column = e.ColumnIndex;

            Cell cell = spreadsheet.GetCell(row, column + 1);

            dataGridView1.Rows[row].Cells[column].Value = cell.Text;
        }

        //handler when user is done selecting cell. sets cell text and valyue
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            string text;

            Cell cell = spreadsheet.GetCell(curRow, curCol);

            if (dataGridView1.Rows[curRow].Cells[curCol].Value == null)
            {
                text = "";
            }
            else
            {
                text = dataGridView1.Rows[curRow].Cells[curCol].Value.ToString();
            }

            cell.Text = text;
            dataGridView1.Rows[curRow].Cells[curCol].Value = cell.Value;
        }

        private void CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell c = (Cell)sender;
            dataGridView1.Rows[c.RowIndex].Cells[c.ColumnIndex].Value = c.Value;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //demo button. Press it!
        private void button1_Click(object sender, EventArgs e)
        {
            spreadsheet.Demo();
        }

        //handler for textbox when user clicks cell. puts cell text into the textbox
        private void CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Enabled = true;
            textBox1.Text = spreadsheet.GetCell(e.RowIndex, e.ColumnIndex ).Text;
            curRow = e.RowIndex;
            curCol = e.ColumnIndex;
        }

        //when a user presses enter (return), leave the text box.
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                LeaveHandler(sender, new EventArgs());
            }
        }

        //leave textbox and put textbox text into the actual datagridview (not just cells)
        private void LeaveHandler(object sender, EventArgs e)
        {
            dataGridView1.Rows[curRow].Cells[curCol].Value = textBox1.Text;
            dataGridView1_CellEndEdit(dataGridView1, new DataGridViewCellEventArgs(curCol, curRow));
            textBox1.Enabled = false;
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        
        
        //uses streamwriter to save the xml of the spreadsheet to a text file. won't overwrite an existing file..
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            XDocument srcTree = new XDocument();
            SaveFileDialog save = new SaveFileDialog();
            //save.ShowDialog();
            save.Filter = "Text File | *.txt";
            if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (Stream s = File.Open(save.FileName, FileMode.CreateNew))
                using (StreamWriter sw = new StreamWriter(s))
                {
                    //XDocument doc = new XDocument(new XComment("Welcome to Sophie's Spreadsheet!"),
                    //    new XElement("SpreadsheetRoot", 
                    //    from el in srcTree.Element("SpreadsheetRoot).Elements() select el);

                    //sw.Write("hi");
                    spreadsheet.save(50, 26, s);
                }
            }
        }

        //loads xml from text file
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        spreadsheet.load(50, 26, reader);
                        //fileContent = reader.ReadToEnd();
                    }
                }
            }
            //MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButtons.OK);
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        // This utility method assigns the value of a ToolStripItem
        // control's Text property to the Text property of the 
        // ToolStripStatusLabel.
        private void UpdateStatus(ToolStripItem item)
        {
            if (item != null)
            {
                string msg = String.Format("{0} selected", item.Text);
                this.statusStrip1.Items[0].Text = msg;
            }
        }

        // This method is the DropDownItemClicked event handler.
        // It passes the ClickedItem object to a utility method
        // called UpdateStatus, which updates the text displayed 
        // in the StatusStrip control.
        private void fileToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.UpdateStatus(e.ClickedItem);
        }

        //brings up a little about box
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" 321 Spreadsheet App\n 5.0.0 \n Sophia Schuur" +
                "\n sophia.schuur@wsu.edu \n Copyright © 2018 WSU" +
                "\n All Rights Reserved\nThis program comes with absolutely no warranty", "\nSophie's Spreadsheet\n");
        }

        private void helpToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.UpdateStatus(e.ClickedItem);
        }
    }
}
