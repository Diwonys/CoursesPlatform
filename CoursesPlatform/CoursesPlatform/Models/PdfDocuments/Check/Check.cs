using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesPlatform.Models.PdfDocuments.Check
{
    public class Check
    {
        public int Number { get; set; }
        public DateTime CreationDate => DateTime.Now;
        public string Href { get; set; }
        public string CompanyName { get; set; }
        public string INN { get; set; }
        public int Outcome { get; set; }
        private readonly PdfDocumentProvider _documentProvider;

        public Check()
        {
            _documentProvider = new();
        }

        public void InitHeader()
        {
            _documentProvider.AddParagraph($"Кассовый чек №{Number}");
            _documentProvider.AddParagraph(CreationDate.ToString());
            _documentProvider.AddParagraph(Href);
            _documentProvider.AddParagraph(CompanyName);
            _documentProvider.AddParagraph($"ИНН: {INN}");
            _documentProvider.AddParagraph("Вид налогообложения: ОСН");
            _documentProvider.AddParagraph(new string('-', 50));
        }

        public void AddProduct(CheckProduct product)
        {
            Outcome += product.SummuryCost;
            _documentProvider.AddParagraph(product.ToString());
        }

        public Stream GetDocument()
        {
            _documentProvider.AddParagraph(new string('-', 50));
            _documentProvider.AddParagraph("Итог" + new string(' ', 15) + "= " + Outcome);
            _documentProvider.AddParagraph("в т.ч. НДС%" + new string(' ', 15) + "= " + (float)Outcome / 100.0f * 20.0f);
            return _documentProvider.Save();
        }
    }
}
