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
            
            var busses = input[1].Split(',').Select((x,i) => Tuple.Create(i,x))
            .Where(x=>x.Item2!="x").Select(x=>new { index = x.Item1, number = int.Parse(x.Item2)})
            .OrderByDescending(x=>x.number);

            var currenttimestamp = 100642633673337;
            //var currenttimestamp = 0;
            var i = 0;
            while(true){
                var ismatch  = true;

                // should be faster as is check most difficault to device first
                foreach(var bus in busses){
                    if((currenttimestamp + bus.index) % bus.number != 0){
                        ismatch = false;

                        // if isnt first bus in list (largest bus number)
                        // skip ahead to next time largest bus number is zero!
                        //currenttimestamp = currenttimestamp + busses.
                        if(bus.index != busses.First().index){
                            currenttimestamp = currenttimestamp + busses.First().number;
                        }
                        else{
                            currenttimestamp++;
                        }
                        break;
                    }
                }
                if(ismatch){
                    Console.WriteLine("anser: " + currenttimestamp);
                    Console.Read();
                }
                if(i % 1000000 == 0)  Console.WriteLine("passing: " + currenttimestamp);
                i++;
            }
        }
    }
}
