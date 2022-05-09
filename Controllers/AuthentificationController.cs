using JwtApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace JwtApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        private readonly IJwtAuthentificationServices _JwtAuthentificationServices;
        private readonly IConfiguration _config;
        public AuthentificationController(IJwtAuthentificationServices jwtAuthentificationServices,IConfiguration config)
        {
            _JwtAuthentificationServices = jwtAuthentificationServices;
            _config = config;
        }


        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]LoginModel model)
        {
            var user = _JwtAuthentificationServices.Authentificate(model.Email, model.Password);
            if(user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email,user.Email)
                };
            var token = _JwtAuthentificationServices.GenerateToken(_config["Jwt:Key"], claims);
                return Ok(token);
            }
            return Unauthorized();
        }
    }
}
