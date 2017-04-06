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

            //var rdrd = ExecuteReader(ProductOption.ToString());
            //while (rdrd.Read())
            //{
            //    ProductOptions.Items.Add(MapOption(rdrd));
            //}


            //modelBuilder.Entity<Models.Product>()
            //    .HasMany(e => e.ProductOptions)
            //    .WithRequired(e => e.Product)
            //    .WillCascadeOnDelete(false);
        }


        public SqlDataReader ExecuteReader(String cmdString)
        {
            cmdString = cmdString.Replace("\r\n", "");

            var conn = Helpers.NewConnection();
            var cmd = new SqlCommand("$"+cmdString, conn);
            conn.Open();

            return cmd.ExecuteReader();
        }

        public void ExecuteNonQuery(String cmdString)
        {
            var conn = Helpers.NewConnection();
            var cmd = new SqlCommand(cmdString, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        protected ProductOption MapOption(SqlDataReader rdr)
        {
            return new ProductOption
            {
                Id = Guid.Parse(rdr["Id"].ToString()),
                ProductId = Guid.Parse(rdr["ProductId"].ToString()),
                Name = rdr["Name"].ToString(),
                Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString()
            };
        }


    }
}
