using Microsoft.Extensions.Configuration;
using System.IO;

namespace Asp_Net_Core_Framwork.Framwork
{
    public static class InitializeConfig
    {
        /// <summary>
        /// 初始化话ConfigurationRoot
        /// </summary>
        /// <returns></returns>
        private static IConfigurationRoot InitializeConfigurationRoot()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();
            return configuration;
        }

        /// <summary>
        /// 获取RedisClient
        /// </summary>
        /// <returns></returns>
        public static RedisClient GetRedisClient()
        {
            var redisClient = RedisClientSingleton.GetInstance(InitializeConfigurationRoot());
            return redisClient;
        }

        /// <summary>
        /// 获取DapperClient
        /// </summary>
        /// <returns></returns>
        public static DapperClient GetDapperClient()
        {
            var dapperClient = DapperClientSingleton.GetInstance(InitializeConfigurationRoot());
            return dapperClient;
        }
    }
}
