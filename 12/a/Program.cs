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
            
            long x = 0;
            long y = 0;
            long direction = 90;

            foreach(var instruction in input){

                var move = instruction.Item1 switch
                {
                    "N" => new Tuple<long,long>(0,instruction.Item2),
                    "F" when direction==0 => new Tuple<long,long>(0,instruction.Item2),
                    "S" => new Tuple<long,long>(0,-instruction.Item2),
                    "F" when direction==180 => new Tuple<long,long>(0,-instruction.Item2),
                    "E" => new Tuple<long,long>(instruction.Item2,0),
                    "F" when direction==90 => new Tuple<long,long>(instruction.Item2,0),
                    "W" => new Tuple<long,long>(-instruction.Item2,0),
                    "F" when direction==270 => new Tuple<long,long>(-instruction.Item2,0),
                    _ => new Tuple<long,long>(0,0)
                };

                if(instruction.Item1=="L"){
                    direction -= instruction.Item2;
                }
                if(instruction.Item1=="R"){
                    direction += instruction.Item2;
                }
                direction = direction % 360;

                if (direction < 0)
                {
                    direction += 360;
                }

                // process move
                x += move.Item1;
                y += move.Item2;              
            }

            Console.WriteLine($"{Math.Abs(x) + Math.Abs(y)}");
            Console.Read();
        }
    }
}
