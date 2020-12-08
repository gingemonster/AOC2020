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
            var visitedlines = new List<int>();
            var currentline = 0;
            var acc = 0;

            while(true){
                if(visitedlines.Contains(currentline)) break;

                var line = input[currentline];
                var parts = Regex.Match(line,"^(\\w*) (\\W)(\\d*)$").Groups;
                visitedlines.Add(currentline);
                
                switch(parts[1].Value){
                    case "acc":
                        acc += parts[2].Value == "+" ? int.Parse(parts[3].Value) : int.Parse(parts[3].Value) * -1;
                        currentline++;
                        break;
                    case "jmp":
                        currentline += parts[2].Value == "+" ? int.Parse(parts[3].Value) : int.Parse(parts[3].Value) * -1;
                        break;
                    default:
                        currentline++;
                        break;
                }
            }
         
            Console.WriteLine(acc);
            Console.Read();
        }      
    }
}
