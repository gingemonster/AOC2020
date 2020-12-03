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
            var grid = File.ReadLines("input.txt").ToArray().Select(x=>x.ToArray()).ToArray();
            
            var directions = new []{(1,1),(3,1),(5,1),(7,1),(1,2)};

            var gridheight = grid.Count();
            var gridwidth = grid[0].Count();
            Int64 total = 1;

            foreach(var d in directions){
                var position = (0,0);
                Int64 hittrees = 0;

                while(position.Item2 < gridheight){
                    if(grid[position.Item2][position.Item1] == '#') hittrees++;
                    position.Item1 = (position.Item1 + d.Item1) % gridwidth; // wrap around
                    position.Item2 += d.Item2;
                }
                total = total * hittrees;
            }

            Console.WriteLine(total);
            Console.Read();
        }
    }
}
