using System;
using MediatR;
using TaskManager.Application.Response;

namespace TaskManager.Application.Command.AuthCommand
{
	public class LoginAuthCommand:IRequest<AuthResponse>
	{
		public string UserName { get; set; }
		public string Password { get; set; }
	}
}

