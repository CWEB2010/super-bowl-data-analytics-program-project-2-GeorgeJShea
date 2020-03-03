/*
Made By George Shea Sseta
Created 2/3/2020
Version 1.0
Version Update 2/3/2020
Use reads from a file and writes to another file then displays html

*/



using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace readSuperBowl
{
    class Program
    {
        static void Main(string[] args)
        {
            string fullPath = Path.GetFullPath("Super_Bowl_Project.csv");

            int counter = 0;
            // all of the things needed
            List<String> Date = new List<String>();

            List<string> SB = new List<string>();
            // dont forget to convert to int
            List<int> Attedence = new List<int>();
            List<string> Qb_Winner = new List<string>();
            List<string> Coach_winner = new List<string>();
            List<string> Winner = new List<string>();

            List<int> Winning_Pnts = new List<int>();
            List<string> Qb_loser = new List<string>();
            List<string> Coach_Loser = new List<string>();
            List<string> Loser = new List<string>();
            // dont forget to convert to int

            List<int> Losing_Pnts = new List<int>();
            List<string> Mvp = new List<string>();
            List<string> Stadium = new List<string>();
            List<string> City = new List<string>();
            List<string> State = new List<string>();
            // used to make sure first set of stuff doesnt kill it
            int temp = 0;

            foreach (string line in System.IO.File.ReadLines(@fullPath))
            {
                string QLine = line.Replace(", ", "");
                string[] things = QLine.Split(",");


                Date.Add(new String(things[0]));
                SB.Add(new String(things[1]));

                if (counter == 0)
                {
                    temp = 0;
                    Attedence.Add(temp);
                }
                else
                {
                    temp = Int32.Parse(things[2]);
                }
                Attedence.Add(temp);

                Qb_Winner.Add(new String(things[3]));
                Coach_winner.Add(new String(things[4]));
                Winner.Add(new String(things[5]));

                if (counter == 0)
                {
                    temp = 0;
                    Attedence.Add(temp);
                }
                else
                {
                    temp = Int32.Parse(things[6]);
                }
                Winning_Pnts.Add(temp);

                Qb_loser.Add(new String(things[7]));
                Coach_Loser.Add(new String(things[8]));
                Loser.Add(new String(things[9]));

                if (counter == 0)
                {
                    temp = 0;
                    Attedence.Add(temp);
                }
                else
                {
                    temp = Int32.Parse(things[10]);
                }
                Losing_Pnts.Add(temp);

                Mvp.Add(new String(things[11]));
                Stadium.Add(new String(things[12]));
                City.Add(new String(things[13]));
                State.Add(new String(things[14]));

                counter = counter + 1;

            }


            int countertwo = 1;
            counter = State.Count;
            while (counter > 1)
            {
                Console.WriteLine("Winning Team " + Winner[countertwo] +
                                    " Year " + Date[countertwo] +
                                    " Winning Quaterback " + Qb_Winner[countertwo] +
                                    " Winning Coach " + Coach_winner[countertwo] +
                                    " MVP " + Mvp[countertwo] +
                                    " Point Diffrence " + Losing_Pnts[countertwo]);

                countertwo = countertwo + 1;
                counter = counter - 1;
            }

            counter = 5;

            //thanks to https://stackoverflow.com/questions/1169759/how-to-get-the-top-3-elements-in-an-int-array-using-linq

            Console.WriteLine("\n\n\n");
            var top = (from i in Attedence
                       orderby i descending
                       select i).Take(5);

            foreach (var x in top)
            {
                int topFive = Attedence.IndexOf(x);

                Console.WriteLine("Year " + Date[topFive] +
                                  " Winner " + Winner[topFive] +
                                  " Losser " + Loser[topFive] +
                                  " City " + City[topFive] +
                                  " State " + State[topFive] +
                                  " Stadium " + Stadium[topFive]);
                Console.WriteLine();

            }

            Console.WriteLine("\n\n\n");

            var result = State
              .GroupBy(s => s)
              .Where(g => g.Count() > 2)
              .OrderByDescending(g => g.Count())
              .Select(g => g.Key);

            foreach (string count in result)
            {

                int index = State.FindIndex(x => x.StartsWith(count));
                Console.WriteLine("City " + City[index] + " State " + State[index] + " Stadium" + Stadium[index]);

            }

            Console.WriteLine("\n\n\n");

            result = Mvp
              .GroupBy(s => s)
              .Where(g => g.Count() > 1)
              .OrderByDescending(g => g.Count())
              .Select(g => g.Key);

            counter = 0;
            foreach (string count in result)
            {
                if (counter > 4)
                {
                    int index = State.FindIndex(x => x.StartsWith(count));
                    Console.WriteLine("Mvp " + Mvp[index] + " Winner " + Winner[index] + " Losser" + Loser[index]);
                    counter = counter + 1;
                }
            }


            Console.WriteLine("\n\n\n");
            Console.WriteLine("This Choach Won the Most Super Bowls");

            result = Coach_winner
              .GroupBy(s => s)
              .Where(g => g.Count() > 3)
              .OrderByDescending(g => g.Count())
              .Select(g => g.Key);

            foreach (string count in result)
            {

                Console.WriteLine(count);

            }

            Console.WriteLine("\n\n\n");
            Console.WriteLine("These Choachs lost the most Super Bowls");

            result = Coach_Loser
              .GroupBy(s => s)
              .Where(g => g.Count() > 3)
              .OrderByDescending(g => g.Count())
              .Select(g => g.Key);

            foreach (string count in result)
            {

                Console.WriteLine(count);

            }

            Console.WriteLine("\n\n\n");
            Console.WriteLine("These Teams Won the most Super Bowls");

            result = Winner
              .GroupBy(s => s)
              .Where(g => g.Count() > 5)
              .OrderByDescending(g => g.Count())
              .Select(g => g.Key);

            foreach (string count in result)
            {

                Console.WriteLine(count);

            }

            Console.WriteLine("\n\n\n");
            Console.WriteLine("These Teams Lost the most Super Bowls");

            result = Loser
          .GroupBy(s => s)
          .Where(g => g.Count() > 4)
          .OrderByDescending(g => g.Count())
          .Select(g => g.Key);

            foreach (string count in result)
            {

                Console.WriteLine(count);

            }

            Console.WriteLine("\n\n\n");
            Console.WriteLine("These Teams Lost the most Super Bowls");


            List<int> Point_Dif = new List<int>();
            foreach (int count in Winning_Pnts)
            {
                if (Winning_Pnts.Count < ((Winning_Pnts.Count) - 1))
                {
                    temp = (Winning_Pnts[count] - Losing_Pnts[count]);
                    Point_Dif.Add(temp);
                }
            }

            Console.WriteLine("\n\n\n");
            Console.WriteLine("this super bowl had the greatest point diffrince");
            top = (from i in Attedence
                       orderby i descending
                       select i).Take(1);

            foreach (var x in top)
            {
                int topFive = Attedence.IndexOf(x);
                Console.WriteLine( SB[topFive]);
                Console.WriteLine();

            }


            // well that one was easy
            Console.WriteLine(Attedence.Average());


        }
    }
}
