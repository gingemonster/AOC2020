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

                    for(var iii=0;iii<alllines.Count();iii++){
                        if(ii==iii || i == iii) continue;

                        if(int.Parse(alllines[i]) + int.Parse(alllines[ii]) + int.Parse(alllines[iii]) ==  2020){
                            Console.WriteLine(int.Parse(alllines[i]) * int.Parse(alllines[ii])* int.Parse(alllines[iii]));
                            solutionfound = true;
                        }
                    }
                    if(solutionfound) break;
                }
                if(solutionfound) break;
            }
            Console.Read();
        }
    }
}
