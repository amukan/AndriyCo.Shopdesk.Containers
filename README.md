# Опис структури контейнеру документів ANDRIY.CO Shopdesk

Файл чеку є контейнером для документів різного типу (зазвичай, товарний документ та касові документи, наприклад, для чеку продажу – видаткова накладна та один або декілька прибуткових касових ордерів, або взагалі не містить ПКО, якщо оплати не було). В одному контейнері є перелік товарів по одній торговій точці. Якщо в чеку на касі були товари з двох або більше торгових точок, то буде створена відповідна кількість контейнерів, кожний з товарами тільки по одній точці. Також контейнер може містити виключно касові документи, якщо це був чек внесення або вилучення коштів.

Приклад імені файлу контейнера документів

DOC_D70659_F11_P1000000826_U5_2020-08-29_11-32-20.tcudoc, де: 

* DOC_D – видаткова накладна з (або без) оплатою – звичайний чек продажу
* DOC_INVENTORY_D - інвентаризація
* DOC_ORDER_D - замовлення
* DOC_PAYIN_D – службовий чек внесення коштів
* DOC_PAYOUT_D - службовий чек вилучення коштів
* DOC_PAYTRANSFER_D - службовий чек переказу коштів на іншу торгову точку
* DOC_PROD_D – акт виробництва
* DOC_PURCHASEINVOICE_D – прибуткова накладна
* DOC_RETURN_D – повернення товару покупцеві
* DOC_RETURNTOSUPPLIER_D – повернення товару постачальнику
* DOC_REVALUATION_D - переоцінка
* DOC_TRANSFER_D – передача товару на іншу торгову точку
* Номер документу (товарний та касові документи мають один номер в межах цього контейнеру) (D70659), 
* ID франчайзі (F11)
* ID торгової точки (P1000000826)
* ID касира-користувача облікової системи (U5)
* Дата та час збереження файлу контейнера (2020-08-29_11-32-20)

> [!IMPORTANT]
> Не використовуйте жоден з зазначених параметрів з імені файлу для визначення будь-чого, наприклад, типу документа. Всі ці параметри мають свої аналоги у самому документі.

Зразок вмісту файлу контейнера наведено в [Додатку 1](#addition1)

У міжсервісній взаємодії використовується той самий формат чеку, але у вигляді Json ([Додаток 2](#addition2)). Json структура має точну відповідність назв елементів, наведених у цьому документі опису. Але для деяких елементів в форматі Xml можливі зміни у назві - вони наведені у дужках. Наприклад, запис **MarketingActionRecords (Xml: MarketingActions)** означає, що колекція **MarketingActionRecords** в Json так і буде називатися, але в Xml буде називатися **MarketingActions**

## <a id="table1">Таблиця 1. Документ ([Document](/Documents/Document.cs))

|Ім'я елементу|Тип даних|Опис|
|:---|:---:|:---|
|AdditionalInfo|String|Додаткова інформація по документу|
|AgentBarcode|String|Штрих-код картки агенту-продавця|
|AgentId|Long|ID торгового агенту-продавця|
|<sup>*</sup>Amount|Double|Сума за документом|
|<sup>*</sup>AmountPaid|Double|Сума оплати|
|BankTransactionInfo (тільки для документів з DocumentType = 8, 16 та PaymentMethod = 1)|Елемент типу [TransactionInfo](#table7)|Докладна інформація по банківській транзакції та платіжному терміналу при оплаті карткою (див. [Таблицю 7](#table7)). Для інших видів оплати (не картка) може бути відсутній.|
|<sup>*</sup>Bias|Int32|Часовий пояс, в якому знаходиться каса, в хвилинах|
|BonusCalculationPrinted|Boolean|Ознака того, що на касі в чеку надруковано повідомлення про нараховані на початку місяця бонуси|
|BonusPaid|Double|Сума бонусної знижки|
|BonusPaymentRecordId|Long|ID запису про списання бонусної суми, отриманого від сервісу CRM|
|<sup>*</sup>ContractorId|Long|ID клієнта з облікової системи франчайзера. (=0 якщо це CRM клієнт)|
|CurrencyId|Byte|Валюта оплати|
|CurrencyRate|Double|Курс валюти|
|CustomerSegments (тільки для документів з DocumentType = 1)|Колекція елементів CustomerSegment|Перелік сегментів, до яких належить покупець, як учасник програми лояльності у CRM системі|
|<sup>*</sup>DateOfApprove|Date|Дата та час (місцеві) затвердження чека|
|<sup>*</sup>DateOfCreate|Date|Дата та час (місцеві) створення чека (вводу першої товарної позиції)|
|DeliveryPointId|Long|ID точки доставки|
|<sup>*</sup>DepartmentId|Long|ID торгової точки|
|<sup>*</sup>DepartmentName|String|Назва торгової точки|
|<sup>*</sup>Details (Xml: Detail) (не обов'язкова для документів з DocumentType = 8, 16, 2048)|Колекція елементів [DocumentDetail](#table2)|Колекція записів з товарами (див. [Таблицю 2](#table2))|
|<sup>*</sup>DocumentGuid|String|Унікальний ідентифікатор документу|
|<sup>*</sup>DocumentNumber|String|Номер документа (бухгалтерський, нумерація може скидатись раз на місяць, чи квартал, чи рік)|
|<sup>*</sup>DocumentType|Int|Тип документу, нумератор (див. [Таблицю 6](#table6))|
|FiscalRegisterFiscalNumber|String|Фіскальний номер фіскального реєстратора|
|FiscalRegisterId|Byte|Нумератор фіскального реєстратора (0...22, докладніше - по запиту)|
|FiscalRegisterName|String|Назва фіскального реєстратора|
|FiscalRegisterSerialNumber|String|Серійний номер фіскального реєстратора|
|FranchiseContractorBarcode|String|Штрих-код картки лояльності клієнта CRM|
|<sup>*</sup>FranchiseContractorId|Long|ID клієнта з CRM. (=0 якщо це клієнт з облікової системи франчайзера)|
|FranchiseContractorPhoneNumber|String|Номер телефону клієнта CRM, наприклад, 380671234567|
|<sup>*</sup>FranchiseeId|Long|ID франчайзі|
|GiftCertificateSumma|Double|Номінал сертифікату.|
|<sup>*</sup>Id|Long|Внутрішній номер документу (внутрішній номер в табл. nakl)|
|<sup>*</sup>IsFiscal|Xml: Byte, Json: Bool|Чек фіскальний (1 або true) або нефіскальний (0 або false)|
|LogRecords|Колекція елементів [LogRecord](#table8)|Перелік записів журналу каси, які стосуються цього чека (див. [Таблицю 8](#table8))|
|MarketingActionRecords (Xml: MarketingActions) (тільки для документів з DocumentType = 1)|Колекція елементів [MarketingActionRecord](#table4)|Перелік подарунків з маркетингових інструментів, які спрацювали для цього чека (див. [Таблицю 4](#table7))|
|MarketingToolRecordDescriptions (тільки для документів з DocumentType = 1)|Колекція елементів [MarketingToolRecordDescription](#table5)|Перелік повідомлень покупцю по періодичних маркетингових інструментах, які спрацювали на сервері та були надруковані у чекові покупця (див. [Таблицю 5](#table5))|
|<sup>*</sup>PaymentMethod (завжди =0 для товарних документів)|Byte|Форма оплати. 0 - готівка, 1 - безготівкова (картка), 2 - кредит, 3 - сертифікат. **Увага! Внаслідок того, що чек може містити декілька ПКО з різними формами оплати, коректне значення PaymentMethod має тільки касовий документ, для товарного документа PaymentMethod завжди =0**|
|PointsFranch|Double|Бали, які були нараховані по товарам франшизи в цьому документу|
|PointsOther|Double|Бали по решті товарів (не франшизи) цього документу|
|SourceDocumentId|Long|ID документу, що став підставою для створення поточного. (=0)|
|<sup>*</sup>Status|Byte|Статус документа (0 - відкладений, 1 - проведений)|
|SupportingDocument|String|Підстава або коментар|
|TopDocumentGuid|String|Унікальний ідентифікатор зв'язаного документу (використовується для повернення товарів, посилання на видаткову накладну)|
|TopDocumentId|Long|Посилання на ID видаткової накладної в документі оплати|
|<sup>*</sup>TransactionTypeId|Long|ID статті руху документа|
|UserFullName|String|Повне ім'я користувача (касира)|
|<sup>*</sup>UserID|Long|ID користувача (касира)|
|<sup>*</sup>UserName|String|Ім'я користувача (касира)|

## <a id="table2">Таблиця 2. Запис з товаром ([DocumentDetail](/Documents/Document.cs))

|Ім'я елементу|Тип даних|Опис|
| :---| :---:| :---|
|<sup>*</sup>AmountToPay|Double|Сума до оплати товару. Розраховується як добуток *SalePrice* * *Quantity*|
|Barcode|String|Штрих-код товару|
|BarcodeId|Long|ID штрих-коду товару|
|BonusSum|Double|Бонусна частка в оплаті за товар|
|CurrentQuantity|Double|Поточна кількість|
|<sup>*</sup>Discount|Double|Знижка як різниця між реєстровою ціною та факт. роздрібної у поточному документі. тобто, *Discount = PrimaryPrice - SalePrice*|
|Discounts|Колекція елементів [Discount](#table3)|Колекція записів про знижку/нарахування на суму товару (див. [Таблицю 3](#table3))|
|<sup>*</sup>DocumentDetailGuid|Guid|Унікальний ідентифікатор товарного запису|
|<sup>*</sup>DocumentId|Long|Посилання на документ, який містить запис|
|ExciseMarkBarcode|String|Штрих-код акцизної марки|
|FranchGoodId|Long|ID товару CRM|
|<sup>*</sup>GoodId|Long|ID товару|
|<sup>*</sup>GoodsCategoryId|Long|ID групи товару|
|<sup>*</sup>GoodsItemName|String|Назва товару|
|<sup>*</sup>GoodsUomId|Long|ID одиниці виміру у БД франчайзі|
|<sup>*</sup>GoodsUomName|String|Назва одиниці виміру|
|<sup>*</sup>Id|Long|ID запису|
|<sup>*</sup>InventoryRecordDate|Date|Дата та час оновлення реєстрового запису (для онлайн режиму - поточні, для офлайн - дата останньої синхронізації офлайн БД)|
|InventoryRecordId|Long|ID реєстрового запису|
|MaxAllowedDiscountPercent|Double|Максимальна дозволена знижка на товар у відсотках за даними облікової системи. Значення від 0 до 1, де 1 - знижка 100%. Значення присутнє тільки у документах продажу.|
|MaxAllowedPrice|Double|Максимальна дозволена ціна на товар за даними облікової системи. Значення присутнє тільки у документах продажу.|
|MinAllowedPrice|Double|Мінімальна дозволена ціна товару. Розраховується як *MinAllowedPrice = PrimaryPrice * (1 - MaxAllowedDiscountPercent)* Значення присутнє тільки у документах продажу.|
|<sup>*</sup>MoneySum|Double|Сума оплати однією з форм оплати (як різниця між сумою товару та бонусною часткою в оплаті)|
|PointsSum|Double|Бали, нараховані за товар|
|PrimaryPrice|Double|Початкова ціна товару. У чеку продажу тут реєстрова ціна, у чеку повернення - ціна, за якою товар було продано, з урахуванням усіх знижок на ціну під час продажу, крім знижки/нарахування на суму в результаті округлення копійками.|
|PurchasePrice|Double|Закупівельна ціна (для документу Прибуткова накладна)|
|<sup>*</sup>Quantity|Double|Кількість в цьому товарному записі (для документів крім "різницевий перерахунок залишків")|
|QuantityDifference|Double|Різниця у кількості (для документа "різницевий" перерахунок залишків"). Від'ємне значення - кількість зменшується, позитивне - кількість збільшується|
|QuantityInPack|Double|Кількість в упаковці (не використовується)|
|QuantityPack|Double|Кількість упаковок (=0)|
|<sup>*</sup>SalePrice|Double|Фактична роздрібна ціна з урахуванням усіх знижок. Це ціна для облікової системи, оскільки може мати багато знаків після коми. У цій ціні враховано всі знижки та округлення, які застосовуються до суми товару. Добуток *SalePrice* та *Quantity* дасть точну суму до оплати товару *AmountToPay*.|
|SalePriceAfterRevaluation|Double|Ціна після переоцінки (для документа переоцінки). Ненульове значення має тільки документ переоцінки.|
|TopDocumentDetailGuid|Guid|Guid товарного запису, на який посилається цей товарний запис. Наприклад, товар-доповнення до певної страви|
|TopGoodId|Long|Id товара, на який посилається цей товарний запис. Наприклад, товар-доповнення до певної страви|
|Uktzed|String|Код українського класифікатора товарів зовнішньої економічної діяльності|

## <a id="table3">Таблиця 3. Запис про знижку або надбавку на суму по товару ([Discount](/Documents/Document.cs))

|Ім'я елементу|Тип даних|Опис|
| :---| :---:| :---|
|<sup>*</sup>DiscountType|Byte|Тип знижки, енумератор: 0 - Фіксована (дисконтна картка, точне зазначення ціни, тощо.), 1 - Накопичувальна, 2 - Колонка прайсу 1, 3 - Колонка прайсу 2, 4 - Колонка прайсу 3, 5 - Колонка прайсу 4, 6 - Колонка прайсу 5, 7 - Не використовується, 8 - Бонусна знижка, 9 - По сертифікату, 10 - Заокруглення копійок, 11 - Колонка прайсу 6, 12 - Колонка прайсу 7, 13 - Колонка прайсу 8, 14 - Колонка прайсу 9, 15 - Колонка прайсу 10, 16 - Колонка прайсу 11, 17 - Колонка прайсу 12, 18 - Колонка прайсу 13, 19 - Колонка прайсу 14, 20 - Колонка прайсу 15|
|<sup>*</sup>DiscountValue|Double|Значення знижки/надбавку на суму по товару. Позитивне значення - знижка, від'ємне - надбавка.|

## <a id="table4">Таблиця 4. Запис по маркетинговій акції та маркетинговому інструменту, яким задовольняє чек ([MarketingActionRecord](/Marketing/MarketingActionRecord.cs))

|Ім'я елементу|Тип даних|Опис|
| :---| :---:| :---|
|<sup>*</sup>BonusPercent|Double|Відсоток бонусів від суми проданого товара (0...1)|
|<sup>*</sup>GiftCode|String(255)|Подарунковий код|
|<sup>*</sup>GoodsItemId|Long|ID товара, для якого спрацював маркетинговий інструмент|
|<sup>*</sup>GoodsItemPrice|Double|Запропонована акціонна ціна на товар|
|<sup>*</sup>GoodsItemQuantity|Double|Кількість подарованого товара|
|<sup>*</sup>Id|Long|ID запису|
|<sup>*</sup>MarketingActionId|Long|ID маркетингової акції|
|<sup>*</sup>MarketingActionName|String(255)|Назва маркетингової акції|
|<sup>*</sup>MarketingToolId|Long|ID маркетингового інструменту|
|<sup>*</sup>MarketingToolName|String(255)|Назва маркетингового інструменту|
|<sup>*</sup>MoneyDiscount|Double|Знижка на ціну в гривнях|
|<sup>*</sup>PointsPercent|Double|Відсоток балів від суми проданого товара (0...1)|
|<sup>*</sup>PresentType (Xml: MarketingPresentType)|Int32|Нумератор типа подарунку|
|<sup>*</sup>PriceColumnNumber|Int32|Номер колонки прайсу|

## <a id="table5">Таблиця 5. Запис по відображеному повідомленню від CRM по акціям для клієнта ([MarketingToolRecordDescription](/Marketing/MarketingActionRecord.cs))

|Ім'я елементу|Тип даних|Опис|
| :---| :---:| :---|
|<sup>*</sup>Id|Long|ID MarketingToolRecord|

## <a id="table6">Таблиця 6. Нумератор типів документів ([DocumentType](/Documents/Document.cs))

|Ім'я елементу|Опис|
| :---| :---|
|AnyDocument = 0|Не використовується|
|SalesInvoice = 1|Видаткова накладна|
|PurchaseInvoice = 2|Прибуткова накладна|
|CustomerOrder = 4|Замовлення від клієнта|
|PayInSlip = 8|Прибутковий касовий ордер|
|PayOutOrder = 16|Видатковий касовий ордер|
|PurchaseOrder = 32|Замовлення постачальнику|
|CustomerReturnOrder = 64|Повернення від покупця|
|SupplierReturnOrder = 128|Повернення постачальнику|
|Correction = 256|Перерахунок залишків|
|Revaluation = 512|Переоцінка|
|GoodsTransferNote = 1024|Накладна на передачу|
|TransferOrder = 2048|Касовий ордер на передачу|

## <a id="table7">Таблиця 7. Докладна інформація по банківській транзакції та платіжному терміналу при оплаті карткою ([TransactionInfo](/Bank/TransactionInfo.cs))

|Ім'я елементу|Тип даних|Опис|
| :---| :---:| :---|
|<sup>*</sup>Amount|Double|Сума транзакції|
|<sup>*</sup>AuthCode|String|Код авторизації (до 6 символів)|
|<sup>*</sup>BankName|String|Назва банку (до 18 символів)|
|<sup>*</sup>CardNumber|String|Номер картки (до 18 символів)|
|<sup>*</sup>InvoiceNumber|Int32|Номер чека.|
|<sup>*</sup>IsSignatureRequired|Bool|Вимагається підпис (true/false)|
|<sup>*</sup>MerchantId|Int32|Код продавця|
|OtherAdditionalData|String|Інші додаткові дані по транзакції (серіалізовані)|
|<sup>*</sup>PaymentSystemName|String|Платіжна система (до 18 символів)|
|<sup>*</sup>PosNumber|String|Номер терміналу (до 18 символів)|
|<sup>*</sup>RRN|String|Код RRN транзакції (до 12 символів)|
|<sup>*</sup>TransactionDate|String|Дата транзакції (у форматі відповіді ПС).|

## <a id="table8">Таблиця 8. Запис журналу дій касира ([LogRecord](/LogRecord.cs))

|Ім'я елементу          |Тип даних|Опис                                 |
|                   :---|    :---:|                                 :---|
|AppVersion             |String   |Назва та версія касового додатку     |
|CashierId              |Long   |ID касира                            |
|ContractorName         |String   |Ім'я контрагента                     |
|DepartmentBalance      |Double   |Залишок в касі                       |
|DepartmentName         |String   |Назва торгової точки                 |
|DocumentSlot           |String   |Номер відкладеного чеку              |
|ErrorDescription       |String   |Опис помилки                         |
|ErrorModule            |String   |Місце помилки                        |
|ErrorNumber            |Long   |Номер помилки                        |
|GoodsItemAmount        |Double   |Сума по товару                       |
|GoodsItemBarcode       |String   |Штрихкод товару                      |
|GoodsItemName          |String   |Назва товару                         |
|GoodsItemPrice         |Double   |Ціна товару                          |
|GoodsItemQuantity      |Double   |Кількість товару                     |
|GoodsItemQuantityReestr|Double   |Поточна кількість товару по реєстру  |
|Id                     |Long   |ID запису                            |
|Info                   |String   |Додаткова інформація                 |
|LogLevel               |Byte     |Рівень логування                     |
|Message                |String   |Подія                                |
|Timestamp              |DateTime |Дата та час запису у форматі UnixDate|

## <a id="addition1">Додаток 1. Зразок файлу (*.TCUDOC), що містить чек та його оплату (XML)

Продаж товару за ціною 36,90 грн/кг та кількістю 1,018 кг, знижка 16,90 грн/кг на ціну, заокруглення підсумкової суми шляхом додавання до суми за товар 0,04 грн.
```xml
<?xml version="1.0" encoding="windows-1251"?>
<ArrayOfDocument address="м. Черкаси, вул. Чорновола, 52/1" hash="27f038ccc45cdeb1361df4a51b7e0264" originalFileName="DOC_D70659_F11_P1000000826_U5_2020-08-29_11-32-20.tcudoc" software="ShopDesk 5.12.614 ©ANDRIY.CO" wpGuid="{D271E27D-1FFC-4E35-A8D2-C44FE78BAB56}" wpName="Shopdesk м. Черкаси, вул. Чорновола, буд. №52/1">
  <Document>
    <AdditionalInfo></AdditionalInfo>
    <AgentBarcode></AgentBarcode>
    <AgentId>0</AgentId>
    <Amount>20.40</Amount>
    <AmountPaid>20.40</AmountPaid>
    <Bias>0</Bias>
    <BonusCalculationPrinted>False</BonusCalculationPrinted>
    <BonusPaid>0.00</BonusPaid>
    <BonusPaymentRecordId>0</BonusPaymentRecordId>
    <ContractorId>111</ContractorId>
    <CurrencyId>0</CurrencyId>
    <CurrencyRate>0</CurrencyRate>
    <CustomerSegments></CustomerSegments>
    <DateOfApprove>2020-08-29 11:32:20</DateOfApprove>
    <DateOfCreate>2020-08-29 11:31:50</DateOfCreate>
    <DeliveryPointId>0</DeliveryPointId>
    <DepartmentId>1000000826</DepartmentId>
    <DepartmentName>м. Черкаси, вул. Чорновола, буд. №52/1</DepartmentName>
    <Detail>
      <DocumentDetail>
        <AmountToPay>20.40</AmountToPay>
        <Barcode></Barcode>
        <BarcodeId>0</BarcodeId>
        <BonusSum>0.00</BonusSum>
        <CurrentQuantity>0.000</CurrentQuantity>
        <Discount>16.86</Discount>
        <Discounts>
          <Item>
            <DiscountType>0</DiscountType>
            <DiscountValue>17.2</DiscountValue>
          </Item>
          <Item>
            <DiscountType>10</DiscountType>
            <DiscountValue>-0.04</DiscountValue>
          </Item>
        </Discounts>
        <DocumentDetailGuid>{94ee0ca6-79e8-4d62-bec8-d26b081f4c8d}</DocumentDetailGuid>
        <DocumentId>70659</DocumentId>
        <FranchGoodId>0</FranchGoodId>
        <GoodId>6155</GoodId>
        <GoodsCategoryId>421</GoodsCategoryId>
        <GoodsItemName>Пельмені ваг.</GoodsItemName>
        <GoodsUomId>1</GoodsUomId>
        <GoodsUomName>кг</GoodsUomName>
        <Id>2</Id>
        <InventoryRecordDate>2020-08-27 14:49:51</InventoryRecordDate>
        <InventoryRecordId>2141101300</InventoryRecordId>
        <MaxAllowedDiscountPercent>0</MaxAllowedDiscountPercent>
        <MaxAllowedPrice>0.00</MaxAllowedPrice>
        <MinAllowedPrice>36.90</MinAllowedPrice>
        <MoneySum>20.40</MoneySum>
        <PointsSum>0.04</PointsSum>
        <PrimaryPrice>36.9</PrimaryPrice>
        <PurchasePrice>0</PurchasePrice>
        <Quantity>1.018</Quantity>
        <QuantityDifference>0.000</QuantityDifference>
        <QuantityInPack>1.000</QuantityInPack>
        <QuantityPack>0.000</QuantityPack>
        <SalePrice>20.0392927308448</SalePrice>
        <SalePriceAfterRevaluation>0.00</SalePriceAfterRevaluation>
        <TopGoodId>0</TopGoodId>
      </DocumentDetail>
    </Detail>
    <DocumentGuid>{fe7ac576-ac4e-4d83-8f77-d949a848b261}</DocumentGuid>
    <DocumentNumber>70659</DocumentNumber>
    <DocumentType>1</DocumentType>
    <FiscalRegisterFiscalNumber>437578</FiscalRegisterFiscalNumber>
    <FiscalRegisterId>22</FiscalRegisterId>
    <FiscalRegisterName>ПРРО Checkbox</FiscalRegisterName>
    <FiscalRegisterSerialNumber>3d79ba352b4f7d41c</FiscalRegisterSerialNumber>
    <FranchiseContractorBarcode></FranchiseContractorBarcode>
    <FranchiseContractorId>0</FranchiseContractorId>
    <FranchiseContractorPhoneNumber></FranchiseContractorPhoneNumber>
    <FranchiseeId>11</FranchiseeId>
    <GiftCertificateSumma>0.00</GiftCertificateSumma>
    <Id>70659</Id>
    <IsFiscal>1</IsFiscal>
    <LogRecords></LogRecords>
    <MarketingActions></MarketingActions>
    <MarketingToolRecordDescriptions></MarketingToolRecordDescriptions>
    <PaymentMethod>0</PaymentMethod>
    <PointsFranch>0.00</PointsFranch>
    <PointsOther>0.00</PointsOther>
    <SourceDocumentId>0</SourceDocumentId>
    <Status>1</Status>
    <SupportingDocument></SupportingDocument>
    <TopDocumentGuid>{fe7ac576-ac4e-4d83-8f77-d949a848b261}</TopDocumentGuid>
    <TopDocumentId>70659</TopDocumentId>
    <TransactionTypeId>4</TransactionTypeId>
    <UserFullName>касир1</UserFullName>
    <UserId>5</UserId>
    <UserName>касир1</UserName>
  </Document>
  <Document>
    <AdditionalInfo></AdditionalInfo>
    <AgentBarcode></AgentBarcode>
    <AgentId>0</AgentId>
    <Amount>20.40</Amount>
    <AmountPaid>20.40</AmountPaid>
    <Bias>0</Bias>
    <BonusCalculationPrinted>False</BonusCalculationPrinted>
    <BonusPaid>0.00</BonusPaid>
    <BonusPaymentRecordId>0</BonusPaymentRecordId>
    <ContractorId>111</ContractorId>
    <CurrencyId>0</CurrencyId>
    <CurrencyRate>0</CurrencyRate>
    <CustomerSegments></CustomerSegments>
    <DateOfApprove>2020-08-29 11:32:20</DateOfApprove>
    <DateOfCreate>2020-08-29 11:32:20</DateOfCreate>
    <DeliveryPointId>0</DeliveryPointId>
    <DepartmentId>1000000826</DepartmentId>
    <DepartmentName>м. Черкаси, вул. Чорновола, буд. №52/1</DepartmentName>
    <Detail></Detail>
    <DocumentGuid>{f38549da-6cfa-4fe4-b65d-5fda5a315550}</DocumentGuid>
    <DocumentNumber>70659</DocumentNumber>
    <DocumentType>8</DocumentType>
    <FiscalRegisterFiscalNumber>437578</FiscalRegisterFiscalNumber>
    <FiscalRegisterId>22</FiscalRegisterId>
    <FiscalRegisterName>ПРРО Checkbox</FiscalRegisterName>
    <FiscalRegisterSerialNumber>3d79ba352b4f7d41c</FiscalRegisterSerialNumber>
    <FranchiseContractorBarcode></FranchiseContractorBarcode>
    <FranchiseContractorId>0</FranchiseContractorId>
    <FranchiseContractorPhoneNumber></FranchiseContractorPhoneNumber>
    <FranchiseeId>11</FranchiseeId>
    <GiftCertificateSumma>0.00</GiftCertificateSumma>
    <Id>70659</Id>
    <IsFiscal>1</IsFiscal>
    <LogRecords></LogRecords>
    <MarketingActions></MarketingActions>
    <MarketingToolRecordDescriptions></MarketingToolRecordDescriptions>
    <PaymentMethod>0</PaymentMethod>
    <PointsFranch>0.00</PointsFranch>
    <PointsOther>0.00</PointsOther>
    <SourceDocumentId>0</SourceDocumentId>
    <Status>1</Status>
    <SupportingDocument>Оплата накладної №70659 від 2020-08-29 11:32:20</SupportingDocument>
    <TopDocumentGuid>{fe7ac576-ac4e-4d83-8f77-d949a848b261}</TopDocumentGuid>
    <TopDocumentId>70659</TopDocumentId>
    <TransactionTypeId>4</TransactionTypeId>
    <UserFullName>касир1</UserFullName>
    <UserId>5</UserId>
    <UserName>касир1</UserName>
  </Document>
</ArrayOfDocument>
```

> [!NOTE]
> Щоб перевірити створену вами структуру XML контейнера, використовуйте **[Сервіс для перевірки структури XML контейнера](https://base2base.com.ua/Shopserver/ContainerTest)**. Зауважте, що сервіс перевіряє тільки структуру, дані не перевіряються.

## <a id="addition2">Додаток 2. Зразок об’єкту для міжсервісного обміну, що містить чек та його оплату (JSON)

```json
{
  "DocFormatDescription": "https://github.com/amukan/AndriyCo.Shopdesk.Containers.git",
  "Address": "м. Черкаси, вул. Чорновола, 52/1",
  "Documents": [
    {
      "AdditionalInfo": "",
      "AgentBarcode": "",
      "AgentId": 0,
      "Amount": 20.4,
      "AmountPaid": 20.4,
      "BankTransactionInfo": null,
      "Bias": 0,
      "BonusCalculationPrinted": false,
      "BonusPaid": 0.0,
      "BonusPaymentRecordId": 0,
      "ContractorId": 111,
      "CurrencyId": 0,
      "CurrencyRate": 0.0,
      "CustomerSegments": [],
      "DateOfApprove": "2020-08-29T11:32:20",
      "DateOfCreate": "2020-08-29T11:31:50",
      "DeliveryPointId": 0,
      "DepartmentId": 1000000826,
      "DepartmentName": "м. Черкаси, вул. Чорновола, буд. №52/1",
      "Details": [
        {
          "AmountToPay": 20.4,
          "Barcode": "",
          "BarcodeId": 0,
          "BonusSum": 0.0,
          "CurrentQuantity": 0.0,
          "Discount": 16.8607072691552,
          "Discounts": [
            {
              "DiscountType": 0,
              "DiscountValue": 17.2
            },
            {
              "DiscountType": 10,
              "DiscountValue": -0.04
            }
          ],
          "DocumentDetailGuid": "94ee0ca6-79e8-4d62-bec8-d26b081f4c8d",
          "DocumentId": 70659,
          "ExciseMarkBarcode": null,
          "FranchGoodId": 0,
          "GoodId": 6155,
          "GoodsCategoryId": 421,
          "GoodsItemName": "Пельмені ваг.",
          "GoodsUomId": 1,
          "GoodsUomName": "кг",
          "Id": 2,
          "InventoryRecordDate": "2020-08-27T14:49:51",
          "InventoryRecordId": 2141101300,
          "MaxAllowedDiscountPercent": 0.0,
          "MaxAllowedPrice": 0.0,
          "MinAllowedPrice": 36.9,
          "MoneySum": 20.4,
          "PointsSum": 0.04,
          "PrimaryPrice": 36.9,
          "PurchasePrice": 0.0,
          "Quantity": 1.018,
          "QuantityDifference": 0.0,
          "QuantityInPack": 1.0,
          "QuantityPack": 0.0,
          "SalePrice": 20.0392927308448,
          "SalePriceAfterRevaluation": 0.0,
          "TopGoodId": 0,
          "Uktzed": null
        }
      ],
      "DocumentGuid": "fe7ac576-ac4e-4d83-8f77-d949a848b261",
      "DocumentNumber": "70659",
      "DocumentType": 1,
      "FiscalRegisterFiscalNumber": "437578",
      "FiscalRegisterId": 22,
      "FiscalRegisterName": "ПРРО Checkbox",
      "FiscalRegisterSerialNumber": "3d79ba352b4f7d41c",
      "FranchiseContractorBarcode": "",
      "FranchiseContractorId": 0,
      "FranchiseContractorPhoneNumber": "",
      "FranchiseeId": 11,
      "GiftCertificateSumma": 0.0,
      "Id": 70659,
      "IsFiscal": true,
      "LogRecords": [],
      "MarketingActionRecords": [],
      "MarketingToolRecordDescriptions": [],
      "PaymentMethod": 0,
      "PointsFranch": 0.0,
      "PointsOther": 0.0,
      "SourceDocumentId": 0,
      "Status": 1,
      "SupportingDocument": "",
      "TopDocumentGuid": "fe7ac576-ac4e-4d83-8f77-d949a848b261",
      "TopDocumentId": 70659,
      "TransactionTypeId": 4,
      "UserFullName": "касир1",
      "UserId": 5,
      "UserName": "касир1"
    },
    {
      "AdditionalInfo": "",
      "AgentBarcode": "",
      "AgentId": 0,
      "Amount": 20.4,
      "AmountPaid": 20.4,
      "BankTransactionInfo": null,
      "Bias": 0,
      "BonusCalculationPrinted": false,
      "BonusPaid": 0.0,
      "BonusPaymentRecordId": 0,
      "ContractorId": 111,
      "CurrencyId": 0,
      "CurrencyRate": 0.0,
      "CustomerSegments": [],
      "DateOfApprove": "2020-08-29T11:32:20",
      "DateOfCreate": "2020-08-29T11:32:20",
      "DeliveryPointId": 0,
      "DepartmentId": 1000000826,
      "DepartmentName": "м. Черкаси, вул. Чорновола, буд. №52/1",
      "Details": [],
      "DocumentGuid": "f38549da-6cfa-4fe4-b65d-5fda5a315550",
      "DocumentNumber": "70659",
      "DocumentType": 8,
      "FiscalRegisterFiscalNumber": "437578",
      "FiscalRegisterId": 22,
      "FiscalRegisterName": "ПРРО Checkbox",
      "FiscalRegisterSerialNumber": "3d79ba352b4f7d41c",
      "FranchiseContractorBarcode": "",
      "FranchiseContractorId": 0,
      "FranchiseContractorPhoneNumber": "",
      "FranchiseeId": 11,
      "GiftCertificateSumma": 0.0,
      "Id": 70659,
      "IsFiscal": true,
      "LogRecords": [],
      "MarketingActionRecords": [],
      "MarketingToolRecordDescriptions": [],
      "PaymentMethod": 0,
      "PointsFranch": 0.0,
      "PointsOther": 0.0,
      "SourceDocumentId": 0,
      "Status": 1,
      "SupportingDocument": "Оплата накладної №70659 від 2020-08-29 11:32:20",
      "TopDocumentGuid": "fe7ac576-ac4e-4d83-8f77-d949a848b261",
      "TopDocumentId": 70659,
      "TransactionTypeId": 4,
      "UserFullName": "касир1",
      "UserId": 5,
      "UserName": "касир1"
    }
  ],
  "Hash": "27f038ccc45cdeb1361df4a51b7e0264",
  "OriginalFileName": "DOC_D70659_F11_P1000000826_U5_2020-08-29_11-32-20.tcudoc",
  "Software": "ShopDesk 5.12.614 ©ANDRIY.CO",
  "WpGuid": "{D271E27D-1FFC-4E35-A8D2-C44FE78BAB56}",
  "WpName": "Shopdesk м. Черкаси, вул. Чорновола, буд. №52/1"
}
```
