using AndriyCo.Shopdesk.Containers.Documents;
using AndriyCo.Shopdesk.Containers.Serialization.Json;
using AndriyCo.Shopdesk.Containers.Serialization.Xml;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;

namespace AndriyCo.Shopdesk.Containers
{
    /// <summary>
    /// Контейнер пакета документів каси, як результат однієї проводки (чек, повернення, замовлення та інше)
    /// </summary>
    /// <remarks>
    /// Чек - це видаткова накладна та прибутковий касовий ордер<br/>
    /// Повернення від покупця - це документ повернення від покупця та видатковий касовий ордер<br/>
    /// Корзина - лише видаткова накладна<br/>
    /// </remarks>
    [XmlType("ArrayOfDocument")]
    public class ContainerDto
    {
        private string hash = string.Empty;

        [XmlAttribute(AttributeName = "address")]
        public string Address { get; set; }

        [XmlAttribute(AttributeName = "docFormatDescription")]
        public string DocFormatDescription
        {
            get => "https://github.com/amukan/AndriyCo.Shopdesk.Containers.git";
            set
            {
                // this property is read only}
            }
        }

        [XmlElement(ElementName = "Document")]
        public List<Document> Documents { get; set; }

        [XmlAttribute(AttributeName = "hash")]
        public string Hash
        {
            get
            {
                if (!string.IsNullOrEmpty(hash))
                    return hash;
                else
                {
                    if (Documents == null)
                        return "";
                    var documents = Documents.ToArray();
                    var str = Converter<Document[]>.Serialize(documents, XmlView.OmitXmlDeclaration);
                    using SHA1Managed sha1 = new();
                    byte[] asciiArray = ASCIIEncoding.ASCII.GetBytes(str);
                    var hash = sha1.ComputeHash(asciiArray);
                    return Convert.ToBase64String(hash);
                }
            }
            set
            {
                hash = value;
            }
        }

        [XmlAttribute(AttributeName = "originalFileName")]
        public string OriginalFileName { get; set; }

        [XmlAttribute(AttributeName = "software")]
        public string Software { get; set; }

        [XmlAttribute(AttributeName = "wpGuid")]
        public string WpGuid { get; set; }

        [XmlAttribute(AttributeName = "wpName")]
        public string WpName { get; set; }

        [XmlAttribute(AttributeName = "xsd")]
        [JsonIgnore]
        public string Xsd { get; set; }

        [XmlAttribute(AttributeName = "xsi")]
        [JsonIgnore]
        public string Xsi { get; set; }

        #region Serialization

        #region XML

        public string SerializeIntoXml()
        {
            XmlView xmlView = XmlView.Pretty | XmlView.FullClosingEmptyTag;
            string xml = Converter<ContainerDto>.Serialize(this, xmlView);
            return xml;
        }

        public byte[] SerializeIntoXmlAndZip()
        {
            string xmlString = SerializeIntoXml();
            byte[] compressedBytes = Compress(xmlString, OriginalFileName);
            return compressedBytes;
        }

        public static ContainerDto DeserializeXml(string xmlString)
        {
            ContainerDto dto = Converter<ContainerDto>.Deserialize(xmlString);
            return dto;
        }

        public static ContainerDto DeserializeXml(byte[] zipBytes)
        {
            string xml = DecompressXmlString(zipBytes);
            return DeserializeXml(xml);
        }

        #endregion XML

        #region JSON

        /// <summary>
        /// Серіалізація у JSON
        /// </summary>
        /// <param name="jsonView">Налаштування серіалізації</param>
        /// <returns></returns>
        public string SerializeIntoJson(JsonView jsonView = JsonView.Pretty | JsonView.HideNullValue | JsonView.HideStringEmptyValue)
        {
            string json = Converter.Serialize(this, jsonView);
            return json;
        }

        public static ContainerDto DeserializeJson(string json)
        {
            ContainerDto dto = Converter.Deserialize<ContainerDto>(json);
            return dto;
        }

        #endregion JSON

        #endregion Serialization

        #region Compression

        public static byte[] Compress(string xmlString, string fileNameInArchive)
        {
            byte[] fileBytes = Encoding.GetEncoding(1251).GetBytes(xmlString);
            byte[] compressedBytes;

            using (MemoryStream outStream = new())
            {
                using (ZipArchive archive = new(outStream, ZipArchiveMode.Create, true))
                {
                    ZipArchiveEntry fileInArchive = archive.CreateEntry(fileNameInArchive, CompressionLevel.Optimal);
                    using Stream entryStream = fileInArchive.Open();
                    using MemoryStream fileToCompressStream = new(fileBytes);
                    fileToCompressStream.CopyTo(entryStream);
                }
                compressedBytes = outStream.ToArray();
            }
            return compressedBytes;
        }

        public static string DecompressXmlString(byte[] zipBytes)
        {
            using MemoryStream zipStream = new(zipBytes);
            using ZipArchive zipArchive = new(zipStream);
            ZipArchiveEntry entry = zipArchive.Entries.Single();
            using var decompressedStream = entry.Open();
            StreamReader reader = new(decompressedStream, Encoding.GetEncoding(1251));
            string xmlString = reader.ReadToEnd();
            return xmlString;
        }

        #endregion

        public string CreateFileName()
        {
            Document document = this.Documents.FirstOrDefault();
            if (document == null)
                return "";

            string infoPart = $"{document.Id}_F{document.FranchiseeId}_P{document.DepartmentId}_{document.DateOfApprove:yyyy-MM-dd_HH_mm_ss}.tcudoc";

            if (document.DocumentType == DocumentType.SalesInvoice)
                return $"DOC_D{infoPart}";

            if (document.DocumentType == DocumentType.ShippingDeclaration)
                return $"DOC_SHIPPINGDECL_D{infoPart}";

            return "";
        }

    }
}