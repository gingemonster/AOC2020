using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace _16
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt").ToArray();
            var i =0;
            var currentline = lines[i];
            var rules = new List<Rule>();
            while(currentline.Length > 0){
                rules.Add(new Rule(lines[i]));
                i++;
                currentline = lines[i];
            }

            // skip yourticket
            i+=5;

            var othertickets = new List<Ticket>();
            while(i<lines.Length){
                othertickets.Add(new Ticket() { Fields = lines[i].Split(",").Select(x=>int.Parse(x)).ToList() });
                i++;
            }

            var invalidfields = new List<int>();

            othertickets.ForEach(t=>{
                invalidfields.AddRange(GetInvalidFields(rules,t));
            });

            Console.WriteLine(invalidfields.Sum());
            Console.Read();
        }
        static List<int> GetInvalidFields(List<Rule> rules, Ticket ticket){
            var invalidfieldvalues = new List<int>();
            foreach(var field in ticket.Fields){
                // check each rule

                if(!(rules.Any(r=>Enumerable.Range(r.FirstRange.Item1,r.FirstRange.Item2-r.FirstRange.Item1+1).Contains(field)) || rules.Any(r=>Enumerable.Range(r.SecondRange.Item1,r.SecondRange.Item2-r.SecondRange.Item1+1).Contains(field)))){
                    invalidfieldvalues.Add(field);
                }
            }
            return invalidfieldvalues;
        }
    }
}
