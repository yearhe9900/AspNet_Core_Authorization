using Asp_Net_Core_Core.Authorization.Domain;
using Dapper;

namespace Asp_Net_Core_Service.Authorization.UserInfo
{
    public interface IUserInfoService
    {
        UserInfoModel Login(DynamicParameters param=null);
    }
}
