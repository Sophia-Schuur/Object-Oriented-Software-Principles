using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1__BSTree
{
    public abstract class BinTree<T>
    {
        // Insert new item of type T
        public abstract void Insert(T val);

        // Returns true if item is in tree
        public abstract bool Contains<T>(T val);

        // Print elements in tree 
        public abstract void InOrder();       
        public abstract void PreOrder();
        public abstract void PostOrder();
    }
}
