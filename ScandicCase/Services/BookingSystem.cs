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
			if (bookingId <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(bookingId), "A valid booking Id must be greater than 0.");
			}

			var localGuest = guest ?? throw new ArgumentNullException(nameof(guest));

            var booking = RandomValues.FirstOrDefault(x => x.Id == bookingId);
            if (booking != null)
            {
	            if (!guest.IsGuestValidForCountry(booking.Hotel.CountryCode))
	            {
		            throw new ValidationException("Title field is obligatory since the hotel is in germany");
				}

                var availableBeds = GetAvailableBeds(booking);
                if (availableBeds > 0)
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
                    throw new ValidationException("There are unfortunately no available beds left in this room, consider booking an additional room.");
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

        private int GetAvailableBeds(Booking booking)
        {
	        var totalBedsInRoomType = GetNumberOfBeds(booking.RoomType);
	        var occupiedBedsInRoom = booking.Guests.Count; // Since it is a list I don't need to do ToList() and if it was just an enumerable the linq method Count() would still work.

	        return totalBedsInRoomType - occupiedBedsInRoom;
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
