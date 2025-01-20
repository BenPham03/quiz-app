using BLL.Services.EmailService;
using BLL.ViewModels;
using DAL.Data;
using DAL.Helpers;
using DAL.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using BLL.ViewModels;
using DAL.Data;
using DAL.Helpers;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BLL.Extentions;
using BLL.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly AuthService _authService;

        public AuthenticationController(UserManager<AppUser> userManager,
                                 RoleManager<IdentityRole> roleManager,
                                 DataDbContext context,
                                 IConfiguration configuration,
                                 SignInManager<AppUser> signInManager,
                                 IEmailSender emailSender,
                                  AuthService authService) // Add this
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterVM registerVM)
        {
            var temp = await _userManager.FindByEmailAsync(registerVM.Email);
            if (temp != null)
            {
                return BadRequest($"User with email {registerVM.Email} is already exists");
            }

            var newUser = new AppUser()
            {
                UserName = registerVM.UserName,
                Email = registerVM.Email,
                SecurityStamp = new Guid().ToString(),
            };

            var result = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(Role.User))
                    await _roleManager.CreateAsync(new IdentityRole(Role.User));

                if (await _roleManager.RoleExistsAsync(Role.User))
                {
                    await _userManager.AddToRoleAsync(newUser, Role.User);
                }
                return Created(nameof(Register), new { message = $"User {registerVM.Email} created!" });
            }

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return BadRequest($"User could not be created. Errors: {errors}");
        }


        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterVM registerVM)
        {
            var temp = await _userManager.FindByEmailAsync(registerVM.Email);
            if (temp != null)
            {
                return BadRequest($"User with email {registerVM.Email} is already exists");
            }

            var newUser = new AppUser()
            {
                UserName = registerVM.UserName,
                Email = registerVM.Email,
                SecurityStamp = new Guid().ToString(),
            };

            var result = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(Role.Admin))
                    await _roleManager.CreateAsync(new IdentityRole(Role.Admin));
                if (!await _roleManager.RoleExistsAsync(Role.User))
                    await _roleManager.CreateAsync(new IdentityRole(Role.User));

                if (await _roleManager.RoleExistsAsync(Role.Admin))
                {
                    await _userManager.AddToRoleAsync(newUser, Role.Admin);
                }
                return Created(nameof(Register), $"User {registerVM.Email} created!");
            }

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return BadRequest($"User could not be created. Errors: {errors}");
        }
        private async Task<AuthResultVM> GenerateToken(AppUser user)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var userRoles = await _userManager.GetRolesAsync(user);
            //Add role to claim
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.UtcNow.AddDays(7),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsRevoked = false,
                UserId = user.Id,
                DateAdded = DateTime.UtcNow,
                DateExpire = DateTime.UtcNow.AddMonths(6),
                Token = Guid.NewGuid().ToString() + " - " + Guid.NewGuid().ToString()
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
            var response = new AuthResultVM()
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = token.ValidTo
            };
            return response;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginVM payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, provide all required fields");
            }
            var user = await _userManager.FindByEmailAsync(payload.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, payload.Password))
            {
                var tokenValue = await GenerateToken(user);
                return Ok(tokenValue);
            }
            return Unauthorized();
        }

        //Google Authentication
        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] string credencial)
        {
            var setting = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["GoogleKeys:ClientId"] }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(credencial, setting);

            var user = await _userManager.FindByEmailAsync(payload.Email);
            if (user != null)
            {
                var token = await GenerateToken(user);
                return Ok(token);
            }
            else
            {
                var username = payload.Name.Replace(' ', '_');
                var newUser = new AppUser
                {
                    UserName = username,
                    Email = payload.Email,
                    SecurityStamp = new Guid().ToString(),
                };
                var result = await _userManager.CreateAsync(newUser);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(Role.User))
                        await _roleManager.CreateAsync(new IdentityRole(Role.User));

                    if (await _roleManager.RoleExistsAsync(Role.User))
                    {
                        await _userManager.AddToRoleAsync(newUser, Role.User);
                    }
                    var token = await GenerateToken(newUser);
                    return Ok(token);
                }
                return BadRequest();
            }
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordVM updatePasswordVM)
        {
            var user = await _userManager.FindByEmailAsync(updatePasswordVM.Email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, updatePasswordVM.NewPassword);

                if (result.Succeeded)
                {
                    return Ok(new { message = "Mật khẩu đã được thay đổi thành công." });
                }
                else
                {
                    return BadRequest("Có lỗi xảy ra khi thay đổi mật khẩu.");
                }

            }
            return BadRequest();
        }
        [HttpPut("edit-timeline")]
        public async Task<IActionResult> UpdateTimeline(string timeline)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                var result = _authService.EditTimeLine(user, timeline);
                return Ok(result);
            }
            return BadRequest();
        }

    }
}
