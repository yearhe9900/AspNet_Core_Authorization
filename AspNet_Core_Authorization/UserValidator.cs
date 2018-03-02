using Asp_Net_Core_Framwork.Utils;
using Asp_Net_Core_Service.Authorization.UserInfo;
using Dapper;
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
        private IUserInfoService _userInfoService;

        public UserValidator(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            DynamicParameters pars = new DynamicParameters();
            pars.Add("loginname", context.UserName);
            pars.Add("password", context.Password);
            var userInfo = _userInfoService.Login(pars);
            if (userInfo != null)
            {
                IEnumerable<Claim> claims = new List<Claim>
                {
                    new Claim("userID",userInfo.UserID)
                };

                context.Result = new GrantValidationResult(subject: userInfo.LoginName, authenticationMethod: "custom", claims: claims);
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential");
            }
            return Task.FromResult(0);
        }
    }
}
