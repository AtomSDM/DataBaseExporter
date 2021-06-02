
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using ConsoleApp1.DataMaster;
using ConsoleApp1.DataMaster.MySQL;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySql.Data.MySqlClient;
using Table = Microsoft.EntityFrameworkCore.Metadata.Internal.Table;


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

            IDataMasterProvider master =
                new MySQLDataMasterProvider("server=localhost;database=politerm;user=root;password=root");

            var table = master.Select("product");

            Dictionary<string, string> asoc = new Dictionary<string, string>();
            
            asoc.Add("id","product_id");
            asoc.Add("article", "model");
            asoc.Add("sss", "sku");
            asoc.Add("kartinka", "image");
            asoc.Add("cena", "price");
            asoc.Add("hz", "shipping");
            asoc.Add("denyochek", "date_available");
            
            master.ImportFromList("test",table,asoc);
            
         }
    }
}   