using Microsoft.AspNetCore.Mvc;
using System; //Using for Uri
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


[Route("api/[controller]")] // api/authmanagement
[ApiController]
[Produces("application/json")]
public class AuthManagementController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtConfig _jwtConfig;

    public AuthManagementController(UserManager<IdentityUser> userManager, IOptionsMonitor<JwtConfig> optionsMonitor)
    {
        _userManager = userManager;
        _jwtConfig = optionsMonitor.CurrentValue;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto user)
    {

        if(ModelState.IsValid)
        {

            var existingUser = await _userManager.FindByEmailAsync(user.Email);

            if(existingUser != null) 
            {
                return BadRequest(new RegistrationResponse() {
                                        Result = false,
                                        Errors = new List<string>(){
                                            "Email already exist"
                                        }});
            }

            var newUser = new IdentityUser(){Email = user.Email, UserName = user.Email};
            var isCreated = await _userManager.CreateAsync(newUser, user.Password);
            if(isCreated.Succeeded)
            {
                var jwtToken = GenerateJwtToken(newUser);

                return Ok(new RegistrationResponse() {
                        Result = true, 
                        Token = jwtToken
                });
            }

            return new JsonResult(new RegistrationResponse(){
                    Result = false,
                    Errors = isCreated.Errors.Select(x => x.Description).ToList()}
                    ) {StatusCode = 500};
        }

        return BadRequest(new RegistrationResponse() {
                                        Result = false,
                                        Errors = new List<string>(){
                                            "Invalid payload"
                                        }});
    }

        private string GenerateJwtToken(IdentityUser user)
    {

        var jwtTokenHandler = new JwtSecurityTokenHandler();


        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new []
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),

            Expires = DateTime.UtcNow.AddHours(8),

            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);

        var jwtToken = jwtTokenHandler.WriteToken(token);

        return jwtToken;
    }

    
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
    {
        if(ModelState.IsValid)
        {

            var existingUser = await _userManager.FindByEmailAsync(user.Email);

            if(existingUser == null) 
            {
                return BadRequest(new RegistrationResponse() {
                    Result = false,
                    Errors = new List<string>(){
                    "Invalid authentication request"
                }});
            }

            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

            if(isCorrect)
            {
                var jwtToken = GenerateJwtToken(existingUser);

                return Ok(new RegistrationResponse() {
                        Result = true, 
                        Token = jwtToken
                });
            }
            else 
            {
                return BadRequest(new RegistrationResponse() {
                    Result = false,
                    Errors = new List<string>(){
                    "Invalid authentication request"
                    }});
            }
        }

        return BadRequest(new RegistrationResponse() {
            Result = false,
            Errors = new List<string>(){
            "Invalid payload"
        }});
    }
}

