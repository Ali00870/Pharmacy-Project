using Microsoft.Data.SqlClient;
using System.Data;

namespace Pharmacy_back.Models
{
    public class DB
    {
        private string ConnectionString = "Data Source=DESKTOP-O1HOQTT\\SQLEXPRESS01; Initial Catalog=sydality; Integrated Security=True; Trust Server Certificate=True";
        public SqlConnection con { get; set; }

        public DB()
        {
            con = new SqlConnection(ConnectionString);
        }

        public void AddMedicine(int product_ID, string name, float price, int quantity, string manufacturer, string dosage, string active_ingredients, string form)
        {
            string query = @"
            INSERT INTO products (id, name, price, quantity, manufacturer) 
            VALUES (@ProductID, @Name, @Price, @Quantity, @Manufacturer);

            INSERT INTO medicine (id, dosage, form, active_ingredients) 
            VALUES (@ProductID, @Dosage, @Form, @ActiveIngredients);
        ";

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ProductID", product_ID);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@Manufacturer", manufacturer);
                cmd.Parameters.AddWithValue("@Dosage", dosage);
                cmd.Parameters.AddWithValue("@Form", form);
                cmd.Parameters.AddWithValue("@ActiveIngredients", active_ingredients);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                // Log exception or handle errors here
                Console.WriteLine($"Error inserting medicine: {e.Message}");
            }
            finally
            {
                con.Close();
            }
        }

        public void AddCosmetic(int product_ID, string name, float price, int quantity, string manufacturer, string type, string description)
        {
            string query = @"
            INSERT INTO products (id, name, price, quantity, manufacturer) 
            VALUES (@ProductID, @Name, @Price, @Quantity, @Manufacturer);

            INSERT INTO cosmetics (id, type, description) 
            VALUES (@ProductID, @Type, @Description);
        ";

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ProductID", product_ID);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@Manufacturer", manufacturer);
                cmd.Parameters.AddWithValue("@Type", type);
                cmd.Parameters.AddWithValue("@Description", description);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                // Log exception or handle errors here
                Console.WriteLine($"Error inserting cosmetic: {e.Message}");
            }
            finally
            {
                con.Close();
            }
        }
    }
}

