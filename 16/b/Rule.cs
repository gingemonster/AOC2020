using System;
using System.Text.RegularExpressions;

namespace _16
{
    public class Rule{

        public Rule(string toparse){
            var groups = Regex.Match(toparse, @"^(.*): (\d*)-(\d*) or (\d*)-(\d*)$").Groups;
            this.Name = groups[1].Value;
            this.FirstRange = Tuple.Create(int.Parse(groups[2].Value),int.Parse(groups[3].Value));
            this.SecondRange = Tuple.Create(int.Parse(groups[4].Value),int.Parse(groups[5].Value));
        }
        public string Name {get;set;}
        public Tuple<int,int> FirstRange {get;set;}

        public Tuple<int,int> SecondRange {get;set;}
    }
    
}