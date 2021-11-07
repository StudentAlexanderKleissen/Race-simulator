using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Media.Imaging;

namespace Graphics
{
    public static class Visualize
    {
        #region graphics
        private const string _StartGrid = "C:\\Users\\Alexander\\source\\repos\\Race-simulator\\Graphics\\Images\\StartGrid.png";
        private const string _Finish = "C:\\Users\\Alexander\\source\\repos\\Race-simulator\\Graphics\\Images\\Finish.png";

        private const string _StraightVertical = "C:\\Users\\Alexander\\source\\repos\\Race-simulator\\Graphics\\Images\\StraightVertical.png";
        //private const string _StraightHorizontal = ".\\Images\\StraightHorizontal.png";

        private const string _StraightHorizontal = "C:\\Users\\Alexander\\source\\repos\\Race-simulator\\Graphics\\Images\\StraightHorizontal.png";
        //private const string _StraightHorizontal = "C:\\Users\\Alexander\\source\repos\\Race-simulator\\Graphics\\Images\\StraightHorizontal.png";

        private const string _Turn0 = "C:\\Users\\Alexander\\source\\repos\\Race-simulator\\Graphics\\Images\\Turn0.png";
        private const string _Turn1 = "C:\\Users\\Alexander\\source\\repos\\Race-simulator\\Graphics\\Images\\Turn1.png";
        private const string _Turn2 = "C:\\Users\\Alexander\\source\\repos\\Race-simulator\\Graphics\\Images\\Turn2.png";
        private const string _Turn3 = "C:\\Users\\Alexander\\source\\repos\\Race-simulator\\Graphics\\Images\\Turn3.png";

        private const string _CarRed = "C:\\Users\\Alexander\\source\\repos\\Race-simulator\\Graphics\\Images\\Red_Race_Car.png";
        //private const string _CarRed = "C:\\Users\\Alexander\\Desktop\\race-simulator-master\\WpfApp\\Images\\CarBlue.png";
        private const string _CarBlue = "C:\\Users\\Alexander\\source\\repos\\Race-simulator\\Graphics\\Images\\CarBlue.png";
        private const string _Broken = "C:\\Users\\Alexander\\source\\repos\\Race-simulator\\Graphics\\Images\\Broken.png";

        #endregion graphics

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

        private static bool ZandvoortHasBeenRacedOn;
        private static bool MonacoHasBeenRacedOn;

        private static int test123;

        public static void Initialize()
        {
            Player1Position = new int[2];
            Player1Position[0] = 48;
            Player1Position[1] = 2;
            Player1Direction = 0;
            Player1WentOverFinish = 0;

            Player2Position = new int[2];
            Player2Position[0] = 46;
            Player2Position[1] = 4;
            Player2Direction = 0;
            Player2WentOverFinish = 0;

            startingX = 0;
            startingY = 0;

            Direction = 0; //0 = east 1 = south 2 = west 3 = north
            LineNumber = 0;
            NumberOfRounds = 2;
            RaceFinished = false;
            ShownScoreScreen = false;

            //Data.CurrentRace.DriversChanged += OnDriversChanged;
            //Data.CurrentRace.NextRace += OnStartNextRace;

            Winner = new Driver();
            Loser = new Driver();
        }

        public static BitmapSource DrawTrack(Track track)
        {
            CoordinateX = 200;
            CoordinateY = 10;

            Bitmap bitmap = UseImages.GetEmptyBitmap(500, 500);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);

            //graphics.DrawImage(new Bitmap(UseImages.Get(_CarRed), 25, 25), new Point(32, 32))

            foreach (Section section in track.Sections)
            {
                //Console.WriteLine(Direction); 
                switch (section.SectionType)
                {

                    case SectionTypes.RightCorner:
                        if (Direction == 0)
                        {

                            startingX = CoordinateX;
                            startingY = CoordinateY;
                            graphics.DrawImage(UseImages.Get(_Turn0), new Point(CoordinateX, CoordinateY));
                            CoordinateX = startingX;
                            CoordinateY = startingY + 64;

                            //    int startingX = CoordinateX;
                            //    int startingY = CoordinateY;
                            //    Console.SetCursorPosition(CoordinateX, CoordinateY);
                            //    foreach (string line in _corner0)
                            //    {
                            //        Console.Write(line);
                            //        LineNumber++;
                            //        if (LineNumber == 4)
                            //        {
                            //            CoordinateY++;
                            //            Console.SetCursorPosition(CoordinateX, CoordinateY);
                            //            LineNumber = 0;
                            //        }
                            //    }

                            //    CoordinateX = startingX + 2;
                            //    CoordinateY = startingY + 5;
                        }
                        else if (Direction == 1)
                        {
                            //    int startingX = (CoordinateX - 2);
                            //    int startingY = CoordinateY;
                            //    Console.SetCursorPosition(CoordinateX - 2, CoordinateY);
                            //    foreach (string line in _corner1)
                            //    {
                            //        Console.Write(line);
                            //        LineNumber++;
                            //        if (LineNumber == 4)
                            //        {
                            //            CoordinateY++;
                            //            Console.SetCursorPosition(CoordinateX - 2, CoordinateY);
                            //            LineNumber = 0;
                            //        }
                            //    }
                            //    CoordinateX = startingX - 10;
                            //    CoordinateY = startingY;
                            startingX = CoordinateX;
                            startingY = CoordinateY;
                            graphics.DrawImage(UseImages.Get(_Turn1), new Point(CoordinateX, CoordinateY));
                            CoordinateX = startingX - 64;
                            CoordinateY = startingY;
                        }
                        else if (Direction == 2)
                        {
                            startingX = CoordinateX;
                            startingY = CoordinateY;
                            graphics.DrawImage(UseImages.Get(_Turn2), new Point(CoordinateX, CoordinateY));
                            CoordinateX = startingX;
                            CoordinateY = startingY - 64;
                            //    Console.SetCursorPosition(CoordinateX, CoordinateY);
                            //    foreach (string line in _corner2)
                            //    {
                            //        Console.Write(line);
                            //        LineNumber++;
                            //        if (LineNumber == 4)
                            //        {
                            //            CoordinateY++;
                            //            Console.SetCursorPosition(CoordinateX, CoordinateY);
                            //            LineNumber = 0;
                            //        }
                            //    }
                            //    CoordinateX = startingX;
                            //    CoordinateY = startingY - 4;
                        }
                        else if (Direction == 3)
                        {
                            startingX = CoordinateX;
                            startingY = CoordinateY;
                            graphics.DrawImage(UseImages.Get(_Turn3), new Point(CoordinateX, CoordinateY));
                            CoordinateX = startingX + 64;
                            CoordinateY = startingY;
                            //    int startingX = CoordinateX;
                            //    int startingY = CoordinateY;
                            //    Console.SetCursorPosition(CoordinateX, CoordinateY - 1);
                            //    foreach (string line in _corner3)
                            //    {
                            //        Console.Write(line);
                            //        LineNumber++;
                            //        if (LineNumber == 4)
                            //        {
                            //            CoordinateY++;
                            //            Console.SetCursorPosition(CoordinateX, CoordinateY - 1);
                            //            LineNumber = 0;
                            //        }
                            //    }
                            //    CoordinateX = startingX + 10;
                            //    CoordinateY = startingY - 1;
                        }
                        Direction++;
                        if (Direction == 4)
                        {
                            Direction = 0;
                        }
                        break;
                
                
            

                    case SectionTypes.StartGrid:
                        startingX = CoordinateX;
                        startingY = CoordinateY;
                        graphics.DrawImage(UseImages.Get(_StartGrid), new Point(CoordinateX, CoordinateY));
                        CoordinateX = startingX + 64;
                        CoordinateY = startingY;
                        //graphics.DrawImage(_StartGrid, new Point(50, 50));
                        //SectionData sectionData = Data.CurrentRace.GetSectionData(section);
                        //foreach (Model.IParticipant participant in Data.CurrentRace.Participants)
                        //{
                        //    _horizontalStartingGrid = AddParticipantsToGraphics(_horizontalStartingGrid, participant);
                        //}

                        //startingX = CoordinateX;
                        //startingY = CoordinateY;
                        //Console.SetCursorPosition(CoordinateX, CoordinateY);
                        //foreach (string line in _horizontalStartingGrid)
                        //{
                        //    Console.Write(line);
                        //    LineNumber++;
                        //    if (LineNumber == 4)
                        //    {
                        //        CoordinateY++;
                        //        Console.SetCursorPosition(CoordinateX, CoordinateY);
                        //        LineNumber = 0;
                        //    }
                        //}
                        //CoordinateX = startingX + 10;
                        //CoordinateY = startingY;
                        break;

                    case SectionTypes.Finish:
                        startingX = CoordinateX;
                        startingY = CoordinateY;
                        //Bitmap bitmapFinish = UseImages.Get(_Finish);
                        //bitmapFinish.RotateFlip(RotateFlipType.Rotate180FlipY);
                        //graphics.DrawImage(bitmapFinish, new Point(CoordinateX, CoordinateY)); 
                        graphics.DrawImage(UseImages.Get(_Finish), new Point(CoordinateX, CoordinateY));
                        CoordinateX = startingX + 64;
                        CoordinateY = startingY;
                        //startingX = CoordinateX;
                        //startingY = CoordinateY;
                        //Console.SetCursorPosition(CoordinateX, CoordinateY);
                        //foreach (string line in _horizontalFinish)
                        //{
                        //    Console.Write(line);
                        //    LineNumber++;
                        //    if (LineNumber == 4)
                        //    {
                        //        CoordinateY++;
                        //        Console.SetCursorPosition(CoordinateX, CoordinateY);
                        //        LineNumber = 0;
                        //    }
                        //}
                        //CoordinateX = startingX + 10;
                        //CoordinateY = startingY;
                        break;

                    case SectionTypes.Straight:
                        if (Direction == 0 || Direction == 2)
                        {
                            startingX = CoordinateX;
                            startingY = CoordinateY;
                            graphics.DrawImage(UseImages.Get(_StraightHorizontal), new Point(CoordinateX, CoordinateY));
                            //CoordinateX = startingX + 64;
                            //CoordinateY = startingY;
                            //{
                            //    startingX = CoordinateX;
                            //    startingY = CoordinateY;
                            //    Console.SetCursorPosition(CoordinateX, CoordinateY);
                            //    foreach (string line in _horizontalStraight)
                            //    {
                            //        Console.Write(line);
                            //        LineNumber++;
                            //        if (LineNumber == 4)
                            //        {
                            //            CoordinateY++;
                            //            Console.SetCursorPosition(CoordinateX, CoordinateY);
                            //            LineNumber = 0;
                            //        }
                            //    }
                            if (Direction == 0)
                            {
                                CoordinateX = startingX + 64;
                                CoordinateY = startingY;
                            }
                            else if (Direction == 2)
                            {
                                CoordinateX = startingX - 64;
                                CoordinateY = startingY;
                            }
                        }
                        else if(Direction == 1 || Direction == 3)
                        {
                            startingX = CoordinateX;
                            startingY = CoordinateY;
                            graphics.DrawImage(UseImages.Get(_StraightVertical), new Point(CoordinateX, CoordinateY));
                            if(Direction == 1)
                            {
                                CoordinateX = startingX;
                                CoordinateY = startingY + 64;
                            } else if(Direction == 3)
                            {
                                CoordinateX = startingX;
                                CoordinateY = startingY - 64;
                            }
                            //if(Direction == 1)
                            //{
                            //    CoordinateX = startingY + 64;
                            //    CoordinateY = startingY;
                            //} else if(Direction == 3)
                            //{
                            //    CoordinateX = startingY;
                            //    CoordinateY = startingY - 64;
                        }
                        //    startingX = CoordinateX;
                        //    startingY = CoordinateY;
                        //    Console.SetCursorPosition(CoordinateX, CoordinateY);
                        //    foreach (string line in _verticalStraight)
                        //    {
                        //        Console.Write(line);
                        //        LineNumber++;
                        //        if (LineNumber == 4)
                        //        {
                        //            CoordinateY++;
                        //            Console.SetCursorPosition(CoordinateX, CoordinateY);
                        //            LineNumber = 0;
                        //        }
                        //    }
                        //    if (Direction == 1)
                        //    {
                        //        CoordinateX = startingX;
                        //        CoordinateY = startingY + 4;
                        //    }
                        //    else if (Direction == 3)
                        //    {
                        //        CoordinateX = startingX;
                        //        CoordinateY = startingY - 4;
                        //    }
                        break;
                }
                
            }
            return UseImages.CreateBitmapSourceFromGdiBitmap(bitmap);
        }
    }
}
    
