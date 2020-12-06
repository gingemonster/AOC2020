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
            var input = File.ReadAllText("input.txt").Split(Environment.NewLine + Environment.NewLine);
            var numberyes=0;
            foreach(var pass in input){
                var answers = pass.Replace(Environment.NewLine,"").ToArray();
                numberyes += answers.GroupBy(x=>x).Count();
            }
            Console.WriteLine(numberyes);
            Console.Read();
        }
    }
}
