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

        public static string CreateCustomerInfo(int customerId, string name, Address billAddr, Address shipAddr, Phone phone)
        //Denna metod ska ta in parametrar för att skapa en customerInfo samt returnera ett meddelande om det fungerat eller ej.
        {
            int row;

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spCreateCustomerInfo", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramCustomerId = new SqlParameter("@CustomerId", SqlDbType.Int);
            cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = Convert.ToInt32(customerId);

            SqlParameter paramName = new SqlParameter("@Name", SqlDbType.VarChar);
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;

            //Behöver kolla hur adress ser ut eller så kopplar vi dem i en mellantabell, kom ihåg att kontrollera sqlDbType
            SqlParameter paramBillAddr = new SqlParameter("@BillAddr", SqlDbType.VarChar);
            cmd.Parameters.Add("@paramBillAddr", SqlDbType.VarChar).Value = billAddr;

            SqlParameter paramShipAddr = new SqlParameter("@ShipAddr", SqlDbType.VarChar);
            cmd.Parameters.Add("@ShipAddr", SqlDbType.VarChar).Value = shipAddr;

            //Kontrollera datatypen
            SqlParameter paramPhone = new SqlParameter("@Phone", SqlDbType.VarChar);
            cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = phone;

            try
            {
                con.Open();
                row = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            //Beroende på hur det ser ut kan vi ändra meddelandena här
            if (row > 0)
            {
                return "Success";
            }
            else
            {
                return "Failed";
            }
        }
        public static List<CustomerInfo> GetCustomerInfo(string customerId)
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
            cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = Convert.ToInt32(customerId);
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
        public static string UpdateCustomerInfo(int customerId, string name, Address billAddr, Address shipAddr, Phone phone)
        //Denna metod ska ta in parametrar för att uppdatera en customerInfo samt returnera ett meddelande om det fungerat eller ej.
        {
            int row;

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spUpdateCustomerInfo", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramCustomerId = new SqlParameter("@CustomerId", SqlDbType.Int);
            cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = Convert.ToInt32(customerId);

            SqlParameter paramName = new SqlParameter("@Name", SqlDbType.VarChar);
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;

            //Behöver kolla hur adress ser ut eller så kopplar vi dem i en mellantabell, kom ihåg att kontrollera sqlDbType
            SqlParameter paramBillAddr = new SqlParameter("@BillAddr", SqlDbType.VarChar);
            cmd.Parameters.Add("@paramBillAddr", SqlDbType.VarChar).Value = billAddr;

            SqlParameter paramShipAddr = new SqlParameter("@ShipAddr", SqlDbType.VarChar);
            cmd.Parameters.Add("@ShipAddr", SqlDbType.VarChar).Value = shipAddr;

            //Kontrollera datatypen
            SqlParameter paramPhone = new SqlParameter("@Phone", SqlDbType.VarChar);
            cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = phone;

            try
            {
                con.Open();
                row = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            //Beroende på hur det ser ut kan vi ändra meddelandena här
            if (row > 0)
            {
                return "Success";
            }
            else
            {
                return "Failed";
            }
        }

        public static string DeleteCustomerInfo(int id) //Det som gör just den customerinfo unik.
        //Denna metod ska ta in parametrar för att skapa en customerInfo samt returnera ett meddelande om det fungerat eller ej.
        {
            int row;

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spDeleteCustomerInfo", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramCustomerId = new SqlParameter("@Id", SqlDbType.Int);
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            try
            {
                con.Open();
                row = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            //Beroende på hur det ser ut kan vi ändra meddelandena här
            if (row > 0)
            {
                return "Success";
            }
            else
            {
                return "Failed";
            }
        }
    }
}
