using System;
using System.Threading.Tasks;
using apifmu.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apifmu.Models;
using apifmu.Dtos;
using System.Linq;
using Geolocation;
using System.Collections.Generic;

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

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var entity = await _dbContext.Pet
                .Include(e => e.Ong)
                .Include(e => e.User)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            return Ok(entity);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var entities = await _dbContext.Pet
                .Include(e => e.Ong)
                .Include(e => e.User)
                .ToListAsync();

            return Ok(entities);
        }

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

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Pet entity)
        {
            _dbContext.Pet.Update(entity);

            await _dbContext.SaveChangesAsync();

            return Ok(entity);
        }

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

        [HttpPost("search")]
        public async Task<ActionResult> Search([FromBody] SearchDto dto)
        {
            var pets = await FilterPets(dto);

            if (pets.Count == 0)
            {
                return Ok(pets);
            }

            var user = await _dbContext.User.FindAsync(dto.UserId);

            pets = pets.OrderBy(e => CalculateDistance(user, e.Ong)).ToList();

            return Ok(pets);
        }

        async Task<List<Pet>> FilterPets(SearchDto dto)
        {
            var query = _dbContext.Pet.Where(e => e.Size == dto.Size).Where(e => e.Sex == dto.Sex);

            if (!string.IsNullOrWhiteSpace(dto.Breed))
            {
                query = query.Where(e => e.Breed == dto.Breed);
            }

            if (!string.IsNullOrWhiteSpace(dto.Color))
            {
                query = query.Where(e => e.Color == dto.Color);
            }

            if (!string.IsNullOrWhiteSpace(dto.Personality))
            {
                query = query.Where(e => e.Personality == dto.Personality);
            }

            return await query.Where(e => !e.WasAdopted).Include(e => e.Ong).ToListAsync();
        }

        double CalculateDistance(User user, Ong ong)
        {
            var origin = new Coordinate(user.Lat, user.Long);
            var destination = new Coordinate(ong.Lat, ong.Long);

            return GeoCalculator.GetDistance(origin, destination, 1, DistanceUnit.Kilometers);
        }
    }
}
