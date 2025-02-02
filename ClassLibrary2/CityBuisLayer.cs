using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Data;

namespace ClassLibrary2
{
    public class CityBuisLayer
    {
        public List<City> GetCities()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CityContext"].ConnectionString;
            List<City> cities = new List<City>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT City_id, City_name FROM City", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    City city = new City();
                    city.City_id = Convert.ToInt32(rdr["City_id"]);
                    city.City_name = rdr["City_name"].ToString();
                    cities.Add(city);
                }
            }

            return cities;
        }

        public void SaveCity(City city)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CityContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spInsertCity", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CityName", city.City_name);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
