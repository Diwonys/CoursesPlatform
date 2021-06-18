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
        public byte[] ByteImage { get; private set; }

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
    }
}
