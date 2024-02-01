using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PizzaOrderSystem
{
    internal class Member
    {
        String name;
        String address;
        String email;
        String password;
        string phone;
        int member_ID;
        public Member(string name, string address, string email, string password, string phone)
        {
            this.name = name;
            this.address = address;
            this.email = email;
            this.password = password;
            this.phone = phone;
        }

        public Member()
        {

        }

        public static void saveMemberToDatabase(Member newMember)
        {
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "INSERT INTO taylor_member (Name, Address, Email, Password, Phone) VALUES (@uname, @uaddress, @uemail, @upassword, @uphone)";
              

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@uname", newMember.getName());
                cmd.Parameters.AddWithValue("@uaddress", newMember.getAddress());
                cmd.Parameters.AddWithValue("@uemail", newMember.getEmail());
                cmd.Parameters.AddWithValue("@upassword", newMember.getPassword());
                cmd.Parameters.AddWithValue("@uphone", newMember.getPhone());
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }
        public static void EditMemberInDatabase(Member updatedMember)
        {
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "UPDATE taylor_member SET Name = @uname, Address = @uaddress, Password = @upassword, Phone = @uphone WHERE Email = @uemail";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@uname", updatedMember.getName());
                cmd.Parameters.AddWithValue("@uaddress", updatedMember.getAddress());
                cmd.Parameters.AddWithValue("@uemail", updatedMember.getEmail());
                cmd.Parameters.AddWithValue("@upassword", updatedMember.getPassword());
                cmd.Parameters.AddWithValue("@uphone", updatedMember.getPhone());
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
                Console.WriteLine("Done.");
            }
        }

        public static bool validateCreateMember(string name, string email, string address, string password, string phone)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid name.");
                return false;
            }

            if (!IsValidEmail(email))
            {
                Console.WriteLine("Invalid email address.");
                return false;
            }

            if (!IsValidPhoneNumber(phone))
            {
                Console.WriteLine("Invalid Phonenumber");
                return false;
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                Console.WriteLine("Address must have input.");
                return false;
            }

            if (password.Length < 8 || !password.Any(char.IsUpper))
            {
                Console.WriteLine("Invalid password. It must be at least 8 characters long and contain at least one uppercase letter.");
                return false;
            }
            return true;
        }

        public static bool validateEditMember(string name, string email, string address, string password, string phone)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid name.");
                return false;
            }

            if (!checkEmail(email))
            {
                Console.WriteLine("Invalid email address.");
                return false;
            }

            if (!IsValidPhoneNumber(phone))
            {
                Console.WriteLine("Invalid Phonenumber");
                return false;
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                Console.WriteLine("Address must have input.");
                return false;
            }

            if (password.Length < 8 || !password.Any(char.IsUpper))
            {
                Console.WriteLine("Invalid password. It must be at least 8 characters long and contain at least one uppercase letter.");
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

        public static bool checkEmail(String emailData)
        {
            string s = emailData;
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {

                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM taylor_member WHERE email=@emailname";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@emailname", s);
                MySqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    myReader.Close();
                    return true;
                }
                else
                {
                    myReader.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
            return false;

        }
        static bool IsValidPhoneNumber(string phoneNumber)
        {
            string phonePattern = @"^\d{3}-\d{3}-\d{4}$";
            return Regex.IsMatch(phoneNumber, phonePattern);
        }
        public static bool checkLogIn(string email, string password)
        {
            string sEmail = email;
            string sPassword = password;
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM taylor_member WHERE email=@uemail AND password=@upassword";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@uemail", sEmail);
                cmd.Parameters.AddWithValue("@upassword", sPassword);
                MySqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    myReader.Close();
                    return true;
                }
                else
                {
                    myReader.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
            return false;
        }
        public static Member GetUserInfo(string email, string password)
        {
            string sEmail = email;
            string sPassword = password;
            string connStr = "server=csitmariadb.eku.edu;user=student;database=csc340_db;port=3306;password=Maroon@21?;";

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT Name, member_ID FROM taylor_member WHERE email=@uemail AND password=@upassword";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@uemail", sEmail);
                cmd.Parameters.AddWithValue("@upassword", sPassword);

                MySqlDataReader myReader = cmd.ExecuteReader();

                if (myReader.Read())
                {
                    string userName = myReader["Name"].ToString();
                    int memberId = Convert.ToInt32(myReader["member_ID"]);

                    myReader.Close();

                    return new Member
                    {
                        name = userName,
                        member_ID = memberId
                    };
                }
                else
                {
                    myReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
                Console.WriteLine("Done.");
            }

            return null;  
        }

        public string getEmail()
        {
            return email;
        }
        public string getName()
        {
            return name;
        }
        public string getAddress()
        {
            return address;
        }
        public int getMemberID()
        {
            return member_ID;
        }
        public string getPhone()
        {
            return phone;
        }

        public string getPassword()
        {
            return password;
        }

        }

    }
