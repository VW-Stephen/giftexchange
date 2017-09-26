using giftexchange.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace giftexchange.Controllers
{
    /// <summary>
    /// API controller for GiftExchange objects
    /// </summary>
    [Route("api/[controller]")]
    public class GiftExchangeController : Controller
    {
        private readonly DataContext _context;

        public GiftExchangeController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a list of all GiftExchange objects
        /// </summary>
        [HttpGet]
        public IEnumerable<GiftExchange> GetAll()
        {
            return _context.GiftExchanges.Include("Participants").Include("Participants.GiftAssignment").ToList();
        }

        /// <summary>
        /// Returns a specific GiftExchange specified by ID
        /// </summary>
        /// <param name="id">ID of the GiftExchange to return</param>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var exchange = _context.GiftExchanges.Include("Participants").Include("Participants.GiftAssignment").FirstOrDefault(g => g.ID == id);
            if (exchange == null)
                return NotFound();

            //exchange.Participants = _context.Participants.Where(p => p.GiftExchangeId == exchange.ID).ToList();
            return new ObjectResult(exchange);
        }

        /// <summary>
        /// Creates a new GiftExchange object with the POST body
        /// </summary>
        /// <param name="item">POSTed GiftExchange data</param>
        [HttpPost]
        public IActionResult Create([FromBody]GiftExchange item)
        {
            if (item == null)
                return BadRequest();

            _context.GiftExchanges.Add(item);
            _context.SaveChanges();

            return new ObjectResult(item);
        }

        /// <summary>
        /// Updates an existing GiftExchange specified by the given ID with the given POST body
        /// </summary>
        /// <param name="id">ID of the GiftExchange to update</param>
        /// <param name="item">New GiftExchange data</param>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]GiftExchange item)
        {
            if (item == null)
                return BadRequest();

            var existing = _context.GiftExchanges.FirstOrDefault(g => g.ID == item.ID);
            if (existing == null)
                return NotFound();

            existing.UpdateFromObject(item);
            _context.SaveChanges();

            return new ObjectResult(existing);
        }

        /// <summary>
        /// Deletes the given GiftExchange
        /// </summary>
        /// <param name="id">ID of the GiftExchange to delete</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _context.GiftExchanges.FirstOrDefault(g => g.ID == id);
            if (existing == null)
                return NotFound();

            _context.GiftExchanges.Remove(existing);
            _context.SaveChanges();

            return new NoContentResult();
        }

        [HttpGet("{id}/shuffle")]
        public IActionResult Shuffle(int id)
        {
            var item = _context.GiftExchanges.Include("Participants").Include("Participants.GiftAssignment").FirstOrDefault(g => g.ID == id);
            if (item == null)
                return NotFound();

            item.Shuffle();
            _context.SaveChanges();

            return new ObjectResult(item);
        }
    }
}
