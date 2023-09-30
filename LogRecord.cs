using AndriyCo.Shopdesk.Containers.Serialization.Xml.Attributes;
using System;
using System.Xml.Serialization;

namespace AndriyCo.Shopdesk.Containers
{
    [XmlType("LogRecord")]
    public class LogRecord
    {
        public string AppVersion { get; set; }
        public int CashierId { get; set; }
        public string ContractorName { get; set; }
        public double DepartmentBalance { get; set; }
        public string DepartmentName { get; set; }
        public string DocumentSlot { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorModule { get; set; }
        public int ErrorNumber { get; set; }
        public double GoodsItemAmount { get; set; }
        public string GoodsItemBarcode { get; set; }
        public string GoodsItemName { get; set; }
        public double GoodsItemPrice { get; set; }
        public double GoodsItemQuantity { get; set; }
        public double GoodsItemQuantityReestr { get; set; }
        public int Id { get; set; }
        public string Info { get; set; }
        public byte LogLevel { get; set; }
        public string Message { get; set; }
        [UnixDate] public DateTime Timestamp { get; set; }
    }
}