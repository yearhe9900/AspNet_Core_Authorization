using Asp_Net_Core_Framwork.Framwork;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Asp_Net_Core_Framwork.Utils
{
    public class DapperDBHelper
    {
        private string _connection;
        public DapperDBHelper()
        {
            _connection = InitializeConfig.GetDapperClient().GetConnectionStr();
        }

        #region ExcuteNonQuery 增、删、改同步操作

        /// <summary>
        /// 增、删、改同步操作
        ///  </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">是否是存储过程</param>
        /// <returns>int</returns>
        public int ExcuteNonQuery<T>(string strSql, DynamicParameters param = null, bool flag = false) where T : class, new()
        {
            int result = 0;
            using (MySqlConnection con = new MySqlConnection(_connection))
            {
                CommandType commandType = flag ? CommandType.StoredProcedure : CommandType.Text;
                result = con.Execute(strSql, param, null, null, commandType);
            }
            return result;
        }

        #endregion

        #region ExcuteNonQueryAsync 增、删、改异步操作

        /// <summary>
        /// 增、删、改异步操作
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">是否是存储过程</param>
        /// <returns>int</returns>
        public async Task<int> ExcuteNonQueryAsync<T>(string strSql, DynamicParameters param = null, bool flag = false) where T : class, new()
        {
            int result = 0;
            using (MySqlConnection con = new MySqlConnection(_connection))
            {
                CommandType commandType = flag ? CommandType.StoredProcedure : CommandType.Text;
                result = await con.ExecuteAsync(strSql, param, null, null, commandType);
            }
            return result;
        }

        #endregion

        #region ExecuteScalar 同步查询操作

        /// <summary>
        /// 同步查询操作
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">是否是存储过程</param>
        /// <returns>object</returns>
        public object ExecuteScalar<T>(string strSql, DynamicParameters param = null, bool flag = false) where T : class, new()
        {
            object result = null;
            using (MySqlConnection con = new MySqlConnection(_connection))
            {
                CommandType commandType = flag ? CommandType.StoredProcedure : CommandType.Text;
                result = con.ExecuteScalar(strSql, param, null, null, commandType);
            }
            return result;
        }

        #endregion

        #region ExecuteScalarAsync 异步查询操作

        /// <summary>
        /// 异步查询操作
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">是否是存储过程</param>
        /// <returns>object</returns>
        public async Task<object> ExecuteScalarAsync<T>(string strSql, DynamicParameters param = null, bool flag = false) where T : class, new()
        {
            object result = null;
            using (MySqlConnection con = new MySqlConnection(_connection))
            {
                CommandType commandType = flag ? CommandType.StoredProcedure : CommandType.Text;
                result = await con.ExecuteScalarAsync(strSql, param, null, null, commandType);
            }
            return result;
        }
        #endregion

        #region FindOne  同步查询一条数据
        /// <summary>
        /// 同步查询一条数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">是否是存储过程</param>
        /// <returns>t</returns>
        public T FindOne<T>(string strSql, DynamicParameters param = null, bool flag = false) where T : class, new()
        {
            IDataReader dataReader = null;
            using (MySqlConnection con = new MySqlConnection(_connection))
            {
                CommandType commandType = flag ? CommandType.StoredProcedure : CommandType.Text;
                dataReader = con.ExecuteReader(strSql, param, null, null, commandType);

                if (dataReader == null || !dataReader.Read()) return null;
                Type type = typeof(T);
                T t = new T();
                foreach (var item in type.GetProperties())
                {
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        //属性名与查询出来的列名比较
                        if (item.Name.ToLower() != dataReader.GetName(i).ToLower()) continue;
                        var kvalue = dataReader[item.Name];
                        if (kvalue == DBNull.Value) continue;
                        item.SetValue(t, kvalue, null);
                        break;
                    }
                }
                return t;
            }
        }
        #endregion

        #region FindOne  异步查询一条数据
        /// <summary>
        /// 异步查询一条数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">是否是存储过程</param>
        /// <returns>t</returns>
        public async Task<T> FindOneAsync<T>(string strSql, DynamicParameters param = null, bool flag = false) where T : class, new()
        {
            IDataReader dataReader = null;
            using (MySqlConnection con = new MySqlConnection(_connection))
            {
                CommandType commandType = flag ? CommandType.StoredProcedure : CommandType.Text;
                dataReader = await con.ExecuteReaderAsync(strSql, param, null, null, commandType);

                if (dataReader == null || !dataReader.Read()) return null;
                Type type = typeof(T);
                T t = new T();
                foreach (var item in type.GetProperties())
                {
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        //属性名与查询出来的列名比较
                        if (item.Name.ToLower() != dataReader.GetName(i).ToLower()) continue;
                        var kvalue = dataReader[item.Name];
                        if (kvalue == DBNull.Value) continue;
                        item.SetValue(t, kvalue, null);
                        break;
                    }
                }
                return t;
            }
        }
        #endregion

        #region FindToList  同步查询数据集合
        /// <summary>
        /// 同步查询数据集合
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">是否是存储过程</param>
        /// <returns>t</returns>
        public IList<T> FindToList<T>(string strSql, DynamicParameters param = null, bool flag = false) where T : class, new()
        {
            IDataReader dataReader = null;
            using (MySqlConnection con = new MySqlConnection(_connection))
            {
                CommandType commandType = flag ? CommandType.StoredProcedure : CommandType.Text;
                dataReader = con.ExecuteReader(strSql, param, null, null, commandType);

                if (dataReader == null || !dataReader.Read()) return null;
                Type type = typeof(T);
                List<T> tlist = new List<T>();
                while (dataReader.Read())
                {
                    T t = new T();
                    foreach (var item in type.GetProperties())
                    {
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            //属性名与查询出来的列名比较
                            if (item.Name.ToLower() != dataReader.GetName(i).ToLower()) continue;
                            var kvalue = dataReader[item.Name];
                            if (kvalue == DBNull.Value) continue;
                            item.SetValue(t, kvalue, null);
                            break;
                        }
                    }
                    if (tlist != null) tlist.Add(t);
                }
                return tlist;
            }
        }
        #endregion

        #region FindToListAsync  异步查询数据集合
        /// <summary>
        /// 异步查询数据集合
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">是否是存储过程</param>
        /// <returns>t</returns>
        public async Task<IList<T>> FindToListAsync<T>(string strSql, DynamicParameters param = null, bool flag = false) where T : class, new()
        {
            IDataReader dataReader = null;
            using (MySqlConnection con = new MySqlConnection(_connection))
            {
                CommandType commandType = flag ? CommandType.StoredProcedure : CommandType.Text;
                dataReader = await con.ExecuteReaderAsync(strSql, param, null, null, commandType);

                if (dataReader == null || !dataReader.Read()) return null;
                Type type = typeof(T);
                List<T> tlist = new List<T>();
                while (dataReader.Read())
                {
                    T t = new T();
                    foreach (var item in type.GetProperties())
                    {
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            //属性名与查询出来的列名比较
                            if (item.Name.ToLower() != dataReader.GetName(i).ToLower()) continue;
                            var kvalue = dataReader[item.Name];
                            if (kvalue == DBNull.Value) continue;
                            item.SetValue(t, kvalue, null);
                            break;
                        }
                    }
                    if (tlist != null) tlist.Add(t);
                }
                return tlist;
            }
        }
        #endregion

        #region +FindToList  同步查询数据集合
        /// <summary>
        /// 同步查询数据集合
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">是否是存储过程</param>
        /// <returns>t</returns>
        public IList<T> FindToListAsPage<T>(string strSql, DynamicParameters param = null, bool flag = false) where T : class, new()
        {
            IDataReader dataReader = null;
            using (MySqlConnection con = new MySqlConnection(_connection))
            {
                CommandType commandType = flag ? CommandType.StoredProcedure : CommandType.Text;
                dataReader = con.ExecuteReader(strSql, param, null, null, commandType);

                if (dataReader == null || !dataReader.Read()) return null;
                Type type = typeof(T);
                List<T> tlist = new List<T>();
                while (dataReader.Read())
                {
                    T t = new T();
                    foreach (var item in type.GetProperties())
                    {
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            //属性名与查询出来的列名比较
                            if (item.Name.ToLower() != dataReader.GetName(i).ToLower()) continue;
                            var kvalue = dataReader[item.Name];
                            if (kvalue == DBNull.Value) continue;
                            item.SetValue(t, kvalue, null);
                            break;
                        }
                    }
                    if (tlist != null) tlist.Add(t);
                }
                return tlist;
            }
        }
        #endregion

        #region FindToListByPage  同步分页查询数据集合
        /// <summary>
        /// 同步分页查询数据集合
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">是否是存储过程</param>
        /// <returns>t</returns>
        public IList<T> FindToListByPage<T>(string strSql, DynamicParameters param = null, bool flag = false) where T : class, new()
        {
            IDataReader dataReader = null;
            using (MySqlConnection con = new MySqlConnection(_connection))
            {
                CommandType commandType = flag ? CommandType.StoredProcedure : CommandType.Text;
                dataReader = con.ExecuteReader(strSql, param, null, null, commandType);

                if (dataReader == null || !dataReader.Read()) return null;
                Type type = typeof(T);
                List<T> tlist = new List<T>();
                while (dataReader.Read())
                {
                    T t = new T();
                    foreach (var item in type.GetProperties())
                    {
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            //属性名与查询出来的列名比较
                            if (item.Name.ToLower() != dataReader.GetName(i).ToLower()) continue;
                            var kvalue = dataReader[item.Name];
                            if (kvalue == DBNull.Value) continue;
                            item.SetValue(t, kvalue, null);
                            break;
                        }
                    }
                    if (tlist != null) tlist.Add(t);
                }
                return tlist;
            }
        }
        #endregion

        #region FindToListByPageAsync  异步分页查询数据集合
        /// <summary>
        /// 异步分页查询数据集合
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">是否是存储过程</param>
        /// <returns>t</returns>
        public async Task<IList<T>> FindToListByPageAsync<T>(string strSql, DynamicParameters param = null, bool flag = false) where T : class, new()
        {
            IDataReader dataReader = null;
            using (MySqlConnection con = new MySqlConnection(_connection))
            {
                CommandType commandType = flag ? CommandType.StoredProcedure : CommandType.Text;
                dataReader = await con.ExecuteReaderAsync(strSql, param, null, null, commandType);

                if (dataReader == null || !dataReader.Read()) return null;
                Type type = typeof(T);
                List<T> tlist = new List<T>();
                while (dataReader.Read())
                {
                    T t = new T();
                    foreach (var item in type.GetProperties())
                    {
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            //属性名与查询出来的列名比较
                            if (item.Name.ToLower() != dataReader.GetName(i).ToLower()) continue;
                            var kvalue = dataReader[item.Name];
                            if (kvalue == DBNull.Value) continue;
                            item.SetValue(t, kvalue, null);
                            break;
                        }
                    }
                    if (tlist != null) tlist.Add(t);
                }
                return tlist;
            }
        }
        #endregion
    }
}
