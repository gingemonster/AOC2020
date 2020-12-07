namespace _1
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class Bag{
        public Bag(string input){
            this.Name = input.Split(" bags contain ")[0];
            //dark orange bags contain 3 bright white bags, 4 muted yellow bags.
            this.BagsContained = new List<Tuple<string, int>>();
            foreach(var bag in input.Split(" contain ")[1].Replace(".","").Split(", ")){
                if (bag == "no other bags") break;
                var parts = bag.Split(" ");
                this.BagsContained.Add(new Tuple<string, int>($"{parts[1]} {parts[2]}",int.Parse(parts[0])));
            }
        }

        public List<Tuple<string,int>> BagsContained{
            get;set;
        }

        public string Name{
            get;set;
        }
    }
}