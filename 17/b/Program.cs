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
                var result = Part2(lines, i);
                watch.Stop();
                if(i==4) Console.WriteLine(result);
                if (i > 1) timings.Add(watch.ElapsedMilliseconds);
                watch.Reset();
            }

            Console.WriteLine(timings.Average());
        }
        
        static long Part2(string input, int runindex)
        {
            var lines = input.Split(Environment.NewLine);
            
            var points = new Dictionary<Tuple<int,int,int,int>, char>();

            for(var i=0;i<lines.Length;i++){
                var chars = lines[i].ToCharArray();
                for(var ii=0;ii<lines[0].Length;ii++){
                    // new point x,y,z
                    points.Add(Tuple.Create(ii,i,0,0), chars[ii]);
                }
            }
           
            for(var i=0;i<6;i++){
                ProcessCycle(points);
                var test = points.Where(p=>p.Value=='#').Count();
            }
            return points.Where(p=>p.Value=='#').Count();
        }

        static void ProcessCycle(Dictionary<Tuple<int,int,int,int>, char> points){
            // for each cycle our set of points needs to grow to point a shell 1 place
            // further out than the edge that has at least active point
            var activepoints = points.Where(p=>p.Value == '#');
            var highestx = activepoints.Max(p=>p.Key.Item1) + 1;
            var highesty = activepoints.Max(p=>p.Key.Item2) + 1;
            var highestz = activepoints.Max(p=>p.Key.Item3) + 1;
            var highestw = activepoints.Max(p=>p.Key.Item4) + 1;
            var lowestx = activepoints.Min(p=>p.Key.Item1) - 1;
            var lowesty = activepoints.Min(p=>p.Key.Item2) - 1;
            var lowestz = activepoints.Min(p=>p.Key.Item3) - 1;
            var lowestw = activepoints.Min(p=>p.Key.Item4) - 1;

            // loop 4d grid
            var clonedictionary = points.ToDictionary(entry => entry.Key, entry => entry.Value); // done want to effect the dictionary as we loop through so use clone
            for(var w=lowestw;w<=highestw;w++){
                for(var z=lowestz;z<=highestz;z++){
                    for(var y=lowesty;y<=highesty;y++){
                        for(var x=lowestx;x<=highestx;x++){
                            // check if we have a entry for this position
                            // otherwise create one inactive
                            var thispointkey = Tuple.Create(x,y,z,w);
                            if(!points.ContainsKey(thispointkey)){
                                points.Add(thispointkey,'.');
                                clonedictionary.Add(thispointkey,'.');
                            }
                            var result = ProcessPoint(clonedictionary, thispointkey);
                            points[thispointkey] = result; 
                        }
                    }
                }                
            }

            // remove empty
            points = points.Where(p=>p.Value!='.').ToDictionary(entry => entry.Key, entry => entry.Value);
        }

        static char ProcessPoint(Dictionary<Tuple<int,int,int,int>, char> points, Tuple<int,int,int,int> pointkey){
            var numactiveneighbours = 0;
            var result = points[pointkey];

            // loop all neighboors +-1 from current point in all four planes
            for(var w = pointkey.Item4-1; w<=pointkey.Item4+1;w++){
                for(var z = pointkey.Item3-1; z<=pointkey.Item3+1;z++){
                    for(var y = pointkey.Item2-1; y<=pointkey.Item2+1;y++){
                        for(var x = pointkey.Item1-1; x<=pointkey.Item1+1;x++){
                            char possiblevalue;
                            var thispoint = Tuple.Create(x,y,z,w);
                            points.TryGetValue(thispoint, out possiblevalue);

                            // first ensuring we arent counting the point itself, is this neighboor active?
                            //if(thispoint != pointkey  && possiblevalue == '#'){
                            if(!(thispoint.Item1 == pointkey.Item1 && thispoint.Item2 == pointkey.Item2 && thispoint.Item3 == pointkey.Item3 && thispoint.Item4 == pointkey.Item4)  && possiblevalue == '#'){
                                numactiveneighbours++;
                            }
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
