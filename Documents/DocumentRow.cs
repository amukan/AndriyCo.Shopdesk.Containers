﻿using AndriyCo.Shopdesk.Containers.Serialization.Xml.Attributes;
using System;

namespace AndriyCo.Shopdesk.Containers.Documents
{
    /// <summary>
    /// Рядок документа у переліку документів
    /// </summary>
    public class DocumentRow
    {
        /// <summary>Id контейнера в базі Shopserver</summary>
        public long ContainerId { get; set; }

        /// <summary>Номер документа на касі</summary>
        public string DocumentNumber { get; set; }

        /// <summary>Guid документа</summary>
        public Guid DocumentGuid { get; set; }

        /// <summary>Тип документа</summary>
        public DocumentType DocumentType { get; set; }

        public string DocumentTypeDescription { get; set; }

        /// <summary>Id підрозділу</summary>
        public long DepartmentId { get; set; }

        /// <summary>Назва підрозділу</summary>
        public string DepartmentName { get; set; }

        //public string CustomerName { get; set; }
        /// <summary>Сума документа</summary>
        public double Amount { get; set; }

        /// <summary>Guid каси</summary>
        public Guid ShopdeskGuid { get; set; }

        /// <summary>ПЗ каси</summary>
        public string ShopdeskVersion { get; set; }

        /// <summary>Дата-час проведення чека на касі (місцевий час)</summary>
        [ShopdeskDate] public DateTime DateOfApprove { get; set; }

        /// <summary>Дата-час створення чека на касі (місцевий час)</summary>
        [ShopdeskDate] public DateTime DateOfCreate { get; set; }

        /// <summary>Id контейнера чека, який створений на підставі цього замовлення покупця</summary>
        public long? ChequeContainerId { get; set; }

        /// <summary>Guid чека, який створений на підставі цього замовлення покупця</summary>
        public Guid? ChequeGuid { get; set; }

        /// <summary>Дата продажу чека на касі, який створений на підставі цього замовлення покупця</summary>
        public DateTime? ChequeDateOfApprove { get; set; }

        /// <summary>Ім'я файлу чека, який створений на підставі цього замовлення покупця</summary>
        public string ChequeFileName { get; set; }
    }
}