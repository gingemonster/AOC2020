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
            
            var busses = input[1].Split(',');

            var currenttimestamp = 100052323673337;

            while(true){
                var offsettimestamp = currenttimestamp;
                var ismatch  = true;
                foreach(var bus in busses){
                    if(bus != "x" && offsettimestamp % int.Parse(bus) != 0){
                        ismatch = false;
                        break;
                    }
                    offsettimestamp++;
                }
                if(ismatch){
                    Console.WriteLine("anser: " + currenttimestamp);
                    Console.Read();
                }
                if(currenttimestamp % 10000000 == 0)  Console.WriteLine("passingo: " + currenttimestamp);
                currenttimestamp++;
            }
        }
    }
}
