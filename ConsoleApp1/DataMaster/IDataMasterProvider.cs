using System.Collections.Generic;

namespace ConsoleApp1.DataMaster
{
    public interface IDataMasterProvider
    {
        public List<Dictionary<string, dynamic>> Select(string tableName);
        public List<Dictionary<string, dynamic>> Select(string tableName, string srowToSelect);
        //public List<Dictionary<string, string>> Select(string tableName, string[] rowToSelect, string where);
        //public List<Dictionary<string, string>> Select(string tableName, string[] rowToSelect, string where = " ", int limitStart = 0, int limit = 0, string orderBy = "");
        public int ImportFromList(string tableName, List<Dictionary<string,string>> dataList);
        public int ImportFromList(string tableName, List<Dictionary<string,dynamic>> dataList, Dictionary<string,string> listAsotiation);
    }
}