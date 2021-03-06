﻿using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BearerTokenAuthentication.Security
{
    public static class JwtSecurityKey
    {
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}
