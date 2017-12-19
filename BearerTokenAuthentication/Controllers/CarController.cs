using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BearerTokenAuthentication.Model;

namespace BearerTokenAuthentication.Controllers
{
    [Authorize(Policy = "Member")]
    public class CarController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            // Get the claims so we can present them on the client
            var token = new Dictionary<string, string>();
            foreach (var claim in HttpContext.User.Claims)
            {
                token.Add(claim.Type, claim.Value);
            }

            // Add some data
            var cars = new List<string> { "Volvo", "Saab", "Ford" };

            var model = new CarModel(cars, token);

            return Ok(model);
        }
    }
}