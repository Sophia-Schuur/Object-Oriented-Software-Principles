//Sophia Schuur
//CptS 321
//Crandall
//Homework 1 - Design a BSTree and various functions using c#
//8/29/2018

using System;
using System.Collections.Generic;
using System.Linq;


namespace HW1__BSTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Node root = null;
            BSTree bst = new BSTree();

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
                        root = bst.insert(root, data);
                    }                                
            }

            //display a new line at end
            Console.WriteLine(); 

            //Display tree contents in sorted order
            Console.Write("Tree contents: ");
            bst.PrintInOrder(root);

            Console.WriteLine();
            Console.WriteLine("Tree Statistics: ");

            //print number of items stats:
            int size = 0;
            size = bst.count(root);
            Console.WriteLine($"   Number of nodes: {size}");

            //print depth of tree
            int depth = 0;
            depth = bst.depth(root);
            Console.WriteLine($"   Number of levels: {depth}");

            //print min num of levels
            double minLevels = 0.0;
            minLevels = Math.Log((size + 1), 2);

            //cant have half a node so we round up
            minLevels = Math.Ceiling(minLevels);
            Console.WriteLine($"Minimum number of levels that a tree with {size} nodes could have = {minLevels}");
        }
    }
}

