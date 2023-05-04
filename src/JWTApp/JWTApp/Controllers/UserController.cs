using JWTApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace JWTApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("Public")]
        public IActionResult Public()
        {
            return Ok("Guess what, you're on public property");
        }

        [HttpGet("Admins")]
        [Authorize(Roles = "Administrator")]
        public IActionResult AdminEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hello, { currentUser?.GivenName }, you are an { currentUser?.Role }");
        }

        [HttpGet("Sellers")]
        [Authorize(Roles = "Seller")]
        public IActionResult SellerEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hello, {currentUser?.GivenName}, you are a {currentUser?.Role}");
        }

        [HttpGet("Admins")]
        [Authorize(Roles = "Administrator,Seller")]
        public IActionResult AdminsAndSellers()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hello, {currentUser?.GivenName}, you are an {currentUser?.Role}");
        }

        private UserModel? GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
                return null;

            var userClaims = identity.Claims;

            return new UserModel
            {
                Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                GivenName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                Surname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
            };

        }
    }
}
