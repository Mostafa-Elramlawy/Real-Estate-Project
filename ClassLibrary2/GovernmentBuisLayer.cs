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
    public class GovernmentBuisLayer
    {
        public List<Government> GetGovernments()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GovernmentContext"].ConnectionString;
            List<Government> governments = new List<Government>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Government_id, Government_name FROM Government", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Government government = new Government();
                    government.Government_id = Convert.ToInt32(rdr["Government_id"]);
                    government.Government_name = rdr["Government_name"].ToString();
                    governments.Add(government);
                }
            }

            return governments;
        }

        public void SaveGovernment(Government government)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GovernmentContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spInsertGovernment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GovernmentName", government.Government_name);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
