using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1.DataMaster
{
    public interface ITableMaster : IEnumerable, IEnumerator
    {
        List<Dictionary<string, string>> Data();
        
        
    }
}