using Microsoft.AspNetCore.Mvc;
using FormulaAirline.API.Services;
using FormulaAirline.API.Models;

namespace FormulaAirline.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IMessageProducer _messageProducer;
        private readonly ILogger<BookingsController> _logger;

        //In-Memory db
        public static readonly List<Booking> _bookings=new();

        public BookingsController(ILogger<BookingsController> logger,IMessageProducer messageProducer)
        {
            _logger = logger;
            _messageProducer=messageProducer;
        }

        [HttpPost]
        public IActionResult CreatingBooking(Booking newBooking){
            if (!ModelState.IsValid) return BadRequest();

            _bookings.Add(newBooking);
            _messageProducer.SendMessage<Booking>(newBooking);

            return Ok();
        }
    }
}