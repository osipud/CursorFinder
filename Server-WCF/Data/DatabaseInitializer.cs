using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_WCF.Data
{
    public class Base
    {
    }

    public class DatabaseInitializer : SqliteDropCreateDatabaseAlways<DatabaseContext>
    {
        public DatabaseInitializer(DbModelBuilder modelBuilder) : base(modelBuilder) { }


        protected override void Seed(DatabaseContext context)
        {

        }

    }


}
