using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace _17
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();
            var timings = new List<long>();
            var lines = File.ReadAllText("input.txt");
            for (var i = 0; i < 20; i++)
            {
                watch.Start();
                var result = Part1(lines, i);
                watch.Stop();
                if(i==4) Console.WriteLine(result);
                if (i > 1) timings.Add(watch.ElapsedMilliseconds);
                watch.Reset();
            }

            Console.WriteLine(timings.Average());
        }
        
        static long Part1(string input, int runindex)
        {
            var lines = input.Split(Environment.NewLine);
            
            var points = new Dictionary<ValueTuple<int,int,int>, char>();

            for(var i=0;i<lines.Length;i++){
                var chars = lines[i].ToCharArray();
                for(var ii=0;ii<lines[0].Length;ii++){
                    // new point x,y,z
                    points.Add(ValueTuple.Create(ii,i,0), chars[ii]);
                }
            }
           
            for(var i=0;i<6;i++){
                ProcessCycle(points);
                var test = points.Where(p=>p.Value=='#').Count();
            }
            return points.Where(p=>p.Value=='#').Count();
        }

        static void ProcessCycle(Dictionary<ValueTuple<int,int,int>, char> points){
            // for each cycle our set of points needs to grow to point a shell 1 place
            // further out than the edge that has at least active point
            var activepoints = points.Where(p=>p.Value == '#');
            var highestx = activepoints.Max(p=>p.Key.Item1) + 1;
            var highesty = activepoints.Max(p=>p.Key.Item2) + 1;
            var highestz = activepoints.Max(p=>p.Key.Item3) + 1;
            var lowestx = activepoints.Min(p=>p.Key.Item1) - 1;
            var lowesty = activepoints.Min(p=>p.Key.Item2) - 1;
            var lowestz = activepoints.Min(p=>p.Key.Item3) - 1;

            // loop 3d grid
            var clonedictionary = points.ToDictionary(entry => entry.Key, entry => entry.Value); // done want to effect the dictionary as we loop through so use clone
            for(var z=lowestz;z<=highestz;z++){
                for(var y=lowesty;y<=highesty;y++){
                    for(var x=lowestx;x<=highestx;x++){
                        // check if we have a entry for this position
                        // otherwise create one inactive
                        if(!points.ContainsKey(ValueTuple.Create(x,y,z))){
                            points.Add(ValueTuple.Create(x,y,z),'.');
                            clonedictionary.Add(ValueTuple.Create(x,y,z),'.');
                        }
                        var result = ProcessPoint(clonedictionary, ValueTuple.Create(x,y,z));
                        points[ValueTuple.Create(x,y,z)] = result; 
                    }
                }
            }
        }

        static char ProcessPoint(Dictionary<ValueTuple<int,int,int>, char> points, ValueTuple<int,int,int> pointkey){
            var numactiveneighbours = 0;
            var result = points[pointkey];

            // loop all neighboors +-1 from current point in all three planes
            for(var z = pointkey.Item3-1; z<=pointkey.Item3+1;z++){
                for(var y = pointkey.Item2-1; y<=pointkey.Item2+1;y++){
                    for(var x = pointkey.Item1-1; x<=pointkey.Item1+1;x++){
                        char possiblevalue;
                        var thispoint = ValueTuple.Create(x,y,z);
                        points.TryGetValue(thispoint, out possiblevalue);

                        // first ensuring we arent counting the point itself, is this neighboor active?
                        if(thispoint != pointkey  && possiblevalue == '#'){
                            numactiveneighbours++;
                        }
                    }
                }
            }
            if(points[pointkey] == '#'){
                // If a cube is active and exactly 2 or 3 of its neighbors are also active, the cube remains active. Otherwise, the cube becomes inactive.
                if(numactiveneighbours !=2 && numactiveneighbours != 3) result = '.';
            }
            else{
                // If a cube is inactive but exactly 3 of its neighbors are active, the cube becomes active. Otherwise, the cube remains inactive.
                if(numactiveneighbours == 3) result = '#';
            }

            return result;
        }
    }
}
