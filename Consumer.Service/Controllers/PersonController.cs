using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Domain.Messages;

namespace Consumer.Service.Controllers
{
    [Produces("application/json")]
    [Route("api/Person")]
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET: api/Person
        [HttpGet]
        public IEnumerable<Person> GetPersons()
        {
            return this._context.Persons;
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson([FromRoute] string id)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            var person = await this._context.Persons.SingleOrDefaultAsync(m => m.Id == id);

            if (person == null)
                return this.NotFound();

            return this.Ok(person);
        }

        // PUT: api/Person/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson([FromRoute] string id, [FromBody] Person person)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            if (id != person.Id)
                return this.BadRequest();

            this._context.Entry(person).State = EntityState.Modified;

            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.PersonExists(id))
                    return this.NotFound();
                throw;
            }

            return this.NoContent();
        }

        // POST: api/Person
        [HttpPost]
        public async Task<IActionResult> PostPerson([FromBody] Person person)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            this._context.Persons.Add(person);
            await this._context.SaveChangesAsync();

            return this.CreatedAtAction("GetPerson", new {id = person.Id}, person);
        }

        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson([FromRoute] string id)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            var person = await this._context.Persons.SingleOrDefaultAsync(m => m.Id == id);
            if (person == null)
                return this.NotFound();

            this._context.Persons.Remove(person);
            await this._context.SaveChangesAsync();

            return this.Ok(person);
        }

        private bool PersonExists(string id)
        {
            return this._context.Persons.Any(e => e.Id == id);
        }
    }
}