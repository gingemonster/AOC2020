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
            
            var memory = new Dictionary<string,Int64>();
            var currentmask = "";

            foreach(var line in input){
                if(line.StartsWith("mask")){
                    currentmask = line.Split(" = ")[1];
                }
                else{
                    var match = Regex.Match(line, "^mem\\[(\\d*)\\] = (\\d*)$");
                    var address =match.Groups[1].Value;
                    var value = match.Groups[2].Value;

                    // apply mask and add to memory
                    memory[address] = Mask(value, currentmask);
                }
            }

            Console.WriteLine(memory.Where(x=>x.Value!=0).Sum(x=>x.Value));
            Console.Read();
        }

        static Int64 Mask(string input, string mask){
            // convert input string to binary representation in 36 bit
            var binary = Convert.ToString(Int64.Parse(input), 2);

            // pad to 36 bit?
            binary = binary.PadLeft(36, '0');

            // apply mask
            var bits = mask.ToCharArray();
            for(var i = 0; i < bits.Length; i++){
                if(bits[i] == 'X') continue;
                binary = binary.Remove(i,1).Insert(i, bits[i].ToString());
            }

            return Convert.ToInt64(binary, 2);
        }
    }
}
