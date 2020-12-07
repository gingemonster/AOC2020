namespace _1
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class Bag{
        public Bag(string input){
            this.Name = input.Split(" bags contain ")[0];
            //dark orange bags contain 3 bright white bags, 4 muted yellow bags.
            this.BagsContained = new List<string>();
            foreach(var bag in input.Split(" contain ")[1].Replace(".","").Split(", ")){
                var parts = bag.Split(" ");
                this.BagsContained.Add($"{parts[1]} {parts[2]}");
            }
        }

        public List<string> BagsContained{
            get;set;
        }

        public string Name{
            get;set;
        }
    }
}