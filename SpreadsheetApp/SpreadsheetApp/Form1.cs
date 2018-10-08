using CptS321;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetApp
{
    public partial class Form1 : Form
    {
        Spreadsheet spreadsheet = new Spreadsheet(50, 26);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            spreadsheet.CellPropertyChanged += CellPropertyChanged;
            dataGridView1.Columns.Clear();
            for (char i = 'A'; i <= 'Z'; i++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.Name = i.ToString();
                dataGridView1.Columns.Add(column);
            }
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add(50);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString();
            }
        }

        private void CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell cell = (Cell)sender;
            if (cell != null && e.PropertyName == "Value")
            {
                dataGridView1.Rows[cell.GetRowIndex()].Cells[cell.GetColIndex()].Value = cell.GetValue();

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            spreadsheet.Demo();
        }
    }
}
