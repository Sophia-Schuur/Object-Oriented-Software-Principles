//Sophia Schuur
//11519303
//CptS 321 Crandall
//11/27/2018
//Evaulate differences in time it takes to execute a normal and threaded mergeSort.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10___MergeSort
{
    class main
    {
        static void Main(string[] args)
        {
            MergeSort mergeSort = new MergeSort();

            Console.WriteLine("Array sizes under test: [8, 64, 256, 1024]");
            Console.WriteLine("The stopwatch made more sense than unix time.");

            //SIZE 8 SORT TIME
            mergeSort.testSort(8);

            //SIZE 64 SORT TIME
            mergeSort.testSort(64);

            //SIZE 256 SORT TIME
            mergeSort.testSort(256);

            //SIZE 1024 SORT TIME
            mergeSort.testSort(1024);
        }
    }
}
