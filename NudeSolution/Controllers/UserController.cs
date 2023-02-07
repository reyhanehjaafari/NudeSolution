using NudeSolution.DAL;
using NudeSolution.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace NudeSolution.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly NudeContext _nudeContext;

        public UserController(NudeContext chatContext)
        {
            _nudeContext= chatContext;
        }


        [HttpPost("create")]
        [ProducesResponseType(200, Type = typeof(UserEntity))]
        [ProducesResponseType(400)]
        public IActionResult CreateUser(UserEntity user)
        {
            try
            {
                _nudeContext.Users.Add(user);
                _nudeContext.SaveChanges();
                return Ok(user);
            }catch (SqlException ex)
            {
               
                return BadRequest(new { mesage ="User cannot be saved! " + ex.Message  });
            }
        }
    }
}
