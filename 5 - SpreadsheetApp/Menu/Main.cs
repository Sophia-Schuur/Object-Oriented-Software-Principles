/*Sophia Schuur
  CptS 321
  Homework 7
  Start: 10/29/2018
  Current: 12/3/2018

  A mini excel-like spreadsheet app that can handle operator precedence, saving and loading via XML, and some error checking. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CptS321;

namespace ExpTreeMain
{
    class ExpTreeMain
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            ExpTree tree = new ExpTree("(A1+(10-b)/1)*2");
            int run = 0;
            string option = null;   

            Console.WriteLine("ExpTree Menu - SpreadsheetApp");
            while (run != 4)
            {
                Console.WriteLine($"\n  Menu (current expression: {tree.GetExpression()})");
                menu.DisplayOptions();

                option = Console.ReadLine();
                run = menu.ChooseOption(option, ref tree);                         
            }
        }
    }
}
