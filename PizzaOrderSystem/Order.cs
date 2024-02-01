using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PizzaOrderSystem
{
    internal class Order
    {
        public string order_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public decimal total;
        public string status;
        public string email;
        public int order_ID;

        public Order()
        {

        }

        public Order(decimal total, string status, string email)
        {
            this.total = total;
            this.status = status;
            this.email = email;
        }


        public static void saveOrderToDatabase(Order newOrder)
        {
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "INSERT INTO taylor_order (order_date, total, status, order_email) VALUES (CURRENT_TIMESTAMP, @utotal, @ustatus, @uemail)";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@uorder", newOrder.getOrderDate());
                cmd.Parameters.AddWithValue("@utotal", newOrder.getTotal());
                cmd.Parameters.AddWithValue("@ustatus", newOrder.getStatus());
                cmd.Parameters.AddWithValue("@uemail", newOrder.getEmail());
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }

        public static string GetOrdersForMember(string memberEmail)
        {
            StringBuilder result = new StringBuilder();

            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "SELECT o.order_id, o.order_date, o.total, o.status, o.order_email " +
                             "FROM taylor_order o " +
                             "INNER JOIN taylor_member m ON o.order_email = m.email " +
                             "WHERE o.order_email = @order_email";

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@order_email", memberEmail);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.AppendLine($"Order ID: {reader.GetInt32("order_id")}");
                        result.AppendLine($"Order Date: {reader.GetDateTime("order_date").ToString("yyyy-MM-dd HH:mm:ss")}");
                        result.AppendLine($"Total: {reader.GetDecimal("total")}");
                        result.AppendLine($"Status: {reader.GetString("status")}");
                        result.AppendLine($"Email: {reader.GetString("order_email")}");
                        result.AppendLine("-----------------------");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }

            return result.ToString();
        }

        public static bool ValidateCreditCard(string name, string cardNumber, string address, string cvs, string email)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid name.");
                return false;
            }

            if (cardNumber.Length != 16 || !cardNumber.All(char.IsDigit))
            {
                Console.WriteLine("Invalid card number. It must be 16 digits.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                Console.WriteLine("Invalid address.");
                return false;
            }

            if (cvs.Length != 3 || !cvs.All(char.IsDigit))
            {
                Console.WriteLine("Invalid CVS. It must be 3 digits.");
                return false;
            }

            if (!IsValidEmail(email))
            {
                Console.WriteLine("Invalid email address.");
                return false;
            }
            return true;
        }

        public static bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (string.IsNullOrEmpty(email))
                return false;

            Regex regex = new Regex(emailPattern);
            return regex.IsMatch(email);
        }

        public string getOrderDate()
        {
            return order_date;
        }
        public decimal getTotal()
        {
            return total;
        }
        public string getStatus()
        {
            return status;
        }
        public string getEmail()
        {
            return email;
        }

    }

}
