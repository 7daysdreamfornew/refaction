using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace refactor_me.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        public virtual ICollection<ProductOption> ProductOptions { get; set; }

        public Product()
        {
            this.ProductOptions = new HashSet<ProductOption>();
        }

        public Product(Guid id)
        {
            Id = id;
        }

        public Product(string name)
        {
            Name = name;
        }

        public void Save()
        {
            SqlDataReader sqlReader = null;
            SqlConnection conn = Helpers.NewConnection();

            try
            {

                SqlCommand sqlDBCommand = new SqlCommand("Product_Save", conn);
                sqlDBCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter NameParam = sqlDBCommand.Parameters.Add("@Name", SqlDbType.VarChar);
                NameParam.Value = this.Name;

                SqlParameter DescriptionParam = sqlDBCommand.Parameters.Add("@Description", SqlDbType.VarChar);
                DescriptionParam.Value = this.Description;

                SqlParameter PriceParam = sqlDBCommand.Parameters.Add("@Price", SqlDbType.Decimal);
                PriceParam.Value = this.Price;

                SqlParameter DeliveryPriceParam = sqlDBCommand.Parameters.Add("@DeliveryPrice", SqlDbType.Decimal);
                DeliveryPriceParam.Value = this.DeliveryPrice;

                conn.Open();

                sqlDBCommand.ExecuteNonQuery();

                conn.Close();


            }
            catch (Exception ex)
            {

                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
                conn.Close();
                throw ex;
            }
        }

        public void Delete()
        {
            SqlConnection conn = Helpers.NewConnection();

            SqlCommand sqlDBCommand = new SqlCommand("Product_Delete", conn);
            sqlDBCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter IdParam = sqlDBCommand.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
            IdParam.Value = this.Id;

            conn.Open();

            sqlDBCommand.ExecuteNonQuery();

            conn.Close();
        }
    }
}