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

            var possiblebuglines = input.Select((line, index) => new { Line = line, Index = index}).Where(x=>x.Line.StartsWith("nop") || x.Line.StartsWith("jmp")).Select(x => new Tuple<int,string>(x.Index,x.Line));
            var acc = 0;
            foreach(var bugline in possiblebuglines){
                var valid = false;
                var visitedlines = new List<int>();
                var currentline = 0;
                acc = 0;

                // fix the line
                var fixedinput = (string[])input.Clone();
                fixedinput[bugline.Item1] = fixedinput[bugline.Item1].Replace("nop","X").Replace("jmp","Y").Replace("X","jmp").Replace("Y","nop");

                while(true){
                    if(visitedlines.Contains(currentline)) break;

                    var line = fixedinput[currentline];
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
                    if(currentline == fixedinput.Length){
                        valid=true;
                        break;
                    }
                }

                if(valid) break;
            }


         
            Console.WriteLine(acc);
            Console.Read();
        }      
    }
}
