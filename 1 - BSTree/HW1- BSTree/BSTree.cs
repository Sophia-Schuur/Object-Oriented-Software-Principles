using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1__BSTree
{
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
            if (root == null)
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
