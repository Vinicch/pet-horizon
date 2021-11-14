using System;
using System.Threading.Tasks;
using apifmu.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apifmu.Models;
using System.Linq;

namespace apifmu.Controllers
{
    [Controller]
    [Route("adoptions")]
    public class AdoptionController : ControllerBase
    {
        private DataContext _dbContext;

        public AdoptionController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var entity = await _dbContext.Adoption
                .Include(e => e.Ong)
                .Include(e => e.Pet)
                .Include(e => e.User)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            return Ok(entity);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var entities = await _dbContext.Adoption
                .Include(e => e.Ong)
                .Include(e => e.Pet)
                .Include(e => e.User)
                .OrderByDescending(e => e.Situation)
                .ToListAsync();

            return Ok(entities);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Adoption entity)
        {
            try
            {
                entity.Ong = null;
                entity.Pet = null;
                entity.User = null;

                entity.Id = new Random().Next(1, int.MaxValue);

                _dbContext.Adoption.Add(entity);

                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Adoption entity)
        {
            if (entity.Situation == 'A')
            {
                var pet = await _dbContext.Pet.Where(e => e.Id == entity.PetId).FirstOrDefaultAsync();

                pet.WasAdopted = true;
                pet.UserId = entity.UserId;

                _dbContext.Pet.Update(pet);

                await _dbContext.SaveChangesAsync();
            }

            entity.Ong = null;
            entity.Pet = null;
            entity.User = null;

            _dbContext.Adoption.Update(entity);

            await _dbContext.SaveChangesAsync();

            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _dbContext.Adoption.FindAsync(id);

            if (entity != null)
            {
                _dbContext.Remove(entity);

                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return NotFound();
            }

        }
    }
}
