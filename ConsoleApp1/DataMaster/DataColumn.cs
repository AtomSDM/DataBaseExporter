using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.DataMaster
{
    public class DataColumn
    {
        private readonly List<object> _data;
        private readonly TableStruct _struct;

        public DataColumn(TableStruct tableStruct, List<object> data) 
        {
            _struct = tableStruct;
            _data = data;
        }

        public dynamic this[string fieldName] => _data[_struct[fieldName].Id];
        public dynamic this[int id] => _data[id];
        
        public string ToString(string separator)
        {
            return string.Join(separator, _data);
        }
        
        public string ToString(string separator, string template)
        {
            return string.Join(separator, _data.Select(a=> string.Format(template, a)));
        }

        public string ToString(string separator, Func<object, string> mutator)
        {
            return string.Join(separator, _data.Select(mutator));
        }
    }
}