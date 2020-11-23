using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using cAmp.Libraries.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace cAmp.Libraries.Common.Security
{
    public class JwtHelper
    {
        public static string CreateJwt(
            IWebSession session, 
            string jwtSecret, 
            int daysValid = 1)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = CreateClaimsIdentities(session);

            var now = DateTime.UtcNow;

            // Create JWToken
            var token = tokenHandler.CreateJwtSecurityToken(subject: claims,
                notBefore: now,
                expires: now.AddDays(daysValid),
                signingCredentials:
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Convert.FromBase64String(jwtSecret)),
                    SecurityAlgorithms.HmacSha256Signature));

            return tokenHandler.WriteToken(token);
        }

        public static ClaimsIdentity CreateClaimsIdentities(IWebSession userSession)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim("UserId", userSession.UserId.ToString()));
            claimsIdentity.AddClaim(new Claim("SessionId", userSession.Id.ToString()));
            claimsIdentity.AddClaim(new Claim("Username", userSession.Username));
            claimsIdentity.AddClaim(new Claim("FirstName", userSession.FirstName));
            claimsIdentity.AddClaim(new Claim("LastName", userSession.LastName));

            return claimsIdentity;
        }

        public static string CreateJwtSecret()
        {
            var hmac = new HMACSHA256();
            var key = Convert.ToBase64String(hmac.Key);

            return key;
        }
    }
}
