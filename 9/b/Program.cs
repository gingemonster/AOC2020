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
            var input = File.ReadLines("input.txt").Select(x=>Int64.Parse(x)).ToArray();
            var invalidvalue = 57195069;

            for(var i=0;i<input.Length;i++){
                var sum = input[i];
                var ii = i + 1;

                // going to ignore checking for going over end of array as should have
                // found solution by then
                while(sum<invalidvalue){
                    sum += input[ii];
                    if(sum == invalidvalue){
                        // get the range amd sort smallest to largest
                        var rangesorted = input.Skip(i).Take(ii - i + 1).OrderBy(x=>x);
                        // sum smallest and largest
                        Console.WriteLine((rangesorted.First() + rangesorted.Last()));
                        Console.Read();
                    }
                    if(sum > invalidvalue){
                        break;
                    }
                    else{
                        ii++;
                    }
                }
            }
        }
    }
}
