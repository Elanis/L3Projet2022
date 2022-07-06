using L3Projet.Business.Interfaces;
using L3Projet.Common;
using L3Projet.Common.DTOModels;
using L3Projet.Common.Models;
using L3Projet.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace L3Projet.Business.Implementations {
    public class UsersService : IUsersService {
        private readonly GameContext context;

        private readonly string JwtKey;
        private readonly string JwtIssuer;

        public UsersService(GameContext context, IOptions<AppSettings> options) {
            this.context = context;

            this.JwtKey = options.Value.JwtKey;
            this.JwtIssuer = options.Value.JwtIssuer;
        }

        private string GenerateJwtToken(string username) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new();
            claims.Add(new Claim(ClaimTypes.Name, username));

            var token = new JwtSecurityToken(JwtIssuer,
              JwtIssuer,
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string? AuthenticateUser(string username, string password) {
            var user = context.Users.FirstOrDefault((x) => x.Username.ToLower() == username.ToLower());

            if (user == default) {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password)) {
                return null;
            }

            return GenerateJwtToken(username);
        }

        public string? Register(UserRegistrationRequest request) {
            if (request == null) {
                return null;
            }

            if (string.IsNullOrWhiteSpace(request.Username)) {
                return null;
            }

            request.Username = request.Username.Trim();

            if (request.Username.Length < 4) {
                return null;
            }

            if (context.Users.Any(x => x.Username == request.Username)) {
                return null;
            }

            if (context.Users.Any(x => x.Mail == request.Mail)) {
                return null;
            }

            if (request.Password != request.ConfirmationPassword) {
                return null;
            }

            Regex passwordRegexp = new(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%_^&*-]).{8,}$");
            if (!passwordRegexp.IsMatch(request.Password)) {
                return null;
            }

            Regex mailRegexp = new("(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e \\-\\x1f\\x21\\x23 -\\x5b\\x5d -\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e -\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])");
            if (!mailRegexp.IsMatch(request.Mail)) {
                return null;
            }

            context.Add(new User() {
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Mail = request.Mail,
                Planets = new List<Planet> {
                    Planet.CreateEmptyPlanet(request.Username)
                }
            });
            context.SaveChanges();

            return GenerateJwtToken(request.Username);
        }

        public UserPublicDataResponse? GetByUsername(string? name) {
            var user = context.Users.FirstOrDefault((x) => x.Username == name);

            if (user == null) {
                return default;
            }

            return new UserPublicDataResponse() {
                Username = user.Username
            };
        }

        public string RenewToken(string? name) {
            if (name == null) {
                return null;
            }

            return GenerateJwtToken(name);
        }

        public IEnumerable<LeaderboardUser> GetLeaderboard() {
            return context.Users.Include(u => u.Planets).ThenInclude(p => p.Buildings).ToList().OrderByDescending(x => x.Points).Select(LeaderboardUser.MapUser);
        }
    }
}
