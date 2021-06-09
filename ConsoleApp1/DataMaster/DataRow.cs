using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ConsoleApp1.DataMaster
{
    public class DataRow
    {
        private readonly Table _table;
        private readonly List<DataColumn> _dataColumns;

        public DataRow(Table table, List<DataColumn> dataColumns)
        {
            _table = table;
            _dataColumns = dataColumns;
        }
        public DataColumn this[int id] => _dataColumns[id];

        public int Count() => _dataColumns.Count;
        
        public override string ToString()
        {
            return string.Join(", ", _dataColumns.Count);
        }
        
    }
}