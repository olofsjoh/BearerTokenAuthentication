using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BearerTokenAuthentication.Model;
using BearerTokenAuthentication.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BearerTokenAuthentication.Controllers
{
    [AllowAnonymous]
    public class TokenController : Controller
    {
        private readonly TokenConfiguration _configuration;

        public TokenController(TokenConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Create([FromBody] LoginInputModel model )
        {
            if (model.Username != "johannes" && model.Password != "olofsson")
                return Unauthorized();

            var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create(_configuration.Key))
                                .AddSubject("todos")
                                .AddIssuer(_configuration.Issuer)
                                .AddAudience(_configuration.Issuer)
                                .AddClaim("MembershipId", "1234")
                                .AddExpiry(1)
                                .Build();

            return Ok(token.Value);
        }
    }
}