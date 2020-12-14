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
            
            var memory = new Dictionary<Int64,Int64>();
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
                    var addresses = GetAddresses(address, currentmask);
                    foreach(var a in addresses){
                        memory[a] = Int64.Parse(value);
                    }
                }
            }

            Console.WriteLine(memory.Where(x=>x.Value!=0).Sum(x=>x.Value));
            Console.Read();
        }

        static Int64[] GetAddresses(string input, string mask){
            // convert input string to binary representation in 36 bit
            var binary = Convert.ToString(Int64.Parse(input), 2);

            // pad to 36 bit?
            binary = binary.PadLeft(36, '0');

            // apply mask
            var bits = mask.ToCharArray();
            for(var i = 0; i < bits.Length; i++){
                if(bits[i] == '0') continue;
                binary = binary.Remove(i,1).Insert(i, bits[i].ToString());
            }

            // create address permutations
            List<Int64> results = new List<Int64>();
            var perms = new List<string>() { binary };
            while(perms.Any(x=>x.Contains("X"))){
                var newperms = new List<string>();
                foreach(var perm in perms){
                    // find first X, create two new perms
                    var firstxindex = perm.IndexOf("X");
                    newperms.Add(perm.Remove(firstxindex,1).Insert(firstxindex, "0"));
                    newperms.Add(perm.Remove(firstxindex,1).Insert(firstxindex,"1"));
                }
                perms = newperms;
            }

            return perms.Select(x=>Convert.ToInt64(x, 2)).ToArray();
        }
    }
}
