using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabMppCsharp.domain;
using LabMppCsharp.utils;
using LabMppCsharp.utils.exceptions;
using LabMppCsharp.utils.mapper;
using MySql.Data.MySqlClient;

namespace LabMppCsharp.repositories
{
    public class DatabaseRepository<T,ID> : IRepository<T,ID> where T:IEntity<ID>
    {
        private readonly DatabaseConnectionManager _databaseConnectionManager;
        private readonly string _tableName;
        private readonly IMapper<T> _mapper;

        public DatabaseRepository(DatabaseConnectionManager databaseConnectionManager, string tableName, IMapper<T> mapper)
        {
            this._databaseConnectionManager = databaseConnectionManager;
            this._tableName = tableName;
            this._mapper = mapper;
        }

        public IList<T> GetAll()
        {
            IList<T> list = new List<T>();
            //            using (var con = _databaseConnectionManager.GetConnection())
            //            {
            var con = _databaseConnectionManager.GetConnection();

            using (var comm = con.CreateCommand())
                {
                    comm.CommandText = $"SELECT * FROM `{_tableName}`";
                    using (var dataR = comm.ExecuteReader())
                    {
                        while (dataR.Read())
                        {
                            var element = _mapper.ToObject(dataR);
                            list.Add(element);
                        }
                    }
                }  
//            }
            return list;
        }

        public T FindById(ID id)
        {
            T el = default(T);
            try
            {
                //                using (var con = _databaseConnectionManager.GetConnection())
                //                {
                var con = _databaseConnectionManager.GetConnection();
                using (var comm = con.CreateCommand())
                {


                    comm.CommandText = $"SELECT * FROM `{_tableName}` WHERE `{_mapper.GetIdPrototype()}` = {id}";

                    using (var result = comm.ExecuteReader())
                    {
                        if (result.HasRows)
                        {
                            result.Read();
                            el = _mapper.ToObject(result);

                        }
                        else throw new RepositoryException($"{_mapper.getEntityName()} not found!");
                    }
                }
            }
            catch (MySqlException e)
            {
                CodeThrowExceptionStatement(e);
            }
            return el;
        }

        public void Update(T element)
        {
            try
            {
                var con = _databaseConnectionManager.GetConnection();
                using (var com = con.CreateCommand())
                {
                    string setCommand = "";
                    setCommand = $"`{_mapper.GetIdPrototype()}` = {element.Id}";
                    Dictionary<string, string> map = _mapper.ToMap(element);
                    map.Remove(_mapper.GetIdPrototype());
                    foreach (var key in map.Keys)
                    {
                        setCommand += $",`{key}` = {map[key]}";
                    }
                    com.CommandText =
                        $"UPDATE `{_tableName}` SET {setCommand} WHERE `{_mapper.GetIdPrototype()}` = {element.Id}";
                    com.ExecuteNonQuery();
                }
            }
            catch (MySqlException e)
            {
                CodeThrowExceptionStatement(e);
            }
        }

        public void Add(T element)
        {
            try
            {
//                using (var con = _databaseConnectionManager.GetConnection())
//                {
                var con = _databaseConnectionManager.GetConnection();
                    using (var comm = con.CreateCommand())
                    {
                        var map = _mapper.ToMap(element);
                        var columns = "`" + _mapper.GetIdPrototype() + "`";
                        var values = map[_mapper.GetIdPrototype()];
                        map.Remove(_mapper.GetIdPrototype());

                        foreach (var key in map.Keys)
                        {
                            columns += ",`" + key + "`";
                            values += "," + map[key];
                        }

                        comm.CommandText = $"INSERT INTO `{_tableName}` ({columns}) VALUES ({values})";

                        var result = comm.ExecuteNonQuery();
                        if (result == 0)
                            throw new RepositoryException($"No {_mapper.getEntityName()} added !");
                    }
//                }
            }
            catch (MySqlException e)
            {
                CodeThrowExceptionStatement(e);
            }
           
        }

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
                        com.CommandText = $"DELETE FROM `{_tableName}` WHERE `{_mapper.GetIdPrototype()}` = {id}";
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

        public Int32 GetSize()
        {
            try
            {
                var con = _databaseConnectionManager.GetConnection();
                using (var comm = con.CreateCommand())
                {
                    comm.CommandText = $"SELECT COUNT(*) FROM `{_tableName}`";
                    Int32 size = Int32.Parse(comm.ExecuteScalar().ToString());
                    return size;
                }
            }
            catch (MySqlException e)
            {
                CodeThrowExceptionStatement(e);
            }
            return Int32.MaxValue;
        }

        public void Clear()
        {
            try
            {
                //                using (var con = _databaseConnectionManager.GetConnection())
                //                {
                var con = _databaseConnectionManager.GetConnection();

                using (var comm = con.CreateCommand())
                    {
                        comm.CommandText = $"DELETE FROM `{_tableName}`";
//                        comm.ExecuteReader();
                        int a = comm.ExecuteNonQuery();
                    }
                }
                //            }

            catch (MySqlException e)
            {
                CodeThrowExceptionStatement(e);
            }
        }

        private void CodeThrowExceptionStatement(Exception e)
        {
            throw new RepositoryException(e);
        }
    }
}
