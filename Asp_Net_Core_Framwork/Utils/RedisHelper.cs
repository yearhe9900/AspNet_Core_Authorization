using Asp_Net_Core_Framwork.Framwork;
using StackExchange.Redis;
using System;

namespace Asp_Net_Core_Framwork.Utils
{
    public class RedisHelper
    {
        private IDatabase _redisDatabase;
        public RedisHelper(string databaseName="Redis_Default")
        {
            _redisDatabase = InitializeConfig.GetRedisClient().GetDatabase(databaseName);
        }

        public bool StringSet(RedisKey key, RedisValue value, TimeSpan? expiry = null, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            return  _redisDatabase.StringSet(key, value);
        }
    }
}
