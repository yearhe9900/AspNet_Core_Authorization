using Asp_Net_Core_Core.Authorization.Domain;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp_Net_Core_Service.Authorization.UserInfo
{
    public interface IUserInfoService
    {
        UserInfoModel Login(DynamicParameters param=null);
    }
}
