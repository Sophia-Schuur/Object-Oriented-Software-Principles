using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CptS321
{
    class Menu
    {
        public Menu() { }

        public void DisplayOptions()
        {
            Console.WriteLine("\t1 = Enter a new expression");
            Console.WriteLine("\t2 = Set a variable value");
            Console.WriteLine("\t3 = Evaluate tree");
            Console.WriteLine("\t4 = Quit");
        }

        public int ChooseOption(string option, ref ExpTree tree)
        {
            string temp = null;
            string varName = null;
            double varValue = 0.0;

            switch(Convert.ToInt32(option))
            {
                case 1://Enter a new expression
                    Console.Write("  Enter new expression: ");
                    temp = Console.ReadLine();
                    tree = new ExpTree(temp);
                    break;

                case 2: // Set a variable value
                    Console.Write("  Enter variable name: ");
                    varName = Console.ReadLine();
                    Console.Write("  Enter variable value: ");
                    temp = Console.ReadLine();
                    varValue = Convert.ToDouble(temp);

                    tree.SetVar(varName, varValue);
                    break;

                case 3: //Evaluate tree
                    Console.WriteLine($"  Tree evaulation: {tree.Eval()}");
                    break;

                case 4: //Quit
                    return 4;
                default:
                    Console.WriteLine("  ERROR: Unknown command.");
                    break;

            }
           
            return 0;
        }
    }
}
