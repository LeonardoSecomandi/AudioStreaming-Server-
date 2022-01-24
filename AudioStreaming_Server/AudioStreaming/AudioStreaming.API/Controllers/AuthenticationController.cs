using AudioStreaming.API.Configuration;
using AudioStreaming.API.Models;
using AudioStreaming.API.Models.DTOS.Requests;
using AudioStreaming.API.Models.DTOS.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AudioStreaming.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly JwtConfig _jwtConfig;

        public AuthenticationController(
            UserManager<UserModel> userManager,
            IOptionsMonitor<JwtConfig> config)
        {
            this._userManager = userManager;
            this._jwtConfig = config.CurrentValue;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] UserRegistrationRequest user)
        {
            if (ModelState.IsValid)
            {
                var ExistingUser = await _userManager.FindByEmailAsync(user.Email);
                if (ExistingUser != null)
                {
                    return BadRequest(new UserRegistrationResponse()
                    {
                        Errors = new List<string>()
                {
                    "Email Gia In Utilizzo"
                },
                        Success = false
                    });
                }
                var ExistingUserByUsername = await _userManager.FindByNameAsync(user.Username);
                if (ExistingUserByUsername != null)
                {
                    return BadRequest(new UserRegistrationResponse()
                    {
                        Errors = new List<string>()
                {
                    "Username Gia in Utilizzo"
                },
                        Success = false
                    });
                }
                var NewUser = new UserModel() { Email = user.Email, UserName = user.Username };
                var IsCreated = await _userManager.CreateAsync(NewUser, user.Password);
                if (IsCreated.Succeeded)
                {
                    var JwtToken = GenerateJwtToken(NewUser);
                    return Ok(new UserRegistrationResponse()
                    {
                        Success = true,
                        Token = JwtToken
                    });
                }
                else
                {
                    return BadRequest(new UserRegistrationResponse()
                    {
                        Errors = IsCreated.Errors.Select(x => x.Description).ToList(),
                        Success = false
                    }); ;
                }
            }
            return BadRequest(new UserRegistrationResponse()
            {
                Errors = new List<string>()
                {
                    "Invalid payload"
                },
                Success = false
            });
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var TokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id",user.Id),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                }),
               // Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var Token = jwtTokenHandler.CreateToken(TokenDescriptor);
            var JwtToken = jwtTokenHandler.WriteToken(Token);

            return JwtToken;
        }

        [HttpPost("Login")]
        public async Task<AuthResult>Login(UserLoginRequest req)
        {
            var isUser = await _userManager.FindByEmailAsync(req.Email);
            if (isUser != null)
            {
                var checkpasswd = await _userManager.CheckPasswordAsync(isUser, req.Password);
                if (checkpasswd)
                {
                    return new AuthResult()
                    {
                        Success = true,
                        Username = isUser.Email,
                        Token = GenerateJwtToken(isUser),
                        Errors = null
                    };
                }
                return new AuthResult()
                {
                    Success = false,
                    Token = null,
                    Errors = new List<string>()
                {
                    "Password non corretta"
                },
                    Username = null
                };
            }
            return new AuthResult()
            {
                Success = false,
                Token = null,
                Errors = new List<string>()
                {
                    "Utente non esistente"
                },
                Username = null
            };
        }
    }
}
