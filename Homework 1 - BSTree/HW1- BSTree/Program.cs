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


    class Node
    {
        public int value;
        public Node left;
        public Node right;       
    }

    class BSTree
    {
        public int size;
        public Node insert(Node root, int insertData)
        {
            if (root == null)
            {
                root = new Node();
                root.value = insertData;
            }
            else if (insertData < root.value)
            {
                root.left = insert(root.left, insertData);
            }
            else
            {
                root.right = insert(root.right, insertData);
            }

            return root;
        }

        public void PrintInOrder(Node root)
        {
            if (root != null)
            {
                PrintInOrder(root.left);
                Console.Write(root.value + " ");
                PrintInOrder(root.right);
            }
        }
        public int depth(Node root) //thank you to geeksforgeeks.com!!
        {
           if(root == null)
            {
                return 0;
            }
            else
            {
                int lDepth = depth(root.left);
                int rDepth = depth(root.right);
                if (lDepth > rDepth)
                {
                    return (lDepth + 1);
                }
                else
                {
                    return (rDepth + 1);
                }
            }
        }

        public int count(Node root)
        {           
            if (root != null)
            {
                size++;
                count(root.left);
                count(root.right);
            }
            return size;
        }
    }
}

