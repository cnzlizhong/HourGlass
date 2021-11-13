using MediatR;
using MinGlass.API.Requests;
using MinGlass.API.Responses;
using MinGlass.API.services;
using MinGlass.Models.Exceptions;
using MinGlass.Repository.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MinGlass.API.UseCases
{
    public class GetCurrentUserUseCase : IRequestHandler<GetCurrentUserRequest, GetCurrentUserResponse>
    {
        private readonly IClaimsService _claimsService;
        private readonly IUserRepository _userRepository;
        public GetCurrentUserUseCase(IClaimsService claimsService, IUserRepository userRepository)
        {
            _claimsService = claimsService;
            _userRepository = userRepository;
        }

        public async Task<GetCurrentUserResponse> Handle(GetCurrentUserRequest request, CancellationToken cancellationToken)
        {
            var userId = _claimsService.GetUserId();
            if (userId == null)
                throw new MinglassUnauthorizedException();

            var user = await _userRepository.GetUserByIdAsync(new Guid(userId));
            if (user == null)
                throw new MinglassException("User not found.");

            return new GetCurrentUserResponse
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
        }
    }
}
