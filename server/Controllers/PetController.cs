using System;
using System.Threading.Tasks;
using apifmu.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apifmu.Models;

namespace apifmu.Controllers
{
    [Controller]
    [Route("pets")]
    public class PetController : ControllerBase
    {
        private DataContext _dbContext;

        public PetController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        //https://localhost:5001/Animal/2
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var entity = await _dbContext.Pet.FindAsync(id);

            return Ok(entity);
        }

        //https://localhost:5001/Animal
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var entities = await _dbContext.Pet.ToListAsync();

            return Ok(entities);
        }

        //https://localhost:5001/Animal (passar json body)
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Pet entity)
        {
            try
            {
                entity.Id = new Random().Next(1, int.MaxValue);

                _dbContext.Pet.Add(entity);

                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }

        //https://localhost:5001/Animal(json completo com o codigo)
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Pet entity)
        {
            _dbContext.Pet.Update(entity);

            await _dbContext.SaveChangesAsync();

            return Ok(entity);
        }


        //https://localhost:5001/Animal/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _dbContext.Pet.FindAsync(id);

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