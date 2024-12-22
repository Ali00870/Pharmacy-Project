using Microsoft.Data.SqlClient;
using System.Data;

namespace Pharmacy_back.Model
{
    public class DB
    {
        public string ConnectionString = "  Data Source=DESKTOP-MINNO8Q; Initial Catalog=master;Integrated Security=True; Trust Server Certificate=True ";
        //public string ConnectionString="Data Source =DESKTOP-O1HOQTT\\SQLEXPRESS01 ; Initial Catalog= sydality ; Integrated Security = True ; Trust Server Certificate = True  ";
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
            string query = $"Select  p.[name],p.price \r\nfrom products p join medicine m on(p.id=m.id) \r\norder by p.[name]\r\noffset {offset} rows fetch next 5 rows only;\r\n";
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
            string query = $"Select  p.[name],p.price \r\nfrom products p join Cosmetics m on(p.id=m.id) \r\norder by p.[name]\r\noffset {offset} rows fetch next 6 rows only;\r\n";
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

            INSERT INTO medicine (id, dosage, form, active_ingredients) 
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
	SqlCommand cmd = new SqlCommand(query, con);


	try
	{
		con.Open();
		count = (int)cmd.ExecuteScalar();
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex);
	}
	finally
	{
		con.Close();
	}


	return count;
}
public int Getpharmacists()
{
	int count = 0;
	string query = "select count(*) from [dbo].[pharmacist]";
	SqlCommand cmd = new SqlCommand(query, con);


	try
	{
		con.Open();
		count = (int)cmd.ExecuteScalar();
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex);
	}
	finally
	{
		con.Close();
	}


	return count;
}
public int Getcustomers()
{
	int count = 0;
	string query = "select count(*) from [dbo].[customer]";
	SqlCommand cmd = new SqlCommand(query, con);


	try
	{
		con.Open();
		count = (int)cmd.ExecuteScalar();
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex);
	}
	finally
	{
		con.Close();
	}


	return count;
}
public int Getstocked()
{
	int count = 0;
	string query = "select count(*) from [dbo].[products] where [quantity] >10";
	SqlCommand cmd = new SqlCommand(query, con);


	try
	{
		con.Open();
		count = (int)cmd.ExecuteScalar();
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex);
	}
	finally
	{
		con.Close();
	}


	return count;
}
public int Getsmallstock()
{
	int count = 0;
	string query = "select count(*) from [dbo].[products] where [quantity] <10 and [quantity]>0";
	SqlCommand cmd = new SqlCommand(query, con);


	try
	{
		con.Open();
		count = (int)cmd.ExecuteScalar();
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex);
	}
	finally
	{
		con.Close();
	}


	return count;
}
public int Getoutofstock()
{
	int count = 0;
	string query = "select count(*) from [dbo].[products] where [quantity] =0";
	SqlCommand cmd = new SqlCommand(query, con);


	try
	{
		con.Open();
		count = (int)cmd.ExecuteScalar();
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex);
	}
	finally
	{
		con.Close();
	}


	return count;
}
public int medicinesept()
{
	int count = 0;
	string query = "SELECT sum(quantity) FROM [dbo].[Customer_order] INNER JOIN [dbo].[medicine] med ON [Product_id] = med.[id] WHERE [delivery_date] >= '2024-09-01 00:00:00'  AND [delivery_date] < '2024-10-01 00:00:00'";
	SqlCommand cmd = new SqlCommand(query, con);


	try
	{
		con.Open();
		count = (int)cmd.ExecuteScalar();
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex);
	}
	finally
	{
		con.Close();
	}


	return count;
}
public int medicineoct()
{
	int count = 0;
	string query = "SELECT sum(quantity) FROM [dbo].[Customer_order] INNER JOIN [dbo].[medicine] med ON [Product_id] = med.[id] WHERE [delivery_date] >= '2024-10-01 00:00:00' AND [delivery_date] < '2024-11-01 00:00:00'";
	SqlCommand cmd = new SqlCommand(query, con);


	try
	{
		con.Open();
		count = (int)cmd.ExecuteScalar();
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex);
	}
	finally
	{
		con.Close();
	}


	return count;
}
public int medicinenov()
{
	int count = 0;
	string query = "SELECT sum(quantity) FROM [dbo].[Customer_order] INNER JOIN [dbo].[medicine] med ON [Product_id] = med.[id] WHERE [delivery_date] >= '2024-11-01 00:00:00' AND [delivery_date] < '2024-12-01 00:00:00'";
	SqlCommand cmd = new SqlCommand(query, con);


	try
	{
		con.Open();
		count = (int)cmd.ExecuteScalar();
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex);
	}
	finally
	{
		con.Close();
	}


	return count;
}
public int cosmeticsnov()
{
	int count = 0;
	string query = "SELECT sum(quantity) FROM [dbo].[Customer_order] INNER JOIN [dbo].[Cosmetics] cosm ON [Product_id] = cosm.[id] WHERE [delivery_date] >= '2024-11-01 00:00:00'  AND [delivery_date] < '2024-12-01 00:00:00'";         
	SqlCommand cmd = new SqlCommand(query, con);


	try
	{
		con.Open();
		count = (int)cmd.ExecuteScalar();
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex);
	}
	finally
	{
		con.Close();
	}


	return count;
}
public int cosmeticsoct()
{
	int count = 0;
	string query = "SELECT sum(quantity) FROM [dbo].[Customer_order] INNER JOIN [dbo].[Cosmetics] cosm ON [Product_id] = cosm.[id] WHERE [delivery_date] >= '2024-10-01 00:00:00'  AND [delivery_date] < '2024-11-01 00:00:00'";
	SqlCommand cmd = new SqlCommand(query, con);


	try
	{
		con.Open();
		count = (int)cmd.ExecuteScalar();
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex);
	}
	finally
	{
		con.Close();
	}


	return count;
}
public int cosmeticssept()
{
	int count = 0;
	string query = "SELECT sum(quantity) FROM [dbo].[Customer_order] INNER JOIN [dbo].[Cosmetics] cosm ON [Product_id] = cosm.[id] WHERE [delivery_date] >= '2024-09-01 00:00:00'  AND [delivery_date] < '2024-10-01 00:00:00'";
	SqlCommand cmd = new SqlCommand(query, con);


	try
	{
		con.Open();
		count = (int)cmd.ExecuteScalar();
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex);
	}
	finally
	{
		con.Close();
	}


	return count;
}
public DataTable Getuserdata()
{
    DataTable dt = new DataTable();
    string query = "select username,email,CONCAT(house_number,', ',district, ', ', street ) AS address from [dbo].[user],[dbo].[customer] where c_username=username";
    SqlCommand cmd = new SqlCommand(query, con);


    try
    {
        con.Open();
        dt.Load(cmd.ExecuteReader());
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
    finally
    {
        con.Close();
    }


    return dt;
}
    }
}
