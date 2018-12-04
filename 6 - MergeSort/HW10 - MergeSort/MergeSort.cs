using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace HW10___MergeSort
{
    class MergeSort
    {
        public MergeSort() {}
       

        //merge two parts of array - credit to Geeks4Geeks.com
        private void merge(int[] arr, int low, int mid, int high)
        {
            //get sizes
            int n1 = mid - low + 1;
            int n2 = high - mid;
            int i = 0;
            int j = 0;

            //some temp arrays
            int[] L = new int[n1];
            int[] R = new int[n2];

            //copy data from given array into temp arrays
            for (i = 0; i < n1; ++i)
            {
                L[i] = arr[low + i];
            }
            for (j = 0; j < n2; ++j)
            {
                R[j] = arr[mid + 1 + j];
            }

            i = 0;
            j = 0;

            //merge subarrays
            int k = low;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }

            //copy remaining data
            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            }
        }

        //sort array using merge(), again credit to geeks4geeks.com
        public void normalSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                //find midpoint 
                int mid = (low + high) / 2;

                //sort first and second halves 
                normalSort(arr, low, mid);
                normalSort(arr, mid + 1, high);

                //merge the sorted halves 
                merge(arr, low, mid, high);
            }
        }

        //threaded sort
        public void threadedSort(int[] arr, int low, int high, int depthRemaining)
        {
            if (low < high)
            {
                int mid = (low + high) / 2;

                if (depthRemaining > 0)
                {
                    Parallel.Invoke(() =>
                    {
                        threadedSort(arr, low, mid, depthRemaining - 1);
                    },

                    () =>
                    {
                        threadedSort(arr, mid + 1, high, 0);
                    }
                );
                }
                else
                {
                    threadedSort(arr, low, mid, 0);
                    threadedSort(arr, mid + 1, high, 0);
                }

                merge(arr, low, mid, high);
            }
        }

        //perform evaulations on any size arrays
        public void testSort(int arrSize)
        {
            Stopwatch stopwatch = new Stopwatch();
            MergeSort mergeSort = new MergeSort();
            MyArray myArray = new MyArray();
 
            Console.WriteLine($"\n - - - Size {arrSize} - - -\n");
            int[] arr = new int[arrSize];
            arr = myArray.generateRandom(arrSize);
            int[] clone = (int[])arr.Clone();


            //NORMAL SORT TIME
            stopwatch.Start();
            long ms1 = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            mergeSort.normalSort(arr, 0, arr.Length - 1);

            long ms2 = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            stopwatch.Stop();   
            
            Console.WriteLine("   Normal Sort time elapsed: {0}", stopwatch.Elapsed);
            //Console.WriteLine($"   Normal Sort time (ms):   {ms2 - ms1}");

            //THREADING SORT TIME
            stopwatch.Start();
            long ms3 = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            mergeSort.threadedSort(clone, 0, clone.Length - 1, 2);

            long ms4 = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            stopwatch.Stop();

            Console.WriteLine("   Merge sort time elapsed:  {0}", stopwatch.Elapsed);
            //Console.WriteLine($"   Merge Sort time (ms):    {ms4 - ms3}");
        }
       
    }
}
