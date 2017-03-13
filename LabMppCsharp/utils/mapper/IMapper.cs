using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LabMppCsharp.utils.mapper
{
    public interface IMapper<T>
    {
        String GetIdPrototype();
        T ToObject(MySqlDataReader ds);
        Dictionary<string, string> ToMap(T el);
        string getEntityName();
    }
}
