using System;
using System.Collections.Generic;
using ConsoleApp1.DataMaster;
using ConsoleApp1.DataMaster.MySQL;


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
                new MySqlDataMasterProvider("server=localhost;database=politerm;user=root;password=root");

            Table table = master.Select("product");

            //table.GetImportString();
            //table.GetImportStringByLimit();
            
            Console.WriteLine(table.Structure[0].Name);

            //Console.WriteLine(table.DataRows[0].ToString());
            //Console.WriteLine("("+table.DataRows[0].ToString(", ", a => MySqlDataMasterProvider.MysqlTypeFormat(a)) + ") ");

            IDataMasterProvider ms =
                new MySqlDataMasterProvider("server=localhost;database=data2;user=root;password=root");
            
            ms.IncludeToTable("product", table);
            
            
            
            //table.stuc[0]; // return "id"
            //table.data[0]; // return data_row
            //table.data[0]["product_id"]; // return "00012"

            //table.GetImportString(); //return "INSERT * INTO TUDA"
            //table.GetImportStringByAsotiation();
            
            
            
            Dictionary<string, string> asoc = new Dictionary<string, string>();
            
            asoc.Add("id","product_id");
            asoc.Add("article", "model");
            asoc.Add("sss", "sku");
            asoc.Add("kartinka", "image");
            asoc.Add("cena", "price");
            asoc.Add("hz", "shipping");
            asoc.Add("denyochek", "date_available");
            
            //master.ImportFromList("test",table,asoc);
            
         }
    }
}   