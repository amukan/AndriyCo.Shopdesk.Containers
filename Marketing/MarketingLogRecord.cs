using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace AndriyCo.Shopdesk.Containers.Marketing
{
    public enum MarketingLogRecordType : int
    {
        MarketingActionRow = 0,
        MarketingToolRow = 1,
        ConditionRow = 2,
        ConditionGoodsItemDetailRow = 3
    }

    [XmlType("MarketingLogRecord")]
    public class MarketingLogRecord
    {
        public MarketingLogRecordType MarketingLogRecordType { get; set; }

        public string Message { get; set; }

        private string MessageColor => MarketingLogRecordType switch
        {
            MarketingLogRecordType.MarketingActionRow => "#000000",
            MarketingLogRecordType.MarketingToolRow => "#4682B4",
            MarketingLogRecordType.ConditionRow => "#006400",
            MarketingLogRecordType.ConditionGoodsItemDetailRow => "#808000",
            _ => "#000000",
        };

        [IgnoreDataMember]
        public string HtmlMessage
        {
            get
            {
                string boldTextTag = "";
                string boldTextCloseTag = "";

                string colorTag = $"<p style=\"color: {MessageColor}; \">";
                string htmlMessage = $"{colorTag}{boldTextTag}{Message}{boldTextCloseTag}</p>";
                return htmlMessage;
            }
        }
    }
}