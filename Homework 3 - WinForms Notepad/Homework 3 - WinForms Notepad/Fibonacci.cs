
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework_3___WinForms_Notepad
{
	public class Fibonacci : TextReader
	{
		private int max = 0;
		private BigInteger curr = 0;

		//CONSTRUCTOR, SET THE MAX TO MAX
		public Fibonacci(int max)
		{
			this.max = max;
		}

		//Override for read line
		public override string ReadLine()
		{
			if (curr > max)
			{
				return null;
			}

			BigInteger fibNumber = fib(curr);
			StringBuilder newString = new StringBuilder();

			newString.Append((curr + 1).ToString());
			newString.Append(":  ");
			newString.Append(fibNumber.ToString());
			curr++;
			return newString.ToString();
		}

		//iterative fibonacci function
		private BigInteger fib(BigInteger n)
		{
			//if(n == 0)
			//{
			//    return 0;
			//}
			//if(n == 1)
			//{
			//    return 1;
			//}
			//return fib(n - 1) + fib(n - 2);

			BigInteger a = 0;
			BigInteger b = 1;

			for (int i = 0; i < n; i++)
			{
				BigInteger temp = a;
				a = b;
				b = temp + b;
			}
			return a;
		}
	}
}
