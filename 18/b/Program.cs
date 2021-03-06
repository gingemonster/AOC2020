﻿using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace _18b
{
    class Program
    {
        static Regex plusregex = new Regex(@"\d* \+ \d*", RegexOptions.Compiled);
        static Regex notnestedexpressions = new Regex(@"\(([^\(\)]*)\)", RegexOptions.Compiled);
        static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();
            var timings = new List<long>();
            var lines = File.ReadAllText("input.txt");
            for (var i = 0; i < 20; i++)
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
        
        static long Part2(string input, int runindex)
        {
            var lines = input.Split(Environment.NewLine).ToList();
            
            return lines.Sum(l=>ProcessLine(l));;
        }

        static long ProcessLine(string input){
            
            var match = notnestedexpressions.Match(input);

            // doing one match at a time to make replacement easier as string wont change between matches
            while(match.Success){
                // expand this parenthasis
                var result = ProcessesExpression(match.Groups[1].Value);

                // replace matche's place in string with result
                input = input.Remove(match.Index,match.Length).Insert(match.Index,result.ToString());

                // get next one that needs expanding
                match = notnestedexpressions.Match(input);
            }
            var finalresult = ProcessesExpression(input);
            return finalresult;
        }

        static long ProcessesExpression(string input){
            
            var plusmatch = plusregex.Match(input);

            // process all the pluses first
            while(plusmatch.Success && input.ToCharArray().Count(c=>c=='+'||c=='*') > 1){
                // expand
                var r = ProcessesExpression(plusmatch.Value);

                // replace matche's place in string with result
                input = input.Remove(plusmatch.Index,plusmatch.Length).Insert(plusmatch.Index,r.ToString());

                // get next one that needs expanding
                plusmatch = plusregex.Match(input);
            }

            var parts = input.Split(' ');
            var result = long.Parse(parts[0]);            

            // + processed before *
            
            for(var i =1; i < parts.Length; i++){
                switch(parts[i]){
                    case "+":
                        result+=long.Parse(parts[i+1]);
                        i++;
                        break;
                    case "*":
                        result*=long.Parse(parts[i+1]);
                        i++;
                        break;
                }
            }
            return result;
        }
    }
}
