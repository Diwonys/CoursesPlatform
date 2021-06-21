using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CoursesPlatform.Models.Teacher.Course.Elements.ViewModels
{
    public class ElementViewModel
    {
        public int? Width { get; set; }
        public int? Height { get; set; }

        public string Text { get; set; }
        public IFormFile Image { get; set; }
        public string ByteImage { get; set; }

        public ContentElement GetContentElement()
        {
            if (Text != null) 
                return CreateParagraph();

            if (Image != null)
            {
                return CreateImage();
            }
            else
            {
                if (ByteImage != null)
                    return CreateByteImage();
            }

            return null;
        }
        private ContentElement CreateByteImage()
        {
            return new Image
            {
                Width = Width,
                Height = Height,
                ByteImage = Convert.FromBase64String(ByteImage)
            };
        }
        private ContentElement CreateImage()
        {
            return new Image
            {
                FormFileImage = Image,
                Width = Width,
                Height = Height,
            };
        }
        private ContentElement CreateParagraph()
        {
            return new Paragraph
            {
                Text = Text,
                Width = Width,
                Height = Height,
            };
        }
    }
}
