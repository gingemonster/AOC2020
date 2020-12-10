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
            var input = File.ReadLines("input.txt").Select(x=>Int64.Parse(x)).ToList();
            input.Sort();
            input.Insert(0,0); // outlet
            var dict = new Dictionary<int,int>();
            for(var i = 0; i < input.Count; i++){
                var runlength = 0;
                while(i + runlength < input.Count - 1 && input[i+runlength+1]-input[i+runlength] == 1){
                    runlength++;
                }
                if(dict.ContainsKey(runlength)){
                    dict[runlength] = dict[runlength] + 1;
                }
                else{
                    dict[runlength] = 1;
                }
                i =  i + runlength;             
            }
            double perms = 1;
            foreach(var run in dict)
            {
                if(run.Key < 2) continue;
                for(var i = 0; i < run.Value; i++){
                    var multiplier = Math.Pow(2,run.Key-1);
                    if(run.Key==4) multiplier--;
                    perms = perms * multiplier;
                }
            }
            Console.WriteLine(perms);
            Console.Read();
        }
    }
}
