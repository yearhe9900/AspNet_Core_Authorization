using System;
using System.Collections.Generic;
using System.Text;

namespace Asp_Net_Core_Core.Authorization.Domain
{
    /// <summary>
    /// 用户信息模型
    /// </summary>
    public class UserInfoModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
