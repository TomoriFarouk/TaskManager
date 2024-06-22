using System;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Command.AuthCommand;
using TaskManager.Application.Response;
using TaskManager.Core.Entities.Identity;

namespace TaskManager.Application.Handlers.AuthHandler
{
    public class RegisterAuthCommandHandler : IRequestHandler<RegisterAuthCommand, AuthResponse>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public RegisterAuthCommandHandler(UserManager<ApplicationUser> userManager, IMediator mediator,ILogger logger)
        {
            _mediator = mediator;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<AuthResponse> Handle(RegisterAuthCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{request}");
            var userByEmail = await _userManager.FindByEmailAsync(request.Email);
            var userByUsername = await _userManager.FindByNameAsync(request.UserName);
            if (userByEmail is not null || userByUsername is not null)
            {
                _logger.LogError($"User with email {request.Email} or username { request.UserName} already exists.");
                throw new ArgumentException($"User with email {request.Email} or username {request.UserName} already exists.");
            }

            ApplicationUser user = new()
            {
                Email = request.Email,
                UserName = request.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                _logger.LogError($"Unable to register user {request.UserName} ");
                throw new ArgumentException($"Unable to register user {request.UserName} ");
            }


            LoginAuthCommand command = new()
            {
                UserName = user.UserName,
                Password = request.Password
            };
            return await _mediator.Send(command, cancellationToken);


        }
    }
}

