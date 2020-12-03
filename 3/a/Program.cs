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
            var position = (0,0);
            var direction = (3,1);

            var gridheight = grid.Count();
            var gridwidth = grid[0].Count();
            var hittrees = 0;

            while(position.Item2 < gridheight){
                if(grid[position.Item2][position.Item1] == '#') hittrees++;
                position.Item1 = (position.Item1 + direction.Item1) % gridwidth; // wrap around
                position.Item2 += direction.Item2;
            }
            Console.WriteLine(hittrees);
            Console.Read();
        }
    }
}
