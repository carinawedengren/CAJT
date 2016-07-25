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
        const string con_str = "Data Source=cajt.database.windows.net;Initial Catalog=CAJT;Integrated Security=False;User ID=cajt;Password=********;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static int GetNumberInStock(int productId)
        //Denna metod tar in ett productId och returnerar hur många av denna product som finns i lagret
        //Om den returnerar -1 Så är det fel productId
        {
            int inStock = -1;

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spGetInstock", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramProductNumber = new SqlParameter("@ProductNumber", SqlDbType.Int);
            cmd.Parameters.Add("@ProductNumber", SqlDbType.Int).Value = productId;
            try
            {
                con.Open();
                var myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    inStock++;
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

            return inStock;
        }
        public static string CreateProduct(string productName, int productNumber, int price, string description)
        //Denna metod ska ta in parametrar för att skapa en Product samt returnera ett meddelande om det fungerat eller ej.
        {
            int row;

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spCreateProduct", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramProductName = new SqlParameter("@ProductName", SqlDbType.VarChar);
            cmd.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = productName;

            SqlParameter paramProductNumber = new SqlParameter("@ProductNumber", SqlDbType.NChar);
            cmd.Parameters.Add("@ProductNumber", SqlDbType.NChar).Value = Convert.ToInt32(productNumber);

            SqlParameter paramPrice = new SqlParameter("@Price", SqlDbType.Money);
            cmd.Parameters.Add("@Price", SqlDbType.Money).Value = price;

            SqlParameter paramDescription = new SqlParameter("@Description", SqlDbType.VarChar);
            cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = description;

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
        public static Product GetProduct(int productNumber)
        //Denna metod ska ta in ett ProductId och returnerar en product 
        {
            Product tmpProduct = new Product();
            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spGetProduct", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramProductNumber = new SqlParameter("@ProductNumber", SqlDbType.Int);
            cmd.Parameters.Add("@ProductNumber", SqlDbType.Int).Value = Convert.ToInt32(productNumber);
            try
            {
                con.Open();
                var myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    tmpProduct.ProductNumber = (int)myReader["ProductNumber"];
                    tmpProduct.ProductName = (string)myReader["ProductName"];
                    tmpProduct.Price = (int)myReader["Price"];
                    tmpProduct.Description = (string)myReader["Description"];
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

            return tmpProduct;
        }
        public static string DeleteProduct(int productNumber)
        //Denna metod ska ta in productNumber för att radera den från SQLservern samt returnera ett meddelande om det fungerat eller ej.
        {
            int row;

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spDeleteProduct", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramProductNumber = new SqlParameter("@ProductNumber", SqlDbType.Int);
            cmd.Parameters.Add("@ProductNumber", SqlDbType.Int).Value = productNumber;

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
        public static string CreateOrder(int customerId, bool paid, string status, List<Product> products)
        //Denna metod ska ta in parametrar för att skapa en Order samt returnera ett meddelande om det fungerat eller ej, eller OID
        {
            int row;

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spCreateOrder", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramCustomerId = new SqlParameter("@CustomerId", SqlDbType.Int);
            cmd.Parameters.Add("@CustomerId", SqlDbType.VarChar).Value = customerId;

            SqlParameter paramStatus = new SqlParameter("@Status", SqlDbType.VarChar);
            cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = status;

            SqlParameter paramDate = new SqlParameter("@Date", SqlDbType.Date);
            cmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.Now;

            if (paid)
            {
                SqlParameter paramPaid = new SqlParameter("@Paid", SqlDbType.Bit);
                cmd.Parameters.Add("@Paid", SqlDbType.Bit).Value = 1;
            }
            else
            {
                SqlParameter paramPaid = new SqlParameter("@Paid", SqlDbType.Bit);
                cmd.Parameters.Add("@Paid", SqlDbType.Bit).Value = 0;
            }
            foreach (var product in products)
            {
                SqlParameter paramProductNumber = new SqlParameter("@ProductNumber", SqlDbType.Int);
                cmd.Parameters.Add("@productNumber", SqlDbType.Int).Value = Convert.ToInt32(product.ProductNumber);
            }

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
        public static Order GetOrder(int orderNumber)
        //Denna metod ska ta in ett OrderNumber och returnera en Order, Ordernumber och OID är samma antar jag
        {
            Order tmpOrder = new Order();
            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spGetOrder", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramOrderId = new SqlParameter("@OrderId", SqlDbType.Int);
            cmd.Parameters.Add("@OrderId", SqlDbType.Int).Value = Convert.ToInt32(orderNumber);
            try
            {
                con.Open();
                var myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    tmpOrder.OrderNumber = (int)myReader["OrderId"];
                    tmpOrder.Status = (Status)myReader["Status"];
                    tmpOrder.Products.Add(GetProduct((int)myReader["ProductNumber"]));
                    tmpOrder.Date = (DateTime)myReader["Date"];
                    if ((int)myReader["Paid"] == 1)
                    {
                        tmpOrder.OrderPaid(); //Får skapa en funktion i order som kan sätta Paid = true
                    }
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

            return tmpOrder;
        }
        public static string UpdateOrder(int customerId, bool paid, string status, List<Product> products)
        //Denna metod ska ta in parametrar för att uppdatera en Order samt returnera ett meddelande om det fungerat eller ej 
        {
            int row;

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spUpdateOrder", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramCustomerId = new SqlParameter("@CustomerId", SqlDbType.Int);
            cmd.Parameters.Add("@CustomerId", SqlDbType.VarChar).Value = customerId;

            SqlParameter paramStatus = new SqlParameter("@Status", SqlDbType.VarChar);
            cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = status;

            SqlParameter paramDate = new SqlParameter("@Date", SqlDbType.Date);
            cmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.Now;

            if (paid)
            {
                SqlParameter paramPaid = new SqlParameter("@Paid", SqlDbType.Bit);
                cmd.Parameters.Add("@Paid", SqlDbType.Bit).Value = 1;
            }
            else
            {
                SqlParameter paramPaid = new SqlParameter("@Paid", SqlDbType.Bit);
                cmd.Parameters.Add("@Paid", SqlDbType.Bit).Value = 0;
            }
            foreach (var product in products)
            {
                SqlParameter paramProductNumber = new SqlParameter("@ProductNumber", SqlDbType.Int);
                cmd.Parameters.Add("@productNumber", SqlDbType.Int).Value = Convert.ToInt32(product.ProductNumber);
            }

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
        public static string DeleteOrder(int orderNumber)
        //Denna metod ska ta in OrderNummer för att radera den från SQLservern samt returnera ett meddelande om det fungerat eller ej.
        {
            int row;

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spDeleteOrder", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramOrderNumber = new SqlParameter("@OrderNumber", SqlDbType.Int);
            cmd.Parameters.Add("@OrderNumber", SqlDbType.Int).Value = orderNumber;

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
        public static string CreateCustomer(string fName, string lName, string userName)
        //Denna metod ska ta in parametrar för att skapa en Customer samt returnera ett meddelande om det fungerat eller ej.
        {
            int row;

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spCreateCustomer", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramFirstName = new SqlParameter("@FirstName", SqlDbType.VarChar);
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = fName;

            SqlParameter paramLastName = new SqlParameter("@LastName", SqlDbType.VarChar);
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lName;

            SqlParameter paramUserName = new SqlParameter("@UserName", SqlDbType.VarChar);
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;

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
        public static Customer GetCustomer(string userName, int activeCartId)
        //Denna metod ska ta in ett UserName och activeCartId sant returnerar en Customer 
        {
            Customer tmpCustomer = new Customer();
            
            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spGetCustomer", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUserName = new SqlParameter("@UserName", SqlDbType.VarChar);
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = Convert.ToInt32(userName);
            try
            {
                con.Open();
                var myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    tmpCustomer.ActiveCart = GetCart(activeCartId);
                    tmpCustomer.Info = GetCustomerInfo(userName);
                    //tmpCustomer.SavedCarts.Add(GetCart) Fattar inte riktigt hur SavedCart ska fungera
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

            return tmpCustomer;
        }
        public static string DeleteCustomer(int userName)
        //Denna metod ska ta in UserName för att radera den från SQLservern samt returnera ett meddelande om det fungerat eller ej.
        {
            int row;

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spCustomer", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUserName = new SqlParameter("@UserName", SqlDbType.Int);
            cmd.Parameters.Add("@UserName", SqlDbType.Int).Value = userName;

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
        public static string CreateCart(int userName, List<Product> products)
        //Denna metod ska ta in parametrar för att skapa en Cart samt returnera ett meddelande om det fungerat eller ej.
        {
            int row;

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spCreateCart", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUserName = new SqlParameter("@UserName", SqlDbType.VarChar);
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;

            foreach (var product in products)
            {
                SqlParameter paramProductNumber = new SqlParameter("@ProductNunber", SqlDbType.Int);
                cmd.Parameters.Add("@productNumber", SqlDbType.Int).Value = Convert.ToInt32(product.ProductNumber);
            }

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
        public static Cart GetCart(int cartId)
        //Denna metod ska ta in ett CartId och returnerar en Cart
        //Kom ihåg att kolla om Cart är tom
        //Lägg in CartName i tabellen
        {
            Cart tmpCart = new Cart();
            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spGetCart", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramCartId = new SqlParameter("@CartId", SqlDbType.Int);
            cmd.Parameters.Add("@CartId", SqlDbType.Int).Value = Convert.ToInt32(cartId);
            try
            {
                con.Open();
                var myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    tmpCart.CartId = (int)myReader["CartId"];
                    //tmpCart.CartName = (string)myReader["CartName"];
                    tmpCart.Customer = GetCustomer((int)myReader["CID"]);
                    tmpCart.Products.Add(GetProduct((int)myReader["PID"]));
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

            return tmpCart;
        }
        public static string UpdateCart(int cartId, int cid, List<Product> products)
        //Denna metod ska ta in parametrar för att uppdatera en Cart samt returnera ett meddelande om det fungerat eller ej 
        {
            int row;

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spCreateProduct", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramProductId = new SqlParameter("@ProductId", SqlDbType.Int);
            cmd.Parameters.Add("@productId", SqlDbType.Int).Value = Convert.ToInt32(productId);

            SqlParameter paramProductName = new SqlParameter("@ProductName", SqlDbType.VarChar);
            cmd.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = productName;

            SqlParameter paramProductNumber = new SqlParameter("@ProductNumber", SqlDbType.NChar);
            cmd.Parameters.Add("@paramProductNumber", SqlDbType.NChar).Value = Convert.ToInt32(productNumber);

            SqlParameter paramPrice = new SqlParameter("@Price", SqlDbType.Money);
            cmd.Parameters.Add("@Price", SqlDbType.Money).Value = price;

            SqlParameter paramDescription = new SqlParameter("@Description", SqlDbType.VarChar);
            cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = description;

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
        public static string DeleteCart(int cartId)
        //Denna metod ska ta in CartId för att radera den från SQLservern samt returnera ett meddelande om det fungerat eller ej.
        {
            int row;

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spDeleteCart", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramCartId = new SqlParameter("@CartId", SqlDbType.Int);
            cmd.Parameters.Add("@CartId", SqlDbType.Int).Value = cartId;

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
        public static string AddAddress(string userName, string street, string city, string zipCode, string country)
        //Denna metod ska ta in parametrar för att lägga till en address samt returnera ett meddelande om det fungerat eller ej.
        {
            int row;

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spAddAddress", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUserName = new SqlParameter("@UserName", SqlDbType.VarChar);
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;

            SqlParameter paramStreet = new SqlParameter("@Street", SqlDbType.VarChar);
            cmd.Parameters.Add("@Street", SqlDbType.VarChar).Value = street;

            SqlParameter paramCity = new SqlParameter("@City", SqlDbType.VarChar);
            cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = city;

            SqlParameter paramZipCode = new SqlParameter("@ZipCode", SqlDbType.VarChar);
            cmd.Parameters.Add("@ZipCode", SqlDbType.VarChar).Value = zipCode;

            SqlParameter paramCountry = new SqlParameter("@Country", SqlDbType.VarChar);
            cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = country;

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

        public static List<Address> GetAddress(string userName)
        //Denna metod ska ta in ett UserName och returnera en lista av addresser som är kopplad till customer, Kom ihåg att kolla om den är tom

        {
            List<Address> tmpList = new List<Address>();
            Address tmpAddress;

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spGetAddress", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUserName = new SqlParameter("@UserName", SqlDbType.VarChar);
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;
            try
            {
                con.Open();
                var myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    tmpAddress = new Address((string)myReader["Street"], (string)myReader["ZipCode"], (string)myReader["City"], (string)myReader["Country"]);
                    tmpList.Add(tmpAddress);
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

            return tmpList;
        }

        public static string DeleteAddress(string userName)
        //Hur vet man vilken address(?)
        //Denna metod ska ta in UserName och radera en address samt returnera ett meddelande om det fungerat eller ej.
        {
            int row;

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spDeleteAddress", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUserName = new SqlParameter("@UserName", SqlDbType.VarChar);
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;

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
        public static string AddPhone(string userName, string phoneNr, string phoneType)
        //Denna metod ska ta in parametrar för att lägga till ett telefonnr samt returnera ett meddelande om det fungerat eller ej.
        {
            int row;

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spAddPhone", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUserName = new SqlParameter("@UserName", SqlDbType.VarChar);
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;

            SqlParameter paramPhoneNr = new SqlParameter("@PhoneNr", SqlDbType.VarChar);
            cmd.Parameters.Add("@PhoneNr", SqlDbType.VarChar).Value = phoneNr;

            SqlParameter paramPhoneType = new SqlParameter("@PhoneType", SqlDbType.VarChar);
            cmd.Parameters.Add("@PhoneType", SqlDbType.VarChar).Value = phoneType;


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

        public static List<Phone> GetPhone(string userName)
        //Denna metod ska ta in ett UserName och returnera en lista av telefonnr som är kopplad till customer, Kom ihåg att kolla om den är tom

        {
            List<Phone> tmpList = new List<Phone>();
            Phone tmpPhone;

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spGetPhone", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUserName = new SqlParameter("@UserName", SqlDbType.VarChar);
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;
            try
            {
                con.Open();
                var myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    tmpPhone = new Phone((string)myReader["PhoneNumber"]);
                    tmpList.Add(tmpPhone);
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

            return tmpList;
        }

        public static string DeletePhone(string userName)
        //Hur vet man vilket telefonNr(?)
        //Denna metod ska ta in UserName och radera ett telefonnummer samt returnera ett meddelande om det fungerat eller ej.
        {
            int row;

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand("spDeletePhone", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUserName = new SqlParameter("@UserName", SqlDbType.VarChar);
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;

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
