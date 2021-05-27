using System;
using System.Collections.Generic;
using ConsoleApp1.models;
using MySql.Data.MySqlClient;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            AtomMySqlMaster OldDb =
                new AtomMySqlMaster
                (
                    new MySqlConnection("server=localhost;database=data2;user=root;password=root")
                );
            
            OldDb.SetTable("product");

            List<string> prod = OldDb.GetExportList(100);

            OldDb.SetTable("product_description");
            
            List<string> prodDesc = OldDb.GetExportList(100);

        }
        
    }
}   