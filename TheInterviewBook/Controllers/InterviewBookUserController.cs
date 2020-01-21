using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheInterviewBook.Models.Internal;

namespace TheInterviewBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "https://www.overflogame.com",
    headers: "accept,content-type,origin,x-my-header", methods: "*")]
    public class InterviewBookUserController : ControllerBase
    {
        private readonly InterviewBookContext _context;

        public InterviewBookUserController(InterviewBookContext context)
        {
            _context = context;
        }

        // GET: api/InterviewBookUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InterviewBookUser>>> GetInterviewBookUser()
        {
            return await _context.InterviewBookUser.ToListAsync();
        }

        // GET: api/InterviewBookUser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InterviewBookUser>> GetInterviewBookUser(Guid id)
        {
            var interviewBookUser = await _context.InterviewBookUser.FindAsync(id);

            if (interviewBookUser == null)
            {
                return NotFound();
            }

            return interviewBookUser;
        }

        // PUT: api/InterviewBookUser/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInterviewBookUser(Guid id, InterviewBookUser interviewBookUser)
        {
            if (id != interviewBookUser.ID)
            {
                return BadRequest();
            }

            _context.Entry(interviewBookUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterviewBookUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/InterviewBookUser
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<InterviewBookUser>> PostInterviewBookUser([FromBody] InterviewBookUser interviewBookUser)
        {
            _context.InterviewBookUser.Add(interviewBookUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInterviewBookUser", new { id = interviewBookUser.ID }, interviewBookUser);
        }

        // DELETE: api/InterviewBookUser/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<InterviewBookUser>> DeleteInterviewBookUser(Guid id)
        {
            var interviewBookUser = await _context.InterviewBookUser.FindAsync(id);
            if (interviewBookUser == null)
            {
                return NotFound();
            }

            _context.InterviewBookUser.Remove(interviewBookUser);
            await _context.SaveChangesAsync();

            return interviewBookUser;
        }

        private bool InterviewBookUserExists(Guid id)
        {
            return _context.InterviewBookUser.Any(e => e.ID == id);
        }
    }
}
