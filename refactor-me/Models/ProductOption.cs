using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace refactor_me.Models
{
    public class ProductOption
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        //[JsonIgnore]
        //public bool IsNew { get; }


        public virtual Product Product { get; set; }

        public ProductOption()
        {

        }

        public ProductOption(Guid id)
        {
            Id = id;
        }

        public void Save()
        {
            SqlConnection conn = Helpers.NewConnection();

            SqlCommand sqlDBCommand = new SqlCommand("Product_Save", conn);
            sqlDBCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter NameParam = sqlDBCommand.Parameters.Add("@Name", SqlDbType.VarChar);
            NameParam.Value = this.Name;

            SqlParameter DescriptionParam = sqlDBCommand.Parameters.Add("@Description", SqlDbType.VarChar);
            DescriptionParam.Value = this.Description;

            conn.Open();

            sqlDBCommand.ExecuteNonQuery();

            conn.Close();
        }

        public void Delete()
        {
            SqlConnection conn = Helpers.NewConnection();

            SqlCommand sqlDBCommand = new SqlCommand("ProductOption_Delete", conn);
            sqlDBCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter IdParam = sqlDBCommand.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
            IdParam.Value = this.Id;

            conn.Open();

            sqlDBCommand.ExecuteNonQuery();

            conn.Close();
        }
    }
}