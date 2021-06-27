using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesPlatform.Models.PdfDocuments.Check
{
    public class CheckProduct
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Count { get; set; }

        public int SummuryCost => Count * Cost;

        public override string ToString()
        {
            return Name + new string(' ', 10) +
                ((float)Cost).ToString(".00") +
                "*" + 
                Count + 
                new string(' ', 4) +
                "=" +
                new string(' ', 4) +
                ((float)SummuryCost).ToString(".00"); 
        }
    }
}
