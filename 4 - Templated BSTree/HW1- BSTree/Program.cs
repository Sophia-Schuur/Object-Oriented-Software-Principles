//Sophia Schuur
//CptS 321
//Crandall
//Homework 4 - Redesign a BSTree into a templated, generic version
//9/24/2018


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1__BSTree
{
    class Program
    {
        static void Main(string[] args)
        {
            BSTree<int> bst = new BSTree<int>();

            //prompt the user for integers
            Console.WriteLine("Enter some integers between 0-100 with a space in between and then press enter: ");
            var integers= Console.ReadLine();
                      
            //parse the string
            string[] Oldsplit = integers.Split(null);

            //check for duplicates
            IEnumerable<string>NewSplit = Oldsplit.Distinct();

            foreach (string s in NewSplit)
            {                            
                    //remove all leading or trailing white space         
                    if (s.Trim() != "")
                    {                       
                        //convert strings to integers
                        int data = Int32.Parse(s);
                        bst.Insert(data);
                    }                                
            }

            //display a new line at end
            Console.WriteLine(); 

            //Display tree contents in order
            Console.Write(" Tree contents in order: ");
            bst.InOrder();

            //Display tree contents pre order
            Console.Write("\n Tree contents pre order: ");
            bst.PreOrder();

            //Display tree contents post order
            Console.Write("\n Tree contents post order: ");
            bst.PostOrder();

            Console.WriteLine();
            Console.WriteLine("\nTree Statistics: ");

            //print number of items stats:
            int size = 0;
            size = bst.count();
            Console.WriteLine($"   Number of nodes: {size}");

            //print depth of tree
            int depth = 0;
            depth = bst.depth();
            Console.WriteLine($"   Number of levels: {depth}");

            //print min num of levels
            double minLevels = 0.0;
            minLevels = Math.Log((size + 1), 2);

            //cant have half a node so we round up
            minLevels = Math.Ceiling(minLevels);
            Console.WriteLine($"   Minimum number of levels that a tree with {size} nodes could have = {minLevels}");

        }
    }
}

