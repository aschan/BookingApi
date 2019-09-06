namespace ScandicCase.Services
{
	using System.Collections.Generic;
	using System.Linq;

	using ScandicCase.Exceptions;
	using ScandicCase.Interfaces;
	using ScandicCase.Models;

	public class BookingSystem : IBookingSystem
    {
        public static List<Booking> RandomValues;

        public BookingSystem()
        {
            PopulateSomeValue();
        }

        public void AddGuestToBooking(int bookingId, Guest guest)
        {
            // requeird objects should be controlled from front end no here 

            var booking = RandomValues.FirstOrDefault(x => x.Id == bookingId);
            if (booking != null)
            {
                var roomSpace = GetRoomSpace(booking.RoomType);
                var guestsNumberinBooking = booking.Guests.ToList().Count;

                if (string.IsNullOrEmpty(guest.Title) && booking.Hotel.CountryCode == Country.DE)
                {
                    throw new ValidationException("Title field is obligatory since the hotel is in germany");
                }
                if (guestsNumberinBooking < roomSpace)
                {
                    var NewBooking = new Guest
                    {
                        FirstName = guest.FirstName,
                        LastName = guest.LastName,
                        Title = guest.Title
                    };
                    booking.Guests.Add(NewBooking);
                }
                else
                {
                    throw new ValidationException("here are unfortunetlly no place for this guest to stay , consider maybe another room ");
                }
            }
            else
            {
                throw new NotFoundException($"Booking with id number {bookingId} Not found");
            }
        }

        public Booking FetchBooking(int bookingId)
        {
            var booking = RandomValues.FirstOrDefault(x => x.Id == bookingId);
            if (booking != null)
            {
                return booking;
            }
            else
            {
                throw new NotFoundException($"Booking with id number {bookingId} Not found");
            }
        }

        public IEnumerable<Booking> GetBookings()
        {
            var bookings = RandomValues.ToList();
            return bookings;
        }

		private int GetRoomSpace(string roomType)
        {
            var roomSpace = 0;
            switch (roomType)
            {
                case "SINGLE":
                    roomSpace = 1;
                    break;
                case "DOUBLE":
                case "TWIN":
                    roomSpace = 2;
                    break;
                case "TRIPLE":
                    roomSpace = 3;
                    break;
                default:
                    roomSpace = 0;
                    break;
            }
            return roomSpace;
        }

		private void PopulateSomeValue()
        {
            var _HotelDanmark = new Hotel()
            {
                Name = "Shiraton",
                CountryCode = Country.DK
            };
            var _HotelSweden = new Hotel()
            {
                Name = "Shiraton",
                CountryCode = Country.SE
            };
            var _HotelGermany = new Hotel()
            {
                Name = "Shiraton",
                CountryCode = Country.DE
            };

            var guest1Danmark = new Guest { FirstName = "Abdallah", LastName = "Rifai", Title = "Mr" };
            var guest2Danmark = new Guest { FirstName = "Eveline", LastName = "Rifai", Title = "Mrs" };
            var guest3Danmark = new Guest { FirstName = "Louise", LastName = "Rifai", Title = "Mrs" };
            var guest1Sweden = new Guest { FirstName = "Abdallah", LastName = "Rifai", Title = "Mr" };
            var guest2Sweden = new Guest { FirstName = "Eveline", LastName = "Rifai", Title = "Mrs" };
            var guest3Germany = new Guest { FirstName = "Louise", LastName = "Rifai", Title = "Mrs" };
            var guest1Germany = new Guest { FirstName = "Abdallah", LastName = "Rifai", Title = "Mr" };
            var guest2Germany = new Guest { FirstName = "Eveline", LastName = "Rifai", Title = "Mrs" };

            var _GuestsDanmark = new List<Guest>();
            _GuestsDanmark.Add(guest1Danmark);
            _GuestsDanmark.Add(guest2Danmark);
            _GuestsDanmark.Add(guest3Danmark);

            var _GuestsSweden = new List<Guest>();
            _GuestsSweden.Add(guest1Sweden);
            _GuestsSweden.Add(guest2Sweden);

            var _GuestsGermany = new List<Guest>();
            _GuestsGermany.Add(guest3Germany);
            _GuestsGermany.Add(guest3Germany);

            RandomValues = new List<Booking>();
            RandomValues.Add(
                new Booking
                {
                    Id = 1,
                    Guests = _GuestsGermany,
                    Hotel = _HotelGermany,
                    RoomType = "TRIPLE"
                });
            RandomValues.Add(
               new Booking
               {
                   Id = 2,
                   Guests = _GuestsSweden,
                   Hotel = _HotelSweden,
                   RoomType = "DOUBLE"
               });
            RandomValues.Add(
               new Booking
               {
                   Id = 3,
                   Guests = _GuestsDanmark,
                   Hotel = _HotelDanmark,
                   RoomType = "TRIPLE"
               });
        }
    }
}
