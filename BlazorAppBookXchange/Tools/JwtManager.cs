﻿using BlazorAppBookXchange.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorAppBookXchange.Tools
{
    public class JwtManager
    {
        private string issuer, audience, secret;

        public JwtManager(IConfiguration config)
        {
            issuer = config.GetSection("JwtToken").GetSection("issuer").ToString();
            audience = config.GetSection("JwtToken").GetSection("audience").ToString();
            secret = config.GetSection("JwtToken").GetSection("secret").ToString();
        }

        public string GenerateToken(MembreModel m)
        {
            //création de crédentials (security key)

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            //création de l'objet à stocker dans le payload

            Claim[] myClaims = new Claim[]
            {
                new Claim(ClaimTypes.Sid, m.IdMembre.ToString()),
                new Claim(ClaimTypes.Email, m.Email)
            };

            //Génération du token
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(myClaims),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityToken token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
