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
            { ">   >   > ", "", "", "" },
            { "          ", "", "", "" },
            { "  >   >   ", "", "", "" },
            { "----------", "", "", "" },
        };

        private static string[,] _horizontalFinish = new string[,] {
            { "----------", "", "", "" },
            { "#         ", "", "", "" },
            { "#         ", "", "", "" },
            { "#         ", "", "", "" },
            { "----------", "", "", "" },
        };

        private static string[,] _corner0 = new string[,] //corner from south to east
        {
            { "", "-----", "", "-\\" },
            { "", "      ", "", " \\" },
            { "", "       ", "", " \\" },
            { "", "        ", "", " \\" },
            { "--\\", "      ", "", "|" }};

        private static string[,] _corner1 = new string[,] //corner from west to south
        {
            { "--/", "      ", "", "|" },
            { "", "        ", "", " /" },
            { "", "        ", "", "/" },
            { "", "       ", "", "/" },
            { "--", "----", "", "/" }};

        private static string[,] _corner2 = new string[,] //corner from 
        {
            { "|", "      ", "", "\\--" },
            { "\\", "         ", "", "" },
            { " \\", "          ", "", "" },
            { "  \\", "           ", "", "" },
            { "   \\", "------", "", "" }};

        private static string[,] _corner3 = new string[,]
            {
            { "/", "------", "", "" },
            { "/", "       ", "", "" },
            { "/", "       ", "", "" },
            { "/", "       ", "", "" },
            { "|", "       ", "", "" },
            };


        //private static string[,] _finishHorizontal = { "----", "  # ", "  # ", "----" };
        //                                                    ;
        #endregion

        private static int CoordinateX;
        private static int CoordinateY;
        private static int Direction;
        private static int LineNumber;
        private static int startingX;
        private static int startingY;

        public static void Initialize()
        {
            CoordinateX = 40;
            CoordinateY = 1;
            Direction = 0; //0 = east 1 = south 2 = west 3 = north
            LineNumber = 0;
        }

        public static void DrawTrack(Track track)
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine(track.Name);

            foreach (Section section in track.Sections)
            {
                Console.WriteLine(Direction);
                switch (section.SectionType)
                {
                    case SectionTypes.StartGrid:
                        startingX = CoordinateX;
                        startingY = CoordinateY;
                        Console.SetCursorPosition(CoordinateX, CoordinateY);
                        foreach (string line in _horizontalStartingGrid)
                        {
                            Thread.Sleep(50);
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
                            Thread.Sleep(50);

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
                                Thread.Sleep(50);

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
                                Thread.Sleep(50);

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
                                Thread.Sleep(50);
                                
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
                            //Direction++;
                        } else if (Direction == 1)
                        {
                            int startingX = (CoordinateX - 2);
                            int startingY = CoordinateY;
                            Console.SetCursorPosition(CoordinateX-2, CoordinateY);
                            foreach (string line in _corner1)
                            {
                                Thread.Sleep(50);

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
                                Thread.Sleep(50);

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
                        }
                        Direction++;
                        break;
                }
                //Console.SetCursorPosition(8, 8);
                //foreach (string line in _finishHorizontal)
                //{
                //    Console.WriteLine(line);
                //}
            }
        }
    }
}
