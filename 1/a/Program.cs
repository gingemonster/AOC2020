using System;
using System.IO;
using System.Linq;

namespace _1
{
    class Program
    {
        static void Main(string[] args)
        {
            var alllines = File.ReadLines("input.txt").ToArray();
            var solutionfound = false;
            for(var i=0;i<alllines.Count();i++){
                for(var ii=0;ii<alllines.Count();ii++){
                    if(i==ii) continue;

                    if(int.Parse(alllines[i]) + int.Parse(alllines[ii]) ==  2020){
                        Console.WriteLine(int.Parse(alllines[i]) * int.Parse(alllines[ii]));
                        solutionfound = true;
                    }
                }
                if(solutionfound) break;
            }
            Console.Read();
        }
    }
}
