
using AutoMapper;
using Domain_Layer.Exceptions;
using Domain_Layer.Models.IdeneityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service_Abstraction;
using Shared.DataTransferObjects.AuthenticationModuleDTos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service_Implementation
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager,
                                       IConfiguration _configuration,
                                       IMapper _mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user is not null; // return true if user is not null
        }

        public async Task<AddressDTo> GetCurrentUserAddressAsync(string email)
        {
            var user = await _userManager
                                 .Users
                                 .Include(U => U.Address)
                                 .FirstOrDefaultAsync(U => U.Email == email) ??
                                 throw new UserNotFoundException(email);
                return _mapper.Map<Address, AddressDTo>(user.Address);
        }

        public async Task<UserDTo> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException(email); ;

            return new UserDTo() { DisplayName = user.DisplayName, Email = user.Email!, Token = await CreateUserTokenAsync(user)};
        }

        public async Task<AddressDTo> UpdateCurrentUserAddressAsync(AddressDTo addressDTo, string email)
        {
            var user = await _userManager
                          .Users
                          .Include(U => U.Address)
                          .FirstOrDefaultAsync(U => U.Email == email)
                          ?? throw new UserNotFoundException(email);

            if(user.Address is not null) // update address
            {
                user.Address.FirstName = addressDTo.FirstName;
                user.Address.LastName = addressDTo.LastName;
                user.Address.City = addressDTo.City;
                user.Address.Country  = addressDTo.Country;
                user.Address.Street = addressDTo.Street;
            }
            else // add new address
            {
                user.Address = _mapper.Map<AddressDTo,Address>(addressDTo);
            }

            await _userManager.UpdateAsync(user);

            return _mapper.Map<Address, AddressDTo>(user.Address);
        }

        public async Task<UserDTo> LoginAsync(LoginDTo loginDTo)
        {
            var user = await _userManager.FindByEmailAsync(loginDTo.Email) ?? throw new UserNotFoundException(loginDTo.Email);

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDTo.Password);
            if (isPasswordValid)
                return new UserDTo()
                {
                    Email = user.Email!,
                    Token = await CreateUserTokenAsync(user),
                    DisplayName = user.DisplayName
                };
            else
                throw new UnAuthorizedException();
        }


        public async Task<UserDTo> RegisterAsync(RegisterDTo registerDTo)
        {
            var applicationUser = new ApplicationUser()
            {
                Email = registerDTo.Email,
                DisplayName = registerDTo.DisplayName,
                PhoneNumber = registerDTo.PhoneNumber,
                UserName = registerDTo.UserName
            };

            var result  =  await _userManager.CreateAsync(applicationUser,registerDTo.Password);
            if (result.Succeeded)
            {
                return new UserDTo()
                {
                    Email = applicationUser.Email,
                    Token = await CreateUserTokenAsync(applicationUser),
                    DisplayName = applicationUser.DisplayName
                };
            }
            else
            {
                var errors = result.Errors.Select(E => E.Description)
                                          .ToList();

                throw new BadRequestExceptions(errors);
            }
        }

        private async Task<string> CreateUserTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new (ClaimTypes.Email, user.Email!),
                new (ClaimTypes.Name, user.UserName!),
                new (ClaimTypes.NameIdentifier, user.Id)
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach(var role in userRoles)
                claims.Add(new(ClaimTypes.Role,role));
            var secretKey = _configuration.GetSection("JwtOptions")["SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            
            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                issuer: _configuration.GetSection("JwtOptions")["Issuer"],
                audience: _configuration.GetSection("JwtOptions")["Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
