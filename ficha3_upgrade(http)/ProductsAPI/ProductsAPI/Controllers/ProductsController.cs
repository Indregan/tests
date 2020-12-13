using ProductsAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductsAPI
{
    public class ProductsController : ApiController
    {
        //        List<Product> products = new List<Product>{
        //
        //            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
        //           new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
        //            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        //    };



        //String connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\malta\Documents\politecnico\5ºano\1ºsemestre\IS\pratica\ficha3_upgrade(http)\ProductsAPI\ProductsAPI\App_Data\DBProds.mdf;Integrated Security = True";

        String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ProductsAPI.Properties.Settings.ConnectionToDB"].ConnectionString;



        // GET: api/Products
        //devolve tudo
/*        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            //return products;
            List<Product> products = new List<Product>();

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand("Select * FROM Prods ORDER BY Id", conn);
                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    Product p = new Product
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Category = reader["Category"] == DBNull.Value ? "" : (string)reader["Category"],
                        Price = (reader["Price"] == DBNull.Value) ? 0 : Convert.ToDecimal(reader["Price"])
                    };

                    products.Add(p);
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
               // throw;
            }
            return products;

        }*/


        // GET: api/Products/5
        //devolve um id especifico

        [Route("api/products/{id:int}")]
        public IHttpActionResult GetProduct(int id)
        {
            //var product = products.FirstOrDefault((p) => p.Id == id);
            //if (product == null)
            //{
            //return NotFound();
            //}
            //    return Ok(product);//Respecting HTTP errors (200 OK)
            //}

            List<Product> productsId = new List<Product>();
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Prods WHERE Id = @idprod";
                command.Parameters.AddWithValue("@idprod", id);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Product p = new Product
                    {
                        Id = (int)reader["id"],
                        Name = (string)reader["Name"],
                        Category = (reader["Category"] == DBNull.Value) ? "" : (string)reader["Category"],
                        Price = (reader["Price"] == DBNull.Value) ? 0 : Convert.ToDecimal(reader["Price"])
                    };

                    productsId.Add(p);
                }
                reader.Close();
                conn.Close();

            }
            catch (Exception ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                //throw;
            }
            return Ok(productsId);
        }
         


           [Route("api/products/{category}")]
            public IEnumerable<Product> GetProductByCategory(string category)
            {
            //   List<Product> prods = new List<Product>();
            //      foreach (Product p in products)
            //         {
            //            if (p.Category == category)
            //           {
            //   prods.Add(p);
            //  }
            //  }
            //     return prods;

            List<Product> products = new List<Product>();

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string sql = "Select * FROM Prods WHERE Category LIKE @category ORDER BY Id";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@category", category);

                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    Product p = new Product
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Category = reader["Category"] == DBNull.Value ? "" : (string)reader["Category"],
                        Price = (reader["Price"] == DBNull.Value) ? 0 : Convert.ToDecimal(reader["Price"])
                    };

                    products.Add(p);
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                //throw;
            }
            return products;

        }




        // POST: api/Products
/*      
        
        public IHttpActionResult PostProduct(Product p)
        {
            SqlConnection conn = null;
            int nRows;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string str = "INSERT INTO Prods values (@name, @category, @price)";
                SqlCommand command = new SqlCommand(str, conn);
                command.Parameters.AddWithValue("@name", p.Name);
                command.Parameters.AddWithValue("@category", p.Category);
                command.Parameters.AddWithValue("@price", p.Price);
                
                nRows = command.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {
                return null;
            }

            return Ok(nRows);
        }*/

        // PUT: api/Products/5
        [Route("api/products/{id:int}")]
        public IHttpActionResult PutProduct(int id, Product p)
        {
            SqlConnection conn = null;
            int nRows;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string str = "UPDATE Prods SET Name = @name, Category = @category, Price = @price WHERE Id=@id";
                System.Diagnostics.Debug.WriteLine(str);
                SqlCommand command = new SqlCommand(str, conn);
                command.Parameters.AddWithValue("@name", p.Name);
                command.Parameters.AddWithValue("@category", p.Category);
                command.Parameters.AddWithValue("@price", p.Price);
                command.Parameters.AddWithValue("@id",id);

                nRows = command.ExecuteNonQuery();
                conn.Close();

                if (nRows > 0)
                {
                    return Ok(p);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return NotFound();
            }
        }

        // DELETE: api/Products/5
        [HttpDelete]
        [Route("api/products/{id:int}")]
        public IHttpActionResult DeleteProduct(int id)
        {
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string str = "delete from Prods where Id=@id";
                SqlCommand command = new SqlCommand(str, conn);
                command.Parameters.AddWithValue("@id", id);

                int nrows = command.ExecuteNonQuery();

                conn.Close();

                if (nrows > 0)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                return NotFound();
            }
        }

    }
}

