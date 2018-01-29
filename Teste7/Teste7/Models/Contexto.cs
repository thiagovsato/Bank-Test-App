using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Teste7.Models
{
    public class Contexto : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Contexto() : base("name=Contexto")
        {
        }

        public System.Data.Entity.DbSet<Teste7.Models.Cliente> Clientes { get; set; }

        public System.Data.Entity.DbSet<Teste7.Models.Deposito> Depositoes { get; set; }

        public System.Data.Entity.DbSet<Teste7.Models.Saque> Saques { get; set; }

        public System.Data.Entity.DbSet<Teste7.Models.Transferencia> Transferencias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transferencia>()
                .HasRequired(c => c.ClienteRecebedor)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transferencia>()
                .HasRequired(c => c.ClientePagador)
                .WithMany()
                .WillCascadeOnDelete(false);
           
        }
    }
}
