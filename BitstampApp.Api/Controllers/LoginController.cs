using BitstampApp.Api.Helpers;
using BitstampApp.Core.Models;
using BitstampApp.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BitstampApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class LoginController : ControllerBase
    {
        private IUserService _userService;
        private readonly IConfiguration _configuration;

        public LoginController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("authorize")]
        public IActionResult Authorize([FromBody] User userParam)
        {
            var user = _userService.Authorize(userParam);
            if (user == null)
            {
                return BadRequest(new { message = "Kullanici veya şifre hatalı!" });
            }

            Helpers.TokenHandler tokenHandler = new Helpers.TokenHandler(_configuration);
            Token token = tokenHandler.CreateAccessToken(user);

            user.AccessToken = token.AccessToken;

            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        //[Authorize]
        [Route("register")]
        public IActionResult Register([FromBody] User userParam)
        {

            userParam.Password = CalculateHash(userParam.Password);

            var user = _userService.Create(userParam);

            if (user == null)
                return BadRequest(new { message = "İşlem başarısız !" });
            return Ok(user);
        }

        public string CalculateHash(string input)
        {
            var salt = GenerateSalt(16);

            var bytes = KeyDerivation.Pbkdf2(input, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);

            return $"{ Convert.ToBase64String(salt) }:{ Convert.ToBase64String(bytes) }";
        }

        private static byte[] GenerateSalt(int length)
        {
            var salt = new byte[length];

            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }

            return salt;
        }
    }
}

