using AndriyCo.Shopdesk.Containers.Serialization.Xml.Attributes;

namespace AndriyCo.Shopdesk.Containers.Bank
{
    /// <summary>
    /// Докладна інформація по банківській транзакції та платіжному терміналу
    /// </summary>
    public class TransactionInfo
    {
        ///<summary> Сума транзакції </summary>
        [Money] public double Amount { get; set; }

        ///<summary> Код авторизації (до 6 символів) </summary>
        public string AuthCode { get; set; }

        ///<summary> Номер картки (до 18 символів) </summary>
        public string CardNumber { get; set; }

        ///<summary> Номер чека. </summary>
        public int InvoiceNumber { get; set; }

        ///<summary> назва банку (до 18 символів) </summary>
        public string BankName { get; set; }

        ///<summary> Вимагається підпис (true/false) </summary>
        [UppercaseTrueFalse] public bool IsSignatureRequired { get; set; }

        ///<summary> Код продавця </summary>
        public int MerchantId { get; set; }

        ///<summary> Платіжна система (до 18 символів) </summary>
        public string PaymentSystemName { get; set; }

        ///<summary> Номер терміналу (до 18 символів) </summary>
        public string PosNumber { get; set; }

        ///<summary> Код RRN транзакції (до 12 символів) </summary>
        public string RRN { get; set; }

        ///<summary> Дата транзакції (у форматі відповіді ПС). </summary>
        public string TransactionDate { get; set; }

        ///<summary> Інші додаткові дані по транзакції (сераілізовані) </summary>
        public string OtherAdditionalData { get; set; }
    }
}