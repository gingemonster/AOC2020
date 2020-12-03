using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _1
{
    class Program
    {
        static void Main(string[] args)
        {
            var alllines = File.ReadLines("input.txt").ToArray();
            var regex = new Regex("(\\d*)-(\\d*) (.): (.*)");
            var valid = 0;
            foreach(var line in alllines){
                var match = regex.Match(line);
                var repeats = match.Groups[4].Value.Count(s => s == char.Parse(match.Groups[3].Value));
                var min = int.Parse(match.Groups[1].Value);
                var max = int.Parse(match.Groups[2].Value);
                var isvalid = repeats >= min && repeats <= max;
                if(isvalid) valid++;
            }
            Console.WriteLine(valid);
            Console.Read();
        }
    }
}
