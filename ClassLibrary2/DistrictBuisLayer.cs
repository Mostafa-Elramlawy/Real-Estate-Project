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
    public class DistrictBuisLayer
    {
        public List<District> GetDistricts()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CityContext"].ConnectionString;
            List<District> districts = new List<District>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT District_ID, District_name FROM District", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    District district = new District();
                    district.District_ID = Convert.ToInt32(rdr["District_ID"]);
                    district.District_name = rdr["District_name"].ToString();
                    districts.Add(district);
                }
            }

            return districts;
        }

        public void SaveDistrict(District district)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CityContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spInsertDistrict", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DistrictName", district.District_name);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
