using Microsoft.Data.SqlClient;
using Pharmacy_back.Pages;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Pharmacy_back.Model
{
    public class DB
    {
        //public string ConnectionString = "  Data Source=DESKTOP-MINNO8Q; Initial Catalog=master;Integrated Security=True; Trust Server Certificate=True ";
        public string ConnectionString = "Data Source =DESKTOP-MINNO8Q; Initial Catalog= master ; Integrated Security = True ; Trust Server Certificate = True  ";
        public SqlConnection Connection;
        public DB()
        {

            Connection = new SqlConnection(ConnectionString);

        }
        public DataTable ProductsFiltered(string Category)
        {
            DataTable d = new DataTable();
            string query = $"--Filter by category medicine----\r\nSelect  p.[name],p.price \r\nfrom products p join medicine m on(p.id=m.id) \r\nwhere m.[type] like'%{Category}%'\r\norder by [type]\r\noffset 0 rows fetch next 5 rows only;\r\n";
            SqlCommand cmd = new SqlCommand(query, Connection);
            try
            {
                Connection.Open();
                d.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { Connection.Close(); }
            return d;
        }
        public DataTable ProductsFiltered2(string Category)
        {
            DataTable d = new DataTable();
            string query = $"--Filter by category Cosmetics----\r\nSelect  p.[name],p.price \r\nfrom products p join Cosmetics m on(p.id=m.id) \r\nwhere m.[type] like'%{Category}%'\r\norder by [type]\r\noffset 0 rows fetch next 5 rows only;\r\n";
            SqlCommand cmd = new SqlCommand(query, Connection);
            try
            {
                Connection.Open();
                d.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { Connection.Close(); }
            return d;
        }
        public DataTable allproducts(int offset = 0)
        {
            DataTable d = new DataTable();
            string query = $"Select  p.id,p.[name],p.price \r\nfrom products p join medicine m on(p.id=m.id) \r\norder by p.[name]\r\noffset {offset} rows fetch next 5 rows only;\r\n";
            SqlCommand cmd = new SqlCommand(query, Connection);
            try
            {
                Connection.Open();
                d.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { Connection.Close(); }
            return d;
        }
        public DataTable allproducts2(int offset = 0)
        {
            DataTable d = new DataTable();
            string query = $"Select  p.id,p.[name],p.price \r\nfrom products p join Cosmetics m on(p.id=m.id) \r\norder by p.[name]\r\noffset {offset} rows fetch next 6 rows only;\r\n";
            SqlCommand cmd = new SqlCommand(query, Connection);
            try
            {
                Connection.Open();
                d.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { Connection.Close(); }
            return d;
        }
        public DataTable pharmacies()
        {
            DataTable d = new DataTable();
            string query = "select pharmacyname from pharmacy";
            SqlCommand cmd = new SqlCommand(query, Connection);
            try
            {
                Connection.Open();
                d.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { Connection.Close(); }
            return d;
        }
        public DataTable pharmaciesAllinfo()
        {
            DataTable d = new DataTable();
            string query = "select * from pharmacy";
            SqlCommand cmd = new SqlCommand(query, Connection);
            try
            {
                Connection.Open();
                d.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { Connection.Close(); }
            return d;
        }
        public DataTable bestsellingMedicine()
        {
            DataTable d = new DataTable();
            string query = "select top 10 c.Product_id,p.[name],p.[price] from products p join medicine m on(p.id=m.id) join Customer_order  c on(p.id=c.Product_id)\r\ngroup by Product_id,p.[name],p.price\r\norder by sum(c.quantity) desc\r\n";
            SqlCommand cmd = new SqlCommand(query, Connection);
            try
            {
                Connection.Open();
                d.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { Connection.Close(); }
            return d;
        }
        public DataTable bestsellingCosmetics()
        {
            DataTable d = new DataTable();
            string query = "select top 10 c.Product_id,p.[name],p.[price] from products p join Cosmetics m on(p.id=m.id) join Customer_order  c on(p.id=c.Product_id)\r\ngroup by Product_id,p.[name],p.price\r\norder by sum(c.quantity) desc\r\n";
            SqlCommand cmd = new SqlCommand(query, Connection);
            try
            {
                Connection.Open();
                d.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { Connection.Close(); }
            return d;
        }
        public void InsertOrder()
        {



        }

        public void AddMedicine(int product_ID, string name, float price, int quantity, string manufacturer, string dosage, string active_ingredients, string form)
        {
            string query = @"
            INSERT INTO products (id, name, price, quantity, manufacturer) 
            VALUES (@ProductID, @Name, @Price, @Quantity, @Manufacturer);

            INSERT INTO medicine (id, dosage, form, active_ingredient) 
            VALUES (@ProductID, @Dosage, @Form, @ActiveIngredients);
        ";

            try
            {
                Connection.Open();
                SqlCommand cmd = new SqlCommand(query, Connection);

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
                Connection.Close();
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
                Connection.Open();
                SqlCommand cmd = new SqlCommand(query, Connection);

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
                Connection.Close();
            }
        }

        public int Getusers()
        {
            int count = 0;
            string query = "select count(*) from [dbo].[user]";
            SqlCommand cmd = new SqlCommand(query, Connection);


            try
            {
                Connection.Open();
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Connection.Close();
            }


            return count;
        }
        public int Getpharmacists()
        {
            int count = 0;
            string query = "select count(*) from [dbo].[pharmacist]";
            SqlCommand cmd = new SqlCommand(query, Connection);


            try
            {
                Connection.Open();
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Connection.Close();
            }


            return count;
        }
        public int Getcustomers()
        {
            int count = 0;
            string query = "select count(*) from [dbo].[customer]";
            SqlCommand cmd = new SqlCommand(query, Connection);


            try
            {
                Connection.Open();
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Connection.Close();
            }


            return count;
        }
        public int Getstocked()
        {
            int count = 0;
            string query = "select count(*) from [dbo].[products] where [quantity] >10";
            SqlCommand cmd = new SqlCommand(query, Connection);


            try
            {
                Connection.Open();
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Connection.Close();
            }


            return count;
        }
        public int Getsmallstock()
        {
            int count = 0;
            string query = "select count(*) from [dbo].[products] where [quantity] <10 and [quantity]>0";
            SqlCommand cmd = new SqlCommand(query, Connection);


            try
            {
                Connection.Open();
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Connection.Close();
            }


            return count;
        }
        public int Getoutofstock()
        {
            int count = 0;
            string query = "select count(*) from [dbo].[products] where [quantity] =0";
            SqlCommand cmd = new SqlCommand(query, Connection);


            try
            {
                Connection.Open();
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Connection.Close();
            }


            return count;
        }
        public int medicinesept()
        {
            int count = 0;
            string query = "SELECT sum(quantity) FROM [dbo].[Customer_order] INNER JOIN [dbo].[medicine] med ON [Product_id] = med.[id] WHERE [delivery_date] >= '2024-09-01 00:00:00'  AND [delivery_date] < '2024-10-01 00:00:00'";
            SqlCommand cmd = new SqlCommand(query, Connection);


            try
            {
                Connection.Open();
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Connection.Close();
            }


            return count;
        }
        public int medicineoct()
        {
            int count = 0;
            string query = "SELECT sum(quantity) FROM [dbo].[Customer_order] INNER JOIN [dbo].[medicine] med ON [Product_id] = med.[id] WHERE [delivery_date] >= '2024-10-01 00:00:00' AND [delivery_date] < '2024-11-01 00:00:00'";
            SqlCommand cmd = new SqlCommand(query, Connection);


            try
            {
                Connection.Open();
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Connection.Close();
            }


            return count;
        }
        public int medicinenov()
        {
            int count = 0;
            string query = "SELECT sum(quantity) FROM [dbo].[Customer_order] INNER JOIN [dbo].[medicine] med ON [Product_id] = med.[id] WHERE [delivery_date] >= '2024-11-01 00:00:00' AND [delivery_date] < '2024-12-01 00:00:00'";
            SqlCommand cmd = new SqlCommand(query, Connection);


            try
            {
                Connection.Open();
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Connection.Close();
            }


            return count;
        }
        public int cosmeticsnov()
        {
            int count = 0;
            string query = "SELECT sum(quantity) FROM [dbo].[Customer_order] INNER JOIN [dbo].[Cosmetics] cosm ON [Product_id] = cosm.[id] WHERE [delivery_date] >= '2024-11-01 00:00:00'  AND [delivery_date] < '2024-12-01 00:00:00'";
            SqlCommand cmd = new SqlCommand(query, Connection);


            try
            {
                Connection.Open();
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Connection.Close();
            }


            return count;
        }
        public int cosmeticsoct()
        {
            int count = 0;
            string query = "SELECT sum(quantity) FROM [dbo].[Customer_order] INNER JOIN [dbo].[Cosmetics] cosm ON [Product_id] = cosm.[id] WHERE [delivery_date] >= '2024-10-01 00:00:00'  AND [delivery_date] < '2024-11-01 00:00:00'";
            SqlCommand cmd = new SqlCommand(query, Connection);


            try
            {
                Connection.Open();
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Connection.Close();
            }


            return count;
        }
        public int cosmeticssept()
        {
            int count = 0;
            string query = "SELECT sum(quantity) FROM [dbo].[Customer_order] INNER JOIN [dbo].[Cosmetics] cosm ON [Product_id] = cosm.[id] WHERE [delivery_date] >= '2024-09-01 00:00:00'  AND [delivery_date] < '2024-10-01 00:00:00'";
            SqlCommand cmd = new SqlCommand(query, Connection);


            try
            {
                Connection.Open();
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Connection.Close();
            }


            return count;
        }
        public DataTable Getuserdata()
        {
            DataTable dt = new DataTable();
            string query = "select username,email,CONCAT(house_number,', ',district, ', ', street ) AS address from [dbo].[user],[dbo].[customer] where c_username=username";
            SqlCommand cmd = new SqlCommand(query, Connection);


            try
            {
                Connection.Open();
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Connection.Close();
            }


            return dt;
        }
        private DataTable getpharmloc(string pharmacyname)
        {
            DataTable d = new DataTable();
            string query = $"select pharmacylocation from pharmacy where pharmacyname='{pharmacyname}'\r\n";
            SqlCommand cmd = new SqlCommand(query, Connection);
            try
            {
                Connection.Open();
                d.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { Connection.Close(); }
            return d;
        }
        private bool UpdateProductQuantity(int pid, int quantity)
        {
            bool b = false;
            int prevQuantity = getQuantity(pid);
            if (prevQuantity - quantity >= 0)
            {
                string query = $"Update products set quantity={prevQuantity - quantity} where id={pid}";
                SqlCommand cmd = new SqlCommand(query, Connection);
                try
                {
                    Connection.Open();
                    cmd.ExecuteNonQuery();

                    b = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    b = false;
                }
                finally { Connection.Close(); }
                return b;

            }
            else { return false; }

        }
        public int InsertOrder(string username, int product_id, int quantity, string pharmacyname, ref string msg)
        {
            DataTable d = getpharmloc(pharmacyname);
            string ploc = d.Rows[0]["pharmacylocation"].ToString();
            bool valid = UpdateProductQuantity(product_id, quantity);
            int success = 0;
            if (valid)
            {
                string mainquery = $"insert into Customer_order(pharmacy_name,pharmacy_location,C_username,Product_id,[status],quantity) values('{pharmacyname}','{ploc}','{username}',{product_id},'Pending',{quantity})\r\n";
                SqlCommand cmd = new SqlCommand(mainquery, Connection);
                msg = "s";
                try
                {
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    success = 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Console.WriteLine("A7a bel smsm");
                    success = 0;
                }
                finally { Connection.Close(); }

                return success;
            }
            else { msg = "f"; return 0; }



        }
        public int getQuantity(int pid)
        {
            DataTable dt = new DataTable();
            string query = $"select quantity from products where id={pid}";
            SqlCommand cmd = new SqlCommand(query, Connection);
            try
            {
                Connection.Open();
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { Connection.Close(); }
            return Convert.ToInt32(dt.Rows[0]["quantity"]);
        }

        public DataTable ViewMedicine(int id)
        {
            string q = @"
               select * from products p join medicine m on p.id = m.id and p.id=@id;";
            DataTable dt = new DataTable();
            try
            {
                Connection.Open();
                SqlCommand cmd = new SqlCommand(q, Connection);


                cmd.Parameters.AddWithValue("@id", id);
                dt.Load(cmd.ExecuteReader());

            }
            catch (SqlException e)
            {
                // Log exception or handle errors here
                Console.WriteLine($"Error inserting medicine : {e.Message}");
            }
            finally
            {
                Connection.Close();
            }

            return dt;
        }
        public DataTable Viewcosmetics(int id)
        {
            string q = @"
               select * from products p join cosmetics c on p.id = c.id and p.id=@id;
                ";
            DataTable dt = new DataTable();

            try
            {
                Connection.Open();
                SqlCommand cmd = new SqlCommand(q, Connection);
                cmd.Parameters.AddWithValue("@id", id);
                dt.Load(cmd.ExecuteReader());
            }
            catch (SqlException e)
            {
                // Log exception or handle errors here
                Console.WriteLine($"Error inserting cosmetics : {e.Message}");
            }
            finally
            {
                Connection.Close();
            }
            return dt;

        }
        public int check(int id)
        {
            string q = @"
             select count(*)from products p join cosmetics c on p.id = c.id and p.id= @id;";
            int count = 0;
            try
            {
                Connection.Open();
                SqlCommand cmd = new SqlCommand(q, Connection);
                cmd.Parameters.AddWithValue("@id", id);



                count = (int)cmd.ExecuteScalar();

            }
            catch (SqlException e)
            {
                // Log exception or handle errors here
                Console.WriteLine($"Error inserting cosmetics : {e.Message}");
            }
            finally
            {
                Connection.Close();
            }
            return count;


        }
    }
}