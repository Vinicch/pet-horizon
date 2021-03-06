using System.Linq;
using System.Threading.Tasks;
using apifmu.Data;
using apifmu.Dtos;
using apifmu.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apifmu.Controllers
{
    [Controller]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private DataContext _dbContext;

        public AuthController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult> Auth([FromBody] AuthDto dto)
        {
            var user = await _dbContext.User.Where(e => e.Email == dto.Email).FirstOrDefaultAsync();

            if (user == null)
                return NotFound();

            if (user.Id != 0)
            {
                var isAuthorized = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);

                if (!isAuthorized)
                    return Unauthorized();
            }

            var token = AuthService.GenerateToken(user);

            user.Password = "";

            return Ok(new
            {
                user,
                token,
            });
        }
    }
}
