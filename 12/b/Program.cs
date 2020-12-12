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
            var input = File.ReadLines("input.txt").Select(x=> new Tuple<string,long>(x.Substring(0,1),Int64.Parse(x.Substring(1)))).ToArray();
            
            long shipx = 0;
            long shipy = 0;
            long wpx = 10;
            long wpy = 1;

            foreach(var instruction in input){

                var move = instruction.Item1 switch
                {
                    "N" => new Tuple<long,long>(0,instruction.Item2),
                    "S" => new Tuple<long,long>(0,-instruction.Item2),
                    "E" => new Tuple<long,long>(instruction.Item2,0),
                    "W" => new Tuple<long,long>(-instruction.Item2,0),
                    "F" => new Tuple<long,long>(instruction.Item2 * wpx, instruction.Item2 * wpy),
                    _ => new Tuple<long,long>(0,0)
                };

                var wpcx = wpx;
                var wpcy = wpy;
                if(instruction.Item1=="R"){
                    if(instruction.Item2==90) {
                        // was was north becomes east, what was east becomes south
                        wpx = wpcy;
                        wpy = -wpcx;
                    }
                    else if(instruction.Item2==180){
                        wpx = -wpcx;
                        wpy = -wpcy;
                    }
                    else if(instruction.Item2==270){
                        wpx = -wpcy;
                        wpy = wpcx;
                    }
                }
                else if(instruction.Item1=="L"){
                    if(instruction.Item2==270) {
                        wpx = wpcy;
                        wpy = -wpcx;
                    }
                    else if(instruction.Item2==180){
                        wpx = -wpcx;
                        wpy = -wpcy;
                    }
                    else if(instruction.Item2==90){
                        wpx = -wpcy;
                        wpy = wpcx;
                    }
                }
               
                // process move
                if(instruction.Item1=="F"){
                    shipx += move.Item1;
                    shipy += move.Item2;  
                }
                else{
                    wpx += move.Item1;
                    wpy += move.Item2;  
                }
            
            }

            Console.WriteLine($"{Math.Abs(shipx) + Math.Abs(shipy)}");
            //Console.Read();
        }
    }
}
