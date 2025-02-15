using System.Xml.Serialization;

namespace StreamMaster.SchedulesDirect.Domain.Models
{
    public class MxfScheduleEntries
    {
        [XmlAttribute("service")]
        public string Service { get; set; } = string.Empty;

        [XmlElement("ScheduleEntry")]
        public List<MxfScheduleEntry> ScheduleEntry { get; set; } = new List<MxfScheduleEntry>();

        public bool ShouldSerializeScheduleEntry()
        {
            ScheduleEntry = this.ScheduleEntry.OrderBy(arg => arg.StartTime).ToList();
            DateTime dateTime = DateTime.MinValue;
            foreach (MxfScheduleEntry mxfScheduleEntry in this.ScheduleEntry)
            {
                if (mxfScheduleEntry.StartTime != dateTime)
                    mxfScheduleEntry.IncludeStartTime = true;
                dateTime = mxfScheduleEntry.StartTime + TimeSpan.FromSeconds(mxfScheduleEntry.Duration);
            }
            return true;
        }
    }
}