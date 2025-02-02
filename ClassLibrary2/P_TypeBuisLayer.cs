using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ClassLibrary2
{
    public class P_TypeBuisLayer
    {
        private readonly string connectionString;

        public P_TypeBuisLayer()
        {
            connectionString = ConfigurationManager.ConnectionStrings["P_TypeContext"].ConnectionString;
        }

        public List<P_Type> GetP_Types()
        {
            List<P_Type> pTypes = new List<P_Type>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT P_Type_ID, P_Type_Name FROM P_Type", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    P_Type pType = new P_Type();
                    pType.P_Type_ID = Convert.ToInt32(rdr["P_Type_ID"]);
                    pType.P_Type_Name = rdr["P_Type_Name"].ToString();
                    pTypes.Add(pType);
                }
            }

            return pTypes;
        }

        public void SaveP_Type(P_Type pType)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spInsertP_Type", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_TypeName", pType.P_Type_Name);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
