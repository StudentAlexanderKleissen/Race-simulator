using Model;
using NUnit.Framework;
using Controller;
namespace ControllerTest
{
    [TestFixture]
    public class Controller_Race_GetSectionDataShould
    {
        private Race _race;

        [SetUp]
        public void SetUp()
        {
            Data.Initialize();
            Data.NextRace();

            _race = new Race(Data.CurrentRace.track, Data.CurrentRace.Participants);
        }

        [Test]
        public void GetSectionData_SectionDataBySection_NotNull()
        {
            Section section = Data.CurrentRace.track.Sections.First.Value;
            SectionData sectionData = _race.GetSectionData(section);
            Assert.NotNull(sectionData);
        }
    }
}