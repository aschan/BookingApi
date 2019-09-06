namespace ScandicCase.Interfaces
{
	using System.Collections.Generic;

	using ScandicCase.Models;

	public interface IBookingSystem
    {
        Booking FetchBooking(int bookingId);

        void AddGuestToBooking(int bookingId, Guest guest);

        IEnumerable<Booking> GetBookings();
    }
}
