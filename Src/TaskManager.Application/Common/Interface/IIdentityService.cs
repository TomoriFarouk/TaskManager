﻿using System;
using TaskManager.Core.Entities.Identity;

namespace TaskManager.Application.Common.Interface
{
	public interface IIdentityService
	{
        Task<bool> SigninUserAsync(string userName, string password);
        Task<ApplicationUser> GetUserByIdAsync(string Id);
        Task<string> GetUserIdAsync(string username);
        Task<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsAsync(string userId);
    }
}

