using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Race_simulator
{
    static class Visualize
    {
        #region graphics
        private static string[,] _verticalStraight = new string[,] {
            { "|", "   ", "   ", "|" },
            { "|", "   ", "   ", "|" },
            { "|", "   ", "   ", "|" },
            { "|", "   ", "   ", "|" } };

        private static string[,] _horizontalStraight = new string[,] {
            { "----------", "", "", "" },
            { "          ", "", "", "" },
            { "          ", "", "", "" },
            { "          ", "", "", "" },
            { "----------", "", "", "" },
        };

        private static string[,] _verticalFinish = new string[,] {
            { "|", "   ", "   ", "|" },
            { "|", " # ", " # ", "|" },
            { "|", "   ", "   ", "|" },
            { "|", "   ", "   ", "|" } };

        private static string[,] _horizontalStartingGrid = new string[,] {
            { "----------", "", "", "" },
            { "        ", ">", " ", "" },
            { "          ", "", "", "" },
            { "      ", ">", "   ", "" },
            { "----------", "", "", "" },
        };

        private static string[,] _horizontalFinish = new string[,] {
            { "----------", "", "", "" },
            { "#         ", "", "", "" },
            { "#         ", "", "", "" },
            { "#         ", "", "", "" },
            { "----------", "", "", "" },
        };

        //private static string[,] _corner0 = new string[,] //corner from south to east
        //{
        //    { "", "-----", "", "-\\   " },
        //    { "", "      ", "", " \\  " },
        //    { "", "       ", "", " \\ " },
        //    { "", "        ", "", " \\" },
        //    { "--\\", "      ", "", "|" }};

        private static string[,] _corner0 = new string[,] //corner from south to east
        {
            { "", "--------", "", "-\\" },
            { "", "        ", "", " |" },
            { "", "        ", "", " |" },
            { "", "         ", "", "|" },
            { "--\\", "      ", "", "|" }};

        //private static string[,] _corner1 = new string[,] //corner from west to south
        //{
        //    { "--/", "      ", "", "|" },
        //    { "", "        ", "", " /" },
        //    { "", "        ", "", "/ " },
        //    { "", "       ", "", "/  " },
        //    { "--", "----", "", "/   " }};

        private static string[,] _corner1 = new string[,] //corner from west to south
        {
            { "--/", "      ", "", "|" },
            { "", "        ", "", " |" },
            { "", "        ", "", " |" },
            { "", "       ", "", "  |" },
            { "--", "-------", "", "/" }};

        //private static string[,] _corner2 = new string[,] //corner from 
        //{
        //    { "|", "      ", "", "\\--" },
        //    { "\\", "         ", "", "" },
        //    { " \\", "          ", "", "" },
        //    { "  \\", "           ", "", "" },
        //    { "   \\", "------", "", "" }};

        private static string[,] _corner2 = new string[,] //corner from 
        {
            { "|", "      ", "", "\\--" },
            { "|", "         ", "", "" },
            { "|", "          ", "", "" },
            { "|", "           ", "", "" },
            { "\\", "---------", "", "" }};

        //private static string[,] _corner3 = new string[,]
        //    {
        //    { "   /", "-------", "", "" },
        //    { "  /", "        ", "", "" },
        //    { " /", "         ", "", "" },
        //    { "/", "          ", "", "" },
        //    { "|", "      ", "/", "---" },
        //    };


        private static string[,] _corner3 = new string[,]
            {
            { "/", "---------", "", "" },
            { "|", "         ", "", "" },
            { "|", "         ", "", "" },
            { "|", "          ", "", "" },
            { "|", "      ", "/", "---" },
            };


        //private static string[,] _finishHorizontal = { "----", "  # ", "  # ", "----" };
        //                                                    ;
        #endregion

        private static int Direction;
        private static int LineNumber;
        private static int CoordinateX;
        private static int CoordinateY;
        private static int startingX;
        private static int startingY;
        private static IParticipant Winner;
        private static IParticipant Loser;
        public static bool RaceFinished;
        private static bool ShownScoreScreen;

        private static int NumberOfRounds;

        private static int[] Player1Position;
        private static int Player1Direction;
        private static int Player1WentOverFinish;

        private static int[] Player2Position;
        private static int Player2Direction;
        private static int Player2WentOverFinish;

        private static bool test = false;
        public static void Initialize()
        {
            Player1Position = new int[2];
            Player1Position[0] = 48;
            Player1Position[1] = 2;
            Player1Direction = 0;

            Player2Position = new int[2];
            Player2Position[0] = 46;
            Player2Position[1] = 4;
            Player2Direction = 0;

            CoordinateX = 40;
            CoordinateY = 1;
            Direction = 0; //0 = east 1 = south 2 = west 3 = north
            LineNumber = 0;
            NumberOfRounds = 2;
            RaceFinished = false;
            ShownScoreScreen = false;
            Player2WentOverFinish = 0;

            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.CurrentRace.NextRace += OnStartNextRace;


            Winner = new Driver();
            Loser = new Driver();
        }

        public static void DrawTrack(Track track)
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Blue;

            foreach (Section section in track.Sections)
            {
                //Console.WriteLine(Direction);
                switch (section.SectionType)
                {
                    case SectionTypes.StartGrid:
                        //SectionData sectionData = Data.CurrentRace.GetSectionData(section);
                        foreach (Model.IParticipant participant in Data.CurrentRace.Participants)
                        {
                            _horizontalStartingGrid = AddParticipantsToGraphics(_horizontalStartingGrid, participant);
                        }
                        

                        startingX = CoordinateX;
                        startingY = CoordinateY;
                        Console.SetCursorPosition(CoordinateX, CoordinateY);
                        foreach (string line in _horizontalStartingGrid)
                        {
                            Console.Write(line);
                            LineNumber++;
                            if (LineNumber == 4)
                            {
                                CoordinateY++;
                                Console.SetCursorPosition(CoordinateX, CoordinateY);
                                LineNumber = 0;
                            }
                        }
                        CoordinateX = startingX + 10;
                        CoordinateY = startingY;
                        break;

                    case SectionTypes.Finish:
                        startingX = CoordinateX;
                        startingY = CoordinateY;
                        Console.SetCursorPosition(CoordinateX, CoordinateY);
                        foreach (string line in _horizontalFinish)
                        {
                            Console.Write(line);
                            LineNumber++;
                            if (LineNumber == 4)
                            {
                                CoordinateY++;
                                Console.SetCursorPosition(CoordinateX, CoordinateY);
                                LineNumber = 0;
                            }
                        }
                        CoordinateX = startingX + 10;
                        CoordinateY = startingY;
                        break;

                    case SectionTypes.Straight:
                        if (Direction == 0 || Direction == 2)
                        {
                            startingX = CoordinateX;
                            startingY = CoordinateY;
                            Console.SetCursorPosition(CoordinateX, CoordinateY);
                            foreach (string line in _horizontalStraight)
                            {
                                Console.Write(line);
                                LineNumber++;
                                if (LineNumber == 4)
                                {
                                    CoordinateY++;
                                    Console.SetCursorPosition(CoordinateX, CoordinateY);
                                    LineNumber = 0;
                                }
                            }
                            if (Direction == 0)
                            {
                                CoordinateX = startingX + 10;
                                CoordinateY = startingY;
                            } else if(Direction == 2)
                            {
                                CoordinateX = startingX - 10;
                                CoordinateY = startingY;
                            }                         
                        }
                        else
                        {
                            startingX = CoordinateX;
                            startingY = CoordinateY;
                            Console.SetCursorPosition(CoordinateX, CoordinateY);
                            foreach (string line in _verticalStraight)
                            {
                                Console.Write(line);
                                LineNumber++;
                                if (LineNumber == 4)
                                {
                                    CoordinateY++;
                                    Console.SetCursorPosition(CoordinateX, CoordinateY);
                                    LineNumber = 0;
                                }
                            }
                            if(Direction == 1)
                            {
                                CoordinateX = startingX;
                                CoordinateY = startingY + 4;
                            }else if(Direction == 3)
                            {
                                CoordinateX = startingX;
                                CoordinateY = startingY - 4;
                            }
                        }
                        break;

                    case SectionTypes.RightCorner:
                        if (Direction == 0)
                        {
                            int startingX = CoordinateX;
                            int startingY = CoordinateY;
                            Console.SetCursorPosition(CoordinateX, CoordinateY);
                            foreach (string line in _corner0)
                            {
                                Console.Write(line);
                                LineNumber++;
                                if (LineNumber == 4)
                                {
                                    CoordinateY++;
                                    Console.SetCursorPosition(CoordinateX, CoordinateY);
                                    LineNumber = 0;
                                }
                            }

                            CoordinateX = startingX + 2;
                            CoordinateY = startingY + 5;
                        } else if (Direction == 1)
                        {
                            int startingX = (CoordinateX - 2);
                            int startingY = CoordinateY;
                            Console.SetCursorPosition(CoordinateX-2, CoordinateY);
                            foreach (string line in _corner1)
                            {
                                Console.Write(line);
                                LineNumber++;
                                if (LineNumber == 4)
                                {
                                    CoordinateY++;
                                    Console.SetCursorPosition(CoordinateX-2, CoordinateY);
                                    LineNumber = 0;
                                }
                            }
                            CoordinateX = startingX - 10;
                            CoordinateY = startingY;
                        } else if(Direction == 2)
                        {
                            int startingX = CoordinateX;
                            int startingY = CoordinateY;
                            Console.SetCursorPosition(CoordinateX, CoordinateY);
                            foreach (string line in _corner2)
                            {
                                Console.Write(line);
                                LineNumber++;
                                if (LineNumber == 4)
                                {
                                    CoordinateY++;
                                    Console.SetCursorPosition(CoordinateX, CoordinateY);
                                    LineNumber = 0;
                                }
                            }
                            CoordinateX = startingX;
                            CoordinateY = startingY - 4;
                        } else if(Direction == 3)
                        {
                            int startingX = CoordinateX;
                            int startingY = CoordinateY;
                            Console.SetCursorPosition(CoordinateX, CoordinateY-1);
                            foreach (string line in _corner3)
                            {
                                Console.Write(line);
                                LineNumber++;
                                if (LineNumber == 4)
                                {
                                    CoordinateY++;
                                    Console.SetCursorPosition(CoordinateX, CoordinateY-1);
                                    LineNumber = 0;
                                }
                            }
                            CoordinateX = startingX + 10;
                            CoordinateY = startingY - 1;
                        }
                        Direction++;
                        if(Direction == 4)
                        {
                            Direction = 0;
                        }
                        break;
                }
            }
        }

        private static string[,] AddParticipantsToGraphics(string[,] graphics, IParticipant participant)
        {
            string[,] newSection = graphics;
            bool isReplaced = false;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (newSection[i, j] == ">" && isReplaced == false)
                    {
                        newSection[i, j] = participant.Name.Substring(0, 1);
                        isReplaced = true;
                    }
                }
            }
            return newSection;
        }
        public static void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            IParticipant Player1 = e.Participants[0];
            double Player1MovementSpeedTemporary = (Player1.Equipment.Performance * Player1.Equipment.Speed / 4)+1;
            Player1MovementSpeedTemporary = Math.Ceiling(Player1MovementSpeedTemporary);
            int Player1MovementSpeed = (int)Convert.ToUInt32(Player1MovementSpeedTemporary);

            IParticipant Player2 = e.Participants[1];
            double Player2MovementSpeedTemporary = Player2.Equipment.Performance * Player2.Equipment.Speed / 4;
            Player2MovementSpeedTemporary = Math.Ceiling(Player2MovementSpeedTemporary);
            int Player2MovementSpeed = (int)Convert.ToUInt32(Player2MovementSpeedTemporary);

            Player1MovementSpeed = 10;
            Player2MovementSpeed = 10;


            if (Player1WentOverFinish == NumberOfRounds+1)
            {
                if (Winner != Player2)
                {
                    Winner = Player1;
                }
                else
                {
                    Loser = Player1;
                }
                //Console.Clear();
                //Console.WriteLine("Speler 1 heeft gewonnen");
            }
            else if (Player2WentOverFinish == NumberOfRounds+1)
            {
                if (Winner != Player1)
                {
                    Winner = Player2;
                }
                else
                {
                    Loser = Player2;
                }
                //Console.Clear();
                //Console.WriteLine("Speler 2 heeft gewonnen");
            }

            //Console.WriteLine("test");
            if(e.Track.Name == "Monaco")
            {
                if (Player1Direction == 0)
                {
                    Console.SetCursorPosition(Player1Position[0], Player1Position[1]);
                    Console.Write(" ");
                    Player1Position[0] += Player1MovementSpeed;
                    //Player1Position[1]++;
                    if (Player1Position[0] > 78)
                    {
                        Player1Position[0] = 78;
                        Player1Position[1] += 1;
                    }
                    Console.SetCursorPosition(Player1Position[0], Player1Position[1]);
                    Console.WriteLine(Player1.Name.Substring(0, 1));
                    if (Player1Position[0] == 78)
                    {
                        Player1Direction++;
                    }
                }
            }

            if (e.Track.Name == "Zandvoort" && Player1WentOverFinish <= NumberOfRounds || e.Track.Name == "Zandvoort" && Player2WentOverFinish <= NumberOfRounds)
            {
                DrawFinishLine("Zandvoort");

                //Console.SetCursorPosition(CoordinateX, CoordinateY);
                //Console.SetCursorPosition(49, 4);
                //Console.WriteLine(1);

                switch (Player1MovementSpeed)
                {
                    case 1:
                        if (Player1Position[0] == 48 && Player1Position[1] == 2)
                        {
                            Player1WentOverFinish++;
                        }
                        break;
                    case 2:
                        if (Player1Position[0] == 48 && Player1Position[1] == 2 || Player1Position[0] == 49 && Player1Position[1] == 2)
                        {
                            Player1WentOverFinish++;
                        }
                        break;
                    case 3:
                        if (Player1Position[0] >= 48 && Player1Position[0] <= 50 && Player1Position[1] == 2)
                        {
                            Player1WentOverFinish++;
                        }
                        break;
                    case 4:
                        if (Player1Position[0] >= 48 && Player1Position[0] <= 51 && Player1Position[1] == 2)
                        {
                            Player1WentOverFinish++;
                        }
                        break;
                    case 5:
                        if (Player1Position[0] >= 48 && Player1Position[0] <= 52 && Player1Position[1] == 2)
                        {
                            Player1WentOverFinish++;
                        }
                        break;
                    case 6:
                        if (Player1Position[0] >= 48 && Player1Position[0] <= 53 && Player1Position[1] == 2)
                        {
                            Player1WentOverFinish++;
                        }
                        break;
                    case 7:
                        if (Player1Position[0] >= 48 && Player1Position[0] <= 54 && Player1Position[1] == 2)
                        {
                            Player1WentOverFinish++;
                        }
                        break;
                    case 8:
                        if (Player1Position[0] >= 48 && Player1Position[0] <= 55 && Player1Position[1] == 2)
                        {
                            Player1WentOverFinish++;
                        }
                        break;
                    case 9:
                        if (Player1Position[0] >= 48 && Player1Position[0] <= 56 && Player1Position[1] == 2)
                        {
                            Player1WentOverFinish++;
                        }
                        break;
                    case 10:
                        if (Player1Position[0] >= 48 && Player1Position[0] <= 57 && Player1Position[1] == 2)
                        {
                            Player1WentOverFinish++;
                        }
                        break;
                    case 11:
                        if (Player1Position[0] >= 48 && Player1Position[0] <= 58 && Player1Position[1] == 2)
                        {
                            Player1WentOverFinish++;
                        }
                        break;
                    case 12:
                        if (Player1Position[0] >= 48 && Player1Position[0] <= 59 && Player1Position[1] == 2)
                        {
                            Player1WentOverFinish++;
                        }
                        break;
                    case 13:
                        if (Player1Position[0] >= 48 && Player1Position[0] <= 60 && Player1Position[1] == 2)
                        {
                            Player1WentOverFinish++;
                        }
                        break;
                    case 14:
                        if (Player1Position[0] >= 48 && Player1Position[0] <= 61 && Player1Position[1] == 2)
                        {
                            Player1WentOverFinish++;
                        }
                        break;
                }
                switch (Player2MovementSpeed)
                {
                    case 1:
                        if (Player2Position[0] == 48 && Player2Position[1] == 4)
                        {
                            Player2WentOverFinish++;
                        }
                        break;
                    case 2:
                        if (Player2Position[0] == 48 && Player2Position[1] == 4 || Player2Position[0] == 49 && Player2Position[1] == 4)
                        {
                            Player2WentOverFinish++;
                        }
                        break;
                    case 3:
                        if (Player2Position[0] >= 48 && Player2Position[0] <= 50 && Player2Position[1] == 4)
                        {
                            Player2WentOverFinish++;
                        }
                        break;
                    case 4:
                        if (Player2Position[0] >= 48 && Player2Position[0] <= 51 && Player2Position[1] == 4)
                        {
                            Player2WentOverFinish++;
                        }
                        break;
                    case 5:
                        if (Player2Position[0] >= 48 && Player2Position[0] <= 52 && Player2Position[1] == 4)
                        {
                            Player2WentOverFinish++;
                        }
                        break;
                    case 6:
                        if (Player2Position[0] >= 48 && Player2Position[0] <= 53 && Player2Position[1] == 4)
                        {
                            Player2WentOverFinish++;
                        }
                        break;
                    case 7:
                        if (Player2Position[0] >= 48 && Player2Position[0] <= 54 && Player2Position[1] == 4)
                        {
                            Player2WentOverFinish++;
                        }
                        break;
                    case 8:
                        if (Player2Position[0] >= 48 && Player2Position[0] <= 55 && Player2Position[1] == 4)
                        {
                            Player2WentOverFinish++;
                        }
                        break;
                    case 9:
                        if (Player2Position[0] >= 48 && Player2Position[0] <= 56 && Player2Position[1] == 4)
                        {
                            Player2WentOverFinish++;
                        }
                        break;
                    case 10:
                        if (Player2Position[0] >= 48 && Player2Position[0] <= 57 && Player2Position[1] == 4)
                        {
                            Player2WentOverFinish++;
                        }
                        break;
                    case 11:
                        if (Player2Position[0] >= 48 && Player2Position[0] <= 58 && Player2Position[1] == 4)
                        {
                            Player2WentOverFinish++;
                        }
                        break;
                    case 12:
                        if (Player2Position[0] >= 48 && Player2Position[0] <= 59 && Player2Position[1] == 4)
                        {
                            Player2WentOverFinish++;
                        }
                        break;
                    case 13:
                        if (Player2Position[0] >= 48 && Player2Position[0] <= 60 && Player2Position[1] == 4)
                        {
                            Player2WentOverFinish++;
                        }
                        break;
                }
  
                if (Player1WentOverFinish <= NumberOfRounds)
                {

                    if (Player1Direction == 0)
                    {
                        Console.SetCursorPosition(Player1Position[0], Player1Position[1]);
                        Console.Write(" ");
                        Player1Position[0] += Player1MovementSpeed;
                        //Player1Position[1]++;
                        if (Player1Position[0] > 78)
                        {
                            Player1Position[0] = 78;
                            Player1Position[1] += 1;
                        }
                        Console.SetCursorPosition(Player1Position[0], Player1Position[1]);
                        Console.WriteLine(Player1.Name.Substring(0, 1));
                        if (Player1Position[0] == 78)
                        {
                            Player1Direction++;
                        }
                    }
                    else if (Player1Direction == 1)
                    {
                        Console.SetCursorPosition(Player1Position[0], Player1Position[1]);
                        Console.Write(" ");
                        //Player1Position[0]++;
                        Player1Position[1] += Player1MovementSpeed;
                        if (Player1Position[1] > 21)
                        {
                            Player1Position[1] = 21;
                            Player1Position[0] -= 1;
                        }
                        Console.SetCursorPosition(Player1Position[0], Player1Position[1]);
                        Console.WriteLine(Player1.Name.Substring(0, 1));
                        if (Player1Position[1] == 21)
                        {
                            Player1Direction++;
                        }
                    }
                    else if (Player1Direction == 2)
                    {
                        Console.SetCursorPosition(Player1Position[0], Player1Position[1]);
                        Console.Write(" ");
                        Player1Position[0] -= Player1MovementSpeed;
                        //Player1Position[1];
                        if (Player1Position[0] < 11)
                        {
                            Player1Position[0] = 11;
                            //Player1Position[1] -= 1;
                        }
                        Console.SetCursorPosition(Player1Position[0], Player1Position[1]);
                        Console.WriteLine(Player1.Name.Substring(0, 1));
                        if (Player1Position[0] == 11)
                        {
                            Player1Direction++;
                        }
                    }
                    else if (Player1Direction == 3)
                    {
                        Console.SetCursorPosition(Player1Position[0], Player1Position[1]);
                        Console.Write(" ");
                        Player1Position[1] -= Player1MovementSpeed;
                        Player1Position[1]--;
                        if (Player1Position[1] < 2)
                        {
                            Player1Position[1] = 2;
                            Player1Position[0]++;
                        }
                        Console.SetCursorPosition(Player1Position[0], Player1Position[1]);
                        Console.WriteLine(Player1.Name.Substring(0, 1));
                        if (Player1Position[1] == 2)
                        {
                            Player1Direction = 0;
                        }
                    }
                } else
                {
                    Console.SetCursorPosition(Player1Position[0], Player1Position[1]);
                    Console.Write(" ");
                }

                if (Player2WentOverFinish <= NumberOfRounds)
                {
                    if (Player2Direction == 0)
                    {
                        //Console.Write(" ");
                        //Console.SetCursorPosition(45, 4);
                        //Console.Write(" ");
                        Console.SetCursorPosition(Player2Position[0], Player2Position[1]);
                        Console.Write(" ");
                        Player2Position[0] += Player2MovementSpeed;
                        //Player1Position[1]++;
                        if (Player2Position[0] > 73)
                        {
                            Player2Position[0] = 73;
                            //Player2Position[1] += 1;
                        }
                        Console.SetCursorPosition(Player2Position[0], Player2Position[1]);
                        Console.WriteLine(Player2.Name.Substring(0, 1));
                        if (Player2Position[0] == 73)
                        {
                            Player2Direction++;
                        }
                    }
                    else if (Player2Direction == 1)
                    {
                        Console.SetCursorPosition(Player2Position[0], Player2Position[1]);
                        Console.Write(" ");
                        //Player1Position[0]++;
                        Player2Position[1] += Player2MovementSpeed;
                        if (Player2Position[1] > 18)
                        {
                            Player2Position[1] = 19;
                            //Player2Position[0] -= 1;
                        }
                        Console.SetCursorPosition(Player2Position[0], Player2Position[1]);
                        Console.WriteLine(Player2.Name.Substring(0, 1));
                        if (Player2Position[1] == 19)
                        {
                            Player2Direction++;
                        }
                    }
                    else if (Player2Direction == 2)
                    {
                        Console.SetCursorPosition(Player2Position[0], Player2Position[1]);
                        Console.Write(" ");
                        Player2Position[0] -= Player2MovementSpeed;
                        //Player1Position[1];
                        if (Player2Position[0] < 16)
                        {
                            Player2Position[0] = 16;
                            //Player1Position[1] -= 1;
                        }
                        Console.SetCursorPosition(Player2Position[0], Player2Position[1]);
                        Console.WriteLine(Player2.Name.Substring(0, 1));
                        if (Player2Position[0] == 16)
                        {
                            Player2Direction++;
                        }
                    }
                    else if (Player2Direction == 3)
                    {
                        Console.SetCursorPosition(Player2Position[0], Player2Position[1]);
                        Console.Write(" ");
                        Player2Position[1] -= Player1MovementSpeed;
                        Player2Position[1]--;
                        if (Player2Position[1] < 4)
                        {
                            Player2Position[1] = 4;
                            Player2Position[0]++;
                        }
                        Console.SetCursorPosition(Player2Position[0], Player2Position[1]);
                        Console.WriteLine(Player2.Name.Substring(0, 1));
                        if (Player2Position[1] == 4)
                        {
                            Player2Direction = 0;
                        }
                    }
                }
                else
                {
                    Console.SetCursorPosition(Player2Position[0], Player2Position[1]);
                    Console.Write(" ");
                }
            }
            else if (ShownScoreScreen == false && e.EveryoneHasFinished == false)
            {
                Console.Clear();
                Console.WriteLine("Race gestopt");
                Console.WriteLine($"{Winner.Name} heeft de race gewonnen");
                Console.WriteLine($"{Loser.Name} heeft verloren");
                //Thread.Sleep(100000);
                Console.Clear();
                ShownScoreScreen = true;
                e.EveryoneHasFinished = true;
            }

        }

        public static void DrawFinishLine(string trackName)
        {
            if (trackName == "Zandvoort")
            {
                Console.SetCursorPosition(50, 2);
                Console.Write("#");
                Console.SetCursorPosition(50, 3);
                Console.Write("#");
                Console.SetCursorPosition(50, 4);
                Console.Write("#");

                Console.SetCursorPosition(CoordinateX, CoordinateY);
            } else if(trackName == "Monaco")
            {
                Console.SetCursorPosition(50, 2);
                Console.Write("#");
                Console.SetCursorPosition(50, 3);
                Console.Write("#");
                Console.SetCursorPosition(50, 4);
                Console.Write("#");

                Console.SetCursorPosition(CoordinateX, CoordinateY);
            }
        }
        public static void OnStartNextRace(object sender, EventArgs e)
        {
            Data.CurrentRace.DriversChanged -= OnDriversChanged;
            //Data.CurrentRace.NextRace -= OnStartNextRace;

            Data.NextRace();
            if (Data.CurrentRace != null)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Initialize();
                DrawTrack(Data.CurrentRace.track);
            }
            else
            {
                Console.WriteLine("Er is geen race meer beschikbaar");
            }
        }
    }
}
