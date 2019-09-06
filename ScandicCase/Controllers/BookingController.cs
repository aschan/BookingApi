namespace ScandicCase.Controllers
{
	using System;
	using System.Collections.Generic;
	using Microsoft.AspNetCore.Mvc;
	using ScandicCase.Interfaces;
	using ScandicCase.Models;

    [Produces("application/json")]
    [Route("api/Booking")]
    public class BookingController : Controller
    {
        private IBookingSystem _service;

        public BookingController(IBookingSystem service)
        {
            _service = service;
        }

        // GET: api/Booking
        [HttpGet]
        public IEnumerable<Booking> Get()
        {
            // a test method to get some fake data 
            var bookings = _service.GetBookings();
            return bookings;
        }

        // GET: api/Booking/{id}
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var booking = _service.FetchBooking(id);
                return Ok(booking);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/Booking
        [HttpPost]
        [Route("{bookingId}")]
        public IActionResult UpdateBooking(int bookingId, [FromBody] Guest guest)
        {
            try
            {
                _service.AddGuestToBooking(bookingId, guest);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
