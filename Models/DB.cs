using Microsoft.Data.SqlClient;
using System.Data;

namespace Pharmacy_back.Models
{
    public class DB
    {
        private string connectionstring = "Data source=localhost;Initial Catalog=sydalityy;Integrated Security=True;Trust Server Certificate=True";
        public SqlConnection con {  get; set; }
       public DB() { 
        con = new SqlConnection(connectionstring);
        }
        public void InsertNewUsers(string Username,string email,string name,string password) {
           
            string query = "insert into   [user] (username,email,[password],[name]) values (@Username,@email,@password,@name)";
            SqlCommand cmd=new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Username", Username);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@password", password);
            try
            {
                con.Open();
               cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { 
            Console.WriteLine(ex.Message);
            }
            finally {
                con.Close();
            }  
                     
                         
        
        
        }

        public int checkPharmacistUsers(string username,string password)
        { int count = 0;
            String query = "select count(*) from pharmacist join [User] u on pharmacist.p_username=u.username and u.username=@username and [password]=@password ";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            try {
                con.Open();               
                
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex) {


            }
            finally { con.Close(); };

            return count;
        
        }
        public int checkusername(string username,string password)
        {
            int count = 0;
            string query = "select count(*) from [User] where username=@username and password=@password";
            try
            {con.Open();
                SqlCommand cmd=new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                count=(int)cmd.ExecuteScalar();
            }
            catch (Exception ex) { }
            finally { con.Close(); }
            return count;
        }
        public void AddPharmacist(string username,string name, string email, string password, int shift, int salary)
        {
            // Queries for inserting into users and pharmacist tables
            string userQuery = "INSERT INTO [User] (username,name ,email, [password]) VALUES (@username,@name, @email, @password)";
            string pharmacistQuery = "INSERT INTO pharmacist (p_username, shift_hours, salary) VALUES (@username, @shift, @salary)";

            try
            {
                
                con.Open();

                
                using (SqlCommand userCmd = new SqlCommand(userQuery, con))
                {
                    userCmd.Parameters.AddWithValue("@username", username);
                    userCmd.Parameters.AddWithValue("@email", email);
                    userCmd.Parameters.AddWithValue("@password", password);                   
                    userCmd.Parameters.AddWithValue("@name", name);
                    userCmd.ExecuteNonQuery();
                }

                
                using (SqlCommand pharmacistCmd = new SqlCommand(pharmacistQuery, con))
                {
                    pharmacistCmd.Parameters.AddWithValue("@username", username);
                    pharmacistCmd.Parameters.AddWithValue("@shift", shift);
                    pharmacistCmd.Parameters.AddWithValue("@salary", salary);
                    pharmacistCmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            finally
            {
                
                con.Close();
            }
        }
        public void AddEmployee(int ID,string name,int salary,int shift)
        {
            string query = "Insert into Employee (id,e_name,salary,shift_hours)values(@id,@name,@salary,@shift)";
            SqlCommand cmd=new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", ID);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@salary",salary);
            cmd.Parameters.AddWithValue("@shift", shift);
            try {
                con.Open();
               cmd.ExecuteNonQuery();
            } catch(SqlException ex) {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            finally { 
                
                con.Close();
            
            }
        }
        public DataTable GetPharmacist()
        {
            DataTable dt = new DataTable();
            string query = "select  * from pharmacist";
            SqlCommand cmd=new SqlCommand(query,con);

            try
            {con.Open();
                dt.Load(cmd.ExecuteReader());

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { 
            con.Close   ( );
            }
            return dt;
        }
        public DataTable Getemployees()
        {
            DataTable dt = new DataTable();
            string query = "select * from employee";
            SqlCommand cmd=new SqlCommand(query,con);
            try {con.Open();
            dt.Load(cmd.ExecuteReader());
            }catch(SqlException ex) { 
                
                Console.WriteLine( ex.Message); }
            finally {
                
                con.Close ( );            
            }


            return dt;
        }

       

        public void UpdatePharmacist(string username, int shift, int salary)
        {
            string query = "update pharmacist set shift_hours=@shift,salary=@salary where p_username=@username";
            SqlCommand cmd=new SqlCommand(query,con);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@salary",salary);
            cmd.Parameters.AddWithValue("@shift",shift);
            try
            {con.Open ();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);  
            }
            finally {
                con.Close();
            
            }


        }
        public void DeletePharmacist(string username)
        {
            // Query to delete related records in the pharmacist_works_on table
            string deletePharmacistWorksOnQuery = "DELETE FROM pharmacist_works_on WHERE p_username = @username";
            // Query to delete the pharmacist record
            string deletePharmacistQuery = "DELETE FROM pharmacist WHERE p_username = @username";
            // Query to delete the user record
            string deleteUserQuery = "DELETE FROM [User] WHERE username = @username";

            try
            {
                con.Open();

                // Delete records in pharmacist_works_on table
                using (SqlCommand worksOnCmd = new SqlCommand(deletePharmacistWorksOnQuery, con))
                {
                    worksOnCmd.Parameters.AddWithValue("@username", username);
                    worksOnCmd.ExecuteNonQuery();
                }

                // Delete pharmacist record
                using (SqlCommand pharmacistCmd = new SqlCommand(deletePharmacistQuery, con))
                {
                    pharmacistCmd.Parameters.AddWithValue("@username", username);
                    pharmacistCmd.ExecuteNonQuery();
                }

                // Delete user record
                using (SqlCommand userCmd = new SqlCommand(deleteUserQuery, con))
                {
                    userCmd.Parameters.AddWithValue("@username", username);
                    userCmd.ExecuteNonQuery();
                }

                Console.WriteLine($"Pharmacist and corresponding user with username '{username}' have been deleted.");
            }
            catch (SqlException ex)
            {
                // Log any SQL errors
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            finally
            {
                // Ensure the database connection is closed
                con.Close();
            }
        }


        public void deletEemployee(int id)
        {
            string query = "delete from employee where id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            try
            {con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex) {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            finally {
                con.Close(); }
        }
       public void UpdateEmployee(int id, string name, int salary, int shiftHours)
       {
    
    string query = "UPDATE Employee SET e_name = @name, salary = @salary, shift_hours = @shiftHours WHERE ID = @id";

    try
    {
        
        con.Open();

       
        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@salary", salary);
            cmd.Parameters.AddWithValue("@shiftHours", shiftHours);

           
            cmd.ExecuteNonQuery();
        }

        Console.WriteLine($"Employee with ID '{id}' updated successfully.");
    }
    catch (SqlException ex)
    {
        
        Console.WriteLine($"SQL Error: {ex.Message}");
    }
    finally
    {
        
        con.Close();
    }
}

    }
}
