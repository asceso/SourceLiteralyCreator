using System;
using System.ComponentModel;
using System.Text;

namespace SourceLiteralyCreator.Models
{
    /// <summary>
    /// Источник - книга
    /// </summary>
    public class BookSource
    {
        [DisplayName("Первый автор")]
        public string FirstAuthor { get; set; }

        [DisplayName("Второй автор")]
        public string SecondAuthor { get; set; }

        [DisplayName("Основное заглавие")]
        public string MainTitle { get; set; }

        [DisplayName("Подзаголовочные данные")]
        public string SubTitle { get; set; }

        [DisplayName("Дополнительные сведения, относящиеся к заглавию")]
        public string AdditionalTitleInfo { get; set; }

        [DisplayName("Сведения об ответственности")]
        public string ResponsibilityInfo { get; set; }

        [DisplayName("Сведения об издании")]
        public string PublicationInfo { get; set; }

        [DisplayName("Место издания")]
        public string PublicationPlace { get; set; }

        [DisplayName("Издательство")]
        public string PublicationCompany { get; set; }

        [DisplayName("Дата издания")]
        public DateTime PublicationDate { get; set; }

        [DisplayName("Количество страниц")]
        public int PageCount { get; set; }

        private string SingleAuthorBook()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{FirstAuthor} ");
            sb.Append($"{MainTitle} ");
            if (!SubTitle.IsNOE())
            {
                sb.Append($":{SubTitle} ");
                if (!AdditionalTitleInfo.IsNOE())
                {
                    sb.Append($":{AdditionalTitleInfo} ");
                }
            }
            if (!ResponsibilityInfo.IsNOE())
            {
                sb.Append($" /{ResponsibilityInfo} ");
            }
            if (!PublicationInfo.IsNOE())
            {
                sb.Append($"{PublicationInfo}. - ");
            }
            switch (PublicationPlace)
            {
                case "Москва":
                    sb.Append("М.:");
                    break;
                case "Санкт-Петербург":
                    sb.Append("СПб.:");
                    break;
                case "Ленинград":
                    sb.Append("Л.:");
                    break;
                default:
                    sb.Append($"{PublicationPlace}.:");
                    break;
            }
            sb.Append($"{PublicationCompany}, ");
            sb.Append($"{PublicationDate.Year}. - ");
            sb.Append($"{PageCount} с.");
            return sb.ToString();
        }
        private string TwoAuthorBook()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{FirstAuthor} ");
            sb.Append($"{MainTitle} ");
            if (!SubTitle.IsNOE())
            {
                sb.Append($":{SubTitle} ");
                if (!AdditionalTitleInfo.IsNOE())
                {
                    sb.Append($":{AdditionalTitleInfo} ");
                }
            }
            if (!ResponsibilityInfo.IsNOE())
            {
                sb.Append($"/ {ResponsibilityInfo} ");
            }
            sb.Append($"// {SecondAuthor} ");
            if (!PublicationInfo.IsNOE())
            {
                sb.Append($"{PublicationInfo}. - ");
            }
            switch (PublicationPlace)
            {
                case "Москва":
                    sb.Append("М.:");
                    break;
                case "Санкт-Петербург":
                    sb.Append("СПб.:");
                    break;
                case "Ленинград":
                    sb.Append("Л.:");
                    break;
                default:
                    sb.Append($"{PublicationPlace}.:");
                    break;
            }
            sb.Append($"{PublicationCompany}, ");
            sb.Append($"{PublicationDate.Year}г. - ");
            sb.Append($"{PageCount} с.");
            return sb.ToString();
        }

        public override string ToString()
        {
            if (SecondAuthor.IsNOE())
            {
                return SingleAuthorBook();
            }
            else
            {
                return TwoAuthorBook();
            }
        }
    }
}
