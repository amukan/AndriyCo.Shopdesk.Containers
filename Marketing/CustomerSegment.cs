using System.Xml.Serialization;

namespace AndriyCo.Shopdesk.Containers
{
    [XmlType("Segment")]
    public class CustomerSegment
    {
        public int SegmentId { get; set; }
        public string SegmentName { get; set; }
    }
}