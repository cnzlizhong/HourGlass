using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinGlass.API.Dtos;
using MinGlass.API.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinGlass.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController: ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDto data)
        {
            var res = await _mediator.Send(new RegisterUserRequest(data));

            return Created("User created", res);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto data)
        {
            var jwtToken = await _mediator.Send(new LoginRequest(data));

            Response.Cookies.Append("jwt", jwtToken, new CookieOptions() { HttpOnly = true });

            return Ok("Login success");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok("Log out success");
        }
    }
}
