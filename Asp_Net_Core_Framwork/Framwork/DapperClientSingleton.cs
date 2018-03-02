using Microsoft.Extensions.Configuration;

namespace Asp_Net_Core_Framwork.Framwork
{
    public class DapperClientSingleton
    {
        private static DapperClient _dapperClient;

        private DapperClientSingleton() { }

        private static object _lockObj = new object();

        public static DapperClient GetInstance(IConfigurationRoot config)
        {
            if (_dapperClient == null)
            {
                lock (_lockObj)
                {
                    if (_dapperClient == null)
                    {
                        _dapperClient = new DapperClient(config);
                    }
                }
            }
            return _dapperClient;
        }
    }
}
