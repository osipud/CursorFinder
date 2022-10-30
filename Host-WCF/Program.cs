using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SQLite;
using Server_WCF;
using System.Runtime.CompilerServices;
using Server_WCF.Data;
using System.Data.Entity.Migrations;
using System.Runtime.Remoting.Contexts;
using Server_WCF.Migrations;


namespace Host_WCF
{
    internal class Program
    {    /// <summary>
         /// Запускать от имени Администратора
         /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine($"SERVER - Дата запуска: {DateTime.Now}");

            using (var host = new ServiceHost(typeof(Server_WCF.Service)))
            {
                host.Open();
                Console.WriteLine("SERVER - Хост запущен!");
                Console.ReadLine();
             
            }
        }
    }
}
