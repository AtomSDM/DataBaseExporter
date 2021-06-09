using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using ConsoleApp1.Extensions;
using MySql.Data.MySqlClient;

namespace ConsoleApp1.DataMaster.MySQL
{
    public class MySqlDataMasterProvider : IDataMasterProvider
    {
        private string _connectionString;
        private MySqlConnection _connection;
        private DbDataReader _reader;
        
        public MySqlDataMasterProvider(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
            _connectionString = connectionString;
            _connection.Open();
        }
        
        public Table Select(string tableName)
        {
            if(_connection.State == ConnectionState.Closed) _connection.Open();
            
            _reader = new MySqlCommand($"SELECT * FROM `{tableName}` LIMIT 10", _connection).ExecuteReader();
            
            return new Table(this);
        }
        public Table Select(string tableName, string[] rowToSelect)
        {
            if(_connection.State == ConnectionState.Closed) _connection.Open();
            
            _reader = new MySqlCommand($"SELECT  FROM `{tableName}` LIMIT 10", _connection).ExecuteReader();
            
            return new Table(this);
        }
        
        public IEnumerable<StructElement> GetStruct()
        {
            for (var i = 0; i < _reader.FieldCount; i++)
            {
                yield return (new StructElement(i, _reader.GetName(i), _reader.GetFieldType(i)));
            }
        }

        public IEnumerable<DataColumn> GetDataColumn(TableStruct struc)
        {
            while (_reader.Read())
            {
                yield return new DataColumn(struc, GetRow(_reader).ToList());
            }
        }

        public string ExportToString(string tableName, Table table)
        {
            var field = string.Join(", ", table.Structure._elements.Select(a => a.Name));

            List<string> data = new List<string>();

            //ToDo: owerwrite this
            for (int i = 0; i < table.DataRows.Count(); i++) data.Add($"({table.DataRows[i].ToString(", ", MysqlTypeFormat)})");
            
            return $"INSERT INTO `{tableName}` ({field}) VALUES {string.Join(", ", data)};";
        }

        public void IncludeToTable(string tableName, Table table)
        {
            if(_connection.State == ConnectionState.Closed) _connection.Open();
            
            new MySqlCommand(this.ExportToString(tableName, table), _connection).ExecuteNonQuery();

            _connection.Close();
        }
        private IEnumerable<object> GetRow(DbDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                yield return reader[i];
            }
        }
        public static string MysqlTypeFormat(object o)
        {
            switch (o)
            {
                case Int32 result:
                    return $"'{result}'";
                    break;
                case Decimal result:
                    return $"'{result.ToString(new CultureInfo("en-US", false).NumberFormat)}'";
                    break;
                case string result:
                    return $"'{result}'";
                    break;
                case bool result:
                    return result ? "'1'" : "'0'";
                    break;
                case DateTime result:
                    return $"'{result:yyyy-MM-dd HH:mm:ss.fff}'";
                    break;
                default:
                    return $"'{o.ToString()}'"; 
            }
        }
    }
}