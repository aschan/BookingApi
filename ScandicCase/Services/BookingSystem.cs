namespace ScandicCase.Services
{
	using System;
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
			// Required objects should be controlled from front end no here 

			// No! It's a public method in a public class so anyone can call it. Assume everyone else are idiots and validate the input.
			// If you did that you wouldn't get a NullReferenceException when posting without a body. It should probably be validated the controller
			// as well but this is where the business logic resides so this is where you must be certain that the submitted parameters are valid.
			if (bookingId <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(bookingId), "A valid booking Id must be greater than 0.");
			}

			var localGuest = guest ?? throw new ArgumentNullException(nameof(guest));

            var booking = RandomValues.FirstOrDefault(x => x.Id == bookingId);
            if (booking != null)
            {
				// These two lines and the if further down ...
                var roomSpace = GetNumberOfBeds(booking.RoomType);
                var guestsNumberinBooking = booking.Guests.ToList().Count;

				// If you move this to the Guest class (or maybe make an extension method if you can't modify the class) that validates the guest. Then you could write:
				// if (!guest.IsValidInCountry(booking.Hotel.CountryCode)) { thrown new ValidationException("Title field is obligatory for guests at German hotels."); }
                if (string.IsNullOrEmpty(guest.Title) && booking.Hotel.CountryCode == Country.DE)
                {
                    throw new ValidationException("Title field is obligatory since the hotel is in germany");
                }

				// ... are all about the booked room so you could probably change GetNumberOfBeds toi GetAvailableBeds and put most of this code there.
				// That way you would get an int between [0 - max beds] telling you how many extra guests can be added.
                if (guestsNumberinBooking < roomSpace)
                {
                    var newBooking = new Guest
					                     {
					                         FirstName = localGuest.FirstName,
					                         LastName = localGuest.LastName,
					                         Title = localGuest.Title
					                     };
                    booking.Guests.Add(newBooking);
                }
                else
                {
                    throw new ValidationException("There are unfortunately no place for this guest to stay , consider maybe another room ");
                }
            }
            else
            {
                throw new NotFoundException($"Booking with id number {bookingId} was not found");
            }
        }

        public Booking FetchBooking(int bookingId)
		{
			if (bookingId <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(bookingId), "A valid booking Id must be greater than 0.");
			}

			var booking = RandomValues.FirstOrDefault(x => x.Id == bookingId);
			return booking ?? throw new NotFoundException($"Booking with id number {bookingId} Not found");
		}

        public IEnumerable<Booking> GetBookings()
        {
            var bookings = RandomValues.ToList();
            return bookings;
        }

		private int GetNumberOfBeds(string roomType)
        {
            int roomSpace;
            switch (roomType.ToUpperInvariant())
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
