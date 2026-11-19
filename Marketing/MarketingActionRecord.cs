using AndriyCo.Shopdesk.Containers.Serialization.Xml.Attributes;
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

        [Obsolete("Застарілий. Використовуйте BonusAsPercentFromAmount")]
        [XmlEnum(Name = "10")] PointsAsPercentFromAmount = 10,

        [XmlEnum(Name = "11")] BonusAsPercentFromAmount = 11,
        [XmlEnum(Name = "13")] PromoCode = 13,

        /// <summary>
        /// Подарунки типу 1+1, чи 2+1 і т.д.
        /// </summary>
        ///<remarks>
        /// Використовується для інструментів типу, купи 2, третя безкоштовна.
        /// <br>При цьому це можуть бути як різні товари, так і той самий товар.</br>
        /// <br>Головне, щоб вони фігурували в переліку товарів, які в чекові утворять цей комплект.</br>
        ///</remarks>
        [XmlEnum(Name = "14")] NPlusOne = 14
    }

    [XmlType("MarketingActionRecord")]
    public class MarketingActionRecord
    {
        /// <summary>
        /// Відсоток нарахованих бонусів від суми покупки
        /// </summary>
        /// <remarks>
        /// Для типу подарунку "BonusAsPercentFromAmount" (11)
        /// </remarks>
        public double BonusPercent { get; set; }

        /// <summary>
        /// Повідомлення для покупця, яке відображається на чеку
        /// </summary>
        public string DescriptionToCustomer { get; set; }

        /// <summary>
        /// Відсоток знижки на товар, на який діє маркетинговий інструмент
        /// </summary>
        /// <remarks>
        /// Для типу подарунку "PercentDiscount" (3)
        /// </remarks>
        public double DiscountPercent { get; set; }

        /// <summary>
        /// Guid товарного рядка документа, до якого відноситься це нарахування
        /// </summary>
        [XmlElement(IsNullable = true)]
        public Guid? DocumentDetailGuid { get; set; }

        /// <summary>
        /// Guid товарного рядка документа, в якому знаходиться найдешевший товар, якому надається знижка за механікою N+1
        /// </summary>
        ///<remarks>
        /// Ціна одиниці товару служить основою для розрахунку знижки, яка буде розподілена між товарами комплекту, які входять в N+1
        /// <br>Поле потрібне для утворення зворотнього зв'язку, щоб розуміти, на підставі якого товару надаються відсоткові знижки, розподілені на товари комплекту N+1</br>
        ///</remarks>
        [XmlElement(IsNullable = true)]
        public Guid? NPlusOneDocumentDetailGuid { get; set; }

        /// <summary>
        /// Подарунковий код для розігришів
        /// </summary>
        public string GiftCode { get; set; }

        /// <summary>
        /// Id товару, який надається як подарунок маркетинговим інструментом, або на який встановлюється фіксована ціна чи знижка
        /// </summary>
        public long GoodsItemId { get; set; }

        /// <summary>
        /// Фіксована ціна товару у якості подарука
        /// </summary>
        /// <remarks>
        /// Для типу подарунку "FixedPrice" (9)
        /// </remarks>
        public double GoodsItemPrice { get; set; }

        /// <summary>
        /// Дозволена кількість товару, на який можна встановити фіксовану ціну або знижку. Якщо 0, то дозволено будь-яку кількість товару
        /// </summary>
        public double AllowedQuantity { get; set; }

        /// <summary>
        /// Кількість товару, яка видається як подарунок
        /// </summary>
        /// <remarks>
        /// Для типу подарунку "PresentGoodsItem" (2)
        /// </remarks>
        public double GoodsItemQuantity { get; set; }

        /// <summary>
        /// Id запису всередині чека
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Id маркетингової акції, яка була застосована до чека
        /// </summary>
        public long MarketingActionId { get; set; }

        /// <summary>
        /// Назва маркетингової акції, яка була застосована до чека
        /// </summary>
        public string MarketingActionName { get; set; }

        /// <summary>
        /// Id маркетингового інструмента, який був застосований до чека
        /// </summary>
        public long MarketingToolId { get; set; }

        /// <summary>
        /// Назва маркетингового інструмента, який був застосований до чека
        /// </summary>
        public string MarketingToolName { get; set; }

        /// <summary>
        /// Грошова знижка на чек, яка була застосована маркетинговим інструментом.
        /// </summary>
        /// <remarks>
        /// Для типу подарунку "MoneyDiscount" (7)
        /// </remarks>
        [Money]
        public double MoneyDiscount { get; set; }

        /// <summary>Кількість нарахованих бонусів</summary>
        [XmlElement("MarketingPresentedBonus")] public double PresentedBonus { get; set; }

        /// <summary>
        /// Тип подарунку, який був наданий маркетинговим інструментом. Визначає, що саме було нараховано покупцю.
        /// </summary>
        [XmlElement("MarketingPresentType")] public PresentType PresentType { get; set; }

        /// <summary>
        /// Номер колонки у прайсі, яка містить ціну товару
        /// </summary>
        /// <remarks>
        /// Для типу подарунку "PriceColumnNumber" (5)
        /// </remarks>
        public int PriceColumnNumber { get; set; }

        /// <summary>
        /// Текст промокоду, виданого покупцю як подарунок маркетингового інструмента.
        /// Контейнер навмисно зберігає саме бізнес-значення промокоду, а не внутрішній Id конкретної CRM,
        /// щоб Shopdesk.Containers залишався незалежним від реалізації програм лояльності різних CRM.
        /// </summary>
        /// <remarks>
        /// Для типу подарунку "PromoCode" (13)
        /// </remarks>
        public string PromoCode { get; set; }
    }

    [XmlType("MarketingToolRecord")]
    public class MarketingToolRecord
    {
        public int Id { get; set; }
    }
}