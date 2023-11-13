using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Salon_Management_NET.Migrations;
using Salon_Management_NET.Model;
using Salon_Management_NET.Model.RequestDTO;
using Salon_Management_NET.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Salon_Management_NET.Controllers
{
    // Add this to the UserController.cs file, or create a new AuthenticationController.cs file

    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly UserRepository _userRepository;

        public AuthenticationController(UserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserRequest userRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userRepository.GetUserByEmailAsync(userRequest.Email);
            if (existingUser != null)
            {
                return BadRequest("Email is already in use");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userRequest.Password);

            var newUser = new User
            {
                Name = userRequest.Name,
                Password = passwordHash, // Note: Consider hashing the password
                Email = userRequest.Email
            };

            await _userRepository.CreateUserAsync(newUser);
            return Ok(newUser);

        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginRequest.Username);
            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            // Verify the password using BCrypt
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password);

            if (isPasswordValid)
            {
                // Generate JWT token
                string token = GenerateJwtToken(user);

                // Return the token or any additional information you want
                return Ok(new { Token = token });
            }

            // Invalid password
            return Unauthorized("Invalid username or password");
        }
        private string GenerateJwtToken(User user)
        {
            var secretKey = _configuration.GetSection("AppSettings:Token").Value;

            // Define claims for the token (you can customize these based on your application)
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Email)
        // Add more claims as needed
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)); // Replace with a secure key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(

                claims: claims,
                expires: DateTime.Now.AddDays(1), // Token expiration time
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
