using System;
using System.Collections.Generic;
using SellTicketsModel.entity;
using persistence.utils;
using MySql.Data.MySqlClient;
using persistence.utils.validator;
using SellTicketsModel.exception;

namespace persistence.repository
{
    public abstract class IdAbstractDatabaseRepository<T> : AbstractDatabaseRepository<T, Int32> where T : IHasId<Int32>
    {
        protected IdAbstractDatabaseRepository(DatabaseConnectionManager databaseConnectionManager, string tableName, IValidator<T> validator) : base(databaseConnectionManager, tableName, validator)
        {
        }

        /// <summary>
        /// Add an item with auto-generate id and return the id.
        /// Throw RepositoryException if the add operation fail.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>the id of added item</returns>
        public Int32 AddId(T item)
        {
            _validator.Validate(item);
            Int32 id = -1;
            MySqlTransaction transaction = null;
            List<String> paramsList = new List<string>();
            List<String> valuesList = new List<string>();
            try
            {
                var con = _databaseConnectionManager.GetConnection();
                transaction = con.BeginTransaction();
                {
                    using (var comm = con.CreateCommand())
                    {
                        var map = ToMap(item);
                        map.Remove(GetIdName());

                        foreach (var key in map.Keys)
                        {
                            paramsList.Add("`" + key + "`");
                            valuesList.Add(map[key]);
                        }

                        comm.CommandText =
                            $"INSERT INTO `{_tableName}` ({String.Join(",", paramsList)}) VALUES ({String.Join(",", valuesList)})";

                        var result = comm.ExecuteNonQuery();
                        if (result == 0)
                            throw new RepositoryException($"No item added !");
                        comm.CommandText = "SELECT LAST_INSERT_ID()";
                        id = comm.ExecuteScalar() is int ? (int)comm.ExecuteScalar() : 0;
                        if (result == 0)
                            throw new RepositoryException($"No item added !");
                        transaction.Commit();
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
            finally
            {
                transaction?.Dispose();
            }
            return id;
        }
    }
}
