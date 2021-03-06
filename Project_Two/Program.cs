/*
Made By George Shea Sseta
Created 2/3/2020
Version 3.0
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
            startUp();
        }
        static void startUp()
        {
            Console.WriteLine("Please Name the file what ever you wish");
            Console.WriteLine("Be sure to add a .html at end of the file");
            Console.WriteLine("Will be stored at \n C:\\Users\\shegeoj\\source\\repos\\readSuperBowl\\readSuperBowl\\bin\\Debug\\netcoreapp3.1");
            Console.Write("\\");
            string endPoint = Console.ReadLine();


            string initStart = Path.GetFullPath("readSuperBowl");

            //Application.Exit

            string[] testHtml = endPoint.Split(".");
            if (endPoint.Contains(".html") && testHtml[testHtml.Length - 1].Length == 4)
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
                Console.WriteLine("\n\n");
                startUp();
            }
        }
        static int Printer(string writeLocation)
        {
            // used to write to another file 
            List<string> writeSystem = new List<string>();

            // start of paragraph and html format complete

            writeSystem.Add("<html>");
            writeSystem.Add("<style> h1 {margin: 3px; text-align: center;} </style > ");
            writeSystem.Add("<style> h4 {margin: 0px; border-style: solid; background-color: lightgray; text-align: center;} </style > ");

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

            SuperBowlObject.intMax(Attedence, SuperList, 5, writeSystem);

            SuperBowlObject.intAver(Attedence, SuperList, writeSystem);


            // will write all new info 
            string[] split = @writeLocation.Split("/");
            try
            {
                File.WriteAllLines(@writeLocation, writeSystem);
                
                Console.WriteLine("Data is found at this website: ");
                Console.WriteLine("file:///" +((fullPath + "\\" + writeLocation).Replace("\\", "/")).Replace("/Super_Bowl_Project.csv", ""));
                Console.WriteLine();
            }
            catch
            {
                Console.WriteLine("file location error");
                Console.WriteLine();
                startUp();
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
            // used to stop over printings
            int counter = 0;
            string[] headers = { "<h1> All Teams that have won a super bowl </h1> <br>",
                                 "<h1> Most Hosted SuperBowl <h1>  <br>",
                                 "<h1> Most Valuable Player <h1> <br>",
                                 "<h1>  Coach who has the most wins </h1> <br>",
                                 "<h1>  Coach who came in second the most </h1> <br>",
                                 "<h1>  Most wins </h1>  <br>" ,
                                 "<h1>  Most 2nd places  </h1> <br>" };

            writeSystem.Add(headers[mode]);

            writeSystem.Add("<h4>");

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

            foreach (string count in result)
            {

                int index = Sorter.FindIndex(x => x.StartsWith(count));
                if (mode == 0)
                {
                    writeSystem.Add("  &emsp; Winning Team " + SuburbList[index].Win +
                    " Year " + SuburbList[index].Dat +
                    " Winning Quaterback " + SuburbList[index].QB_Win +
                    " Winning Coach " + SuburbList[index].Coach_Win +
                    " MVP " + SuburbList[index].MVP +
                    " Point Diffrence " + (SuburbList[index].Win_Pnts - SuburbList[index].Lose_pnts) + "<br>");
                }
                else if (mode == 1)
                {
                    // most hosted superbowl
                    writeSystem.Add(("  &emsp; City " + SuburbList[index].Cit + " State " + SuburbList[index].Stat +
                        " Stadium " + SuburbList[index].Stad + "<br>"));
                }
                else if (mode == 2)
                {
                    // mvp
                    writeSystem.Add("  &emsp; Mvp " + SuburbList[index].MVP + " Winner " + SuburbList[index].Win +
                        " Losser " + SuburbList[index].Lose + "  <br>");
                }
                else if (mode == 3)
                {
                    // coach winning
                    writeSystem.Add("  &emsp; " + SuburbList[index].Coach_Win + "  <br>");
                }
                else if (mode == 4)
                {
                    // coach losing or winning team and loosing team
                    writeSystem.Add("  &emsp; " + SuburbList[index].Coach_Lose + "  <br>");
                }
                else if (mode == 5)
                {
                    // winner
                    writeSystem.Add("  &emsp; " + SuburbList[index].Win + "  <br>");
                }
                else if (mode == 6)
                {
                    // winner
                    writeSystem.Add("  &emsp; " + SuburbList[index].Lose + "  <br>");
                }
            }
            writeSystem.Add("</h4>");
            writeSystem.Add("<h4> </h4>");
        }
        static public void intAver(List<int> Sorter, List<SuperBowlObject> SuburbList, List<string> writeSystem)
        {
            int counter = 0;
            foreach (SuperBowlObject x in SuburbList)
            {
                Sorter.Add(SuburbList[counter].Attend);
                counter = counter + 1;
            }
            writeSystem.Add("<h1> Average Attendce  </h1> <br>");
            writeSystem.Add("<h4> &emsp; " + Convert.ToString(Sorter.Average()) + "</h4> <br>");
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
            writeSystem.Add("<h1>  Biggest Point Diffrence </h1> <br>");
            writeSystem.Add("<h4> &emsp; " + SuburbList[Point_Dif.IndexOf(Point_Dif.Max())].Roman + "</h4> <br>");
        }
        static public void intMax(List<int> Sorter, List<SuperBowlObject> SuburbList, int strenght, List<string> writeSystem)
        {
            int counter = 0;
            writeSystem.Add("<h1> Top 5 Attended super bowls  </h1> <br>");
            foreach (SuperBowlObject x in SuburbList)
            {
                Sorter.Add((SuburbList[counter].Attend));
                counter = counter + 1;
            }

            var top = (from i in Sorter
                   orderby i descending
                   select i).Take(strenght);
            writeSystem.Add("<h4>");

            foreach (var x in top)
            {
                int topFive = Sorter.IndexOf(x);
                writeSystem.Add("&emsp; " + SuburbList[topFive].Roman + "<br>");

            }
            writeSystem.Add("</h4>");
            writeSystem.Add("<h4> </h4>");
        }
    }
}
