namespace ScandicCase.Models
{
	using System.Collections.Generic;

	public class Booking
    {
        public int Id { get; set; }

        public List<Guest> Guests { get; set; }

        public string RoomType { get; set; }

        public Hotel Hotel { get; set; }
    }
}
