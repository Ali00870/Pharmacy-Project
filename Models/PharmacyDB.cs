using Microsoft.Data.SqlClient;

namespace Pharmacy_back.Models
{
    public class PharmacyDB
    {
        private string ConnectionString = "Data Source =DESKTOP-O1HOQTT\\SQLEXPRESS01 ; Initial Catlog= sydality ; Integrated Security = True ; Trust Server Certificate = True  ";
        public SqlConnection con { get; set; }
        public PharmacyDB()
        {
            con = new SqlConnection(ConnectionString);
        }
    }
}
 