using System;
using System.Threading.Tasks;
using apifmu.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apifmu.Models;

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

        //https://localhost:5001/pets/2
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var entity = await _dbContext.Adoption.FindAsync(id);

            return Ok(entity);
        }

        //https://localhost:5001/pets
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var entities = await _dbContext.Adoption.ToListAsync();

            return Ok(entities);
        }

        //https://localhost:5001/pets (passar json body)
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Adoption entity)
        {
            try
            {
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

        //https://localhost:5001/pets(json completo com o codigo)
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Adoption entity)
        {
            _dbContext.Adoption.Update(entity);

            await _dbContext.SaveChangesAsync();

            return Ok(entity);
        }


        //https://localhost:5001/pets/1
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
