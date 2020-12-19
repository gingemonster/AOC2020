using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace _19a
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
                var result = Part1(lines, i);
                watch.Stop();
                if(i==0) Console.WriteLine(result);
                if (i > 1) timings.Add(watch.ElapsedMilliseconds);
                watch.Reset();
            }

            Console.WriteLine(timings.Average());
        }
        
        static long Part1(string[] input, int runindex)
        {
            var rules = input.Where(l=>l.Contains(":")).ToDictionary(l=>l.Split(": ")[0],l=>l.Split(": ")[1].Replace("\"",string.Empty));
            var expandedrules = ProcessRules(rules);
            var ruleregex = new Regex(expandedrules, RegexOptions.Compiled);
            var validmessages = input.Where(l=>!l.Contains(":") && l.Length>0 && ruleregex.Match(l).Success).Count();

            return validmessages;
        }

        static Regex getnumber = new Regex(@"\d+", RegexOptions.Compiled);

        static string ProcessRules(Dictionary<string,string> rules){
            var resultingrule = rules["0"];

            // for each number in rule replace it
            var numbertoprocess = getnumber.Match(resultingrule);
            while(numbertoprocess.Success){
                // process match's rule
                var ruletoreplacewith = rules[numbertoprocess.Value];
                if(ruletoreplacewith.Contains("|")) ruletoreplacewith = "(" + ruletoreplacewith + ")";
                resultingrule = resultingrule.Remove(numbertoprocess.Index,numbertoprocess.Length).Insert(numbertoprocess.Index, ruletoreplacewith);


                // get next one
                numbertoprocess = getnumber.Match(resultingrule);
            }

            return "^" + resultingrule.Replace(" ", string.Empty) + "$";
        }

    }
}
