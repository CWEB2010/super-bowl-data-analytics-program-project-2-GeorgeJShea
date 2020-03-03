/*
Made By George Shea Sseta
Created 2/3/2020
Version 2.0
Version Update 3/3/2020
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
            Console.WriteLine("Please Name the file what ever you wish");
            Console.WriteLine("Be sure to add a .txt at end of the file");
            Console.Write("Example C:\\Users\\shegeoj\\source\\repos\\readSuperBowl\\readSuperBowl\\bin\\Debug\\netcoreapp3.1\\");
            string endPoint = Console.ReadLine();


            endPoint = (@"C:\Users\shegeoj\source\repos\readSuperBowl\readSuperBowl\bin\Debug\netcoreapp3.1\" + endPoint);

            if (endPoint.Contains(".txt")) 
            {
                if (endPoint.Contains("/"))
                {
                    Console.WriteLine("sorry only this dir allowed");
                }
                else
                {
                    var myFile = File.Create(endPoint);
                    myFile.Close();


                    Printer(endPoint);
                }
            }
            else
            {
                Console.WriteLine("extenstion invalid");
            }

        }
        static int Printer(string writeLocation)
        {
            // used to write to another file 
            List<string> writeSystem = new List<string>();


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

            List<SuperBowlObject> SuperList = new List<SuperBowlObject>();

            foreach (string line in System.IO.File.ReadLines(@fullPath))
            {
                string QLine = line.Replace(", ", "");
                string[] things = QLine.Split(",");

                if (counter == 0)
                {
                    things[2] = "1";
                    things[6] = "1";
                    things[10] = "1";
                }

                SuperList.Add(new SuperBowlObject
                {
                    Dat = things[0],
                    Roman = things[1],
                    Attend = Int32.Parse(things[2]),
                    QB_Win = things[3],
                    Coach_Win = things[4],
                    Win = things[5],
                    Win_Pnts = Int32.Parse(things[6]),
                    Qb_lose = things[7],
                    Coach_Lose = things[8],
                    Lose = things[9],
                    Lose_pnts = Int32.Parse(things[10]),
                    MVP = things[11],
                    Stad = things[12],
                    Cit = things[13],
                    Stat = things[14]
                });

                counter = counter + 1;

            }

            List<string> tempList = new List<string>();
  
            SuperBowlObject.StrOrg(State, SuperList, 0, 0, writeSystem);

            SuperBowlObject.StrOrg(State, SuperList, 7, 1, writeSystem);

            SuperBowlObject.StrOrg(Mvp, SuperList, 2, 2, writeSystem);

            SuperBowlObject.StrOrg(Coach_winner, SuperList, 13, 3, writeSystem);

            SuperBowlObject.StrOrg(Coach_Loser, SuperList, 14, 4, writeSystem);

            SuperBowlObject.StrOrg(Winner, SuperList, 12, 5, writeSystem);

            SuperBowlObject.StrOrg(Loser, SuperList, 4, 6, writeSystem);


            SuperBowlObject.intDif(Winning_Pnts, SuperList, 5, writeSystem);

            SuperBowlObject.intMax(Attedence, SuperList, 1, writeSystem);

            SuperBowlObject.intAver(Attedence, SuperList, writeSystem);


            // will write all new info 

            try
            {
                File.WriteAllLines(@writeLocation, writeSystem);
            }
            catch
            {
                Console.WriteLine("file location error");
            }
            return 0;
        }
    }
    class SuperBowlObject
    {
        //thanks to https://stackoverflow.com/questions/1169759/how-to-get-the-top-3-elements-in-an-int-array-using-linq
        public string Dat { get; set; } = "1l";
        public string Roman { get; set; } = "I";   //1
        public int Attend { get; set; } = 1;   //2
        public string QB_Win { get; set; } = "I";    //3
        public string Coach_Win { get; set; } = "I";  //4
        public string Win { get; set; } = "I";      //5
        public int Win_Pnts { get; set; } = 1; // 6
        public string Qb_lose { get; set; } = "I";  //7
        public string Coach_Lose { get; set; } = "I";  //8
        public string Lose { get; set; } = "I";    //9
        public int Lose_pnts { get; set; } = 1;    //10
        public string MVP { get; set; } = "I";    //11
        public string Stad { get; set; } = "I";   //12
        public string Cit { get; set; } = "I";   //13
        public string Stat { get; set; } = "I";   //14

        static public void StrOrg(List<string> Sorter, List<SuperBowlObject> SuburbList, int strenght, int mode, List<string> writeSystem)
        {

            int counter = 0;
            foreach (SuperBowlObject x in SuburbList)
            {
                Sorter.Add(new string(SuburbList[counter].Stat));
                counter = counter + 1;
            }

            var result = Sorter
              .GroupBy(s => s)
              .Where(g => g.Count() > strenght)
              .OrderByDescending(g => g.Count())
              .Select(g => g.Key);
            writeSystem.Add("All Teams That Have One");
            foreach (string count in result)
            {

                int index = Sorter.FindIndex(x => x.StartsWith(count));
                if (mode == 0)
                {
                    writeSystem.Add("Winning Team " + SuburbList[index].Win +
                    " Year " + SuburbList[index].Dat +
                    " Winning Quaterback " + SuburbList[index].QB_Win +
                    " Winning Coach " + SuburbList[index].Coach_Win +
                    " MVP " + SuburbList[index].MVP +
                    " Point Diffrence " + (SuburbList[index].Win_Pnts - SuburbList[index].Lose_pnts));
                }
                else if (mode == 1)
                {
                    // most hosted superbowl
                    writeSystem.Add("Most Hosted SuperBowl");
                    writeSystem.Add(("City " + SuburbList[index].Cit + " State " + SuburbList[index].Stat +
                        " Stadium " + SuburbList[index].Stad));
                }
                else if (mode == 2)
                {
                    // mvp
                    writeSystem.Add("Most Valuable Player");
                    writeSystem.Add("Mvp " + SuburbList[index].MVP + " Winner " + SuburbList[index].Win +
                        " Losser " + SuburbList[index].Lose);
                }
                else if (mode == 3)
                {
                    // coach winning
                    writeSystem.Add("Coach who has the most wins");
                    writeSystem.Add(SuburbList[index].Coach_Win);
                }
                else if (mode == 4)
                {
                    // coach losing or winning team and loosing team
                    writeSystem.Add("Coach who came in second the most");
                    writeSystem.Add(SuburbList[index].Coach_Lose);
                }
                else if (mode == 5)
                {
                    // winner
                    writeSystem.Add("Most wins");
                    writeSystem.Add(SuburbList[index].Win);
                }
                else if (mode == 6)
                {
                    // winner
                    writeSystem.Add("Most 2nd places");
                    writeSystem.Add(SuburbList[index].Lose);
                }
            }
        }
        static public void intAver(List<int> Sorter, List<SuperBowlObject> SuburbList, List<string> writeSystem)
        {
            int counter = 0;
            foreach (SuperBowlObject x in SuburbList)
            {
                Sorter.Add((SuburbList[counter].Attend));
                counter = counter + 1;
            }
            writeSystem.Add("Average Attendce");
            writeSystem.Add(Convert.ToString(Sorter.Average()));
        }
        static public void intDif(List<int> Sorter, List<SuperBowlObject> SuburbList, int strenght, List<string> writeSystem)
        {
            List<int> Point_Dif = new List<int>();
            int counter = 0;
            foreach (SuperBowlObject count in SuburbList)
            {
                int temp = (SuburbList[counter].Win_Pnts - SuburbList[counter].Lose_pnts);
                Point_Dif.Add(temp);
                counter = counter + 1;
            }
            writeSystem.Add("Biggest Point Diffrence");
            writeSystem.Add(SuburbList[Point_Dif.IndexOf(Point_Dif.Max())].Roman);
        }
        static public void intMax(List<int> Sorter, List<SuperBowlObject> SuburbList, int strenght, List<string> writeSystem)
        {
            int counter = 0;
            writeSystem.Add("Top 5 Attended super bowls");
            foreach (SuperBowlObject x in SuburbList)
            {
                Sorter.Add((SuburbList[counter].Attend));
                counter = counter + 1;
            }

            var top = (from i in Sorter
                   orderby i descending
                   select i).Take(strenght);

            foreach (var x in top)
            {
                int topFive = Sorter.IndexOf(x);
                writeSystem.Add(SuburbList[topFive].Roman);

            }

        }
    }
}
