using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Asp_Net_Core_Service.Authorization.UserInfo;

namespace Api.Controllers
{
    [Route("api/identity")]
    public class IdentityController : ControllerBase
    {
        private IUserInfoService _userInfoService;

        public IdentityController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        [HttpGet]
        public string Get()
        {
            string result ="[{'title':'0-0','key':'0-0','children':[{'title':'0-0-0','key':'0-0-0','children':[{'title':'0-0-0-0','key':'0-0-0-0'},{'title':'0-0-0-1','key':'0-0-0-1'},{'title':'0-0-0-2','key':'0-0-0-2'}]},{'title':'0-0-1','key':'0-0-1','children':[{'title':'0-0-1-0','key':'0-0-1-0'},{'title':'0-0-1-1','key':'0-0-1-1'},{'title':'0-0-1-2','key':'0-0-1-2'}]},{'title':'0-0-2','key':'0-0-2'}]},{'title':'0-1','key':'0-1','children':[{'title':'0-1-0','key':'0-1-0','children':[{'title':'0-1-0-0','key':'0-1-0-0'},{'title':'0-1-0-1','key':'0-1-0-1'},{'title':'0-1-0-2','key':'0-1-0-2'}]},{'title':'0-1-1','key':'0-1-1','children':[{'title':'0-1-1-0','key':'0-1-1-0'},{'title':'0-1-1-1','key':'0-1-1-1'},{'title':'0-1-1-2','key':'0-1-1-2'}]},{'title':'0-1-2','key':'0-1-2'}]},{'title':'0-2','key':'0-2'}]";
            return result;
        }
    }
}