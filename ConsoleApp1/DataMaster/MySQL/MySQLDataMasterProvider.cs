using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Channels;
using ConsoleApp1.DataMaster;
using ConsoleApp1.Extensions;
using MySql.Data.MySqlClient;

namespace ConsoleApp1.DataMaster.MySQL
{
    public class MySQLDataMasterProvider : IDataMasterProvider
    {
        private string _connectionString;
        private MySqlConnection _connection;
        public MySQLDataMasterProvider(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
            _connectionString = connectionString;
            _connection.Open();
        }
        
        public List<Dictionary<string, string>> Select(string tableName)
        {
            DbDataReader reader;
            
            try
            {
                
                reader = new MySqlCommand($"SELECT * FROM `{tableName}` LIMIT 10", _connection).ExecuteReader(); 
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return reader.ToList();
        }

        public List<Dictionary<string, string>> Select(string tableName, string[] rowToSelect)
        {
            throw new System.NotImplementedException();
        }

        public int ImportFromList(string tableName, List<Dictionary<string, string>> dataList)
        {
            throw new NotImplementedException();
        }
        
        public int ImportFromList(string tableName, List<Dictionary<string, string>> dataList, Dictionary<string,string> listAsotiation)
        {
            try
            {
                if(_connection.State == ConnectionState.Open) _connection.Close();
                
                _connection.Open();
                
                string ImpStr = "";
                string tableStruct = "";

                foreach (var element in listAsotiation)
                {
                    tableStruct += element.Key + ", ";
                }

                string dataImportValues = "";

                foreach (var element in dataList)
                {
                    string dis = ""; //data import string
                    
                    foreach (var e in listAsotiation)
                    {
                        dis += "\""+element[e.Value] + "\", ";
                    }

                    dataImportValues += $"({dis.Substring(0, dis.Length - 2)}), ";
                }

                string cmdt = string.Format
                (
                    "INSERT INTO `{0}` ({1}) VALUES {2};",
                    tableName,
                    tableStruct.Substring(0, tableStruct.Length - 2),
                    dataImportValues.Substring(0, dataImportValues.Length - 2)
                );
                
                new MySqlCommand(
                    cmdt,
                    _connection).ExecuteReader();
                _connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                
                return 1;
            }

            return 0;
        }
    }
}