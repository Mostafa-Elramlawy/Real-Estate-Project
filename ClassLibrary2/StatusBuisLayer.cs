using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class StatusBuisLayer
    {
        public List<Status> GetStatuses()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StatusContext"].ConnectionString;
            List<Status> statuses = new List<Status>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Status_ID, Status_name FROM Status", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Status status = new Status();
                    status.Status_ID = Convert.ToInt32(rdr["Status_ID"]);
                    status.Status_Name = rdr["Status_name"].ToString();
                    statuses.Add(status);
                }
            }

            return statuses;
        }

        public void SaveStatus(Status status)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StatusContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spInsertStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatusName", status.Status_Name);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
