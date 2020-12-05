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

            var byr = values.First(x=>x.StartsWith("byr")).Split(":")[1];
            if(!IsNumberInRange(byr,1920,2002)) return false;

            var iyr = values.First(x=>x.StartsWith("iyr")).Split(":")[1];
            if(!IsNumberInRange(iyr,2010,2020)) return false;

            var eyr = values.First(x=>x.StartsWith("eyr")).Split(":")[1];
            if(!IsNumberInRange(eyr,2020,2030)) return false;   

            var hgt = values.First(x=>x.StartsWith("hgt")).Split(":");
            if(hgt[1].EndsWith("cm")){
                if(!IsNumberInRange(hgt[1].Replace("cm",""),150,193)) return false;
            }
            else if(hgt[1].EndsWith("in")){
                if(!IsNumberInRange(hgt[1].Replace("in",""),59,76)) return false;
            }
            else{
                // invalid height format
                return false;
            }

            if(!Regex.IsMatch(values.First(x=>x.StartsWith("hcl")), "^hcl:#([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$")) return false;

            if(!Regex.IsMatch(values.First(x=>x.StartsWith("ecl")), "^ecl:(amb|blu|brn|gry|grn|hzl|oth)$")) return false;

            if(!Regex.IsMatch(values.First(x=>x.StartsWith("pid")), "^pid:\\d{9}$")) return false;

            return true;
        }

        static bool IsNumberInRange(string field, int min, int max){
            var year = 0;
            if(!int.TryParse(field,out year)) return false;
            if(year<min || year>max) return false;
            return true;
        }
    }
}
