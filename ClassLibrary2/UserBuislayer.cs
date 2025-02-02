using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Windows.Input;

namespace ClassLibrary2
{
    public class UserBuislayer
    {
        private string connectionString;

        public UserBuislayer(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public IEnumerable<User> Users
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["UserContext"].ConnectionString;
                List<User> users = new List<User>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllusers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        User user = new User();
                        user.Name = rdr["Name"].ToString();
                        user.UserID = Convert.ToInt16(rdr["UserID"]);
                        user.UserName = rdr["UserName"].ToString();
                        user.Email = rdr["Email"].ToString();
                        user.PhoneNumber = Convert.ToInt32(rdr["PhoneNumber"]);
                        user.Gender = rdr["Gender"].ToString();
                        user.Role_id = Convert.ToInt16(rdr["Role_id"]);

                        users.Add(user);
                    }
                }
                return users;
            }
        }

        public void AddUser(User user)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UserContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramName = new SqlParameter("@Name", user.Name);
                cmd.Parameters.Add(paramName);

                SqlParameter paramUserName = new SqlParameter("@UserName", user.UserName);
                cmd.Parameters.Add(paramUserName);

                SqlParameter paramEmail = new SqlParameter("@Email", user.Email);
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramPhoneNumber = new SqlParameter("@PhoneNumber", user.PhoneNumber);
                cmd.Parameters.Add(paramPhoneNumber);

                SqlParameter paramGender = new SqlParameter("@Gender", user.Gender);
                cmd.Parameters.Add(paramGender);

                SqlParameter paramPassword = new SqlParameter("@Password", user.Password);
                cmd.Parameters.Add(paramPassword);

                SqlParameter paramConfirm_Password = new SqlParameter("@Confirm_Password", user.Confirm_Password);
                cmd.Parameters.Add(paramConfirm_Password);

                SqlParameter paramAddress = new SqlParameter("@Address", user.Address);
                cmd.Parameters.Add(paramAddress);

                SqlParameter paramimage = new SqlParameter("@image", user.image);
                cmd.Parameters.Add(paramimage);

                SqlParameter paramRole_id = new SqlParameter("@Role_id", user.Role_id);
                cmd.Parameters.Add(paramRole_id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateUser(User user)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UserContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters for the stored procedure
                SqlParameter paramUserID = new SqlParameter("@UserID", user.UserID);
                cmd.Parameters.Add(paramUserID);

                SqlParameter paramName = new SqlParameter("@Name", user.Name);
                cmd.Parameters.Add(paramName);

                SqlParameter paramUserName = new SqlParameter("@UserName", user.UserName);
                cmd.Parameters.Add(paramUserName);

                SqlParameter paramEmail = new SqlParameter("@Email", user.Email);
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramPhoneNumber = new SqlParameter("@PhoneNumber", user.PhoneNumber);
                cmd.Parameters.Add(paramPhoneNumber);

                SqlParameter paramGender = new SqlParameter("@Gender", user.Gender);
                cmd.Parameters.Add(paramGender);

                SqlParameter paramPassword = new SqlParameter("@Password", DBNull.Value);
                cmd.Parameters.Add(paramPassword);

                SqlParameter paramConfirm_Password = new SqlParameter("@Confirm_Password", user.Confirm_Password);
                cmd.Parameters.Add(paramConfirm_Password);

                SqlParameter paramAddress = new SqlParameter("@Address", user.Address);
                cmd.Parameters.Add(paramAddress);

                SqlParameter paramImage = new SqlParameter("@image", user.image);
                cmd.Parameters.Add(paramImage);

                SqlParameter paramRole_id = new SqlParameter("@Role_id", user.Role_id);
                cmd.Parameters.Add(paramRole_id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int GetRoleByUserID(int UserID)
        {
            int roleID;
            string connectionString = ConfigurationManager.ConnectionStrings["UserContext"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetRoleByUserID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserID);
                    // ...


                    connection.Open();
                    roleID = (int)command.ExecuteScalar();
                }
            }

            return roleID;
        }
    }
}