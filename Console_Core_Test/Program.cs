using Asp_Net_Core_Framwork.Framwork;
using Asp_Net_Core_Framwork.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Console_Core_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //测试RedisHelper
            RedisHelper redisHelper = new RedisHelper("Redis_1");
            redisHelper.StringSet("test", "123");

            string sqlStr = "select * from user";

            DapperDBHelper dapperDBHelper = new DapperDBHelper();

            var a = dapperDBHelper.FindOne<User>(sqlStr);
        }
    }
}
