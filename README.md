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

Не використовуйте жоден з зазначених параметрів з імені файлу для визначення будь-чого, наприклад, типу документа. Всі ці параметри мають свої аналоги у самому документі (тип документу зазначається в DocumentType)

Зразок вмісту файлу контейнера наведено в Додатку 2

У міжсервісній взаємодії використовується той самий формат чеку, але у вигляді Json. Json структура має точну відповідність назв елементів, наведених у цьому документі опису. Але для деяких елементів в форматі Xml можливі зміни у назві - вони наведені у дужках. Наприклад, запис **MarketingActionRecords (Xml: MarketingActions)** означає, що колекція **MarketingActionRecords** в Json так і буде називатися, але в Xml буде називатися **MarketingActions**

# <a id="table1">Таблиця 1. Документ (Document)

|Ім'я елементу|Тип даних|Опис|
|:---|:---:|:---|
|AdditionalInfo|String|Додаткова інформація по документу|
|AgentBarcode|String|Штрих-код картки агенту-продавця|
|AgentId|UInt32|ID торгового агенту-продавця|
|Amount|Double|Сума за документом|
|AmountPaid|Double|Сума оплати|
|BankTransactionInfo|Елемент типу [TransactionInfo](#table7)|Докладна інформація по банківській транзакції та платіжному терміналу при оплаті карткою (див. [Таблицю 7](#table7)). Для інших видів оплати (не картка) може бути відсутній.|
|Bias|Int32|Часовий пояс, в якому знаходиться каса, в хвилинах|
|BonusCalculationPrinted|Boolean|Ознака того, що на касі в чеку надруковано повідомлення про нараховані на початку місяця бонуси|
|BonusPaid|Double|Сума бонусної знижки|
|BonusPaymentRecordId|UInt32|ID запису про списання бонусної суми, отриманого від сервісу CRM|
|ContractorId|UInt32|ID клієнта з облікової системи франчайзера. (=0 якщо це CRM клієнт)|
|CurrencyId|Byte|Валюта оплати|
|CurrencyRate|Double|Курс валюти|
|CustomerSegments|Колекція елементів CustomerSegment|Перелік сегментів, до яких належить покупець, як учасник програми лояльності у CRM системі|
|DateOfApprove|Date|Дата та час (місцеві) затвердження чека|
|DateOfCreate|Date|Дата та час (місцеві) створення чека (вводу першої товарної позиції)|
|DeliveryPointId|UInt32|ID точки доставки|
|DepartmentId|UInt32|ID торгової точки|
|DepartmentName|String|Назва торгової точки|
|Details (Xml: Detail)|Колекція елементів [DocumentDetail](#table7)|Колекція записів з товарами (див. [Таблицю 2](#table2))|
|DocumentGuid|String|Унікальний ідентифікатор документу|
|DocumentNumber|UInt32|Номер документа (бухгалтерський, нумерація може скидатись раз на місяць, чи квартал, чи рік)|
|DocumentType|UInt32|Тип документу, нумератор (див. [Таблицю 6](#table6))|
|FiscalRegisterFiscalNumber|String|Фіскальний номер фіскального реєстратора|
|FiscalRegisterId|Byte|Нумератор фіскального реєстратора (0...22, докладніше - по запиту)|
|FiscalRegisterName|String|Назва фіскального реєстратора|
|FiscalRegisterSerialNumber|String|Серійний номер фіскального реєстратора|
|FranchiseContractorBarcode|String|Штрих-код картки лояльності клієнта CRM|
|FranchiseContractorId|UInt32|ID клієнта з CRM. (=0 якщо це клієнт з облікової системи франчайзера)|
|FranchiseContractorPhoneNumber|String|Номер телефону клієнта CRM, наприклад, 380671234567|
|FranchiseeId|UInt32|ID франчайзі|
|GiftCertificateSumma|UInt16|Номінал сертифікату.|
|Id|UInt32|Внутрішній номер документу (внутрішній номер в табл. nakl)|
|IsFiscal|Xml: Byte, Json: Bool|Чек фіскальний (1 або true) або нефіскальний (0 або false)|
|LogRecords|Колекція елементів LogRecord|Перелік записів журналу каси, які стосуються цього чека|
|MarketingActionRecords (Xml: MarketingActions)|Колекція елементів [MarketingActionRecord](#table4)|Перелік подарунків з маркетингових інструментів, які спрацювали для цього чека (див. [Таблицю 4](#table7))|
|MarketingToolRecordDescriptions|Колекція елементів [MarketingToolRecordDescription](#table5)|Перелік повідомлень покупцю по періодичних маркетингових інструментах, які спрацювали на сервері та були надруковані у чекові покупця (див. [Таблицю 5](#table5))|
|PaymentMethod(=0 для товарного документа)|Byte|Форма оплати. 0 - готівка, 1 - безготівкова (картка), 2 - кредит, 3 - сертифікат. **Увага! Внаслідок того, що чек може містити декілька ПКО з різними формами оплати, коректне значення PaymentMethod має тільки касовий документ, для товарного документа PaymentMethod завжди =0**|
|PointsFranch|Double|Бали, які були нараховані по товарам франшизи в цьому документу|
|PointsOther|Double|Бали по решті товарів (не франшизи) цього документу|
|SourceDocumentId|UInt32|ID документу, що став підставою для створення поточного. (=0)|
|Status|Byte|Статус документа (0 - відкладений, 1 - проведений)|
|SupportingDocument|String|Підстава або коментар|
|TopDocumentGuid|String|Унікальний ідентифікатор зв'язаного документу (використовується для повернення товарів, посилання на видаткову накладну)|
|TopDocumentId|UInt32|Посилання на ID видаткової накладної в документі оплати|
|TransactionTypeId|UInt16|ID статті руху документа|
|UserFullName|String|Повне ім'я користувача (касира)|
|UserID|UInt32|ID користувача (касира)|
|UserName|String|Ім'я користувача (касира)|

# <a id="table2">Таблиця 2. Запис з товаром (DocumentDetail)

|Ім'я елементу|Тип даних|Опис|
| :---| :---:| :---|
|AmountToPay|Double|Сума до оплати товару. Розраховується як добуток *SalePrice* * *Quantity*|
|Barcode|String|Штрих-код товару|
|BarcodeId|UInt32|ID штрих-коду товару|
|BonusSum|Double|Бонусна частка в оплаті за товар|
|CurrentQuantity|Double|Поточна кількість|
|Discount|Double|Знижка як різниця між реєстровою ціною та факт. роздрібної у поточному документі. тобто, *Discount = PrimaryPrice - SalePrice*|
|Discounts|Колекція елементів [Discount](#table3)|Колекція записів про знижку/нарахування на суму товару (див. [Таблицю 3](#table3))|
|DocumentDetailGuid|Guid|Унікальний ідентифікатор товарного запису|
|DocumentId|UInt32|Посилання на документ, який містить запис|
|ExciseMarkBarcode|String|Штрих-код акцизної марки|
|FranchGoodId|UInt32|ID товару CRM|
|GoodId|UInt32|ID товару|
|GoodsCategoryId|UInt32|ID групи товару|
|GoodsItemName|String|Назва товару|
|GoodsUomId|UInt32|ID одиниці виміру у БД франчайзі|
|GoodsUomName|String|Назва одиниці виміру|
|Id|UInt32|ID запису|
|InventoryRecordDate|Date|Дата та час оновлення реєстрового запису (для онлайн режиму - поточні, для офлайн - дата останньої синхронізації офлайн БД)|
|InventoryRecordId|UInt32|ID реєстрового запису|
|MaxAllowedDiscountPercent|Double|Максимальна дозволена знижка на товар у відсотках за даними облікової системи. Значення від 0 до 1, де 1 - знижка 100%. Значення присутнє тільки у документах продажу.|
|MaxAllowedPrice|Double|Максимальна дозволена ціна на товар за даними облікової системи. Значення присутнє тільки у документах продажу.|
|MinAllowedPrice|Double|Мінімальна дозволена ціна товару. Розраховується як *MinAllowedPrice = PrimaryPrice * (1 - MaxAllowedDiscountPercent)* Значення присутнє тільки у документах продажу.|
|MoneySum|Double|Сума оплати однією з форм оплати (як різниця між сумою товару та бонусною часткою в оплаті)|
|PointsSum|Double|Бали, нараховані за товар|
|PrimaryPrice|Double|Початкова ціна товару. У чеку продажу тут реєстрова ціна, у чеку повернення - ціна, за якою товар було продано, з урахуванням усіх знижок на ціну під час продажу, крім знижки/нарахування на суму в результаті округлення копійками.|
|PurchasePrice|Double|Закупівельна ціна (для документу Прибуткова накладна)|
|Quantity|Double|Кількість в цьому товарному записі (для документів крім "різницевий перерахунок залишків")|
|QuantityDifference|Double|Різниця у кількості (для документа "різницевий" перерахунок залишків"). Від'ємне значення - кількість зменшується, позитивне - кількість збільшується|
|QuantityInPack|Double|Кількість в упаковці (не використовується)|
|QuantityPack|Double|Кількість упаковок (=0)|
|SalePrice|Double|Фактична роздрібна ціна з урахуванням усіх знижок. Це ціна для облікової системи, оскільки може мати багато знаків після коми. У цій ціні враховано всі знижки та округлення, які застосовуються до суми товару. Добуток *SalePrice* та *Quantity* дасть точну суму до оплати товару *AmountToPay*.|
|SalePriceAfterRevaluation|Double|Ціна після переоцінки (для документа переоцінки). Ненульове значення тільки у документах переоцінки.|

# <a id="table3">Таблиця 3. Запис про знижку або надбавку на суму по товару (Discount)

|Ім'я елементу|Тип даних|Опис|
| :---| :---:| :---|
|DiscountType|Byte|Тип знижки, енумератор: 
0 - Фіксована (дисконтна картка, точне зазначення ціни, тощо.) 
1 - Накопичувальна 
2 - Колонка прайсу 
3 - Колонка прайсу 
4 - Колонка прайсу 
5 - Колонка прайсу 
6 - Колонка прайсу 
7 - Не використовується
8 - Бонусна знижка
9 - По сертифікату
10 - Заокруглення копійок
11 - Колонка прайсу 6
12 - Колонка прайсу 7
13 - Колонка прайсу 8
14 - Колонка прайсу 9
15 - Колонка прайсу 10
16 - Колонка прайсу 11
17 - Колонка прайсу 12
18 - Колонка прайсу 13
19 - Колонка прайсу 14
20 - Колонка прайсу 15|
|DiscountValue|Double|Значення знижки/надбавку на суму по товару. Позитивне значення - знижка, від'ємне - надбавка.|

# <a id="table4">Таблиця 4. Запис по маркетинговій акції та маркетинговому інструменту, яким задовольняє чек (MarketingActionRecord)

|Ім'я елементу|Тип даних|Опис|
| :---| :---:| :---|
|Id|UInt16|ID запису|
|GoodsItemId|UInt32|ID товара, для якого спрацював маркетинговий інструмент|
|MarketingActionId|UInt32|ID маркетингової акції|
|MarketingActionName|String(255)|Назва маркетингової акції|
|MarketingToolId|UInt32|ID маркетингового інструменту|
|MarketingToolName|String(255)|Назва маркетингового інструменту|
|PresentType (Xml: MarketingPresentType)|UInt16|Нумератор типа подарунку|
|GoodsItemPrice|Double|Запропонована акціонна ціна на товар|
|PriceColumnNumber|UInt16|Номер колонки прайсу|
|GiftCode|String(255)|Подарунковий код|
|PointsPercent|Double|Відсоток балів від суми проданого товара (0...1)|
|BonusPercent|Double|Відсоток бонусів від суми проданого товара (0...1)|
|MoneyDiscount|Double|Знижка на ціну в гривнях|
|GoodsItemQuantity|Double|Кількість подарованого товара|

# <a id="table5">Таблиця 5. Запис по відображеному повідомленню від CRM по акціям для клієнта (MarketingToolRecordDescription)

|Ім'я елементу|Тип даних|Опис|
| :---| :---:| :---|
|Id|UInt32|ID MarketingToolRecord|

# <a id="table6">Таблиця 6. Нумератор типів документів (DocumentType)

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

# <a id="table7">Таблиця 7. Докладна інформація по банківській транзакції та платіжному терміналу при оплаті карткою (TransactionInfo)

|Ім'я елементу|Тип даних|Опис|
| :---| :---:| :---|
|Amount|Double|Сума транзакції|
|AuthCode|String|Код авторизації (до 6 символів)|
|BankName|String|Назва банку (до 18 символів)|
|CardNumber|String|Номер картки (до 18 символів)|
|InvoiceNumber|Unt32|Номер чека.|
|IsSignatureRequired|Bool|Вимагається підпис (true/false)|
|MerchantId|UInt32|Код продавця|
|OtherAdditionalData|String|Інші додаткові дані по транзакції (сераілізовані)|
|PaymentSystemName|String|Платіжна система (до 18 символів)|
|PosNumber|String|Номер терміналу (до 18 символів)|
|RRN|String|Код RRN транзакції (до 12 символів)|
|TransactionDate|String|Дата транзакції (у форматі відповіді ПС).|