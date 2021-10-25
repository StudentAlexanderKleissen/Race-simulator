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

        private static string[,] _corner2 = new string[,] //corner from 
        {
            { "|", "      ", "", "\\--" },
            { "\\", "         ", "", "" },
            { " \\", "          ", "", "" },
            { "  \\", "           ", "", "" },
            { "   \\", "------", "", "" }};

        private static string[,] _corner3 = new string[,]
            {
            { "   /", "-------", "", "" },
            { "  /", "        ", "", "" },
            { " /", "         ", "", "" },
            { "/", "          ", "", "" },
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
        private static int[] Player1Position;
        private static int Player1Direction;

        public static void Initialize()
        {
            CoordinateX = 40;
            CoordinateY = 1;
            Direction = 0; //0 = east 1 = south 2 = west 3 = north
            LineNumber = 0;

            Data.CurrentRace.DriversChanged += OnDriversChanged;
        }

        public static void DrawTrack(Track track)
        {
            if (track.Name == "Zandvoort") { 
                Player1Position = new int[2];
                Player1Position[0] = 48;
                Player1Position[1] = 2;
                Player1Direction = 0;
        }
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine(track.Name);
            

            foreach (Section section in track.Sections)
            {
                Console.WriteLine(Direction);
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
                    if(newSection[i,j] == ">" && isReplaced == false)
                    {
                        newSection[i, j] = participant.Name.Substring(0,1);
                        isReplaced = true;
                    }
                }
            }
            return newSection;
        }
        public static void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {

            if(e.Track.Name == "Zandvoort")
            {
                Console.SetCursorPosition(50, 2);
                Console.Write("#");
                Console.SetCursorPosition(50, 3);
                Console.Write("#");
                Console.SetCursorPosition(50, 4);
                Console.Write("#");
                
                Console.SetCursorPosition(CoordinateX, CoordinateY);
            }
            

            if (Player1Direction == 0 ) { 
                Console.SetCursorPosition(Player1Position[0], Player1Position[1]);
                Console.Write(" ");
                Player1Position[0]++;
                //Player1Position[1]++;
                Console.SetCursorPosition(Player1Position[0], Player1Position[1]);
                Console.WriteLine("A");
                if(Player1Position[0] == 78)
                {
                    Player1Direction++;
                }
            } else if (Player1Direction == 1)
            {
                Console.SetCursorPosition(Player1Position[0], Player1Position[1]);
                Console.Write(" ");
                //Player1Position[0]++;
                Player1Position[1]++;
                Console.SetCursorPosition(Player1Position[0], Player1Position[1]);
                Console.WriteLine("A");
            }
        }
    }
}
