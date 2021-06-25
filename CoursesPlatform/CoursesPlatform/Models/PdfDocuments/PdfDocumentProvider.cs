using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using System.IO;
using System.Linq.Expressions;
using System.Text;

namespace CoursesPlatform.Models.PdfDocuments
{
    class PdfDocumentProvider
    {
        private readonly Document _document;
        private readonly Section _section;

        public PdfDocumentProvider()
        {
            _document = new();
            _section = _document.AddSection();
            InitStyleSection();
        }
        private void InitStyleSection()
        {
            _section.PageSetup.PageFormat = PageFormat.B5;
            _section.PageSetup.Orientation = Orientation.Portrait;
            _section.PageSetup.BottomMargin = 10;
            _section.PageSetup.TopMargin = 10;
        }

        public void AddParagraph(string text)
        {

            Paragraph paragraph = new Paragraph();
            _section.Add(paragraph);
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Format.Font.Size = 20;
            paragraph.AddText(text);
        }

        public void Save(string path)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(false);
            renderer.Document = _document;
            renderer.RenderDocument();
            renderer.Save(path);
        }

        public Stream Save()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
  
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);

            renderer.Document = _document;
            renderer.RenderDocument();

            MemoryStream stream = new MemoryStream();
            renderer.Save(stream, false);
            return stream;
        }
    }
}
