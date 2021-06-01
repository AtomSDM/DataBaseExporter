using System.Collections.Generic;
using System.Data.Common;

namespace ConsoleApp1.Extensions
{
    public static  class ReaderExtensionsw
    {
        public static List<Dictionary<string,string>> ToList(this DbDataReader reader)
        {
            //ToDo: Реализовать зависимость от типов из таблицы базы данных
            // как вариант типизацию можно реализовать через динамический тип языка C# но это как вариант. Это позволит разделить на типы сейчас так как их использует .NET,
            // а пото в реализации каждой из вариантом експорта отображение типов настраивать локально.
            //
            
            var DataTable = new List<Dictionary<string,string>>();

            var tableStruct = new List<string>();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                tableStruct.Add(reader.GetName(i));
            }

            while (reader.Read())
            {
                Dictionary<string, string> RowDict = new Dictionary<string, string>();

                int c = 0;
                
                foreach (var element in tableStruct)
                {
                    RowDict.Add(element, reader[c].ToString());
                    c++;
                }
                
                DataTable.Add(RowDict);
            }
            
            
            return DataTable;
        }
    }
}