namespace refactor_me.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using refactor_me.Models;
    using System.Data.SqlClient;
    using System.Collections.Generic;
    using System.Data;

    public partial class ProductContext : DbContext
    {
        public ProductContext()
            : base("DBConnection")
        {
        }
        
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductOption> ProductOptions { get; set; }
   
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Product>()
                .HasMany(e => e.ProductOptions)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);
        }
    }
}
