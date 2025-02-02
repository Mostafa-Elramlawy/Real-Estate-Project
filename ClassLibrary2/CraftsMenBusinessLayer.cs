using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;

namespace ClassLibrary2
{
    public class CraftsMenBusinessLayer
    {
        private string connectionString;

        public CraftsMenBusinessLayer(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<CraftsMen2> CraftsMens
        {
            get
            {
                List<CraftsMen2> craftsmens = new List<CraftsMen2>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetCraftsMen", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        CraftsMen2 craftsman = new CraftsMen2();
                        craftsman.CM_ID = Convert.ToInt32(rdr["CM_ID"]);
                        craftsman.CM_Name = rdr["CM_Name"].ToString();
                        craftsman.CM_Address = rdr["CM_Address"].ToString();
                        craftsman.CM_Job = rdr["CM_Job"].ToString();
                        craftsman.CM_Phone = Convert.ToInt32(rdr["CM_Phone"]);

                        craftsmens.Add(craftsman);
                    }
                }
                return craftsmens;
            }
        }

        public void AddCraftsMen(CraftsMen2 craftsman)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddCraftsMen", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CM_Name", craftsman.CM_Name);
                cmd.Parameters.AddWithValue("@CM_Address", craftsman.CM_Address);
                cmd.Parameters.AddWithValue("@CM_Job", craftsman.CM_Job);
                cmd.Parameters.AddWithValue("@CM_Phone", craftsman.CM_Phone);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void UpdateCraftsMen(CraftsMen2 craftsman)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateCraftsMen", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CM_ID", craftsman.CM_ID);
                cmd.Parameters.AddWithValue("@CM_Name", craftsman.CM_Name);
                cmd.Parameters.AddWithValue("@CM_Address", craftsman.CM_Address);
                cmd.Parameters.AddWithValue("@CM_Job", craftsman.CM_Job);
                cmd.Parameters.AddWithValue("@CM_Phone", craftsman.CM_Phone);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCraftsMen(int CM_id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteCraftsMen", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CM_ID", CM_id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
