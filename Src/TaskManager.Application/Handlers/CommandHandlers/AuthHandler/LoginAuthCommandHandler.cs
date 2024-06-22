using System;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Command.AuthCommand;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interface;
using TaskManager.Application.Response;

namespace TaskManager.Application.Handlers.AuthHandler
{
    public class LoginAuthCommandHandler : IRequestHandler<LoginAuthCommand, AuthResponse>
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ILogger _logger;
        private readonly IIdentityService _identityService;

        public LoginAuthCommandHandler(IIdentityService identityService, ITokenGenerator tokenGenerator,ILogger logger)
        {
            _identityService = identityService;
            _tokenGenerator = tokenGenerator;
            _logger = logger;
        }

        public async Task<AuthResponse> Handle(LoginAuthCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{request}");
            var result = await _identityService.SigninUserAsync(request.UserName, request.Password);

            if (!result)
            {
                _logger.LogError($"Invalid username or password");
                throw new BadRequestException("Invalid username or password");
            }

            var (userId, fullName, userName, email, roles) = await _identityService.GetUserDetailsAsync(await _identityService.GetUserIdAsync(request.UserName));

            string token = _tokenGenerator.GenerateJWTToken((userId, userName, roles));
            _logger.LogError($"Login Successful for {userName}");
            return new AuthResponse()
            {
                UserId = userId,
                Name = userName,
                Token = token
            };
        }
    }
}

