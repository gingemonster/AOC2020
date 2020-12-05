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
            var input = File.ReadAllLines("input.txt");
            var maxseatid = 0;
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
                if(seatid>maxseatid) maxseatid = seatid;
            }
            Console.WriteLine(maxseatid);
            Console.Read();
        }
    }
}
