
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
        public DataTable allproducts(int offset=0)
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
        public DataTable allproducts2(int offset=0)
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


    }
}
