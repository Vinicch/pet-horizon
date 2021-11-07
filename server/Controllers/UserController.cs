using System;
using System.Threading.Tasks;
using apifmu.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apifmu.Models;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

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

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var entity = await _dbContext.User.Include(e => e.Ong).Where(e => e.Id == id).FirstOrDefaultAsync();

            return Ok(entity);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var entities = await _dbContext.User.Include(e => e.Ong).ToListAsync();

            return Ok(entities);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] User entity)
        {
            try
            {
                entity.Id = new Random().Next(1, int.MaxValue);
                entity.Password = HashPassword(entity.Password);

                _dbContext.User.Add(entity);

                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] User entity)
        {
            _dbContext.User.Update(entity);

            await _dbContext.SaveChangesAsync();

            return Ok(entity);
        }

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

        string HashPassword(string password)
        {
            var salt = new byte[128 / 8];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetNonZeroBytes(salt);
            }

            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
            ));

            return hashed;
        }
    }
}
