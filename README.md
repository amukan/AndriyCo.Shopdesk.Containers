# Опис структури контейнеру документів ANDRIY.CO Shopdesk

Файл чеку є контейнером для документів різного типу (зазвичай, товарний документ та касові документи, наприклад, для чеку продажу – видаткова накладна та один або декілька прибуткових касових ордерів, або взагалі не містить ПКО, якщо оплати не було). В одному контейнері є перелік товарів по одній торговій точці. Якщо в чеку на касі були товари з двох або більше торгових точок, то буде створена відповідна кількість контейнерів, кожний з товарами тільки по одній точці. Також контейнер може містити виключно касові документи, якщо це був чек внесення або вилучення коштів.

Приклад імені файлу контейнера документів

DOC_D70659_F11_P1000000826_U5_2020-08-29_11-32-20.tcudoc, де: 

* DOC_D – видаткова накладна з (або без) оплатою – звичайний чек продажу
* DOC_INVENTORY_D - інвентаризація (перерахунок залишків)
* DOC_CUSTOMERORDER_D - замовлення від покупця
* DOC_ORDER_D - замовлення постачальнику
* DOC_PAYIN_D – службовий чек внесення коштів
* DOC_PAYOUT_D - службовий чек вилучення коштів
* DOC_PAYTRANSFER_D - службовий чек переказу коштів на іншу торгову точку
* DOC_PROD_D – акт виробництва
* DOC_PURCHASEINVOICE_D – прибуткова накладна
* DOC_RETURN_D – повернення товару покупцеві
* DOC_RETURNTOSUPPLIER_D – повернення товару постачальнику
* DOC_REVALUATION_D - переоцінка
* DOC_TRANSFER_D – передача товару на іншу торгову точку
* DOC_SHIPPINGDECL_D - декларація на прихід від постачальника
* Номер документу (товарний та касові документи мають один номер в межах цього контейнеру) (D70659), 
* ID франчайзі (F11)
* ID торгової точки (P1000000826)
* ID касира-користувача облікової системи (U5)
* Дата та час збереження файлу контейнера (2020-08-29_11-32-20)

> [!IMPORTANT]
> Не використовуйте жоден з зазначених параметрів з імені файлу для визначення будь-чого, наприклад, типу документа. Всі ці параметри мають свої аналоги у самому документі.

Зразок вмісту файлу контейнера наведено в [Додатку 1](#addition1)

У міжсервісній взаємодії використовується той самий формат чеку, але у вигляді Json ([Додаток 2](#addition2)). Json структура має точну відповідність назв елементів, наведених у цьому документі опису. Але для деяких елементів в форматі Xml можливі зміни у назві або у типу даних - вони наведені у дужках. Наприклад, запис **MarketingActionRecords (`Xml`: MarketingActions)** означає, що колекція **MarketingActionRecords** в Json так і буде називатися, але в Xml буде називатися **MarketingActions**

## <a id="table1">Таблиця 1. Документ ([Document](/Documents/Document.cs))

|Ім'я елементу|[req](## "Обов'язкове поле: '+' - так, '-' - ні")|Тип даних|Опис|
|:---|:---:|:---|:---|
|AdditionalInfo		                | - |String |Додаткова інформація по документу|
|AgentBarcode		                | - |String	|Штрих-код картки агенту-продавця|
|AgentId		                    | - |Int64	|ID торгового агенту-продавця|
|Amount		            			| + |Double	|Сума за документом|
|AmountPaid		        			| + |Double	|Сума оплати|
|BankTransactionInfo (тільки для документів з DocumentType = 8, 16 та PaymentMethod = 1)| - |Елемент типу [TransactionInfo](#table7)	|Докладна інформація по банківській транзакції та платіжному терміналу при оплаті карткою (див. [Таблицю 7](#table7)). Для інших видів оплати (не картка) може бути відсутній.|
|Bias		            			| + |Int32	|Часовий пояс, в якому знаходиться каса, в хвилинах|
|BonusFranch		                | - |Double	|Бонуси, нараховані по товарах франшизи, що продані в цьому чеку|
|BonusOther		                    | - |Double	|Бонуси по решті товарів - по товарах, що продаються поза франшизою, і які продані в цьому чеку|
|BonusPaid		                    | - |Double	|Сума бонусної знижки|
|BonusPaymentRecordId		        | - |Int64	|ID запису про списання бонусної суми, отриманого від сервісу CRM|
|ContractorId		    			| + |Int64	|ID клієнта з облікової системи франчайзера. (=0 якщо це CRM клієнт)|
|CurrencyId		                    | - |Byte	|Валюта оплати|
|CurrencyRate		                | - |Double	|Курс валюти|
|CustomerSegments (тільки для документів з DocumentType = 1)| - |Колекція елементів CustomerSegment	|Перелік сегментів, до яких належить покупець, як учасник програми лояльності у CRM системі|
|DateOfApprove		    			| + |Date	|Дата та час (місцеві) затвердження чека у форматі *yyyy-MM-dd HH:mm:ss*|
|DateOfCreate		    			| + |Date	|Дата та час (місцеві) створення чека (вводу першої товарної позиції) у форматі *yyyy-MM-dd HH:mm:ss*|
|DeliveryAddress					| - |String	|Адреса доставки для покупця|
|DeliveryPointId		            | - |Int64	|ID точки доставки|
|DeliveryProvider					| - |String	|Назва служби доставки (Глово, Болт тощо)|
|DepartmentId		    			| + |Int64	|ID торгової точки|
|DepartmentName		    			| + |String	|Назва торгової точки|
|Details (`Xml`: Detail) (не обов'язкова для документів з DocumentType = 8, 16, 2048)| + |Колекція елементів [DocumentDetail](#table2)	|Колекція записів з товарами (див. [Таблицю 2](#table2))|
|DocumentGuid		    			| + |String	|Унікальний ідентифікатор документу|
|DocumentNumber		    			| + |String	|Номер документа (бухгалтерський, нумерація може скидатись раз на місяць, чи квартал, чи рік)|
|DocumentType		    			| + |Int32	|Тип документу, нумератор (див. [Таблицю 6](#table6))|
|EditPermissions					| + |(`Xml`: Перелік Int32 (*значення дозвільних флагів*) через пробіл) (`Json`: Int32 (*сума значень дозвільних флагів*))|Права на редагування, нумератор-колекція флагів (див. [EditPermissions](/Documents/Document.cs)). Береться до уваги тільки для документів, які імпортуються із зовнішніх систем. Приклад значення повного переліку дозволів для **XML: \<EditPermissions\>1 2 4 8 16 32\<\/EditPermissions\>**, Приклад значення повного переліку дозволів для **JSON: "EditPermissions": 63**. Працює разом з обмеженнями по товарних записах [EditDetailRestrictions](#table9) на зменшення/збільшення ціни/кількості. Наприклад, якщо EditPermissions не містить флага 4 (дозвіл на збільшення кількості), то перевірка обмеження по збільшенню кількості виконуватися не буде. Якщо флаг 4 присутній, то перевірка на обеження кількості виконуватися буде.| 
|FiscalRegisterFiscalNumber		    | - |String	|Фіскальний номер фіскального реєстратора|
|FiscalRegisterId		            | - |Byte	|Нумератор фіскального реєстратора (0...22, докладніше - по запиту)|
|FiscalRegisterName		            | - |String	|Назва фіскального реєстратора|
|FiscalRegisterSerialNumber		    | - |String	|Серійний номер фіскального реєстратора|
|FranchiseContractorBarcode		    | + |String	|Штрих-код картки лояльності клієнта CRM. Якщо значення немає, вузол може бути порожнім, але має бути присутнім.|
|FranchiseContractorId  			| + |Int64	|ID клієнта з CRM. (=0 якщо це клієнт з облікової системи франчайзера)|
|FranchiseContractorPhoneNumber		| + |String	|Номер телефону клієнта CRM, наприклад, 380671234567. Якщо значення немає, вузол може бути порожнім, але має бути присутнім.|
|FranchiseeId		    			| + |Int64	|ID франчайзі|
|GiftCertificateSumma		        | - |Double	|Номінал сертифікату.|
|Id		                			| + |Int64	|Внутрішній номер документу (внутрішній номер в табл. nakl)|
|IsFiscal		        			| + |`Xml`: Byte, `Json`: Bool	|Чек фіскальний (1 або true) або нефіскальний (0 або false)|
|LogRecords		                    | - |Колекція елементів [LogRecord](#table8)|Перелік записів журналу каси, які стосуються цього чека (див. [Таблицю 8](#table8))|
|MarketingActionRecords (`Xml`: MarketingActions) (тільки для документів з DocumentType = 1)| - |Колекція елементів [MarketingActionRecord](#table4)|Перелік подарунків з маркетингових інструментів, які спрацювали для цього чека (див. [Таблицю 4](#table4))|
|MarketingLogRecords		        | - |Колекція елементів [MarketingLogRecord]|Перелік записів журналу обробки маркетингових інструментів|
|MarketingToolRecordDescriptions (тільки для документів з DocumentType = 1)| - |Колекція елементів [MarketingToolRecordDescription](#table5)|Перелік повідомлень покупцю по періодичних маркетингових інструментах, які спрацювали на сервері та були надруковані у чекові покупця (див. [Таблицю 5](#table5))|
|MessageFromCustomer                | - |String	|Повідомлення від покупця для оператора (продавця, касира, кухаря, кур'єра тощо)|
|NumberOfUtensils					| - |Int32	|Кількість приборів для замовлення покупця (виделки, ложки, палички тощо)|
|PaymentMethod (завжди =0 для товарних документів)| + |Byte|Форма оплати. 0 - готівка, 1 - безготівкова (картка), 2 - кредит, 3 - сертифікат та інші ([public enum PaymentMethod](/Documents/Document.cs))  **Увага! Внаслідок того, що чек може містити декілька ПКО/ВКО з різними формами оплати, коректне значення PaymentMethod має тільки касовий документ, для товарного документа PaymentMethod завжди =0**|
|SaleChannel		                | + |Int32	|Канал продажу (1 - каса, 2 - E-Commerce, 4 - QR Меню, 8 - Vending Machine тощо)|
|SessionDocumentNumber		        | - |String	|Номер документа у зміні (нумерація починається з 1 після відкриття зміни, може мати числовий сталий префікс)|
|SourceDocumentGuid					| - |Guid	|Унікальний ідентифікатор документа, що став підставою для створення поточного|
|SourceDocumentId		            | - |Int64	|ID документу, що став підставою для створення поточного (=0)|
|Status		            			| + |Byte	|Статус документа (0 - відкладений, 1 - проведений). Це рекомендація для облікової системи, затверджувати документ чи ні. Цей статус налаштовується для різних типів документів на касі і зазвичай для продажів рекомендується проводити цей чек в обліковій системі, а решта типів документів зазвичай потребує уваги від операторів облікової системи, тому автоматичне затвердження не рекомендується. Рішення по затвердженню документа залишається за вашою обліковою системою.|
|SupportingDocument		            | - |String	|Підстава або коментар|
|TopDocumentGuid		            | - |Guid	|Унікальний ідентифікатор зв'язаного документу (використовується для повернення товарів, посилання на видаткову накладну)|
|TopDocumentId		                | - |Int64	|Посилання на ID видаткової накладної в документі оплати|
|TransactionTypeId					| + |Int64	|ID статті руху документа|
|UserFullName		                | - |String	|Повне ім'я користувача (касира)|
|UserID		            			| + |Int64	|ID користувача (касира)|
|UserName		        			| + |String	|Ім'я користувача (касира)|

## <a id="table2">Таблиця 2. Запис з товаром ([DocumentDetail](/Documents/Document.cs))

|Ім'я елементу|[req](## "Обов'язкове поле: '+' - так, '-' - ні")|Тип даних|Опис|
|:---|:---:|:---|:---|
|AmountToPay                        | + |Double     |Сума до оплати товару. Розраховується як добуток *SalePrice* * *Quantity*|
|Barcode                            | - |String     |Штрих-код товару|
|BarcodeId                          | - |Int64      |ID штрих-коду товару|
|BonusGeneratePercent               | - |Double     |Відсоток нарахування бонусів відносно суми оплати (зазвичай береться з такого ж параметру групи товарів, до якої належить цей товар). Значення від 0 до 1|
|BonusSum                           | - |Double     |Бонусна частка в оплаті за товар|
|CalculatedBonus                    | - |Double     |Бонуси, нараховані за товар|
|CurrentQuantity                    | - |Double     |Поточна кількість|
|Discount                           | + |Double     |Знижка як різниця між реєстровою ціною та факт. роздрібної у поточному документі. тобто, *Discount = PrimaryPrice - SalePrice*|
|Discounts                          | - |Колекція елементів [Discount](#table3)|Колекція записів про знижку/нарахування на суму товару (див. [Таблицю 3](#table3))|
|DocumentDetailGuid                 | + |Guid       |Унікальний ідентифікатор товарного запису|
|DocumentId                         | + |Int64      |Посилання на документ, який містить запис|
|EditDetailRestrictions             | - |[EditDetailRestrictions](#table9)|Дозволи на редагування кількості та ціни, якщо документ надійшов із зовнішньої системи|
|ExciseMarkBarcode                  | - |String     |Штрих-код акцизної марки|
|FranchGoodId                       | + |Int64      |ID товару CRM|
|GoodId                             | + |Int64      |ID товару|
|GoodsCategoryId                    | + |Int64      |ID групи товару|
|GoodsItemName                      | + |String     |Назва товару|
|GoodsItemType                      | + |Byte       |Тип товару. Нумератор [GoodsItemType](/Documents/Document.cs). Ingredient = 1 (Елементарний товар (інгредієнт)), Product = 2 (Виріб), Job = 4 (Робота/послуга)|
|GoodsUomId                         | + |Int64      |ID одиниці виміру у БД франчайзі|
|GoodsUomName                       | + |String     |Назва одиниці виміру|
|Id                                 | + |Int64      |ID запису|
|InventoryRecordDate                | + |Date       |Дата та час оновлення реєстрового запису (для онлайн режиму - поточні, для офлайн - дата останньої синхронізації офлайн БД) у форматі *yyyy-MM-dd HH:mm:ss*|
|InventoryRecordId                  | - |Int64      |ID реєстрового запису|
|MaxAllowedDiscountPercent          | - |Double     |Максимальна дозволена знижка на товар у відсотках за даними облікової системи. Значення від 0 до 1, де 1 - знижка 100%. Значення присутнє тільки у документах продажу.|
|MaxAllowedPrice                    | - |Double     |Максимальна дозволена ціна на товар за даними облікової системи. Значення присутнє тільки у документах продажу.|
|MinAllowedPrice                    | - |Double     |Мінімальна дозволена ціна товару. Розраховується як *MinAllowedPrice = PrimaryPrice * (1 - MaxAllowedDiscountPercent)* Значення присутнє тільки у документах продажу.|
|MoneySum                           | + |Double     |Сума оплати однією з форм оплати (як різниця між сумою товару та бонусною часткою в оплаті)|
|PrimaryPrice                       | - |Double     |Початкова ціна товару. У чеку продажу тут реєстрова ціна, у чеку повернення - ціна, за якою товар було продано, з урахуванням усіх знижок на ціну під час продажу, крім знижки/нарахування на суму в результаті округлення копійками.|
|PurchasePrice                      | - |Double     |Закупівельна ціна (для документу Прибуткова накладна)|
|Quantity                           | + |Double     |Кількість в цьому товарному записі (для документів крім "різницевий перерахунок залишків")|
|QuantityDifference                 | - |Double     |Різниця у кількості (для документа "різницевий" перерахунок залишків"). Від'ємне значення - кількість зменшується, позитивне - кількість збільшується|
|QuantityInPack                     | - |Double     |Кількість в упаковці (не використовується)|
|QuantityPack                       | - |Double     |Кількість упаковок (=0)|
|SalePrice                          | + |Double     |Фактична роздрібна ціна з урахуванням усіх знижок. Це ціна для облікової системи, оскільки може мати багато знаків після коми. У цій ціні враховано всі знижки та округлення, які застосовуються до суми товару. Добуток *SalePrice* та *Quantity* дасть точну суму до оплати товару *AmountToPay*.|
|SalePriceAfterRevaluation          | - |Double     |Ціна після переоцінки (для документа переоцінки). Ненульове значення має тільки документ переоцінки.|
|SourceDocumentDetailGuid           | - |Guid       |Guid товарного запису в документі, що став підставою для створення поточного документа, і з якого створено цей товарний запис|
|TopDocumentDetailGuid              | - |Guid       |Guid товарного запису, на який посилається цей товарний запис. Наприклад, товар-доповнення до певної страви|
|TopGoodId                          | - |Int64      |Id товара, на який посилається цей товарний запис. Наприклад, товар-доповнення до певної страви|
|Uktzed                             | - |String     |Код українського класифікатора товарів зовнішньої економічної діяльності|

## <a id="table3">Таблиця 3. Запис про знижку або надбавку на суму по товару ([Discount](/Documents/Document.cs))

|Ім'я елементу|[req](## "Обов'язкове поле: '+' - так, '-' - ні")|Тип даних|Опис|
|:---|:---:|:---|:---|
|DiscountType   | + |Byte   |Тип знижки, енумератор: 0 - Фіксована (дисконтна картка, точне зазначення ціни, тощо.), 1 - Накопичувальна, 2 - Колонка прайсу 1, 3 - Колонка прайсу 2, 4 - Колонка прайсу 3, 5 - Колонка прайсу 4, 6 - Колонка прайсу 5, 7 - Не використовується, 8 - Бонусна знижка, 9 - По сертифікату, 10 - Заокруглення копійок, 11 - Колонка прайсу 6, 12 - Колонка прайсу 7, 13 - Колонка прайсу 8, 14 - Колонка прайсу 9, 15 - Колонка прайсу 10, 16 - Колонка прайсу 11, 17 - Колонка прайсу 12, 18 - Колонка прайсу 13, 19 - Колонка прайсу 14, 20 - Колонка прайсу 15|
|DiscountValue  | + |Double |Значення знижки/надбавки на суму по товару. Позитивне значення - знижка, від'ємне - надбавка.|

## <a id="table4">Таблиця 4. Запис по маркетинговій акції та маркетинговому інструменту, яким задовольняє чек ([MarketingActionRecord](/Marketing/MarketingActionRecord.cs))

|Ім'я елементу|[req](## "Обов'язкове поле: '+' - так, '-' - ні")|Тип даних|Опис|
|:---|:---:|:---|:---|
|BonusPercent           | + |Double     |Відсоток бонусів від суми проданого товара (0...1)|
|DescriptionToCustomer  | - |String(255)|Повідомлення покупцю|
|DiscountPercent        | + |Double     |Знижка у відсотках (0...1)|
|DocumentDetailGuid     | - |Guid       |Унікальний ідентифікатор товарного запису, до якого відноситься запис по маркетинговій акції та маркетинговому інструменту|
|GiftCode               | - |String(255)|Подарунковий код|
|GoodsItemId            | + |Int64      |ID товара, для якого спрацював маркетинговий інструмент|
|GoodsItemPrice         | + |Double     |Запропонована акціонна ціна на товар|
|GoodsItemQuantity      | + |Double     |Кількість подарованого товара|
|Id                     | + |Int64      |ID запису|
|MarketingActionId      | + |Int64      |ID маркетингової акції|
|MarketingActionName    | + |String(255)|Назва маркетингової акції|
|MarketingToolId        | + |Int64      |ID маркетингового інструменту|
|MarketingToolName      | + |String(255)|Назва маркетингового інструменту|
|MoneyDiscount          | + |Double     |Знижка на ціну в гривнях|
|PresentedBonus (`Xml`: MarketingPresentedBonus)| + |Double|Кількість нарахованих бонусів|
|PresentType (`Xml`: MarketingPresentType)      | + |Int32 |Нумератор типа подарунку|
|PriceColumnNumber      | + |Int32|Номер колонки прайсу|

## <a id="table5">Таблиця 5. Запис по відображеному повідомленню від CRM по акціям для клієнта ([MarketingToolRecordDescription](/Marketing/MarketingActionRecord.cs))

|Ім'я елементу|[req](## "Обов'язкове поле: '+' - так, '-' - ні")|Тип даних|Опис|
|:---   |:---:  |:---   |:---|
|Id     | +     |Int64  |ID MarketingToolRecord|

## <a id="table6">Таблиця 6. Нумератор типів документів ([DocumentType](/Documents/Document.cs))

|Ім'я елементу              |Опис|
|:---                       |:---                       |
|AnyDocument = 0            |Не використовується        |
|SalesInvoice = 1           |Видаткова накладна         |
|PurchaseInvoice = 2        |Прибуткова накладна        |
|CustomerOrder = 4          |Замовлення від клієнта     |
|PayInSlip = 8              |Прибутковий касовий ордер  |
|PayOutOrder = 16           |Видатковий касовий ордер   |
|PurchaseOrder = 32         |Замовлення постачальнику   |
|CustomerReturnOrder = 64   |Повернення від покупця     |
|SupplierReturnOrder = 128  |Повернення постачальнику   |
|Correction = 256           |Перерахунок залишків       |
|Revaluation = 512          |Переоцінка                 |
|GoodsTransferNote = 1024   |Накладна на передачу       |
|TransferOrder = 2048       |Касовий ордер на передачу  |
|Production = 8192          |Виробництво                |
|PayInSlipHold = 65536      |Прибутковий касовий ордер (попереднє утримання оплати по картці)|
|ShippingDeclaration = 131072 |Декларація на прихід від постачальника|

## <a id="table7">Таблиця 7. Докладна інформація по банківській транзакції та платіжному терміналу при оплаті карткою ([TransactionInfo](/Bank/TransactionInfo.cs))

|Ім'я елементу|[req](## "Обов'язкове поле: '+' - так, '-' - ні")|Тип даних|Опис|
|:---                   |:---:|:--- |:---                                               |
|Amount                 | + |Double |Сума транзакції                                    |
|AuthCode               | + |String |Код авторизації (до 6 символів)                    |
|BankName               | + |String |Назва банку (до 18 символів)                       |
|CardNumber             | + |String |Номер картки (до 18 символів)                      |
|InvoiceNumber          | + |Int32  |Номер чека.                                        |
|IsSignatureRequired    | + |Bool   |Вимагається підпис (true/false)                    |
|MerchantId             | + |Int32  |Код продавця                                       |
|OtherAdditionalData    | - |String |Інші додаткові дані по транзакції (серіалізовані)  |
|PaymentSystemName      | + |String |Платіжна система (до 18 символів)                  |
|PosNumber              | + |String |Номер терміналу (до 18 символів)                   |
|RRN                    | + |String |Код RRN транзакції (до 12 символів)                |
|TransactionDate        | + |String |Дата транзакції (у форматі відповіді ПС).          |

## <a id="table8">Таблиця 8. Запис журналу дій касира ([LogRecord](/LogRecord.cs))

|Ім'я елементу          |Тип даних|Опис                                 |
|:---                   |:---     |:---                                 |
|AppVersion             |String   |Назва та версія касового додатку     |
|CashierId              |Int64    |ID касира                            |
|ContractorName         |String   |Ім'я контрагента                     |
|DepartmentBalance      |Double   |Залишок в касі                       |
|DepartmentName         |String   |Назва торгової точки                 |
|DocumentSlot           |String   |Номер відкладеного чеку              |
|ErrorDescription       |String   |Опис помилки                         |
|ErrorModule            |String   |Місце помилки                        |
|ErrorNumber            |Int64    |Номер помилки                        |
|GoodsItemAmount        |Double   |Сума по товару                       |
|GoodsItemBarcode       |String   |Штрихкод товару                      |
|GoodsItemName          |String   |Назва товару                         |
|GoodsItemPrice         |Double   |Ціна товару                          |
|GoodsItemQuantity      |Double   |Кількість товару                     |
|GoodsItemQuantityReestr|Double   |Поточна кількість товару по реєстру  |
|Id                     |Int64    |ID запису                            |
|Info                   |String   |Додаткова інформація                 |
|LogLevel               |Byte     |Рівень логування                     |
|Message                |String   |Подія                                |
|Timestamp              |DateTime |Дата та час запису у форматі UnixDate (*yyyy-MM-ddTHH:mm:ss.fff*)|

## <a id="table9">Таблиця 9. Дозволи на редагування кількості та ціни товарного запису ([EditDetailRestrictions](/Documents/Document.cs))

|Ім'я елементу          |Тип даних|Опис                                 |
|:---                   |:---     |:---                                 |
|PricePercentTolerance  |[PercentTolerance](#table10)|Діапазон редагування ціни товару у відсотках відносно реєстрової ціни|
|QuantityPercentTolerance  |[PercentTolerance](#table10)|Діапазон редагування кількості товару у відсотках відносно поточної кількості|

## <a id="table10">Таблиця 10. Діапазон допусків редагування значень ([PercentTolerance](/Documents/Document.cs))

|Ім'я елементу          |Тип даних|Опис                                 |
|:---                   |:---     |:---                                 |
|DecreasePercent        |Double   |Відсоток зменшення. Не може бути від'ємним або більшим за 1. Лише від 0 до 1. 1 - це 100%, 0 - це 0%. 0 означає, що зменшення не допускається.|
|IncreasePercent        |Double   |Відсоток збільшення. Не може бути від'ємним. 1 - це 100%, 0.1 - це 10%|

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
    <Bias>-180</Bias>
    <BonusFranch>0.00</BonusFranch>
    <BonusOther>0.00</BonusOther>
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
        <PrimaryPrice>36.9</PrimaryPrice>
        <PurchasePrice>0</PurchasePrice>
        <Quantity>1.018</Quantity>
        <QuantityDifference>0.000</QuantityDifference>
        <QuantityInPack>1.000</QuantityInPack>
        <QuantityPack>0.000</QuantityPack>
        <SalePrice>20.0392927308448</SalePrice>
        <SalePriceAfterRevaluation>0.00</SalePriceAfterRevaluation>
        <SourceDocumentDetailGuid p5:nil="true" xmlns:p5="http://www.w3.org/2001/XMLSchema-instance"></SourceDocumentDetailGuid>
        <TopDocumentDetailGuid p5:nil="true" xmlns:p5="http://www.w3.org/2001/XMLSchema-instance"></TopDocumentDetailGuid>
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
	<SourceDocumentGuid p3:nil="true" xmlns:p3="http://www.w3.org/2001/XMLSchema-instance"></SourceDocumentGuid>
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
    <Bias>-180</Bias>
    <BonusFranch>0.00</BonusFranch>
    <BonusOther>0.00</BonusOther>
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
    <SourceDocumentGuid p3:nil="true" xmlns:p3="http://www.w3.org/2001/XMLSchema-instance"></SourceDocumentGuid>
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
      "Bias": -180,
      "BonusFranch": 0.0,
      "BonusOther": 0.0,
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
          "PrimaryPrice": 36.9,
          "PurchasePrice": 0.0,
          "Quantity": 1.018,
          "QuantityDifference": 0.0,
          "QuantityInPack": 1.0,
          "QuantityPack": 0.0,
          "SalePrice": 20.0392927308448,
          "SalePriceAfterRevaluation": 0.0,
          "SourceDocumentDetailGuid": null,
          "TopDocumentDetailGuid": null,
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
      "SourceDocumentGuid": null,
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
      "Bias": -180,
      "BonusFranch": 0.0,
      "BonusOther": 0.0,
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
      "SourceDocumentGuid": null,
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
