using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmAppApi.Models;

namespace FarmAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly FarmAppDBContext dbContext;

        public StatesController(FarmAppDBContext context)
        {
            dbContext = context;
        }

        // GET: api/States
        [HttpGet]
        public async Task<ActionResult<IEnumerable<State>>> GetStates()
        {
            return await dbContext.States
                .Include(s => s.Towns)
                .ToListAsync();
        }

        // GET: api/States/5
        [HttpGet("{id}")]
        public async Task<ActionResult<State>> GetState(int id)
        {
            var state = await dbContext.States.FindAsync(id);

            if (state == null)
            {
                return NotFound();
            }

            return state;
        }

        // PUT: api/States/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutState(int id, State state)
        {
            if (id != state.IdState)
            {
                return BadRequest();
            }

            dbContext.Entry(state).State = EntityState.Modified;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateExists(id))
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

        // POST: api/States
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<State>> PostState(State state)
        {
            dbContext.States.Add(state);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction("GetState", new { id = state.IdState }, state);
        }

        // DELETE: api/States/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState(int id)
        {
            var state = await dbContext.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }

            dbContext.States.Remove(state);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool StateExists(int id)
        {
            return dbContext.States.Any(e => e.IdState == id);
        }
    }
}
