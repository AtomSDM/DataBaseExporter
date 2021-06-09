using System.Collections.Generic;
using System.Data.Common;

namespace ConsoleApp1.DataMaster
{
    public interface IDataMasterProvider
    {
        public Table Select(string tableName);
        public Table Select(string tableName, string[] rowToSelect);
        public string ExportToString(string tableName, Table table);
        public void IncludeToTable(string tableName, Table table);
        public IEnumerable<StructElement> GetStruct();
        public IEnumerable<DataColumn> GetDataColumn(TableStruct struc);
    }
}