using finWise.Interfaces;
using finWise.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace finWise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userInterface;
        private readonly IConfiguration _configuration;

        public UserController(IUserInterface userInterface, IConfiguration configuration)
        {
            _userInterface = userInterface;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDetails user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var registeredUser = await _userInterface.RegisterAsync(user);
                if (registeredUser != null)
                {
                    return Ok("User registered successfully. Thank you!");
                }
                return BadRequest("Failed to register user.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string userId, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userInterface.LoginAsync(userId, password);
            if (user != null)
            {
                var token = GenerateToken(user);
                return Ok(new { Token = token });
            }

            return BadRequest("ID or password is not correct");
        }

        private string GenerateToken(UserDetails userToken)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userToken.UserName),
                new Claim(ClaimTypes.Email, userToken.UserEmail),
                new Claim(ClaimTypes.Role, userToken.Role),  // Ensure Role is added
                new Claim("age", userToken.Age.ToString(), ClaimValueTypes.Integer)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [Authorize(Roles = "User")]
        [HttpDelete("deleteAccount")]
        public async Task<IActionResult> Delete(string emailId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deleted = await _userInterface.DeleteAsync(emailId);
            if (deleted)
            {
                return Ok("Account deleted successfully.");
            }
            return NotFound("User not found.");
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ResetPassword(string userEmail, string newPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newPass = await _userInterface.ResetPasswordAsync(userEmail, newPassword);
            if (newPass != null)
            {
                return Ok("Your password is changed.");
            }
            return BadRequest("Failed to change the password.");
        }

        [Authorize(Roles = "User")]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfileAsync(string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userInterface.GetProfileAsync(userId);
            if (user != null)
            {
                return Ok(user);
            }

            return NotFound("User not found.");
        }
    }
}
