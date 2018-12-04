using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework_2___WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<int> list = new List<int> ();
            Random random = new Random ();

            for(int i = 0; i < 10000; i++)
            {
                list.Add(random.Next(20000));
            }
            Dictionary<int, int> dictionary = new Dictionary<int, int>();

            //NUMBER 1

            for(int i = 0; i < 10000; i++)
            {
                if (!dictionary.ContainsKey(list[i]))
                {
                    dictionary.Add(list[i], list[i]);
                }             
            }
            textBox1.Text += Environment.NewLine + "Test 1: HashSet method: ";
            textBox1.Text += dictionary.Count() + " unique numbers";
            textBox1.Text += Environment.NewLine + "Time complexity is O(n), as I go to every element once and perform a single action. ";


            //NUMBER 2
            int count = 0;
            bool isDuplicate = false;

            for(int i = 0; i < 10000; i++)
            {
                for(int j = i+1; j < 10000; j++)
                {
                    if(list[i] == list[j])
                    {
                        isDuplicate = true;
                    }
                }

                if(isDuplicate == false)
                {
                    count++;
                }
                isDuplicate = false;
            }
            textBox1.Text += Environment.NewLine + "Test 2: O(1) storage method: ";
            textBox1.Text += count + " unique numbers";

            //NUMBER 3

            list.Sort();
            int newCount = 1;
            for (int i = 0; i < 10000 - 1; i++)
            {
                if(list[i] != list[i+1])
                {
                    newCount++;
                }
            }
            textBox1.Text += Environment.NewLine + "Test 3: Sorted method: ";
            textBox1.Text += newCount + " unique numbers";
        }
    }
}
