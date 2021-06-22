using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesPlatform.Models.Teacher.Course.Elements
{
    public class Image : ContentElement
    {
        public string Name { get; set; }
        public byte[] ByteImage { get; set; }

        [NotMapped]
        private IFormFile _image;
        [NotMapped]
        public IFormFile FormFileImage
        {
            get => _image;
            set
            {
                _image = value;
                Name = _image.FileName;
                ByteImage = GetBytesFromFile(_image);
            }
        }

        private byte[] GetBytesFromFile(IFormFile formFile)
        {
            using (var stream = new MemoryStream())
            {
                formFile.CopyTo(stream);
                return stream.ToArray();
            }
        }

        public FormFile GetFormFileImage()
        {
            var stream = new MemoryStream(ByteImage);
            return new FormFile(stream, 0, ByteImage.Length, Name, Name);
        }
        public override string GetContent()
        {
            return "data:image/jpeg;base64," + Convert.ToBase64String(ByteImage);
        }

        public override string GetImageBase64String()
        {
            return Convert.ToBase64String(ByteImage);
        }
    }
}
