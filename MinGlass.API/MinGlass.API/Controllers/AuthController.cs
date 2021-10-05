using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinGlass.API.Dtos;
using MinGlass.API.Requests;
using Newtonsoft.Json.Linq;
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
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserDto data)
        {
            var res = await _mediator.Send(new RegisterUserRequest(data));

            return Created("User created", res);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto data)
        {
            var jwtToken = await _mediator.Send(new LoginRequest(data));

            dynamic responseObj = new JObject();
            responseObj.jwtToken = jwtToken;
            responseObj.tokenType = "Bearer";

            var response = new JsonResult(responseObj);
            return response;
        }
    }
}
