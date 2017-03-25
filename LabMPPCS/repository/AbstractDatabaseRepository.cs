using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabMppCsharp.utils;
using LabMppCsharp.utils.exceptions;
using LabMPPCS.domain;
using LabMPPCS.Validator;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace LabMPPCS.repository
{
    public abstract class AbstractDatabaseRepository<T,ID> : IDatabaseRepository<T,ID> where T:IHasId<ID>
    {
        protected readonly DatabaseConnectionManager _databaseConnectionManager;
        protected readonly string _tableName;
        protected IValidator<T> _validator;
        protected AbstractDatabaseRepository(DatabaseConnectionManager databaseConnectionManager, string tableName,IValidator<T> validator )
        {
            _databaseConnectionManager = databaseConnectionManager;
            _tableName = tableName;
            _validator = validator;
        }

        /// <summary>
        /// Retun all elements.
        /// If any error occur, RepositoryException will be throw
        /// </summary>
        /// <returns></returns>
        public IList<T> GetAll()
        {
            IList<T> list = new List<T>();
            var con = _databaseConnectionManager.GetConnection();
        
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = $"SELECT * FROM `{_tableName}`";
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var element = ToObject(dataR);
                        list.Add(element);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Clear all elements
        /// Throw RepositoryException if operation wil be unsuccesfull
        /// </summary>
        public void Clear()
        {
            try
            {
                var con = _databaseConnectionManager.GetConnection();
                using (var comm = con.CreateCommand())
                {
                    comm.CommandText = $"DELETE FROM `{_tableName}`";
                    comm.ExecuteNonQuery();
                }
            }

            catch (MySqlException e)
            {
                CodeThrowExceptionStatement(e);
            }
        }

        /// <summary>
        /// Add a valid item
        /// Throw RepositoryException else
        /// </summary>
        /// <param name="item"></param>
        public virtual void Add(T item)
        {
            _validator.Validate(item);
            try
            {
         
                var con = _databaseConnectionManager.GetConnection();

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
                }
            }
            catch (MySqlException e)
            {
                CodeThrowExceptionStatement(e);
            }
        }   

        /// <summary>
        /// update item from collections with given params id id this exist and item is valid
        /// throw RepositoryException else
        /// </summary>
        /// <param name="item"></param>
        public void Update(T item)
        {
            _validator.Validate(item);
            try
            {
                var con = _databaseConnectionManager.GetConnection();
                using (var com = con.CreateCommand())
                {
                    string setCommand = "";
                    setCommand = $"`{GetIdName()}` = {item.Id}";
                    Dictionary<string, string> map = ToMap(item);
                    map.Remove(GetIdName());
                    foreach (var key in map.Keys)
                    {
                        setCommand += $",`{key}` = {map[key]}";
                    }
                    com.CommandText =
                        $"UPDATE `{_tableName}` SET {setCommand} WHERE `{GetIdName()}` = {item.Id}";
                    com.ExecuteNonQuery();
                }
            }
            catch (MySqlException e)
            {
                CodeThrowExceptionStatement(e);
            }
        }

        /// <summary>
        /// Delete the items with Id = "id", if it exist
        /// Throw RepositoryException else
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deleted item</returns>
        public T Delete(ID id)
        {
            T el = this.FindById(id);
            try
            {
                if (null != el)
                {
                    var con = _databaseConnectionManager.GetConnection();
                    using (var com = con.CreateCommand())
                    {
                        com.CommandText = $"DELETE FROM `{_tableName}` WHERE `{GetIdName()}` = {id}";
                        com.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException e)
            {
                CodeThrowExceptionStatement(e);
            }
            return el;
        }

        /// <summary>
        /// Find a element by id
        /// Throw RepositoryException if there don't exist 1 item with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Searched item by id</returns>
        public T FindById(ID id)
        {
            T el = default(T);
            try
            {
                var con = _databaseConnectionManager.GetConnection();
                using (var comm = con.CreateCommand())
                {
                    comm.CommandText = $"SELECT * FROM `{_tableName}` WHERE `{GetIdName()}` = {"'" + id.ToString() + "'"}";
                    using (var result = comm.ExecuteReader())
                    {
                        if (result.HasRows)
                        {
                            result.Read();
                            el = ToObject(result);
          
                        }
                        else throw new RepositoryException($"{GetIdName()} not found!");
                    }
                }
            }
            catch (MySqlException e)
            {
                CodeThrowExceptionStatement(e);
            }
            return el;
        }

        public IList<T> GetAllByProperties(Dictionary<string, string> dictionary)
        {
            IList<T> list = new List<T>();
            var con = _databaseConnectionManager.GetConnection();
            List<String> conditionList = new List<string>(dictionary.Count);
            String condition = "";

            foreach (var pair in dictionary)
            {
                conditionList.Add("`" + pair.Key + "`=" + pair.Value);
            }
            condition = String.Join(",", conditionList);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = $"SELECT * FROM `{_tableName}` WHERE {condition}";
                
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var element = ToObject(dataR);
                        list.Add(element);
                    }
                }
            }

            return list;
        }
    

        /// <summary>
        /// Throw RepositoryException(e) where e : Exception
        /// </summary>
        /// <param name="e"></param>
        protected void CodeThrowExceptionStatement(Exception e)
        {
            throw new RepositoryException(e);
        }

        protected abstract String GetIdName();
        protected abstract Dictionary<String, String> ToMap(T item);
        protected abstract T ToObject(MySqlDataReader reader);
        public int Size()
        {
            Int32 size = -1;
            try
            {
                var con = _databaseConnectionManager.GetConnection();
                using (var comm = con.CreateCommand())
                {
                    comm.CommandText = $"SELECT COUNT(*) FROM `{_tableName}`";
                    size = Int32.Parse(comm.ExecuteScalar().ToString());
                    return size;
                }
            }
            catch (MySqlException e)
            {
                CodeThrowExceptionStatement(e);
            }
            return size;
        }


    }
}
