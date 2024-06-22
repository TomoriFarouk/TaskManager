using System;
using MediatR;

namespace TaskManager.Application.Command.NotificationCommand
{
    public class IsReadCommand : IRequest<String>
    {
        public int Id { get; set; }

        public IsReadCommand(int Id)
        {
            this.Id = Id;
        }

    }
}

