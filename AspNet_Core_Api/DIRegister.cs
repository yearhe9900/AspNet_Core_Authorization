using Asp_Net_Core_Service.Authorization.UserInfo;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_Core_Api
{
    /// <summary>
    /// 依赖初始化
    /// </summary>
    public class DIRegister
    {
        /// <summary>
        /// 添加依赖项
        /// </summary>
        /// <param name="services"></param>
        public void AddTransient(IServiceCollection services)
        {
            services.AddTransient<IUserInfoService ,UserInfoService>();
        }
    }
}
