// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Asp_Net_Core_Service.Authorization.UserInfo;

namespace Api.Controllers
{
    [Route("identity")]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        private IUserInfoService _userInfoService;

        public IdentityController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(User.Claims.Select(c => new { c.Type, c.Value }));
        }
    }
}