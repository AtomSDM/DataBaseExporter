using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace ConsoleApp1.models
{
    public class AtomMySqlMaster
    {
        private MySqlConnection _connector; private DbDataReader _reader;
        public string CurentTable { get; private set; } = null;
        public List<string> CurentTableSchem  = new List<string>();
        
        public AtomMySqlMaster(MySqlConnection connector)
        {
            _connector = connector;
            _connector.Open();
        }

        public DbDataReader GetReaderBySqlExpresion(string cmd)
        {
            DbDataReader reader;
            try
            {
                if (_connector.State == ConnectionState.Open)
                {
                    reader = new MySqlCommand(cmd, _connector).ExecuteReader();
                }
                else
                {
                    _connector.Open();
                    reader = new MySqlCommand(cmd, _connector).ExecuteReader();
                    _connector.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                reader = null;
                throw;
            }

            return reader;
        }

        public void SetTable(string tableName)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM `{tableName}` ", _connector);
               
                
                _reader = cmd.ExecuteReader(); 
                
                for (int i = 0; i < _reader.FieldCount; i++)
                {
                    CurentTableSchem.Add(_reader.GetName(i));;
                }
                
                CurentTable = tableName;              
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw; 
            } 
            
        }
        
        public void SetTable(string tableName, List<string> rows)
                {
                    try
                    {
                        MySqlCommand cmd = new MySqlCommand($"SELECT {ListToString(rows)} FROM `{tableName}` ", _connector);
                        
                        _reader = cmd.ExecuteReader(); 
                        
                        for (int i = 0; i < _reader.FieldCount; i++)
                        {
                            CurentTableSchem.Add(_reader.GetName(i));;
                        }
                        
                        CurentTable = tableName; 
                        
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw; 
                    } 
                    
                }

        public List<string> GetExportList(int lim)
        {
            List<string> queryList;
            queryList = new();

            string columnListString = ListToString(CurentTableSchem);

            string templateExportString = $"INSERT INTO `product` ({columnListString}) VALUES ";

            string exportString = templateExportString;

            int counter = 0;
            
            while (_reader.Read())
            {
                string dataString = "(";

                for (int i = 0; i < _reader.FieldCount; i++)
                {
                    switch (_reader[i])
                    {
                        case Int32 result:
                            dataString += $"'{result}', ";
                            break;
                        case Decimal result:
                            //Для того что бы дробь отображалась с точкой а не запятой, по другому SQL не понимает
                            //
                            //https://docs.microsoft.com/en-us/dotnet/api/system.globalization.numberformatinfo?view=net-5.0
                            //
                            dataString += $"'{result.ToString(new CultureInfo("en-US", false).NumberFormat)}', ";
                            break;
                        case string result:
                            dataString += $"'{result}', ";
                            break;
                        case bool result:
                            dataString += result ? "'1', " : "'0', ";
                            break;
                        case DateTime result:
                            //Для того что бы дата выглядела подобающим образом
                            dataString += $"'{result:yyyy-MM-dd HH:mm:ss.fff}', ";
                            break;
                    }
                }

                exportString += dataString.Substring(0, dataString.Length - 2) + "),";

                counter++;
                
                if (counter >= lim)
                {
                    exportString = exportString.Substring(0, exportString.Length - 1) + ";";
                    queryList.Add(exportString);
                    exportString = templateExportString;
                    counter = 0;
                }
                
            }
            
            if (counter > 0)
            {
                exportString = exportString.Substring(0, exportString.Length - 1) + ";";
                queryList.Add(exportString);
            }

            return queryList;
        }

        private string ListToString(List<string> list)
        {
            string str = "";
            
            foreach (string element in list)
            {
               str += "`"+element + "`, ";    
            }

            str = str.Substring(0, str.Length - 2);

            return str;
        }

        private void run()
        {
            AtomMySqlMaster OldDb =
                new AtomMySqlMaster
                (
                   new MySqlConnection("server=localhost;database=data2;user=root;password=root") 
                );
            
            OldDb.SetTable("product");

            //List<string> prod = OldDb.GetExportList(100, );

            OldDb.SetTable("product_description");
            
            List<string> prodDesc = OldDb.GetExportList(100);
        }
    }
    
    
}