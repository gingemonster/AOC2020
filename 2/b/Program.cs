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
                var index1 = int.Parse(match.Groups[1].Value) - 1;
                var index2 = int.Parse(match.Groups[2].Value) - 1;
                var isvalid = match.Groups[4].Value[index1] == char.Parse(match.Groups[3].Value) ^ match.Groups[4].Value[index2] == char.Parse(match.Groups[3].Value);
                if(isvalid) valid++;
            }
            Console.WriteLine(valid);
            Console.Read();
        }
    }
}
