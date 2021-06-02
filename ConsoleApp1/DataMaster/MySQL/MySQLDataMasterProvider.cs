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
        
        public List<Dictionary<string, dynamic>> Select(string tableName)
        {
            DbDataReader reader;

            try
            {
                reader = new MySqlCommand($"SELECT * FROM `{tableName}` LIMIT 10", _connection).ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return reader.ToList();
        }

        public List<Dictionary<string, dynamic>> Select(string tableName, string rowToSelect)
        {
            DbDataReader reader;

            try
            {
                reader = new MySqlCommand($"SELECT {rowToSelect} FROM `{tableName}` LIMIT 10", _connection).ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return reader.ToList();
        }

        public int ImportFromList(string tableName, List<Dictionary<string, string>> dataList)
        {
            throw new NotImplementedException();
        }

        ///<summary>
        ///Этот метод импортирует данные в уже готовую таблицу, с настройкой асоциаций между столбцами
        ///</summary>
        ///<returns>
        ///Код результата запроса. 0 - без ошибок, 1+ с ошибкой.
        ///</returns>
        ///<param name="tableName">Название табллицы в которую производится импорт</param>
        ///<param name="dataList">Данные которые нужно Импортировать в таблицу</param>
        ///<param name="listAsotiation">Словарь асоциаций между таблицами</param>
        public int ImportFromList(string tableName, List<Dictionary<string, dynamic>> dataList,
            Dictionary<string, string> listAsotiation)
        {
            //ToDo: Это нада будет как-то оптимизировать

            try
            {
                if (_connection.State == ConnectionState.Open) _connection.Close();

                _connection.Open();

                //Построение строки стобцов
                Console.WriteLine(string.Join(",", listAsotiation.Keys));

                List<string> dataImportList = new List<string>();

                foreach (Dictionary<string, dynamic> rows in dataList)
                {
                    dataImportList.Add(string.Format(@"({0})", string.Join(",", rows.MutateToSQLImportContains(listAsotiation))));
                }

                //Команда для запроса в базу данных
                string cmd = string.Format
                (
                    "INSERT INTO `{0}` ({1}) VALUES {2};",
                    tableName, //название таблицы в которую експортируются данные
                    string.Join(",", listAsotiation.Keys), //Столбцы которые будут заполняться
                    string.Join(",", dataImportList) //Данные которые будут импортироваться
                );

                Console.WriteLine(cmd);
                
                //new MySqlCommand(cmd, _connection).ExecuteNonQuery(); //Выполнение команды

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