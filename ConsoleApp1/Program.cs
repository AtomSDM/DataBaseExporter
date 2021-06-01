
using System;
using System.Collections.Generic;
using System.Data.Common;
using ConsoleApp1.DataMaster;
using ConsoleApp1.DataMaster.MySQL;
using MySql.Data.MySqlClient;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //выборку в базе данных с таблицы product
            //создаем массив типа productList['model']
            
            //выборку в базе данных с таблицы product_description by product_id
            //присоединяем все к списку productList['description'] = product_descriptionList['name']
            //

            IDataMasterProvider master = new MySQLDataMasterProvider("server=localhost;database=politerm;user=root;password=root");

            var table = master.Select("product");

            Console.WriteLine(table[0]["image"]);

            Dictionary<string, string> asoc = new Dictionary<string, string>();
            
            asoc.Add("article", "model");
            asoc.Add("image", "image");
            asoc.Add("cena", "price");

            master.ImportFromList("test",table,asoc);

        }
    }
}   