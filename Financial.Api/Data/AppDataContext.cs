using Financial.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Financial.Api.Data
{
    public class AppDataContext : DbContext
    {

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;


        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.ApplyConfiguration(new CategoryMapping());
            modelBuilder.ApplyConfiguration(new TransactionMapping());*/

            //Varre todas as classes que implementão IEntityTypeConfiguration e cria a base, se tem uma que não pode ser criada, usar metodo comentado acima
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
