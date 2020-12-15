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
            var turns = new List<int>() { 1,0,16,5,17,4 };
            var countstartingnumbers = turns.Count;
            for(var i=countstartingnumbers;i<2020;i++){
                if(turns.Count <= countstartingnumbers || turns.IndexOf(turns[i-1]) == turns.Count-1){
                    turns.Add(0);
                }
                else{
                    var test1 = turns.Select((v, i) => new { Index = i, Value = v });
                    var test2 = test1.Where(x=> x.Index!=turns.Count-1 && x.Value == turns[i-1]);
                    var result = test2.Max(x=>x.Index);
                    turns.Add(i - result - 1);
                }
            }

            Console.WriteLine(turns.Last());
            Console.Read();
        }
    }
}
