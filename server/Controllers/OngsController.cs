using System;
using System.Threading.Tasks;
using apifmu.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apifmu.Models;
using Microsoft.AspNetCore.Authorization;

namespace apifmu.Controllers
{
    [Controller]
    [Route("ongs")]
    public class OngController : ControllerBase
    {
        private DataContext _dbContext;

        public OngController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "true")]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var entity = await _dbContext.Ong.FindAsync(id);

            return Ok(entity);
        }

        [HttpGet]
        [Authorize(Roles = "true")]
        public async Task<ActionResult> GetAll()
        {
            var entities = await _dbContext.Ong.ToListAsync();

            return Ok(entities);
        }

        [HttpPost]
        [Authorize(Roles = "true")]
        public async Task<ActionResult> Create([FromBody] Ong entity)
        {
            try
            {
                entity.Id = new Random().Next(1, int.MaxValue);

                _dbContext.Ong.Add(entity);

                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }

        [HttpPut]
        [Authorize(Roles = "true")]
        public async Task<ActionResult> Update([FromBody] Ong entity)
        {
            _dbContext.Ong.Update(entity);

            await _dbContext.SaveChangesAsync();

            return Ok(entity);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "true")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _dbContext.Ong.FindAsync(id);

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
