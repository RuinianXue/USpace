using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace UIDisplay.Myscripts
{
    internal class MysqlBase
    {
        private MySqlConnection conn = null;
        private MySqlCommand command = null;
        private MySqlDataReader reader = null;

        /// <summary>
        /// 构造方法里建议连接
        /// </summary>
        /// <param name="connstr"></param>
        public MysqlBase()
        {
            string connStr = $"Database={Settings.DatbaseName};Data Source={Settings.DatabaseHost};User Id={Settings.DatabaseUsername};Password={Settings.DatabasePassword};pooling=false;CharSet=utf8;port={Settings.DatabasePort};";
            conn = new MySqlConnection(connStr);
        }

        public void Dispose()
        {
            conn?.Dispose();
            command?.Dispose();
            reader?.Dispose();
        }

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
                conn.Close();
            }
        }

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
                }
            }
            catch (MySqlException ex)
            {
                // 记录异常信息，可以根据需要进行扩展
                Console.WriteLine($"Error in CommonExecute: {ex.Message}");
            }
            return res;
        }

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

        public DataSet GetDataSet(string sql, string tablename, params MySqlParameter[] parameters)
        {
            using (conn)
            {
                conn.Open();
                using (command = new MySqlCommand(sql, conn))
                {
                    command.Parameters.AddRange(parameters);
                    DataSet dataset = new DataSet();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataset, tablename);
                    }
                    return dataset;
                }
            }
        }

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

        /*/// <summary>
        /// 检测数据库连接状态
        /// </summary>
        /// <returns></returns>
        public bool CheckConnectStatus()
        {
            bool result = false;
            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    result = true;
                }

            }
            catch
            {
                result = false;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        /// <summary>
        /// 增、删、改公共方法
        /// </summary>
        /// <returns></returns>
        public int commonExecute(string sql)
        {
            int res = -1;
            try
            {
                using (conn)
                {
                    conn.Open();
                    command = new MySqlCommand(sql, conn);
                    res = command.ExecuteNonQuery();
                }
            }
            catch (MySqlException)
            {
                Console.WriteLine($"Error in CommonExecute: {ex.Message}");
            }
            return res;
        }
        /// <summary>
        /// 查询方法
        /// 注意：尽量不要用select * from table表（返回的数据过长时，DataTable可能会出错），最好指定要查询的字段。
        /// </summary>
        /// <returns></returns>
        public DataTable query(string sql)
        {
            using (conn)
            {
                conn.Open();
                command = new MySqlCommand(sql, conn);
                DataTable dt = new DataTable();
                using (reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    dt.Load(reader);
                }
                return dt;
            }
        }
        /// <summary>
        /// 获取DataSet数据集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql, string tablename)
        {
            using (conn)
            {
                conn.Open();
                command = new MySqlCommand(sql, conn);
                DataSet dataset = new DataSet();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dataset, tablename);
                conn.Close();
                return dataset;
            }
        }
        /// <summary>
        /// 实现多SQL语句执行的数据库事务
        /// </summary>
        /// <param name="SQLStringList">SQL语句集合（多条语句）</param>
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
        }*/
    }
}
