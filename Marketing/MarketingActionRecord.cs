using System;
using System.Xml;
using System.Xml.Serialization;

namespace AndriyCo.Shopdesk.Containers.Marketing
{
    public enum PresentType : int
    {
        [XmlEnum(Name = "1")] Points = 1,
        [XmlEnum(Name = "2")] PresentGoodsItem = 2,
        [XmlEnum(Name = "3")] PercentDiscount = 3,
        [XmlEnum(Name = "4")] MessageOnly = 4,
        [XmlEnum(Name = "5")] PriceColumnNumber = 5,
        [XmlEnum(Name = "6")] GiftCode = 6,
        [XmlEnum(Name = "7")] MoneyDiscount = 7,
        [XmlEnum(Name = "8")] Bonus = 8,
        [XmlEnum(Name = "9")] FixedPrice = 9,
        [XmlEnum(Name = "10")] PointsAsPercentFromAmount = 10,
        [XmlEnum(Name = "11")] BonusAsPercentFromAmount = 11
    }

    [XmlType("MarketingActionRecord")]
    public class MarketingActionRecord
    {
        public double BonusPercent { get; set; }

        public double DiscountPercent { get; set; }

        [XmlElement(IsNullable = true)]
        public Guid? DocumentDetailGuid { get; set; }

        public string GiftCode { get; set; }

        public long GoodsItemId { get; set; }

        public double GoodsItemPrice { get; set; }

        public double GoodsItemQuantity { get; set; }

        public long Id { get; set; }

        public long MarketingActionId { get; set; }

        public string MarketingActionName { get; set; }

        public long MarketingToolId { get; set; }

        public string MarketingToolName { get; set; }

        public string DescriptionToCustomer { get; set; }

        public double MoneyDiscount { get; set; }

        /// <summary>Кількість нарахованих бонусів</summary>
        [XmlElement("MarketingPresentedBonus")] public double PresentedBonus { get; set; }

        [XmlElement("MarketingPresentType")] public PresentType PresentType { get; set; }

        public int PriceColumnNumber { get; set; }
    }

    [XmlType("MarketingToolRecord")]
    public class MarketingToolRecord
    {
        public int Id { get; set; }
    }
}