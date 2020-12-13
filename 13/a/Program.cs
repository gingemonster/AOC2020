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
            var input = File.ReadLines("input.txt").ToArray();
            
            var arrival = int.Parse(input[0]);
            var busses = input[1].Split(',');

            while(true){
                var busarriving = busses.Where(x => x != "x" && arrival % int.Parse(x) == 0).FirstOrDefault();
                if(busarriving!=null){
                    Console.WriteLine(int.Parse(busarriving) * (arrival - int.Parse(input[0])));
                    Console.Read();
                }
                arrival++;
            }
        }
    }
}
