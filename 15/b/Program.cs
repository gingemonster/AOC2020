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
            var turns = new Dictionary<int,int>();
            //1,0,16,5,17,4
            turns.Add(1,0);
            turns.Add(0,1);
            turns.Add(16,2);
            turns.Add(5,3);
            turns.Add(17,4);
            
            //turns.Add(2,6);
            var countstartingnumbers = turns.Count;
            var lastturn = 4;
            for(var i=countstartingnumbers;i<30000000-1;i++){
                var thisturn = 0;
                if(!turns.ContainsKey(lastturn)){
                    thisturn = 0;
                }
                else{
                    thisturn = i-turns[lastturn];
                }
                turns[lastturn] = i;
                lastturn = thisturn;
            }
            Console.WriteLine(lastturn);
            Console.Read();
        }
    }
}
