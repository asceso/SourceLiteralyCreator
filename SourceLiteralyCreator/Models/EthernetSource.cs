using System;
using System.ComponentModel;
using System.Text;

namespace SourceLiteralyCreator.Models
{
    /// <summary>
    /// Источник - электронный ресурс
    /// </summary>
    public class EthernetSource
    {
        [DisplayName("Название источника")]
        public string SourceName { get; set; }

        [DisplayName("Режим доступа")]
        public string SourceURL { get; set; }

        [DisplayName("Дата посещения")]
        public DateTime VisitDate { get; set; }

        public EthernetSource() => VisitDate = new DateTime(2010, 1, 1);

        public override string ToString() => $"{SourceName} [Электронный ресурс] - Режим доступа: {SourceURL} Дата посещения: {VisitDate.ToShortDateString()}";
    }
}
