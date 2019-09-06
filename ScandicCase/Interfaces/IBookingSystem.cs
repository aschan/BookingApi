using ScandicCase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScandicCase.Interfaces
{
    public interface IBookingSystem
    {
        Booking FetchBooking(int bookingId);
        void AddGuestToBooking(int bookingId, Guest guest);
        IEnumerable<Booking> GetBookings();
    }
}
