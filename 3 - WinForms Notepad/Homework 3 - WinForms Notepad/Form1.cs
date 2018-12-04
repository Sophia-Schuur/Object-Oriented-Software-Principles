//Sophia Schuur
//11519303

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework_3___WinForms_Notepad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadText(TextReader sr)
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                textBox1.Text += line + "\r\n";
            }
        }
        private void loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Text File | *.txt";
            open.ShowDialog();

            StreamReader sr = new StreamReader(open.FileName);
            LoadText(sr);
        }

        private void loadFibonacciNumbersfirst50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadText(new Fibonacci(49));
        }

        private void loadFibonacciNumbersfirst100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadText(new Fibonacci(99));
        }

        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            //save.ShowDialog();
            save.Filter = "Text File | *.txt";
            if(save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (Stream s = File.Open(save.FileName + ".txt", FileMode.CreateNew))
                using (StreamWriter sw = new StreamWriter(s))
                {
                    sw.Write(textBox1.Text);
                }
            }
        }
    }  
}
