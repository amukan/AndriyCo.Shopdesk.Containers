using AndriyCo.Shopdesk.Containers.Bank;
using AndriyCo.Shopdesk.Containers.Marketing;
using AndriyCo.Shopdesk.Containers.Resources;
using AndriyCo.Shopdesk.Containers.Serialization.Xml.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AndriyCo.Shopdesk.Containers.Documents
{
    /// <summary>
    /// Типи документів
    /// </summary>
    public enum DocumentType : int
    {
        /// <summary>
        /// Будь який документ (для відбору та фільтрації, в документах не використовується)
        /// </summary>
        AnyDocument = 0,

        /// <summary>Видаткова накладна</summary>
        [XmlEnum(Name = "1")] SalesInvoice = 1,

        /// <summary>Прибуткова накладна</summary>
        [XmlEnum(Name = "2")] PurchaseInvoice = 2,

        /// <summary>Замовлення від покупця</summary>
        [XmlEnum(Name = "4")] CustomerOrder = 4,

        /// <summary>Прибутковий касовий ордер</summary>
        [XmlEnum(Name = "8")] PayInSlip = 8,

        /// <summary>Видатковий касовий ордер</summary>
        [XmlEnum(Name = "16")] PayOutOrder = 16,

        /// <summary>Замовлення постачальнику</summary>
        [XmlEnum(Name = "32")] PurchaseOrder = 32,

        /// <summary>Повернення від покупця</summary>
        [XmlEnum(Name = "64")] CustomerReturnOrder = 64,

        /// <summary>Повернення постачальнику</summary>
        [XmlEnum(Name = "128")] SupplierReturnOrder = 128,

        /// <summary>Переоблік залишків</summary>
        [XmlEnum(Name = "256")] Correction = 256,

        /// <summary>Переоцінка</summary>
        [XmlEnum(Name = "512")] Revaluation = 512,

        /// <summary>Накладна на передачу</summary>
        [XmlEnum(Name = "1024")] GoodsTransferNote = 1024,

        /// <summary>кассовый ордер на передачу. Point1_number - звідки, Point2_number - куди</summary>
        [XmlEnum(Name = "2048")] TransferOrder = 2048,

        /// <summary>Виробництво</summary>
        [XmlEnum(Name = "8192")] Production = 8192,

        /// <summary>Прибутковий касовий ордер (попереднє утримання оплати по картці)</summary>
        [XmlEnum(Name = "65536")] PayInSlipHold = 65536,

        /// <summary>Декларація на прихід від постачальника</summary>
        [XmlEnum(Name = "131072")] ShippingDeclaration = 131072,
    }

    [Flags]
    public enum EditPermissions : int
    {
        [XmlEnum(Name = "0")] None = 0,
        [XmlEnum(Name = "1")] AddRows = 1,
        [XmlEnum(Name = "2")] RemoveRows = 2,
        [XmlEnum(Name = "4")] IncreaseQuantity = 4,
        [XmlEnum(Name = "8")] DecreaseQuantity = 8,
        [XmlEnum(Name = "16")] IncreasePrice = 16,
        [XmlEnum(Name = "32")] DecreasePrice = 32,
    }

    /// <summary>
    /// Тип товару
    /// </summary>
    public enum GoodsItemType : int
    {
        /// <summary>
        /// Елементарний товар (інгредієнт)
        /// </summary>
        [XmlEnum(Name = "1")] Ingredient = 1,

        /// <summary>
        /// Виріб
        /// </summary>
        [XmlEnum(Name = "2")] Product = 2,

        /// <summary>
        /// Робота/послуга
        /// </summary>
        [XmlEnum(Name = "4")] Job = 4
    }

    /// <summary>
    /// Форми та види оплат
    /// </summary>
    /// <remarks>
    /// Існує дві форми оплати: готівка та безготівка.<br/>
    /// Безготівка (все окрім готівки) може бути карткою, кредитом, подарунковим сертифікатом, банківським переказом і т.д.<br/>
    ///</remarks>
    public enum PaymentMethod : int
    {
        /// <summary>
        /// Готівка
        /// </summary>
        [XmlEnum(Name = "0")] Cash = 0,
        /// <summary>
        /// Картка через POS-термінал на касі
        /// </summary>
        [XmlEnum(Name = "1")] Card = 1,
        /// <summary>
        /// Кредит
        /// </summary>
        /// <remarks>
        /// Дехто використовує для доставщиків
        /// </remarks>
        [XmlEnum(Name = "2")] Credit = 2,
        /// <summary>
        /// Оплата подарунковим сертифікатом
        /// </summary>
        [XmlEnum(Name = "3")] GiftCertificate = 3,
        /// <summary>
        /// Оплата бонусами в контексті програми лояльності або бонусна знижка в контексті фіскального обліку
        /// </summary>
        [XmlEnum(Name = "4")] Bonus = 4,
        /// <summary>
        /// Банковський переказ (з рахунку на рахунок)
        /// </summary>
        [XmlEnum(Name = "5")] BankTransfer = 5,
        /// <summary>
        /// Оплата карткою через інтернет (онлайн)
        /// </summary>
        [XmlEnum(Name = "6")] CardOnline = 6
    }

    /// <summary>
    /// Канал продажу
    /// </summary>
    public enum SaleChannel : int
    {
        /// <summary>Не визначено</summary>
        [XmlEnum(Name = "0")] NotSet = 0,

        /// <summary>Каса</summary>
        [XmlEnum(Name = "1")] PointOfSale = 1,

        /// <summary>Електронна торгівля, веб-сайт</summary>
        [XmlEnum(Name = "2")] ECommerce = 2,

        /// <summary>QR меню</summary>
        [XmlEnum(Name = "4")] QrMenu = 4,

        /// <summary>Торговий автомат</summary>
        [XmlEnum(Name = "8")] VendingMachine = 8
    }

    public static class SaleChannelExtensions
    {
        public static string GetDescription(this SaleChannel saleChannel) => saleChannel switch
        {
            SaleChannel.PointOfSale => CommonTranslator.PointOfSale,
            SaleChannel.ECommerce => CommonTranslator.ECommerce,
            SaleChannel.QrMenu => CommonTranslator.QrMenu,
            SaleChannel.VendingMachine => CommonTranslator.VendingMachine,
            _ => string.Empty,
        };
    }

    [XmlType("Item")]
    public class Discount
    {
        public byte DiscountType { get; set; }
        public double DiscountValue { get; set; }
    }

    /// <summary>
    /// Будь який документ (видаткова накладна, прибутковий касовий ордер, повернення покупцю, видатковий касовий ордер, замовлення від покупця, замовлення постачальнику та інше)<br/>
    /// Входить до контейнера документів як елемент колекції документів Details
    /// </summary>
    /// <remarks>
    /// Реестровий запис - запис товару на певній торговій точці (складі). Вміщує Id товару, поточну кількість та роздрібну ціну (реєстрова ціна).<br/>
    /// На кожній точці ціна на товар може відрізнятись.<br/> Реєстрова ціна є опорною, друкується на ціннику. Саме відносно неї вираховуються знижки.
    /// </remarks>
    [XmlType("Document")]
    public class Document
    {
        /// <summary>
        /// Додаткова, будь яка текстова інформація. Примітка.
        /// </summary>
        [XmlElement(IsNullable = true)] public string AdditionalInfo { get; set; }

        /// <summary>Штрих-код картки агента-продавця</summary>
        public string AgentBarcode { get; set; }

        /// <summary>Id торгового агента-продавця</summary>
        public long AgentId { get; set; }

        /// <summary>Сума документа</summary>
        [Money] public double Amount { get; set; }

        /// <summary>Сума сплати (зазвичай дорівнює сумі документа, але може бути меньшою у випадку часткової оплати)</summary>
        [Money] public double AmountPaid { get; set; }

        /// <summary>Докладна інформація по банківській транзакції та платіжному терміналу</summary>
        public TransactionInfo BankTransactionInfo { get; set; }

        /// <summary>Часовий пояс, в якому знаходиться каса</summary>
        public int Bias { get; set; }

        /// <summary>Істина, якщо на касі в чеку надруковано повідомлення про нараховані на початку місяця бонуси</summary>
        [UppercaseTrueFalse]
        [Obsolete("Нарахування бонусів з балів більше не використовується. Тому ця ознака в нових версія використовуватись не повинно.")]
        public bool BonusCalculationPrinted { get; set; }

        /// <summary>
        /// Бонуси, нараховані по товарах франшизи, що продані в цьому чеку
        /// </summary>
        [Money] public double BonusFranch { get; set; }

        /// <summary>
        /// Бонуси, нараховані по "інших" товарах, тобто товарах, що продаються поза франшизою, і які продані в цьому чеку
        /// </summary>
        [Money] public double BonusOther { get; set; }

        /// <summary>
        /// Сума бонусів, якими додатково розрахувався покупець<br/>
        /// З точки зору програми лояльності бонуси є платіжним засобом нарівні з грошовою одиницею. Але з точки зору фіскального обліку бонуси можуть виступати лише у ролі знижки.
        /// </summary>
        [Money] public double BonusPaid { get; set; }

        /// <summary>ID запису про списання бонусної суми, отриманого від сервісу CRM</summary>
        public long BonusPaymentRecordId { get; set; }

        /// <summary>ID клієнта з облікової системи франчайзі (=0 якщо це CRM клієнт)</summary>
        public long ContractorId { get; set; }

        /// <summary>Ім'я клієнта з облікової системи франчайзі (порожнє, якщо це CRM клієнт)</summary>
        public string ContractorName { get; set; }

        /// <summary>Валюта оплати</summary>
        public long CurrencyId { get; set; }

        /// <summary>Курс валюти</summary>
        public double CurrencyRate { get; set; }

        /// <summary>
        /// Перелік сегментів, до яких належить покупець, як учасник програми лояльності у CRM системі
        /// </summary>
        public List<CustomerSegment> CustomerSegments { get; set; }

        /// <summary>Дата-час проведення чека на касі (місцевий час)</summary>
        [ShopdeskDate] public DateTime DateOfApprove { get; set; }

        /// <summary>Дата-час створення чека на касі (місцевий час)</summary>
        [ShopdeskDate] public DateTime DateOfCreate { get; set; }

        /// <summary>Id точки доставки в обліковій системі франчайзі</summary>
        public long DeliveryPointId { get; set; }

        /// <summary>Id торгової точки в обліковій системі</summary>
        public long DepartmentId { get; set; }

        /// <summary>Назва торгової точки в обліковій системі</summary>
        public string DepartmentName { get; set; }

        [XmlArray(ElementName = "Detail")] public List<DocumentDetail> Details { get; set; }

        /// <summary>Унікальний ідентифікатор документу</summary>
        public Guid DocumentGuid { get; set; }

        /// <summary>Номер документа (бухгалтерський, нумерація може скидатись раз на місяць, чи квартал, чи рік)</summary>
        public string DocumentNumber { get; set; }

        /// <summary>Тип документа</summary>
        public DocumentType DocumentType { get; set; }

        /// <summary>Перелік дозволених операцій редагування для товарного документа</summary>
        /// <remarks>
        /// Значення флагів для xml представлені як колекція значень через пробіл
        /// </remarks>
        public EditPermissions EditPermissions { get; set; }

        /// <summary>Фіскальний номер фіскального реєстратора</summary>
        public string FiscalRegisterFiscalNumber { get; set; }

        /// <summary>Нумератор фіскального реєстратора</summary>
        public byte FiscalRegisterId { get; set; }

        /// <summary>Назва фіскального реєстратора</summary>
        public string FiscalRegisterName { get; set; }

        /// <summary>Серійний номер фіскального реєстратора</summary>
        public string FiscalRegisterSerialNumber { get; set; }

        /// <summary>Штрих-код картки лояльності клієнта CRM</summary>
        public string FranchiseContractorBarcode { get; set; }

        /// <summary>ID клієнта з CRM системи. (=0, якщо це клієнт з облікової системи франчайзі)</summary>
        public long FranchiseContractorId { get; set; }

        /// <summary>Номер телефону клієнта CRM, наприклад, 380671234567</summary>
        public string FranchiseContractorPhoneNumber { get; set; }

        /// <summary>ID франчайзі в CRM системі франшизи</summary>
        public long FranchiseeId { get; set; }

        /// <summary>Номінал сертифікату, яким сплачувався чек</summary>
        [Money] public double GiftCertificateSumma { get; set; }

        /// <summary>
        /// ID документа в таблиці локальної бази даних касового додатку
        /// </summary>
        public long Id { get; set; }

        /// <summary>Ознака фіскального чека</summary>
        [ZeroOne] public bool IsFiscal { get; set; }

        /// <summary>
        /// Перелік записів журналу каси, які стосуються цього чека
        /// </summary>
        public List<LogRecord> LogRecords { get; set; }

        /// <summary>Перелік подарунків з маркетингових інструментів, які спрацювали для цього чека</summary>
        [XmlArray(ElementName = "MarketingActions")] public List<MarketingActionRecord> MarketingActionRecords { get; set; }

        /// <summary>
        /// Перелік записів журналу обробки маркетингових інструментів
        /// </summary>
        public List<MarketingLogRecord> MarketingLogRecords { get; set; }

        /// <summary>
        /// Перелік повідомлень покупцю по періодичних маркетингових інструментах, які спрацювали на сервері та були надруковані у чекові покупця
        /// </summary>
        /// <remarks>
        /// Такий собі зворотній зв'язок, щоб знати, де і коли були надруковані повідомлення
        /// </remarks>
        public List<MarketingToolRecord> MarketingToolRecordDescriptions { get; set; }

        ///<summary>
        ///Форма оплати. 0 - готівка, 1 – безготівкова (картка), 2 – кредит, 3 – сертифікат.<br/>
        ///Увага! Внаслідок того, що чек може містити декілька ПКО з різними формами оплати, коректне значення PaymentMethod має тільки касовий документ, для товарного документа PaymentMethod завжди =0
        /// </summary>
        public PaymentMethod PaymentMethod { get; set; }

        /// <summary>
        /// Канал продажу (каса, QR Меню, E-Commerce, Vending Machine тощо)
        /// </summary>
        public SaleChannel SaleChannel { get; set; }

        /// <summary>Номер документа у зміні (нумерація починається з 1 після відкриття зміни, може мати числовий сталий префікс)</summary>
        public string SessionDocumentNumber { get; set; }

        /// <summary>
        /// Guid документа, що став підставою для створення поточного
        /// </summary>
        public Guid? SourceDocumentGuid { get; set; }

        /// <summary>
        /// ID документа, що став підставою для створення поточного. (=0)
        /// </summary>
        public long SourceDocumentId { get; set; }

        /// <summary>
        /// Статус документа (0 - відкладений, 1 - проведений)
        /// </summary>
        public byte Status { get; set; }

        /// <summary>Підстава або коментар</summary>
        public string SupportingDocument { get; set; }

        /// <summary>
        /// Запланована дата-час видачі замовлення (відвантаження товару)
        /// </summary>
        [ShopdeskDate] public DateTime? TimeToIssue { get; set; }

        /// <summary>
        /// Унікальний ідентифікатор документа, з яким пов'язаний цей документ (зазвичай для документу оплати, що пов'язаний з товарним документом цього чека)
        /// </summary>
        public Guid? TopDocumentGuid { get; set; }

        /// <summary>
        /// Id документа в базі даних касового додатку, з яким пов'язаний цей документ (зазвичай для документу оплати, що пов'язаний з товарним документом цього чека)
        /// </summary>
        public long TopDocumentId { get; set; }

        /// <summary>ID статті руху документа</summary>
        public long TransactionTypeId { get; set; }

        /// <summary>Повне ім’я користувача (касира)</summary>
        public string UserFullName { get; set; }

        /// <summary>ID користувача (касира)</summary>
        public long UserId { get; set; }

        /// <summary>Логін користувача (касира)</summary>
        public string UserName { get; set; }
    }

    /// <summary>
    /// Товарний запис у товарному документі
    /// </summary>
    [XmlType("DocumentDetail")]
    public class DocumentDetail
    {
        /// <summary>
        /// Сума до оплати товару
        /// <para>Розраховується за формулою<br/>
        /// Є результатом <b>SalePrice * Quantity</b><br/></para>
        /// </summary>
        [Money]
        public double AmountToPay
        {
            get => SalePrice * Quantity;
            set
            {
                // нічого не робимо в set. Така конструкція потрібна для серіалізації readonly властивостей в XML
            }
        }

        /// <summary>
        /// Штрих-код, за яким було розпізнано товар
        /// </summary>
        public string Barcode { get; set; }

        /// <summary>Id штрих-коду товару у таблиці штрих-кодів бази даних облікової системи франчайзі</summary>
        public long BarcodeId { get; set; }

        /// <summary>
        /// Відсоток нарахування бонусів відносно суми оплати (зазвичай береться з такого ж параметру групи товарів, до якої належить цей товар)
        /// </summary>
        public double BonusGeneratePercent { get; set; }

        /// <summary>Бонусна частка в оплаті за товар</summary>
        [Money] public double BonusSum { get; set; }

        /// <summary>Бонуси, нараховані за товар</summary>
        [Money] public double CalculatedBonus { get; set; }

        /// <summary>Поточна кількість товару точці на момент продажу (довідниково)</summary>
        [Quantity] public double CurrentQuantity { get; set; }

        /// <summary>
        /// Знижка як різниця між реєстровою ціною та факт. роздрібної у поточному документі. Discount = PrimaryPrice – SalePrice
        /// </summary>
        [XmlElement("Discount")] [Money] public double Discount { get; set; }

        /// <summary>
        /// Колекція записів про знижку/нарахування на суму товару
        /// </summary>
        public List<Discount> Discounts { get; set; } = new();

        /// <summary>Унікальний ідентифікатор товарного запису</summary>
        public Guid? DocumentDetailGuid { get; set; }

        /// <summary>
        /// Посилання на документ, який містить запис
        /// </summary>
        public long DocumentId { get; set; }

        /// <summary>Штрих-код акцизної марки</summary>
        public string ExciseMarkBarcode { get; set; }

        /// <summary>ID товару в базі CRM системи</summary>
        public long FranchGoodId { get; set; }

        /// <summary>Id товару у БД франчайзі</summary>
        public long GoodId { get; set; }

        /// <summary>Id одиниці виміру у БД франчайзі</summary>
        public long GoodsCategoryId { get; set; }

        /// <summary>Назва товару</summary>
        public string GoodsItemName { get; set; }

        /// <summary>
        /// Тип товару
        /// </summary>
        public GoodsItemType GoodsItemType { get; set; } = GoodsItemType.Ingredient;

        /// <summary>Id одиниці виміру у БД франчайзі</summary>
        public long GoodsUomId { get; set; }

        /// <summary>Назва одиниці виміру</summary>
        public string GoodsUomName { get; set; }

        /// <summary>
        /// Id товарного запису в базі касового додатку
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Дата та час оновлення реєстрового запису (для онлайн режиму – поточні, для офлайн – дата останньої синхронізації офлайн БД)
        /// </summary>
        [ShopdeskDate] public DateTime InventoryRecordDate { get; set; }

        /// <summary>ID реєстрового запису в базі облікової системи франчайзі</summary>
        public long InventoryRecordId { get; set; }

        /// <summary>
        /// Максимальна дозволена знижка на товар у відсотках за даними облікової системи. Значення від 0 до 1, де 1 – знижка 100%. Значення присутнє тільки у документах продажу.
        /// </summary>
        public double MaxAllowedDiscountPercent { get; set; }

        /// <summary>
        /// Максимальна дозволена ціна на товар за даними облікової системи. Значення присутнє тільки у документах продажу.
        /// </summary>
        [Money] public double MaxAllowedPrice { get; set; }

        /// <summary>
        /// Мінімальна дозволена ціна товару<br/>
        /// Розраховується як MinAllowedPrice = PrimaryPrice* (1 -MaxAllowedDiscountPercent)<br/>
        /// Значення присутнє тільки у документах продажу.
        /// </summary>
        [Money]
        public double MinAllowedPrice
        {
            get => Math.Max(PrimaryPrice * (1 - MaxAllowedDiscountPercent), 0.01d);
            set
            {
                // нічого не робимо в set. Така конструкція потрібна для серіалізації readonly властивостей в XML
            }
        }

        /// <summary>
        /// Сума оплати однією з форм оплати (як різниця між сумою товару та бонусною часткою в оплаті)
        /// </summary>
        [Money] public double MoneySum { get; set; }

        /// <summary>
        /// Початкова ціна товару<br/>
        /// У чеку продажу тут реєстрова ціна,<br/>
        /// у чеку повернення – ціна, за якою товар було продано, з урахуванням усіх знижок на ціну під час продажу, крім знижки/нарахування на суму в результаті округлення копійками.
        /// </summary>
        public double PrimaryPrice { get; set; }

        /// <summary>Закупівельна ціна (для документу Прибуткова накладна)</summary>
        public double PurchasePrice { get; set; }

        /// <summary>
        /// Кількість в цьому товарному записі (для документів крім «перерахунок залишків з фіксацією різниці»)
        /// </summary>
        [Quantity] public double Quantity { get; set; }

        /// <summary>
        /// Різниця у кількості (для документа «перерахунок залишків з фіксацією різниці»). Від’ємне значення – кількість зменшується, позитивне – кількість збільшується
        /// </summary>
        [Quantity] public double QuantityDifference { get; set; }

        /// <summary>Кількість в упаковці (не використовується)</summary>
        [Quantity] public double QuantityInPack { get; set; }

        /// <summary>Кількість упаковок (=0)</summary>
        [Quantity] public double QuantityPack { get; set; }

        /// <summary>
        /// Фактична роздрібна ціна з урахуванням усіх знижок. Це ціна для облікової системи, оскільки може мати багато знаків після коми.<br/>
        /// У цій ціні враховано всі знижки та округлення, які застосовуються до суми товару.<br/>
        /// <b>SalePrice * Quantity</b> дасть точну суму до оплати товару <b>AmountToPay</b>.
        /// </summary>
        public double SalePrice { get; set; }

        /// <summary>Ціна після переоцінки (для документа переоцінки). Значення присутнє тільки у документах переоцінки.</summary>
        [Money] public double SalePriceAfterRevaluation { get; set; }

        /// <summary>Guid товарного запису в документі, що став підставою для створення поточного документа, і з якого створено цей товарний запис</summary>
        public Guid? SourceDocumentDetailGuid { get; set; }

        /// <summary>Guid товарного запису, на який посилається цей товарний запис</summary>
        /// <remarks>Наприклад, товар-доповнення до певної страви</remarks>
        public Guid? TopDocumentDetailGuid { get; set; }

        public long TopGoodId { get; set; }

        /// <summary>Код українського класифікатора товарів зовнішньо економічної діяльності</summary>
        public string Uktzed { get; set; }
    }
}