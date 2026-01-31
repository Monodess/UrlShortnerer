using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AspUrlShortnerer.Domain.Services
{
    
    public class UserLogin
    {
        //each field will be a added to jwt claims
        public string Name { get; set; }
        public string Password { get; set; }
    }
    public class JwtService
    {
        //interrface for key values 
        private readonly IConfiguration _config; 
        //Via POST with Annonymos allowed jwt constructor is called 
        public JwtService(IConfiguration config)
        {
            _config = config; 
        }

        public string CreateToken (UserLogin user)
        {
            var claims = new[]
            {
                //creating jwt token fields (userlogin class info) 
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("role", "admin")
            };
            //crypto method
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            //algo that servers validates token with
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"], 
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30), 
            signingCredentials: creds

                );
            return new JwtSecurityTokenHandler().WriteToken(token); 
        }

    }
}
