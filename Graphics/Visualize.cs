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
        private const string _StartGrid = ".\\Images\\StartGrid.png";
        private const string _Finish = ".\\Images\\Finish.png";

        private const string _StraightVertical = ".\\Images\\StraightVertical.png";
        //private const string _StraightHorizontal = ".\\Images\\StraightHorizontal.png";

        private const string _StraightHorizontal = ".\\Images\\StraightHorizontal.png";
        //private const string _StraightHorizontal = "C:\\Users\\Alexander\\source\repos\\Race-simulator\\Graphics\\Images\\StraightHorizontal.png";

        private const string _Turn0 = ".\\Images\\Turn0.png";
        private const string _Turn1 = ".\\Images\\Turn1.png";
        private const string _Turn2 = ".\\Images\\Turn2.png";
        private const string _Turn3 = ".\\Images\\Turn3.png";

        private const string _CarRed = ".\\Images\\Red_Race_Car.png";
        //private const string _CarRed = "C:\\Users\\Alexander\\Desktop\\race-simulator-master\\WpfApp\\Images\\CarBlue.png";
        private const string _CarBlue = ".\\Images\\Blue_Race_Car.png";
        private const string _Broken = ".\\Images\\Broken_Race_Car.png";

        #endregion graphics

        private static int Direction;
        private static int LineNumber;
        private static int CoordinateX;
        private static int CoordinateY;
        private static int startingX;
        private static int startingY;
        public static IParticipant Winner;
        public static IParticipant Loser;
        public static bool RaceFinished;
        private static bool ShownScoreScreen;

        private static int NumberOfRounds;

        private static int[] Player1Position;
        private static int Player1Direction;
        private static int Player1WentOverFinish;
        private static bool Player1WentOverFinishDelay;
        public static IParticipant Player1;

        private static int[] Player2Position;
        private static int Player2Direction;
        private static int Player2WentOverFinish;
        private static bool Player2WentOverFinishDelay;
        public static IParticipant Player2;

        private static bool ZandvoortHasBeenRacedOn;
        private static bool MonacoHasBeenRacedOn;

        private static int TopRightCornerRight;
        private static int BottomRightCornerRight;
        private static int TopRightCornerLeft;
        private static int BottomRightCornerLeft;

        public static Bitmap bitmap = UseImages.GetEmptyBitmap(500, 500);
        //public static Bitmap bitmap = UseImages.GetEmptyBitmap(800, 500);

        public static void Initialize()
        {
            Player1Position = new int[2];
            Player1Position[0] = 227;
            Player1Position[1] = 46;
            Player1Direction = 0;
            Player1WentOverFinish = 0;
            Player1WentOverFinishDelay = false;

            Player2Position = new int[2];
            Player2Position[0] = 198;
            Player2Position[1] = 22;
            Player2Direction = 0;
            Player2WentOverFinish = 0;
            Player2WentOverFinishDelay = false;

            startingX = 0;
            startingY = 0;

            Direction = 0; //0 = east 1 = south 2 = west 3 = north
            LineNumber = 0;
            NumberOfRounds = 2;
            RaceFinished = false;
            ShownScoreScreen = false;

            Winner = null;
            Loser = null;
            //Data.CurrentRace.DriversChanged += OnDriversChanged;
            //Data.CurrentRace.NextRace += OnStartNextRace;
        }

        public static BitmapSource DrawTrack(Track track)
        {
            if (track.Name == "Zandvoort")
            {
                TopRightCornerLeft = 405;
                TopRightCornerRight = 431;
                
            } else if(track.Name == "Monaco")
            {
                TopRightCornerLeft = 600;
                TopRightCornerRight = 619;
            }

            CoordinateX = 200;
            CoordinateY = 10;

            MoveCar(track);
            //Bitmap bitmap = UseImages.GetEmptyBitmap(500, 500);
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
                        }
                        else if (Direction == 1)
                        {
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
                        }
                        else if (Direction == 3)
                        {
                            startingX = CoordinateX;
                            startingY = CoordinateY;
                            graphics.DrawImage(UseImages.Get(_Turn3), new Point(CoordinateX, CoordinateY));
                            CoordinateX = startingX + 64;
                            CoordinateY = startingY;
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
                        break;

                    case SectionTypes.Finish:
                        startingX = CoordinateX;
                        startingY = CoordinateY;
                        graphics.DrawImage(UseImages.Get(_Finish), new Point(CoordinateX, CoordinateY));
                        CoordinateX = startingX + 64;
                        CoordinateY = startingY;
                        break;

                    case SectionTypes.Straight:
                        if (Direction == 0 || Direction == 2)
                        {
                            startingX = CoordinateX;
                            startingY = CoordinateY;
                            graphics.DrawImage(UseImages.Get(_StraightHorizontal), new Point(CoordinateX, CoordinateY));
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
                        else if (Direction == 1 || Direction == 3)
                        {
                            startingX = CoordinateX;
                            startingY = CoordinateY;
                            graphics.DrawImage(UseImages.Get(_StraightVertical), new Point(CoordinateX, CoordinateY));
                            if (Direction == 1)
                            {
                                CoordinateX = startingX;
                                CoordinateY = startingY + 64;
                            } else if (Direction == 3)
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
                DrawCar(graphics);
            }
            if(MainWindow.IsZandvoortFinished && MainWindow.IsMonacoFinished)
            {
                bitmap = new Bitmap(1, 1);
            }
            return UseImages.CreateBitmapSourceFromGdiBitmap(bitmap);
        }

        public static void DrawCar(System.Drawing.Graphics graphics)
        {
            //Creates and orients the red racing car the right way
            if (Player1WentOverFinish <= NumberOfRounds)
            {
                Bitmap bitmapCarRed = new Bitmap(_CarRed);

                //Checks if the equipment is broken
                if (Player1.Equipment.IsBroken)
                {
                    bitmapCarRed = UseImages.Get(_Broken);
                }

                if (Player1Direction == 0)
                {
                    bitmapCarRed.RotateFlip(RotateFlipType.Rotate180FlipY);
                    graphics.DrawImage(new Bitmap(bitmapCarRed, 25, 15), new Point(Player1Position[0], Player1Position[1]));
                }
                else if (Player1Direction == 1)
                {
                    bitmapCarRed.RotateFlip(RotateFlipType.Rotate90FlipY);
                    graphics.DrawImage(new Bitmap(bitmapCarRed, 15, 25), new Point(Player1Position[0], Player1Position[1]));
                }
                else if (Player1Direction == 2)
                {
                    graphics.DrawImage(new Bitmap(bitmapCarRed, 25, 15), new Point(Player1Position[0], Player1Position[1]));
                }
                else if (Player1Direction == 3)
                {
                    bitmapCarRed.RotateFlip(RotateFlipType.Rotate270FlipY);
                    graphics.DrawImage(new Bitmap(bitmapCarRed, 15, 25), new Point(Player1Position[0], Player1Position[1]));
                }
            }

            if (Player2WentOverFinish <= NumberOfRounds)
            {
                Bitmap bitmapCarBlue = new Bitmap(_CarBlue);

                //Checks if the equipment is broken
                if (Player2.Equipment.IsBroken)
                {
                    bitmapCarBlue = UseImages.Get(_Broken);
                }

                //Creates and orients the blue racing car the right way
                if (Player2Direction == 0)
                {
                    bitmapCarBlue.RotateFlip(RotateFlipType.Rotate180FlipY);
                    graphics.DrawImage(new Bitmap(bitmapCarBlue, 25, 15), new Point(Player2Position[0], Player2Position[1]));
                }
                else if (Player2Direction == 1)
                {
                    bitmapCarBlue.RotateFlip(RotateFlipType.Rotate90FlipY);
                    graphics.DrawImage(new Bitmap(bitmapCarBlue, 15, 25), new Point(Player2Position[0], Player2Position[1]));
                }
                else if (Player2Direction == 2)
                {
                    graphics.DrawImage(new Bitmap(bitmapCarBlue, 25, 15), new Point(Player2Position[0], Player2Position[1]));
                }
                else if (Player2Direction == 3)
                {
                    bitmapCarBlue.RotateFlip(RotateFlipType.Rotate270FlipY);
                    graphics.DrawImage(new Bitmap(bitmapCarBlue, 15, 25), new Point(Player2Position[0], Player2Position[1]));
                }
            }
        }

        public static void MoveCar(Track track)
        {
            //Initialize the speed of the two Players
            double Player1MovementSpeedTemporary = Player1.Equipment.Performance * Player1.Equipment.Speed;
            Player1MovementSpeedTemporary = Math.Ceiling(Player1MovementSpeedTemporary);
            int Player1MovementSpeed = (int)Convert.ToUInt32(Player1MovementSpeedTemporary);
            Player1MovementSpeed = 100;

            //Player2 gets 20 extra movementspeed because it starts on the outside of the track so has to take the longer route
            double Player2MovementSpeedTemporary = (Player2.Equipment.Performance * Player2.Equipment.Speed) + 20;
            Player2MovementSpeedTemporary = Math.Ceiling(Player2MovementSpeedTemporary);
            int Player2MovementSpeed = (int)Convert.ToUInt32(Player2MovementSpeedTemporary);
            Player1MovementSpeed = 100;

            //Move the RED racing car
            if (Player1.Equipment.IsBroken == false)
            {
                if (Player1Direction == 0)
                {
                    //Player1Position[0] += 30;
                    Player1Position[0] += Player1MovementSpeed;

                    if (Player1Position[0] >= TopRightCornerLeft)
                    {
                        Player1Position[1] += Player1Position[0] - TopRightCornerLeft;
                        Player1Position[0] = TopRightCornerLeft;
                        //Player1Position[1] += 20;
                        Player1Direction++;
                    }
                }
                else if (Player1Direction == 1)
                {
                    Player1Position[1] += Player1MovementSpeed;
                    if (Player1Position[1] >= 278)
                    {
                        Player1Position[0] -= Player1Position[1] - 278;
                        Player1Position[1] = 278;
                        Player1Direction++;
                    }
                }
                else if (Player1Direction == 2)
                {
                    Player1Position[0] -= Player1MovementSpeed;
                    if (Player1Position[0] <= 45)
                    {
                        Player1Position[1] -= 45 - Player1Position[0];
                        Player1Position[0] = 45;
                        Player1Direction++;

                        //If the player goes over the finish again, it now registrates it as a new lap
                        Player1WentOverFinishDelay = false;
                    }
                }
                else
                {
                    Player1Position[1] -= Player1MovementSpeed;
                    if (Player1Position[1] <= 46)
                    {
                        Player1Position[0] += 46 - Player1Position[1];
                        Player1Position[1] = 46;
                        Player1Direction = 0;
                    }
                }
            }

            //Move the BLUE racing car
            if (Player2.Equipment.IsBroken == false)
            {
                if (Player2Direction == 0)
                {
                    //Player1Position[0] += 30;
                    Player2Position[0] += Player2MovementSpeed;

                    if (Player2Position[0] >= (TopRightCornerRight - 6))
                    {
                        Player2Position[1] += Player2Position[0] - TopRightCornerRight;
                        Player2Position[0] = TopRightCornerRight;
                        Player2Direction++;
                    }
                }
                else if (Player2Direction == 1)
                {
                    Player2Position[1] += Player2MovementSpeed;

                    if (Player2Position[1] >= 300)
                    {
                        Player2Position[0] -= Player2Position[1] - 300;
                        Player2Position[1] = 300;
                        Player2Direction++;
                    }
                }
                else if (Player2Direction == 2)
                {
                    Player2Position[0] -= Player2MovementSpeed;

                    if (Player2Position[0] <= 19)
                    {
                        Player2Position[1] -= 19 - Player2Position[0];
                        Player2Position[0] = 19;
                        Player2Direction++;

                        //If the player goes over the finish again, it now registrates it as a new lap
                        Player2WentOverFinishDelay = false;
                    }
                }
                else
                {
                    Player2Position[1] -= Player2MovementSpeed;

                    if (Player2Position[1] <= 22)
                    {
                        Player2Position[0] += 22 - Player2Position[1];
                        Player2Position[1] = 22;
                        Player2Direction = 0;
                    }
                }
            }


            //If a player goes over the finish, the lap the player has finished is +1
            if (Player1Position[0] > 325 && Player1Position[1] == 46 && Player1WentOverFinishDelay == false)
            {
                Player1WentOverFinish++;

                //If Player1 has finished all laps, it has either won or lost
                if (Player1WentOverFinish > NumberOfRounds && Winner == null)
                {
                    Winner = Player1;
                } else if(Player1WentOverFinish > NumberOfRounds && Winner == Player2)
                {
                    Loser = Player1;
                }
                //The delay is there because otherwise every time the player goes past the finish it is registered as an lap
                Player1WentOverFinishDelay = true;
            }

            //If a player goes over the finish, the lap the player has finished is +1
            if (Player2Position[0] > 325 && Player2Position[1] == 22 && Player2WentOverFinishDelay == false)
            {
                Player2WentOverFinish++;

                //If Player2 has finished all laps, it has either won or lost
                if (Player2WentOverFinish > NumberOfRounds && Winner == null)
                {
                    Winner = Player2;
                }
                else if (Player2WentOverFinish > NumberOfRounds && Winner == Player1)
                {
                    Loser = Player2;
                }
                //The delay is there because otherwise every time the player goes past the finish it is registered as an lap
                Player2WentOverFinishDelay = true;
            }

        }
    }
}
    
