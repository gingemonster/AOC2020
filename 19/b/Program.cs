using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace _19b
{
    class Program
    {
        static Regex notnestedexpressions = new Regex(@"\(([^\(\)]*)\)", RegexOptions.Compiled);
        static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();
            var timings = new List<long>();
            var lines = File.ReadAllLines("input.txt");
            for (var i = 0; i < 20; i++)
            {
                watch.Start();
                var result = Part2(lines, i);
                watch.Stop();
                if(i==0) Console.WriteLine(result); // 271 too high, 221 too low
                if (i > 1) timings.Add(watch.ElapsedMilliseconds);
                watch.Reset();
            }

            Console.WriteLine(timings.Average());
        }
        
        static long Part2(string[] input, int runindex)
        {
            var rules = input.Where(l=>l.Contains(":")).ToDictionary(l=>l.Split(": ")[0],l=>l.Split(": ")[1].Replace("\"",string.Empty));
            var expandedrules = ProcessRules(rules);
            var ruleregex = new Regex(expandedrules, RegexOptions.Compiled);
            var test = expandedrules.ToCharArray().Count(c=>c=='+');
            var messages = input.Where(l=>!l.Contains(":") && l.Length>0);
            var validmessages = new List<string>();
            foreach(var message in messages){
                var match = ruleregex.Match(message);
                if(match.Success){
                    var part1s = match.Groups.Cast<Group>().Where(g=>g.Name=="partone").First().Captures;
                    var part2s = match.Groups.Cast<Group>().Where(g=>g.Name=="parttwo").First().Captures;
                    if(part1s.Count() == part2s.Count()){
                        validmessages.Add(message);
                    }
                }
            }

            return validmessages.Count();
        }

        static Regex getnumber = new Regex(@"\d+\+?", RegexOptions.Compiled);

        static string ProcessRules(Dictionary<string,string> rules){
            var resultingrule = rules["0"];

            // for each number in rule replace it
            var numbertoprocess = getnumber.Match(resultingrule);
            while(numbertoprocess.Success){
                // process match's rule
                var ruletoreplacewith = rules[numbertoprocess.Value.Replace("+", string.Empty)];
                
                if(ruletoreplacewith.Contains("|")) ruletoreplacewith = "(?:" + ruletoreplacewith + ")";
                if(numbertoprocess.Value.Contains("+")) ruletoreplacewith+="+";
                //if(ruletoreplacewith.Contains("+")) ruletoreplacewith = "(?r_" + numbertoprocess.Value.Replace("+", string.Empty) + " " + ruletoreplacewith + ")";
                resultingrule = resultingrule.Remove(numbertoprocess.Index,numbertoprocess.Length).Insert(numbertoprocess.Index, ruletoreplacewith);


                // get next one
                numbertoprocess = getnumber.Match(resultingrule);
            }

            return "^" + resultingrule.Replace(" ", string.Empty) + "$";
        }

    }
}
