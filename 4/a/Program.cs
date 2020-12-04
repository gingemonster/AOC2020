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
            var input = File.ReadAllText("input.txt");
            //input.Replace(Environment.NewLine," ");
            var rows = input.Split(new string[] {Environment.NewLine + Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            var validcount = 0;
            foreach(var row in rows){
                var pairs = row.Replace(Environment.NewLine," ").Split(" ");
                validcount += IsValid(pairs) ? 1 : 0;
            }
            Console.WriteLine(validcount);
            Console.Read();
        }

        static bool IsValid(string[] values){
            if(values.Length < 7 || (values.Length < 8 && values.Any(x => x.StartsWith("cid")))) return false;
            return true;
        }
    }
}
