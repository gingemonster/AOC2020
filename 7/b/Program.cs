using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace _1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var bags = new List<Bag>();
            foreach(var bag in input){
                bags.Add(new Bag(bag));
            }

            var parentbags = new List<Tuple<string, int>>() { new Tuple<string,int>("shiny gold",1) };
            var numbagscontained = 0;
            while(true){
                var childrenbags = DirectChildBags(parentbags, bags);
                numbagscontained += childrenbags.Sum(c=>c.Item2);
                if(childrenbags.Count() == 0) break;
                parentbags = childrenbags;
            }

            Console.WriteLine(numbagscontained);
            Console.Read();
        }
        private static List<Tuple<string,int>> DirectChildBags(List<Tuple<string, int>> parentbags, List<Bag> bags){
            var result = new List<Tuple<string,int>>();
            parentbags.ForEach(pb=>{
                // get the parent and return its children with the number of that child times the number of the parent
                result.AddRange(bags.Where(b=>b.Name == pb.Item1).First().BagsContained.Select(x=> new Tuple<string, int>(x.Item1,x.Item2 * pb.Item2)));
            });
            return result;
        }        
    }
}
