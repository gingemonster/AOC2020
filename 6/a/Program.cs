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

            var childrenbags = new List<string>() { "shiny gold" };
            var parentbags = new List<string>();
            var bagsvisited = new List<string>();
            while(true){
                parentbags = DirectParentBags(childrenbags, bags);
                bagsvisited.AddRange(parentbags);
                childrenbags = parentbags.Distinct().ToList();
                if(childrenbags.Count == 0) break;
            }
            Console.WriteLine(bagsvisited.Distinct().Count());
            Console.Read();
        }
        private static List<string> DirectParentBags(List<string> childrenbags, List<Bag> bags){
            var result = new List<string>();
            childrenbags.ForEach(cb=>{
                result.AddRange(bags.Where(bags=>bags.BagsContained.Contains(cb)).Select(b=>b.Name).ToList());
            });
            return result;
        }        
    }
}
