using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace _1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadLines("input.txt").Select(x=>Int64.Parse(x)).ToList();
            input.Sort();
            input.Insert(0,0); // outlet
            var differences = input.Select((x,i)=> i + 1 < input.Count ? input[i+1]-x : 3);
            var num1 = differences.Where(x=>x==1).Count();
            var num3 = differences.Where(x=>x==3).Count();
            Console.WriteLine(num1*num3);
            Console.Read();
        }
    }
}
