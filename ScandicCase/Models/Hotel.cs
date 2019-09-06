using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScandicCase.Models
{
    public class Hotel
    {
        public string Name { get; set; }
        public Country CountryCode { get; set; }
    }
    public enum Country
    {
        SE,  
        DK,
        DE   // germany
    }
}
