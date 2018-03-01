using Asp_Net_Core_Framwork.Framwork;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Asp_Net_Core_Framwork.Utils
{
    public class RedisHelper
    {
        private IDatabase _redisDatabase;
        public RedisHelper(string databaseName)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();

            var redisClient = RedisClientSingleton.GetInstance(configuration);

            _redisDatabase = redisClient.GetDatabase(databaseName);
        }

        public bool StringSet(RedisKey key, RedisValue value, TimeSpan? expiry = null, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            return  _redisDatabase.StringSet(key, value);
        }
    }
}
