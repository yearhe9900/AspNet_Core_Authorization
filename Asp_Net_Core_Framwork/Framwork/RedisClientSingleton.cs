﻿using Microsoft.Extensions.Configuration;

namespace Asp_Net_Core_Framwork.Framwork
{
    public class RedisClientSingleton
    {
        private static RedisClient _redisClinet;

        private RedisClientSingleton() { }

        private static object _lockObj = new object();

        public static RedisClient GetInstance(IConfigurationRoot config)
        {
            if (_redisClinet == null)
            {
                lock (_lockObj)
                {
                    if (_redisClinet == null)
                    {
                        _redisClinet = new RedisClient(config);
                    }
                }
            }
            return _redisClinet;
        }
    }
}
