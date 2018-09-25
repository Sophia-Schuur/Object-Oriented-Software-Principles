using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1__BSTree
{
    //NODE CLASS
    class Node<T>
    {
        private T value;
        public Node<T> left = null;
        public Node<T> right = null;

        //Constructor
        public Node()
        { }

        //Constructor with value argument
        public Node(T newValue)
        {
            value = newValue;
            left = null;
            right = null;
        }

        //Constructor with all arguments
        public Node(T newValue, Node<T> newLeft, Node<T> newRight)
        {
            value = newValue;
            left = newLeft;
            right = newRight;
        }

        //Getter
        public T getValue()
        {
            return value;
        }

        //Operator overrides - Thank you to Stack Overflow for help!
        public static bool operator ==(Node<T> a, Node<T> b)
        {
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            if ((object)a == null || (object)b == null)
            {
                return false;
            }

            return Comparer<T>.Default.Compare(a.value, b.value) == 0;
        }

        public static Boolean operator !=(Node<T> a, Node<T> b)
        {
            return !(a == b);
        }

        public static bool operator >(Node<T> a, Node<T> b)
        {
            return Comparer<T>.Default.Compare(a.value, b.value) > 0;
        }

        public static bool operator <(Node<T> a, Node<T> b)
        {
            return Comparer<T>.Default.Compare(a.value, b.value) < 0;
        }

        public static bool operator >=(Node<T> a, Node<T> b)
        {
            return Comparer<T>.Default.Compare(a.value, b.value) >= 0;
        }

        public static bool operator <=(Node<T> a, Node<T> b)
        {
            return Comparer<T>.Default.Compare(a.value, b.value) <= 0;
        }
    }
}
