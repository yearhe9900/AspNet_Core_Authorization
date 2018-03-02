using Microsoft.Extensions.Configuration;
using System;

namespace Asp_Net_Core_Framwork.Framwork
{
    public class DapperClient
    {
        private IConfigurationRoot _config;

        public DapperClient(IConfigurationRoot config)
        {
            _config = config;
        }

        /// <summary>
        /// 检查入参数
        /// </summary>
        /// <param name="configName">RedisConfig配置文件中的名称</param>
        /// <returns></returns>
        private IConfigurationSection CheckeConfig()
        {
            IConfigurationSection redisConfig = _config.GetSection("UrlConfig");
            if (redisConfig == null)
            {
                throw new ArgumentNullException($"找不到对应的UrlConfig配置！");
            }

            var connStr = redisConfig["MySqlConnection"];

            if (string.IsNullOrEmpty(connStr))
            {
                throw new ArgumentNullException($"找不到对应的MySqlConnection");
            }
            return redisConfig;
        }

        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public string GetConnectionStr()
        {
            IConfigurationSection redisConfig = CheckeConfig();
            return redisConfig["MySqlConnection"];
        }
    }
}
