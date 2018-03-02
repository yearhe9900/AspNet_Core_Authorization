using Asp_Net_Core_Framwork.Utils;

namespace Console_Core_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //测试RedisHelper
            RedisHelper redisHelper = new RedisHelper("Redis_1");
            redisHelper.StringSet("test", "123");

        }
    }
}
