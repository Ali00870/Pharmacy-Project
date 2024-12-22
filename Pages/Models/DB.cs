
using Microsoft.Data.SqlClient;
using System.Data;



namespace Pharmacy_back.Pages.Models
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
            DataTable d=new DataTable();
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
            string query = $"--Filter by category Cosmetics----\r\nSelect  p.id,p.[name],p.price \r\nfrom products p join Cosmetics m on(p.id=m.id) \r\nwhere m.[type] like'%{Category}%'\r\norder by [type]\r\noffset 0 rows fetch next 5 rows only;\r\n";
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
        public DataTable allproducts(int offset=0)
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
        public DataTable allproducts2(int offset=0)
        {
            DataTable d = new DataTable();
            string query = $"Select  p.id,p.[name],p.price from products p join Cosmetics m on(p.id=m.id) order by p.[name] offset {offset} rows fetch next 6 rows only;";
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
        public int InsertOrder(string username,int product_id,int quantity,string pharmacyname,ref string msg)
        {
            DataTable d = getpharmloc(pharmacyname);
            string ploc = d.Rows[0]["pharmacylocation"].ToString();
            bool valid = UpdateProductQuantity(product_id, quantity);
            int success = 0;
            if (valid) {
                string mainquery = $"insert into Customer_order(pharmacy_name,pharmacy_location,C_username,Product_id,[status],quantity) values('{pharmacyname}','{ploc}','{username}',{product_id},'Pending',{quantity})\r\n";
                SqlCommand cmd=new SqlCommand(mainquery, Connection);
                msg ="s";
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
        public DataTable pharmacies()
        {
            DataTable d = new DataTable();
            string query = "select pharmacyname from pharmacy";
            SqlCommand cmd= new SqlCommand(query, Connection);
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
        private DataTable getpharmloc(string pharmacyname)
        {
            DataTable d= new DataTable();
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
        private bool UpdateProductQuantity(int pid,int quantity)
        {
            bool b=false;
            int prevQuantity=getQuantity(pid);
            if(prevQuantity-quantity >= 0) {
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
                    b=false;
                }
                finally { Connection.Close(); }
                return b;

            }
            else { return false; }
            
        }
        

        public int getQuantity(int pid)
        {
            DataTable dt = new DataTable();
            string query = $"select quantity from products where id={pid}";
            SqlCommand cmd= new SqlCommand(query, Connection);
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

    }
}
