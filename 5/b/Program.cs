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
            var seatids = new List<int>();
            foreach(var pass in input){
                var passarray = pass.ToCharArray();
                var rowlow = 0;
                var rowhigh = 127;
                var collow = 0;
                var colhigh = 7;


                for(var i=0;i<7;i++){
                    if(passarray[i]=='F'){
                        // lower half
                        rowhigh = rowlow + (rowhigh-rowlow)/2; 
                    }
                    else{
                        rowlow = rowhigh - (rowhigh-rowlow)/2;
                    }
                }

                for(var i=7;i<10;i++){
                    if(passarray[i]=='L'){
                        // lower half
                        colhigh = collow + (colhigh-collow)/2; 
                    }
                    else{
                        collow = colhigh - (colhigh-collow)/2;
                    }
                }


                var seatid = rowlow * 8 + collow;
                seatids.Add(seatid);
            }

            seatids.Sort();

            for(var i=1;i<seatids.Count-1;i++){
                if(seatids[i] != seatids[i-1] + 1 ){
                    Console.WriteLine(seatids[i]-1);
                }
            }
            
            Console.Read();
        }
    }
}
