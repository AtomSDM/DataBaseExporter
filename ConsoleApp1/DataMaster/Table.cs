using System.Data;
using System.Data.Common;
using System.Linq;

namespace ConsoleApp1.DataMaster
{
    public class Table
    {
        public TableStruct Structure { get; }
        public DataRow DataRows { get; }
        
        private IDataMasterProvider _dataMasterProvider;
        
        public Table(IDataMasterProvider dataMasterProvider)
        {
            _dataMasterProvider = dataMasterProvider;
            
            Structure = new TableStruct(dataMasterProvider.GetStruct().ToList());

            DataRows = new DataRow(this, dataMasterProvider.GetDataColumn(Structure).ToList());
        }

    }
}