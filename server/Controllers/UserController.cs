using System;
using System.Threading.Tasks;
using apifmu.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apifmu.Models;

namespace apifmu.Controllers
{
    [Controller]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private DataContext _dbContext;

        public UserController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        //https://localhost:5001/usuario/2
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var entity = await _dbContext.User.FindAsync(id);

            return Ok(entity);
        }

        //https://localhost:5001/usuario
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var entities = await _dbContext.User.ToListAsync();

            return Ok(entities);
        }

        //https://localhost:5001/usuario (passar json body)
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] User entity)
        {
            try
            {
                entity.Id = new Random().Next(1, int.MaxValue);

                _dbContext.User.Add(entity);

                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }

        //https://localhost:5001/usuario(json completo com o codigo)
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] User entity)
        {
            _dbContext.User.Update(entity);

            await _dbContext.SaveChangesAsync();

            return Ok(entity);
        }


        //https://localhost:5001/usuario/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _dbContext.User.FindAsync(id);

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
