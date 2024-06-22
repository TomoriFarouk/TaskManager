using System;
using MediatR;
using TaskManager.Application.Response;

namespace TaskManager.Application.Command.AuthCommand
{
	public class RegisterAuthCommand:IRequest<AuthResponse>
	{
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}

