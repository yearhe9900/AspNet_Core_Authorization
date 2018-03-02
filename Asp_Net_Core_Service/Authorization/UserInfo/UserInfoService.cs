using Asp_Net_Core_Core.Authorization.Domain;
using Asp_Net_Core_Framwork.Utils;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp_Net_Core_Service.Authorization.UserInfo
{
    public class UserInfoService : IUserInfoService
    {
        DapperDBHelper _dapperDBHelper = new DapperDBHelper();

        public UserInfoModel Login(DynamicParameters param = null)
        {
            string sqlStr = "select * from UserInfo where LoginName =@loginname and Password =@password";

            var userInfo = _dapperDBHelper.FindOne<UserInfoModel>(sqlStr, param);

            return userInfo == null ? null : userInfo;
        }
    }
}