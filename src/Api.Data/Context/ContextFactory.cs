using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            //usado para criar as migrações.
            var connectionString = "Server=localhost;Port=3306;DataBase=dbAPI2;Uid=root;Pwd=mudar@123";
           // var connectionString = "Data Source=NBPITANG-169\\SQLEXPRESS;Initial Catalog=dbApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var optionBuilder = new DbContextOptionsBuilder<MyContext>();
            optionBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8,0,21)),
               mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend));
            //optionBuilder.UseSqlServer(connectionString);

            return new MyContext(optionBuilder.Options);
        }
    }
}
