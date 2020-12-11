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
            var updatedgrid = File.ReadLines("input.txt").Select(x=>x.ToCharArray()).ToArray();
            char[][] initialgrid = null;

            while(initialgrid == null || !GridsAreEqual(updatedgrid,initialgrid)){
                initialgrid = CloneGrid(updatedgrid);
                for(var y = 0; y < updatedgrid.Length; y++){
                    for(var x = 0; x < updatedgrid[0].Length; x++){
                        UpdateSeat(updatedgrid, initialgrid, x, y);
                    }
                }
            }

            var occupiedseats = updatedgrid.Sum(x=>x.Count(y=>y=='#'));


            Console.WriteLine(occupiedseats);
            Console.Read();
        }

        private static char[][] CloneGrid(char[][] toclone){
            var cloned = new char[toclone.Length][];
            for(var row = 0; row < toclone.Length; row++){
                var columns = new char[toclone[row].Length];
                Array.Copy(toclone[row],columns,toclone[row].Length);
                cloned[row] = columns;
            }
            return cloned;
        }

        private static bool GridsAreEqual(char[][] a, char[][] b){
            for(var row = 0; row < a.Length; row++){
                for(var col = 0; col < a[0].Length; col++){
                    if(a[row][col] != b[row][col]) return false;
                }
            }
            return true;
        }

        private static void UpdateSeat(char[][] updatedseats, char[][] initialseats, int x, int y){
            if(updatedseats[y][x] == '.') return;

            var occupiedadjacent = CountOccupiedAdjacentSeats(initialseats,x,y);
            if(updatedseats[y][x] == 'L' && occupiedadjacent == 0) updatedseats[y][x] = '#';
            if(updatedseats[y][x] == '#' && occupiedadjacent > 4) updatedseats[y][x] = 'L';
        }

        private static int CountOccupiedAdjacentSeats(char[][] seats, int x, int y){
            // should really move somewhere global
            var gridwidth = seats[0].Length;
            var gridheight = seats.Length;
            var numoccupied = 0;
            
            if(IsOccupiedAlongVector(seats,new Tuple<int, int>(y,x), new Tuple<int, int>(-1,0))) numoccupied++;// start straight up
            if(IsOccupiedAlongVector(seats,new Tuple<int, int>(y,x), new Tuple<int, int>(-1,1))) numoccupied++;//top right
            if(IsOccupiedAlongVector(seats,new Tuple<int, int>(y,x), new Tuple<int, int>(0,1))) numoccupied++;;//right
            if(IsOccupiedAlongVector(seats,new Tuple<int, int>(y,x), new Tuple<int, int>(1,1))) numoccupied++;//bottom right
            if(IsOccupiedAlongVector(seats,new Tuple<int, int>(y,x), new Tuple<int, int>(1,0))) numoccupied++;// bottom
            if(IsOccupiedAlongVector(seats,new Tuple<int, int>(y,x), new Tuple<int, int>(1,-1))) numoccupied++;// bottom left
            if(IsOccupiedAlongVector(seats,new Tuple<int, int>(y,x), new Tuple<int, int>(0,-1))) numoccupied++;// left
            if(IsOccupiedAlongVector(seats,new Tuple<int, int>(y,x), new Tuple<int, int>(-1,-1))) numoccupied++;// top left

            return numoccupied;
        }

        private static bool IsOccupiedAlongVector(char[][] seats, Tuple<int,int> currentposition, Tuple<int,int> vector){
            while(true){
                currentposition = new Tuple<int, int>(currentposition.Item1 + vector.Item1,currentposition.Item2 + vector.Item2);

                // check if valid position
                if(currentposition.Item1 < 0 || currentposition.Item2 < 0 || currentposition.Item1 >= seats.Length || currentposition.Item2 >= seats[0].Length) return false;
                
                var positionvalue = seats[currentposition.Item1][currentposition.Item2];
                if(positionvalue == '.') continue;

                // check if occupied
                return positionvalue == '#' ? true : false;
            }
        }
    }
}
