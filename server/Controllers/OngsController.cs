using System;
using System.Threading.Tasks;
using apifmu.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apifmu.Models;

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

        //https://localhost:5001/Ongs/2
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var entity = await _dbContext.Ong.FindAsync(id);

            return Ok(entity);
        }

        //https://localhost:5001/Ongs
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var entities = await _dbContext.Ong.ToListAsync();

            return Ok(entities);
        }

        //https://localhost:5001/Ongs (passar json body)
        [HttpPost]
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

        //https://localhost:5001/Ongs(json completo com o codigo)
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Ong entity)
        {
            _dbContext.Ong.Update(entity);

            await _dbContext.SaveChangesAsync();

            return Ok(entity);
        }


        //https://localhost:5001/Ongs/1
        [HttpDelete("{id}")]
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
