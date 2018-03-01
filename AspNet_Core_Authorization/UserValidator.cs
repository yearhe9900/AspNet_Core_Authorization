using Asp_Net_Core_Framwork.Utils;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNet_Core_Authorization
{
    public class UserValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (context.UserName == "admin" && context.Password == "123")
            {
                IEnumerable<Claim> claims = new List<Claim>
                {
                    new Claim("userID",Guid.NewGuid().ToString())
                };

                context.Result = new GrantValidationResult(subject: "admin", authenticationMethod: "custom",claims:claims);
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential");
            }
            return Task.FromResult(0);
        }
    }
}
