//Sophia Schuur
//321 Homework 3 - Crandall
//9/20/2018
//Build a WinForms program that can save and load text files, and loads
//iterative fibonaccis of BigIntegers


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

//#include "Fibonacci.h"

namespace Homework_3___WinForms_Notepad
{
    
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
