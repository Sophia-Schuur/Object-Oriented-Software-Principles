using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10___MergeSort
{
    class MyArray
    {
        public MyArray() { }

        //return random array of integers
        public int[] generateRandom(int size)
        {
            int Min = 0;
            int Max = Int32.MaxValue;
            //int Max = 1300;

            int[] arr = new int[size];

            Random randNum = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = randNum.Next(Min, Max);
            }
            //print array
            //Console.WriteLine(" - Unsorted - ");
            //printArray(arr);

            return arr;
        }

        //print all elements in an array
        //used this just for testing
        public void printArray(int[] arr)
        {
            foreach (var item in arr)
            {
                Console.Write(" " + item.ToString() + " ");
            }
            Console.WriteLine("");
        }
    }
}
