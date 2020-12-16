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
            var lines = File.ReadAllLines("test.txt").ToArray();
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
                othertickets.Add(new Ticket() { Fields = lines[i].Split(",").Select(x=>int.Parse(x)).Select(l=> new Field() { Value = l }).ToList() });
                i++;
            }
            
            othertickets.ForEach(t=> FindValidRulesPerField(rules, t));

            var validtickets = othertickets.Where(x=>x.Fields.Any(f=>f.MatchingRuleIndexes.Count==0));




            Console.WriteLine("invalidfields.Sum()");
            Console.Read();
        }
        static void FindValidRulesPerField(List<Rule> rules, Ticket ticket){
            foreach(var field in ticket.Fields){
                // check each rule
                for(var i = 0; i <rules.Count; i++){
                    if(Enumerable.Range(rules[i].FirstRange.Item1,rules[i].FirstRange.Item2-rules[i].FirstRange.Item1+1).Contains(field.Value) || Enumerable.Range(rules[i].SecondRange.Item1,rules[i].SecondRange.Item2-rules[i].SecondRange.Item1+1).Contains(field.Value)){
                        field.MatchingRuleIndexes.Add(i);
                    }
                }
            }
        }
    }
}
