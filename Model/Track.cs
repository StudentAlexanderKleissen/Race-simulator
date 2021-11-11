using System;
using System.Collections.Generic;

namespace Model
{
    public class Track
    {
        public int AmountOfStraightSections;
        public int AmountOfCornerSections;
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }
        public string TrackPhoto { get; set; }
        public Track(string name, SectionTypes[] sections)
        {
            Name = name;
            Sections = ArrayToLinkedList(sections);

            foreach (Section section in Sections)
            {
                if (section.SectionType == SectionTypes.Straight)
                {
                    AmountOfStraightSections++;
                }
                else if (section.SectionType == SectionTypes.RightCorner || section.SectionType == SectionTypes.LeftCorner)
                {
                    AmountOfCornerSections++;
                }
            }

            if(name == "Zandvoort")
            {
                TrackPhoto = "/Images/Zandvoort.jpg"; 
            } else if(name == "Monaco")
            {
                TrackPhoto = "/Images/Monaco.jpg";
            }
        }
        private LinkedList<Section> ArrayToLinkedList(SectionTypes[] sections)
        {
            LinkedList<Section> sectionsList = new LinkedList<Section>();

            foreach (SectionTypes section in sections)
            {
                sectionsList.AddLast(
                    new Section()
                    {
                        SectionType = section
                    }
                );
            }

            return sectionsList;
        }


        public override string ToString()
        {
            return $"Track name: {Name}{System.Environment.NewLine}Sections: {AmountOfCornerSections + AmountOfStraightSections}{System.Environment.NewLine}Straight Sections: {AmountOfStraightSections}{System.Environment.NewLine}Corner Sections: {AmountOfCornerSections}";
        }

        public List<string> StringToList()
        {
            List<string> list = new List<string>();
            list.Add($"Track name: {Name}{System.Environment.NewLine}Sections: {AmountOfCornerSections + AmountOfStraightSections}{System.Environment.NewLine}Straight Sections: {AmountOfStraightSections}{System.Environment.NewLine}Corner Sections: {AmountOfCornerSections}");
            return list;
        }
    }
}
