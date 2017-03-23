using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabMppCsharp.utils;
using LabMppCsharp.utils.exceptions;
using LabMPPCS.domain;
using MySql.Data.MySqlClient;

namespace LabMPPCS.repository
{
    abstract class IdAbstractDatabaseRepository<T> : AbstractDatabaseRepository<T,Int32> where T : IHasId<Int32>
    {
        protected IdAbstractDatabaseRepository(DatabaseConnectionManager databaseConnectionManager, string tableName) : base(databaseConnectionManager, tableName)
        {
        }

        public Int32 AddId(T item)
        {
            Int32 id = -1;
            MySqlTransaction transaction = null;
            try
            {
                var con = _databaseConnectionManager.GetConnection();
                transaction = con.BeginTransaction();
                {
                    using (var comm = con.CreateCommand())
                    {
                        var map = ToMap(item);
                        var columns = "`" + GetIdName() + "`";
                        var values = map[GetIdName()];
                        map.Remove(GetIdName());

                        foreach (var key in map.Keys)
                        {
                            columns += ",`" + key + "`";
                            values += "," + map[key];
                        }

                        comm.CommandText = $"INSERT INTO `{_tableName}` ({columns}) VALUES ({values})";

                        var result = comm.ExecuteNonQuery();
                        if (result == 0)
                            throw new RepositoryException($"No item added !");
                        comm.CommandText = "SELECT LAST_INSERT_ID()";
                        id = comm.ExecuteScalar() is int ? (int) comm.ExecuteScalar() : 0;
                        if (result == 0)
                            throw new RepositoryException($"No item added !");
                    }
                }
            }
                
            catch (MySqlException e)
            {
                try
                {
                    transaction?.Rollback();
                }
                catch (ArgumentException e1)
                {
                    CodeThrowExceptionStatement(new RepositoryException(e.Message + e1.Message));
                }
                CodeThrowExceptionStatement(e);
            }
            return id;
        }
    }
}
