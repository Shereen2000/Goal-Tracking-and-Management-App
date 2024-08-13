using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Goal_Management_Web_App.Models;
using Microsoft.AspNetCore.Identity;

namespace Goal_Management_Web_App.Infrastructure
{
    public class CustomEmailValidator : IUserValidator<User>
    {    private static readonly string[] _allowedDomains = new[] { "gmail.com", "outlook.com"};
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            
             if (_allowedDomains.Any(domain => user.Email.ToLower().EndsWith($"@{domain}")))
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "EmailDomainError",
                    Description = "Email address domain not allowed"
                }));
            }
        }
    }
}