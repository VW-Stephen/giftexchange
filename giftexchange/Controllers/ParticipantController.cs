using giftexchange.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace giftexchange.Controllers
{
    /// <summary>
    /// API controller for GiftExchange objects
    /// </summary>
    [Route("api/[controller]")]
    public class ParticipantController : Controller
    {
        private readonly DataContext _context;

        public ParticipantController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a list of all Participant objects
        /// </summary>
        [HttpGet]
        public IEnumerable<Participant> GetAll()
        {
            return _context.Participants.ToList();
        }

        /// <summary>
        /// Returns a specific Participant specified by ID
        /// </summary>
        /// <param name="id">ID of the Participant to return</param>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var participant = _context.Participants.FirstOrDefault(g => g.ID == id);
            if (participant == null)
                return NotFound();
            return new ObjectResult(participant);
        }

        /// <summary>
        /// Creates a new Participant object with the POST body
        /// </summary>
        /// <param name="item">POSTed Participant data</param>
        [HttpPost]
        public IActionResult Create([FromBody]Participant item)
        {
            if (item == null)
                return BadRequest();

            var exchange = _context.GiftExchanges.FirstOrDefault(g => g.ID == item.GiftExchangeId);
            if (exchange == null)
                return BadRequest();

            _context.Participants.Add(item);
            _context.SaveChanges();

            return GetById(item.ID);
            //return new ObjectResult(item);
        }

        /// <summary>
        /// Updates an existing Participant specified by the given ID with the given POST body
        /// </summary>
        /// <param name="id">ID of the Participant to update</param>
        /// <param name="item">New Participant data</param>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Participant item)
        {
            if (item == null)
                return BadRequest();

            var existing = _context.Participants.FirstOrDefault(g => g.ID == item.ID);
            if (existing == null)
                return NotFound();

            existing.UpdateFromObject(item);
            _context.SaveChanges();
            return new ObjectResult(existing);
        }

        /// <summary>
        /// Deletes the given Participant
        /// </summary>
        /// <param name="id">ID of the Participant to delete</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _context.Participants.FirstOrDefault(g => g.ID == id);
            if (existing == null)
                return NotFound();

            // TODO: If Participants have changed in a given GiftExchange, we have to reset it. We could also just shuffle again if requirements wanted.
            _context.Participants.Remove(existing);
            _context.SaveChanges();

            return new NoContentResult();
        }
    }
}
