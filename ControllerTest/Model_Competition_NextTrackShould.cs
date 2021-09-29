using NUnit.Framework;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerTest
{
    [TestFixture]
    public class Model_Competition_NextTrackShould
    {
        private Competition _competition;

        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            Track result = _competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_OneInQueu_ReturnTrack()
        {
            SectionTypes[] sections =
            {
                SectionTypes.StartGrid,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Finish
            };

            Track track = new Track("Zandvoort", sections);
            _competition.Tracks.Enqueue(track);
            Track result = _competition.NextTrack();
            Assert.AreEqual(result, track);
        }

        [Test]
        public void NextTrack_OneInQueu_RemoveTrackFromQueue()
        {
            SectionTypes[] sections =
{
                SectionTypes.StartGrid,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Finish
            };

            Track track = new Track("Zandvoort", sections);
            _competition.Tracks.Enqueue(track);
            Track result = _competition.NextTrack();
            Track result1 = _competition.NextTrack();
            Assert.IsNull(result1);
        }

        [Test]
        public void NextTrack_TwoInQueu_ReturnNextTrack()
        {
            SectionTypes[] sections =
            {
                SectionTypes.StartGrid,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Finish
            };

            Track track1 = new Track("Zandvoort", sections);
            Track track2 = new Track("Baku", sections);

            _competition.Tracks.Enqueue(track1);
            _competition.Tracks.Enqueue(track2);

            Track result = _competition.NextTrack();
            Assert.AreEqual(result, track1);

            result = _competition.NextTrack();
            Assert.AreEqual(result, track2);
        }
    }
}
