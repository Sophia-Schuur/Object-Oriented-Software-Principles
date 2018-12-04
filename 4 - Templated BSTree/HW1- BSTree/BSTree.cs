using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1__BSTree
{
    


    //BSTREE CLASS
    class BSTree<T> : BinTree<T> where T : IComparable<T>
    {
        private Node<T> root = null;
        public int size;

        //Contains override
        public override bool Contains<T>(T val)
        {
            Node<T> cur = root as Node<T>;
            Node<T> temp = new Node<T>(val);
            while (cur != null)
            {
                if (temp == cur)
                {
                    return true;
                }
                else if (temp < cur)
                {
                    cur = cur.left;
                }
                else
                {
                    cur = cur.right;
                }
            }
            return false;
        }

        //Insert() override. Sets the value for the new node to be compared with in insertInternal
        public override void Insert(T val)
        {
            Node<T> newNode = new Node<T>(val);
            insertInternal(ref root, ref newNode);
        }

        //Does the actual inserting
        private void insertInternal(ref Node<T> newRoot, ref Node<T> newNode)
        {
            if (newRoot == null)
            {
                newRoot = newNode;
            }
            else if (newNode < newRoot)
            {
                insertInternal(ref newRoot.left, ref newNode);
            }
            else if (newNode > newRoot)
            {
                insertInternal(ref newRoot.right, ref newNode);
            }
        }

        
        //Print InOrder override
        public override void InOrder()
        {
            PrintInOrder(ref root);
        }
        public void PrintInOrder(ref Node<T> root)
        {
            if (root != null)
            {
                PrintInOrder(ref root.left);
                Console.Write(root.getValue() + " ");
                PrintInOrder(ref root.right);
            }
        }

        //Print PreOrder override
        public override void PreOrder()
        {
            PrintPreOrder(root);
        }
        public void PrintPreOrder(Node<T> root)
        {
            if (root != null)
            {
                Console.Write(root.getValue() + " ");
                PrintPreOrder(root.left);
                PrintPreOrder(root.right);
            }
        }

        //Print PostOrder override
        public override void PostOrder()
        {
            PrintPostOrder(root);
        }
        public void PrintPostOrder(Node<T> root)
        {
            if (root != null)
            {
                PrintPostOrder(root.left);
                PrintPostOrder(root.right);
                Console.Write(root.getValue() + " ");
            }
        }


        //public depth function
        public int depth()
        {
            int depth = 0;
            depth = depthInternal(root);
            return depth;
        }

        //does the actual depth count
        private int depthInternal(Node<T> root) //thank you to geeksforgeeks.com!!
        {
            if (root == null)
            {
                return 0;
            }
            else
            {
                int lDepth = depthInternal(root.left);
                int rDepth = depthInternal(root.right);
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

        //does the actual size counting
        private int countInternal(Node<T> root)
        {
            if (root != null)
            {
                size++;
                countInternal(root.left);
                countInternal(root.right);
            }
            return size;
        }

        //public count function
        public int count()
        {
            size = countInternal(root);
            return size;
        }
    }

}
