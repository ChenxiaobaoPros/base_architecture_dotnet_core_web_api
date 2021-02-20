using System;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

using Xunit;
/*
testes unitários com XUnit
é m framework para executar o testes. Precisamos de um atriuto chamado [Fact]
existe outro atributo chamado Skip, para definir que o método de teste está desabilitado 
ou não será executado
Ex.: [Fact(Skip="Teste ainda não finalizado")]
Temos também a possibilidade de usar o Trait, que permite organizar os testes em grupos, 
criando nomes de categorias e atributos
Ex.: [Trait("Crud Usuário", "Adicionar")]

Temos também o Assert, o afirmar, é a parte final dos tests. Onde montamos as comparações dos resultados obtidos
e esperados dos testes.
Assert.True();
Assert.False();
Assert.Equal();
*/
namespace Api.Data.Test
{
    public abstract class BaseTest
    {
        public BaseTest()
        {

        }
    }
    public class DbTeste : IDisposable
    {
        private string dataBaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
        public ServiceProvider ServiceProvider { get; private set; }


        public DbTeste()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<MyContext>(o =>
               o.UseMySql($"Server=localhost;Port=3306;DataBase={dataBaseName};Uid=root;Pwd=mudar@123",
                new MySqlServerVersion(new Version(8, 0, 21)),
                    mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend)),
                ServiceLifetime.Transient
            );

            ServiceProvider = serviceCollection.BuildServiceProvider();
            using (var context = ServiceProvider.GetService<MyContext>())
            {
                //Criando o banco de dados e fazendo as migrações
                context.Database.EnsureCreated();
            }

        }

        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<MyContext>())
            {

                context.Database.EnsureDeleted();
            }
        }
    }
}
