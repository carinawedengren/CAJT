using CAJTLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLUtils
{
    public class SqlUtilities
    {
        const string con_str = "";

        public static List<CustomerInfo> GetCustomerInfo(string CustomerId)
        //Denna metod ska ta in ett CustomerId(eller Id) och returnera en lista av 
        //CustomerInfo(eller en, beroende på om de ska välja först eller om vi hämtar hela listan och de sedan väljer), 
        //för kunden ska ju kunna ha flera. Då får kunden även välja vilken de vill använda.
        {
            List<CustomerInfo> tmplist = new List<CustomerInfo>();
            CustomerInfo tmpCustomerInfo;

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spGetCustomerInfo", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramCustomerId = new SqlParameter("@CustomerId", SqlDbType.Int);
            cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = Convert.ToInt32(CustomerId);
            try
            {
                con.Open();
                var myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    tmpCustomerInfo = new CustomerInfo();
                    tmpCustomerInfo.BillingAddress = (Address)myReader["BillingAddress"];
                    tmpCustomerInfo.ShippingAddress = (Address)myReader["Shippingaddress"];
                    tmpCustomerInfo.CustomerId = (int)myReader["CustomerId"];
                    tmpCustomerInfo.Name = (string)myReader["Name"];
                    tmpCustomerInfo.Phone = (Phone)myReader["phone"];
                    tmplist.Add(tmpCustomerInfo);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }

            return tmplist;
        }
    }
}
