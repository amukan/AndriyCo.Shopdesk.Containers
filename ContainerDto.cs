using AndriyCo.Shopdesk.Containers.Documents;
using AndriyCo.Shopdesk.Containers.Serialization.Json;
using AndriyCo.Shopdesk.Containers.Serialization.Xml;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

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
        [XmlAttribute(AttributeName = "xsf")]
        public readonly string DocFormatDescription = "https://github.com/amukan/AndriyCo.Shopdesk.Containers.git";

        private string hash = string.Empty;

        [XmlAttribute(AttributeName = "address")]
        public string Address { get; set; }

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
                    //var hash = sha1.ComputeHash(str.ToArrayASCIIFormat());
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

        public string SerializeIntoXml()
        {
            XmlView xmlView = XmlView.Pretty | XmlView.FullClosingEmptyTag;
            string xml = Converter<ContainerDto>.Serialize(this, xmlView);
            return xml;
        }

        /// <summary>
        /// Сериалізація у JSON з найуживанішим форматуванням - є відступи, приховуються пусті теги
        /// </summary>
        /// <returns></returns>
        public string SerializeIntoJson()
        {
            JsonView jsonView = JsonView.Pretty
                                | JsonView.HideNullValue
                                | JsonView.HideStringEmptyValue;
            return SerializeIntoJson(jsonView);
        }

        /// <summary>
        /// Сериалізація у JSON з форматуванням, яке можна тонко налаштувати
        /// </summary>
        /// <returns></returns>
        public string SerializeIntoJson(JsonView jsonView)
        {
            string json = Converter.Serialize(this, jsonView);
            return json;
        }

        public static ContainerDto DeserializeXml(string xml)
        {
            ContainerDto dto = Converter<ContainerDto>.Deserialize(xml);
            return dto;
        }

        public static ContainerDto DeserializeJson(string json)
        {
            ContainerDto dto = Converter.Deserialize<ContainerDto>(json);
            return dto;
        }
    }
}