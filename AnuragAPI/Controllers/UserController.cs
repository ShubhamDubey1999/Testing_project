using Microsoft.AspNetCore.Mvc;

namespace AnuragAPI.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("api/AddUser")]
        public async Task<IActionResult> UserDetails([FromBody] UserDto user)
        {
            List<UserDto> obj = new List<UserDto>();
            if (user != null) obj.Add(user);
            return Ok(obj);
        }
    }
    public class UserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
