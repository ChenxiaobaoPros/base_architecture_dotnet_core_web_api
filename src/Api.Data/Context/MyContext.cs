using System;
using Api.Data.Mapping;
using Api.Data.Seeds;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public MyContext(DbContextOptions<MyContext> options): base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity> (new UserMap().Configure);

            modelBuilder.Entity<UfEntity> (new UfMap().Configure);
            modelBuilder.Entity<MunicipioEntity> (new MunicipioMap().Configure);
            modelBuilder.Entity<CepEntity> (new CepMap().Configure);

            //Se eu quiser ter um usuário padrão quando criar o banco
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Administrador",
                    Email = "edsongomes1@gmail.com",
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                }
            );

            UfSeeds.Ufs(modelBuilder);
           


        }
    }

}
