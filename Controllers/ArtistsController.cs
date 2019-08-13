using System.Collections.Generic;
using System.Threading.Tasks;
using miprimerAPI.Context;
using miprimerAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test2.Entities;
using test2.Services;

namespace miprimerAPI.Controllers {
    [Route ("api/[Controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase {
        private readonly ApplicationDbContext context;
        private readonly IEmailService emailService;
        private readonly ISendQueue sendqueue;
        public ArtistsController (ApplicationDbContext context, IEmailService emailService, ISendQueue sendqueue) {
            this.sendqueue = sendqueue;
            this.emailService = emailService;
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> Get () {
            return await context.Artists.ToListAsync ();
        }

        [HttpGet ("{id}", Name = "searchArtistById")]
        public async Task<ActionResult<Artist>> Get (int id) {
            var artist = await context.Artists.FirstOrDefaultAsync (x => x.Id == id);
            if (artist == null) {
                return NotFound ();
            }
            return artist;
        }
        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [ProducesResponseType (200)]
        public async Task<ActionResult<Artist>> Post ([FromBody] Artist artist) {
            await context.Artists.AddAsync (artist);
            await context.SaveChangesAsync ();

            return new CreatedAtRouteResult ("searchArtistById", new { id = artist.Id }, artist);
        }
     
        [HttpPut ("{id}")]
        public async Task<ActionResult<Artist>> Put (int id, [FromBody] Artist artist) {
            if (id != artist.Id) {
                return NotFound (new { mesasge = "Artist not found" });
            }
            context.Entry (artist).State = EntityState.Modified;
            await context.SaveChangesAsync ();
            return artist;
        }

        [HttpDelete ("{id}")]
        public async Task<ActionResult<Artist>> Delete (int id) {
            var artist = await context.Artists.FirstOrDefaultAsync (x => x.Id == id);
            if (artist == null) {
                return NotFound ();
            }
            context.Artists.Remove (artist);
            await context.SaveChangesAsync ();
            return Ok (artist);

        }

    }

}