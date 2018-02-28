using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_Core_Authorization.Utils
{
    public static class DapperDBHelper
    {
        #region 同步查询操作

        /// <summary>
        /// 同步查询操作
        /// </summary>
        /// <typeparam name="T">模型实体</typeparam>
        /// <param name="connection">连接字符串</param>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">是否是存储过程</param>
        /// <returns>object</returns>
        public static object ExecuteScalar<T>(string connection, string sqlStr, DynamicParameters param, bool flag = false) where T : class, new()
        {
            object result = null;

            using (MySqlConnection mySqlConnection = new MySqlConnection(connection))
            {
                CommandType commandType = flag ? CommandType.StoredProcedure : CommandType.Text;
                result = mySqlConnection.ExecuteScalar(sqlStr, param, null, null, commandType);
            }

            return result;
        }

        #endregion

        #region 异步查询操作

        /// <summary>
        /// 异步查询操作
        /// </summary>
        /// <typeparam name="T">模型实体</typeparam>
        /// <param name="connection">连接字符串</param>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">是否是存储过程</param>
        /// <returns>object</returns>
        public static async Task<object> ExecuteScalarAsync<T>(string connection, string sqlStr, DynamicParameters param, bool flag = false) where T : class, new()
        {
            object result = null;

            using (MySqlConnection mySqlConnection = new MySqlConnection(connection))
            {
                CommandType commandType = flag ? CommandType.StoredProcedure : CommandType.Text;
                result = await mySqlConnection.ExecuteScalarAsync(sqlStr, param, null, null, commandType);
            }

            return result;
        }

        #endregion
    }
}
