# AndriyCo.Shopdesk.Containers
Опис структури контейнеру документів ANDRIY.CO Shopdesk

# Формат XML файлу "TCUDOC"

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

У міжсервісній взаємодії використовується той самий формат чеку, але у вигляді Json. Json структура має точну відповідність назв елементів, наведених у цьому документі опису. Але для деяких елементів в форматі Xml можливі зміни у назві - вони наведені у дужках. Наприклад, запис MarketingActionRecords (Xml: MarketingActions) означає, що колекція MarketingActionRecords в Json так і буде називатися, але в Xml буде називатися MarketingActions

# Таблиця 1. Документ (Document)

|Ім'я елементу|Тип даних|Опис|
|AdditionalInfo |String |Додаткова інформація по документу |
|AgentBarcode |String |Штрих-код картки агенту-продавця |
|AgentId |UInt32 |ID торгового агенту-продавця |
|Amount |Double |Сума за документом |
|AmountPaid |Double |Сума оплати |
|BankTransactionInfo |Елемент типу [TransactionInfo](file:///D:/GoogleDrive/Documents/Shopdesk%20-%20%D0%9E%D0%BF%D0%B8%D1%81%20%D1%84%D0%B0%D0%B9%D0%BB%D1%83%20%D1%87%D0%B5%D0%BA%D0%B0.docx#bookmark=id.xvir7l) |Докладна інформація по банківській транзакції та платіжному терміналу при оплаті карткою (див. [Таблицю 7](file:///D:/GoogleDrive/Documents/Shopdesk%20-%20%D0%9E%D0%BF%D0%B8%D1%81%20%D1%84%D0%B0%D0%B9%D0%BB%D1%83%20%D1%87%D0%B5%D0%BA%D0%B0.docx#bookmark=id.xvir7l)). Для інших видів оплати (не картка) може бути відсутній. |
|Bias |Int32 |Часовий пояс, в якому знаходиться каса, в хвилинах |
|BonusCalculationPrinted |Boolean |Ознака того, що на касі в чеку надруковано повідомлення про нараховані на початку місяця бонуси |
|BonusPaid |Double |Сума бонусної знижки |
|BonusPaymentRecordId |UInt32 |ID запису про списання бонусної суми, отриманого від сервісу CRM |
|ContractorId |UInt32 |ID клієнта з облікової системи франчайзера. (=0 якщо це CRM клієнт) |
|CurrencyId |Byte |Валюта оплати |
|CurrencyRate |Double |Курс валюти |
|CustomerSegments |Колекція елементів CustomerSegment |Перелік сегментів, до яких належить покупець, як учасник програми лояльності у CRM системі |
|DateOfApprove |Date |Дата та час (місцеві) затвердження чека |
|DateOfCreate |Date |Дата та час (місцеві) створення чека (вводу першої товарної позиції) |
|DeliveryPointId |UInt32 |ID точки доставки |
|DepartmentId |UInt32 |ID торгової точки |
|DepartmentName |String |Назва торгової точки |
|Details (Xml: Detail) |Колекція елементів [DocumentDetail](file:///D:/GoogleDrive/Documents/Shopdesk%20-%20%D0%9E%D0%BF%D0%B8%D1%81%20%D1%84%D0%B0%D0%B9%D0%BB%D1%83%20%D1%87%D0%B5%D0%BA%D0%B0.docx#bookmark=id.2r0uhxc) |Колекція записів з товарами (див. [Таблицю 2](file:///D:/GoogleDrive/Documents/Shopdesk%20-%20%D0%9E%D0%BF%D0%B8%D1%81%20%D1%84%D0%B0%D0%B9%D0%BB%D1%83%20%D1%87%D0%B5%D0%BA%D0%B0.docx#bookmark=id.2r0uhxc)) |
|DocumentGuid |String |Унікальний ідентифікатор документу |
|DocumentNumber |UInt32 |Номер документа (бухгалтерський, нумерація може скидатись раз на місяць, чи квартал, чи рік) |
|DocumentType |UInt32 |Тип документу, нумератор (див. [Таблицю 6](file:///D:/GoogleDrive/Documents/Shopdesk%20-%20%D0%9E%D0%BF%D0%B8%D1%81%20%D1%84%D0%B0%D0%B9%D0%BB%D1%83%20%D1%87%D0%B5%D0%BA%D0%B0.docx#bookmark=id.2iq8gzs)) |
|FiscalRegisterFiscalNumber |String |Фіскальний номер фіскального реєстратора |
|FiscalRegisterId |Byte |Нумератор фіскального реєстратора (0...22, докладніше - по запиту) |
|FiscalRegisterName |String |Назва фіскального реєстратора |
|FiscalRegisterSerialNumber |String |Серійний номер фіскального реєстратора |
|FranchiseContractorBarcode |String |Штрих-код картки лояльності клієнта CRM |
|FranchiseContractorId |UInt32 |ID клієнта з CRM. (=0 якщо це клієнт з облікової системи франчайзера) |
|FranchiseContractorPhoneNumber |String |Номер телефону клієнта CRM, наприклад, 380671234567 |
|FranchiseeId |UInt32 |ID франчайзі |
|GiftCertificateSumma |UInt16 |Номінал сертифікату. |
|Id |UInt32 |Внутрішній номер документу (внутрішній номер в табл. nakl) |
|IsFiscal |Xml: Byte, Json: Bool |Чек фіскальний (1 або true) або нефіскальний (0 або false) |
|LogRecords |Колекція елементів LogRecord |Перелік записів журналу каси, які стосуються цього чека |
|MarketingActionRecords (Xml: MarketingActions) |Колекція елементів [MarketingActionRecord](file:///D:/GoogleDrive/Documents/Shopdesk%20-%20%D0%9E%D0%BF%D0%B8%D1%81%20%D1%84%D0%B0%D0%B9%D0%BB%D1%83%20%D1%87%D0%B5%D0%BA%D0%B0.docx#bookmark=id.1jlao46) |Перелік подарунків з маркетингових інструментів, які спрацювали для цього чека (див. [Таблицю 4](file:///D:/GoogleDrive/Documents/Shopdesk%20-%20%D0%9E%D0%BF%D0%B8%D1%81%20%D1%84%D0%B0%D0%B9%D0%BB%D1%83%20%D1%87%D0%B5%D0%BA%D0%B0.docx#bookmark=id.1jlao46)) |
|MarketingToolRecordDescriptions |Колекція елементів [MarketingToolRecordDescription](file:///D:/GoogleDrive/Documents/Shopdesk%20-%20%D0%9E%D0%BF%D0%B8%D1%81%20%D1%84%D0%B0%D0%B9%D0%BB%D1%83%20%D1%87%D0%B5%D0%BA%D0%B0.docx#bookmark=id.43ky6rz) |Перелік повідомлень покупцю по періодичних маркетингових інструментах, які спрацювали на сервері та були надруковані у чекові покупця (див. [Таблицю 5](file:///D:/GoogleDrive/Documents/Shopdesk%20-%20%D0%9E%D0%BF%D0%B8%D1%81%20%D1%84%D0%B0%D0%B9%D0%BB%D1%83%20%D1%87%D0%B5%D0%BA%D0%B0.docx#bookmark=id.43ky6rz)) |
|PaymentMethod(=0 для товарного документа) |Byte |Форма оплати. 0 - готівка, 1 -- безготівкова (картка), 2 -- кредит, 3 -- сертифікат.Увага! Внаслідок того, що чек може містити декілька ПКО з різними формами оплати, коректне значення PaymentMethod має тільки касовий документ, для товарного документа PaymentMethod завжди =0 |
|PointsFranch |Double |Бали, які були нараховані по товарам франшизи в цьому документу |
|PointsOther |Double |Бали по решті товарів (не франшизи) цього документу |
|SourceDocumentId |UInt32 |ID документу, що став підставою для створення поточного. (=0) |
|Status |Byte |Статус документа (0 - відкладений, 1 - проведений) |
|SupportingDocument |String |Підстава або коментар |
|TopDocumentGuid |String |Унікальний ідентифікатор зв'язаного документу (використовується для повернення товарів, посилання на видаткову накладну) |
|TopDocumentId |UInt32 |Посилання на ID видаткової накладної в документі оплати |
|TransactionTypeId |UInt16 |ID статті руху документа |
|UserFullName |String |Повне ім'я користувача (касира) |
|UserID |UInt32 |ID користувача (касира) |
|UserName |String |Ім'я користувача (касира) |

| ІМ’Я ЕЛЕМЕНТУ  | ТИП ДАНИХ | ОПИС |
| ------------- | ------------- | ------------- |
| Content Cell  | Content Cell  | Content Cell  |
| Content Cell  | Content Cell  | Content Cell  |