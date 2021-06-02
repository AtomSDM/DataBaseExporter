using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;

namespace ConsoleApp1.Extensions
{
    public static  class ReaderExtensionsw
    {
        public static List<Dictionary<string,dynamic>> ToList(this DbDataReader reader)
        {
            //ToDo: Реализовать зависимость от типов из таблицы базы данных
            // как вариант типизацию можно реализовать через динамический тип языка C# но это как вариант. Это позволит разделить на типы сейчас так как их использует .NET,
            // а пото в реализации каждой из вариантом експорта отображение типов настраивать локально.
            //
            
            var DataTable = new List<Dictionary<string,dynamic>>();

            var tableStruct = new List<string>();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                tableStruct.Add(reader.GetName(i));
            }

            while (reader.Read())
            {
                Dictionary<string, dynamic> RowDict = new Dictionary<string, dynamic>();

                int c = 0;
                
                foreach (var element in tableStruct)
                {
                    RowDict.Add(element, reader[c]);
                    c++;
                }
                
                DataTable.Add(RowDict);
            }
            
            
            return DataTable;
        }

        public static IEnumerable<string> MutateToSQLImportContains(this Dictionary<string,dynamic> dict, Dictionary<string,string> asoc)
        {
            foreach (var obj in asoc.Values)
            {
                switch (dict[obj])
                {
                    case Int32 result:
                        yield return $"'{result}'";
                        break;
                    case Decimal result:
                        yield return $"'{result.ToString(new CultureInfo("en-US", false).NumberFormat)}'";
                        break;
                    case string result:
                        yield return $"'{result}'";
                        break;
                    case bool result:
                        yield return result ? "'1'" : "'0'";
                        break;
                    case DateTime result:
                        yield return $"'{result:yyyy-MM-dd HH:mm:ss.fff}'";
                        break;
                }
                
            }
        }
    }
}