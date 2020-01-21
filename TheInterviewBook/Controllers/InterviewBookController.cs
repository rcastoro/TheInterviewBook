using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TheInterviewBook.Models.Internal;
using TheInterviewBook.Models.External;

namespace TheInterviewBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "https://www.overflogame.com",
    headers: "accept,content-type,origin,x-my-header", methods: "*")]
    public class InterviewBookController : ControllerBase
    {
        private readonly InterviewBookContext _context;

        public InterviewBookController(InterviewBookContext context)
        {
            _context = context;
        }

        // GET: api/InterviewBook
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InterviewBook>>> GetInterviewBook()
        {
            return await _context.InterviewBook.ToListAsync();
        }

        // GET: api/InterviewBook/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InterviewBook>> GetInterviewBook(Guid id)
        {
            var interviewBook = await _context.InterviewBook.FindAsync(id);

            if (interviewBook == null)
            {
                return NotFound();
            }

            return interviewBook;
        }

        // PUT: api/InterviewBook/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInterviewBook(Guid id, InterviewBook interviewBook)
        {
            if (id != interviewBook.ID)
            {
                return BadRequest();
            }

            _context.Entry(interviewBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterviewBookExists(id))
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

        private bool InterviewBookExists(Guid id)
        {
            return _context.InterviewBook.Any(e => e.ID == id);
        }

        // NHL CALL
        [Route("nhl")]
        [HttpGet]
        public async Task<ActionResult<NHLTeams>> GetNHLTeams()
        {
            string baseUri = @"https://statsapi.web.nhl.com/";

            NHLTeams teams = new NHLTeams();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage res = await client.GetAsync("api/v1/teams");
                if(res.IsSuccessStatusCode)
                {
                    string jStr = res.Content.ReadAsStringAsync().Result;
                    teams = JsonConvert.DeserializeObject<NHLTeams>(jStr);
                }
                return teams;
            }
        }
    }
}
