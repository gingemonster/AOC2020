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
            var input = File.ReadLines("test.txt").ToArray();
            
            var busses = input[1].Split(',');
            var time = 0L;
            var inc = long.Parse(busses[0]);
            for(var i = 1; i < busses.Length; i++){
                if (!busses[i].Equals("x"))
                {
                    var newTime = int.Parse(busses[i]);
                    while (true)
                    {
                        time += inc;
                        if ((time + i) % newTime == 0)
                        {
                            inc *= newTime;
                            break;
                        }
                    }
                }
            }
            Console.Write(time);
        }
    }
}
