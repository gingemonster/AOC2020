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
            var watch = new System.Diagnostics.Stopwatch();
            var timings = new List<long>();
            var lines = File.ReadAllLines("input.txt").ToArray();
            for (var i = 0; i < 12; i++)
            {
                watch.Start();
                var result = Part2(lines, i);
                watch.Stop();
                if(i==4) Console.WriteLine(result);
                if (i > 1) timings.Add(watch.ElapsedMilliseconds);
                watch.Reset();
            }

            Console.WriteLine(timings.Average());
        }
        
        static long Part2(string[] lines, int runindex)
        {
            
            var i =0;
            var currentline = lines[i];
            var rules = new List<Rule>();
            while(currentline.Length > 0){
                rules.Add(new Rule(lines[i]));
                i++;
                currentline = lines[i];
            }

            // skip yourticket
            i+=2;
            var myticket = new Ticket() { Fields = lines[i].Split(",").Select(x=>int.Parse(x)).Select(l=> new Field() { Value = l }).ToList() };
            i+=3;

            var othertickets = new List<Ticket>() { myticket };
            while(i<lines.Length){
                othertickets.Add(new Ticket() { Fields = lines[i].Split(",").Select(x=>int.Parse(x)).Select(l=> new Field() { Value = l }).ToList() });
                i++;
            }
            
            othertickets.ForEach(t=> FindValidRulesPerField(rules, t));

            var validtickets = othertickets.Where(x=>x.Fields.All(f=>f.MatchingRuleIndexes.Count>0)).ToArray();

            // find fields across all tickets that only have one rule in common
            var foundruleindexes = new List<int>();
            var fieldguesses = new List<FieldGuesses>();
            for(var ii = 0; ii < validtickets[0].Fields.Count; ii++){
                var intersecting = validtickets.Select(t=>t.Fields[ii].MatchingRuleIndexes).Cast<IEnumerable<int>>().Aggregate((x,y) => x.Intersect(y));
                fieldguesses.Add(new FieldGuesses() { FieldIndex = ii, RuleGuesses = intersecting.ToList() });
            }

            var indexesremoved = new List<int>();
            while(fieldguesses.Any(fg=>fg.RuleGuesses.Count>1)){
                // find first fieldguess with only 1 rule guess
                var lastsingleruleguess = fieldguesses.Where(fg=>fg.RuleGuesses.Count==1 && !indexesremoved.Contains(fg.RuleGuesses[0])).Last(); // hopefully will keep moving forward
                var toremove = lastsingleruleguess.RuleGuesses.First();
                indexesremoved.Add(toremove);
                // remove this rule index from all others and continue
                fieldguesses.Where(fg=>fg!=lastsingleruleguess).ToList().ForEach(fg=>fg.RuleGuesses.Remove(toremove));
            }

            var departurerules = rules.Select((r,i) => new { Rule= r, Index= i}).Where(r=>r.Rule.Name.ToLower().Contains("departure")).ToList();

            // get values for each departure by finding matching field
            foreach(var departurerule in departurerules){
                // find matching field
                var fieldindex = fieldguesses.Where(fg=>fg.RuleGuesses[0] == departurerule.Index).First().FieldIndex;
                var value = myticket.Fields[fieldindex].Value;
            }

            var test = departurerules.Select(dr=>{
                var fieldindex = fieldguesses.Where(fg=>fg.RuleGuesses[0] == dr.Index).First().FieldIndex;
                return myticket.Fields[fieldindex].Value;
            });

            long answer = 1;
            departurerules.ForEach(dr=>{
                var fieldindex = fieldguesses.Where(fg=>fg.RuleGuesses[0] == dr.Index).First().FieldIndex;
                var value = myticket.Fields[fieldindex].Value;
                answer = answer * value;
            });

            return answer;
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
