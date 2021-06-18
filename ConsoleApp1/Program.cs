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
        //Initialize IdataMaster and connect to database 
        IDataMasterProvider master =
            new MySqlDataMasterProvider("server=localhost;database=politerm;user=root;password=root");
        
        //Get Data from table `product`
            Table table = master.Select("product");
        
            // Initialize IdataMaster another databse
            IDataMasterProvider ms =
                new MySqlDataMasterProvider("server=localhost;database=data2;user=root;password=root");
            
        //Insert data into table `product` in another database
            ms.IncludeToTable("product", table);
            
            
            
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