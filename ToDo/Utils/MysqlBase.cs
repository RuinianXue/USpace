using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace UIDisplay.Utils
{
    internal class MysqlBase
    {
        private MySqlConnection conn = null;
        private MySqlCommand command = null;
        private MySqlDataReader reader = null;

        /// <summary>
        /// MySQL数据库操作基类构造方法，建议在构造方法里进行数据库连接
        /// </summary>
        public MysqlBase()
        {
            string connStr = $"Database={Settings.DatbaseName};Data Source={Settings.DatabaseHost};User Id={Settings.DatabaseUsername};Password={Settings.DatabasePassword};pooling=false;CharSet=utf8;port={Settings.DatabasePort};";
            conn = new MySqlConnection(connStr);
        }

        /// <summary>
        /// 释放资源，包括连接、命令和读取器
        /// </summary>
        public void Dispose()
        {
            conn?.Dispose();
            command?.Dispose();
            reader?.Dispose();
        }

        /// <summary>
        /// 检查数据库连接状态
        /// </summary>
        /// <returns>连接是否成功</returns>
        public bool CheckConnectStatus()
        {
            try
            {
                conn.Open();
                return conn.State == ConnectionState.Open;
            }
            catch (Exception ex)
            {
                // 记录异常信息，可以根据需要进行扩展
                Console.WriteLine($"Error in CheckConnectStatus: {ex.Message}");
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行通用的SQL语句，返回影响的行数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns>影响的行数</returns>
        public int CommonExecute(string sql, params MySqlParameter[] parameters)
        {
            int res = -1;
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (command = new MySqlCommand(sql, conn))
                    {
                        command.Parameters.AddRange(parameters);
                        res = command.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch (MySqlException ex)
            {
                // 记录异常信息，可以根据需要进行扩展
                Console.WriteLine($"Error in CommonExecute: {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return res;
        }

        /// <summary>
        /// 执行SQL查询语句，返回DataTable
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns>查询结果的DataTable</returns>
        public DataTable Query(string sql, params MySqlParameter[] parameters)
        {
            using (conn)
            {
                conn.Open();
                using (command = new MySqlCommand(sql, conn))
                {
                    command.Parameters.AddRange(parameters);
                    using (reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
            }
        }

        /// <summary>
        /// 异步获取数据集
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="tablename">数据表名称</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns>异步获取的数据集</returns>
        public async Task<DataSet> GetDataSetAsync(string sql, string tablename, params MySqlParameter[] parameters)
        {
            using (conn)
            {
                await conn.OpenAsync();
                using (command = new MySqlCommand(sql, conn))
                {
                    command.Parameters.AddRange(parameters);
                    DataSet dataset = new DataSet();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        await adapter.FillAsync(dataset, tablename);
                    }
                    return dataset;
                }
            }
        }

        /// <summary>
        /// 执行SQL事务，支持批量执行多条SQL语句
        /// </summary>
        /// <param name="SQLStringList">SQL语句列表</param>
        /// <returns>事务执行是否成功</returns>
        public bool ExecuteSqlTran(List<string> SQLStringList)
        {
            bool flag = false;
            using (conn)
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    MySqlTransaction tran = conn.BeginTransaction();
                    cmd.Transaction = tran;
                    try
                    {
                        foreach (string strsql in SQLStringList)
                        {
                            if (!string.IsNullOrWhiteSpace(strsql))
                            {
                                cmd.CommandText = strsql;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        tran.Commit();
                        flag = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error in ExecuteSqlTran: {ex.Message}");
                        tran.Rollback();
                    }
                }
            }
            return flag;
        }
    }
}
