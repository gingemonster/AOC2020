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
            var preamblelength = 25;

            for(var i=preamblelength;i<input.Length;i++){
                // get previous pre-amble numbers
                var preamble = input.Skip(i-preamblelength).Take(preamblelength).ToList();
                if(!IsValid(preamble, input[i])){
                    Console.WriteLine(input[i]);
                    Console.Read();
                }
            }
        }

        private static bool IsValid(List<long> preamble, long totest){
            // find two numbers in preamble that adds up to totest
            for(var i=0;i<preamble.Count;i++){
                if ((preamble.Any(x=>x == totest - preamble[i]) && preamble.IndexOf(totest - preamble[i]) != i) || preamble.Count(x=>x==totest - preamble[i]) > 1){
                    return true;
                }
            }
            return false;
        }
    }
}
