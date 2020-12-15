using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();
            var timings = new List<long>();
            for (var i = 0; i < 12; i++)
            {
                watch.Start();
                Part2();
                watch.Stop();
                if (i > 1) timings.Add(watch.ElapsedMilliseconds);
                watch.Reset();
            }

            Console.WriteLine(timings.Average());
        }

        private static void Part2()
        {
            var turns = new Dictionary<int,int>(29999999);

            //1,0,16,5,17,4
            turns.Add(1,0);
            turns.Add(0,1);
            turns.Add(16,2);
            turns.Add(5,3);
            turns.Add(17,4);
            
            var countstartingnumbers = turns.Count;
            var lastturn = 4;
            for(var i=countstartingnumbers;i<29999999;i++){
                var thisturn = 0;
                var lastturnentry = turns.GetValueOrDefault(lastturn,-1);
                if(lastturnentry != -1){
                    thisturn = i-lastturnentry;
                }
                turns[lastturn] = i;
                lastturn = thisturn;
            }
            Console.WriteLine(lastturn);
        }
    }
}