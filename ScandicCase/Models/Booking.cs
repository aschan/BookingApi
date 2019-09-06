using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScandicCase.Models
{

    public class Booking
    {
        public int Id { get; set; }
        public List<Guest> Guests { get; set; }
        public string RoomType { get; set; }
        public Hotel Hotel { get; set; }
    }
}
