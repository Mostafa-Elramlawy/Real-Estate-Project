using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ClassLibrary2
{
    public class TransactionsBuislayer
    {
        public IEnumerable<Transactions> Transactions
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["TransactionsContext"].ConnectionString;
                List<Transactions> transactions = new List<Transactions>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetTransactions", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Transactions transactions1 = new Transactions();

                        transactions1.Transaction_ID = Convert.ToInt32(rdr["Transaction_ID"]);
                        transactions1.Transaction_date = Convert.ToDateTime(rdr["Transaction_date"]);
                        transactions1.Pro_Price = Convert.ToInt32(rdr["Pro_Price"]);
                        transactions1.AuctionID = Convert.ToInt32(rdr["AuctionID"]);
                        transactions1.UserID = Convert.ToInt32(rdr["UserID"]);
                        transactions.Add(transactions1);
                    }
                }
                return transactions;
            }
        }

        public void AddTransaction(Transactions transactions)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TransactionsContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddTransactions", con);
                cmd.CommandType = CommandType.StoredProcedure;

                //SqlParameter paramTransaction_ID = new SqlParameter();
                //paramTransaction_ID.ParameterName = "@Transaction_ID";
                //paramTransaction_ID.Value = transactions.Transaction_ID;
                //cmd.Parameters.Add(paramTransaction_ID);


                SqlParameter paramTransaction_date = new SqlParameter();
                paramTransaction_date.ParameterName = "@Transaction_date";
                paramTransaction_date.Value = transactions.Transaction_date;
                cmd.Parameters.Add(paramTransaction_date);

                SqlParameter paramPro_Price = new SqlParameter();
                paramPro_Price.ParameterName = "@Pro_Price";
                paramPro_Price.Value = transactions.Pro_Price;
                cmd.Parameters.Add(paramPro_Price);

                SqlParameter paramAuctionID = new SqlParameter();
                paramAuctionID.ParameterName = "@AuctionID";
                paramAuctionID.Value = transactions.AuctionID;
                cmd.Parameters.Add(paramAuctionID);

                SqlParameter paramUserID = new SqlParameter();
                paramUserID.ParameterName = "@UserID";
                paramUserID.Value = transactions.UserID;
                cmd.Parameters.Add(paramUserID);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Property GetPropertyByAuctionID(int auctionID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TransactionsContext"].ConnectionString;
            Property property = new Property();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetPropertyById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AuctionID", auctionID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    property.Title = rdr["Title"].ToString();
                    property.ImagePath = rdr["ImagePath"].ToString();
                }
            }

            return property;
        }

        public IEnumerable<Transactions> Transactions1(int auctionID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TransactionsContext"].ConnectionString;
            List<Transactions> transactions = new List<Transactions>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetMaxProPriceByAuctionID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AuctionID", auctionID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Transactions transaction = new Transactions();
                    transaction.AuctionID = Convert.ToInt32(rdr["AuctionID"]);
                    transaction.Pro_Price = Convert.ToInt32(rdr["MaxProPrice"]);
                    transaction.UserName = rdr["UserName"].ToString();
                    transaction.Title = rdr["Title"].ToString();
                    transaction.NumberOfBedRooms = rdr["NumberOfBedRooms"].ToString();
                    transaction.NumberOfBathRooms = rdr["NumberOfBathRooms"].ToString();
                    transaction.NumOfLivingRoom = rdr["NumOfLivingRoom"].ToString();
                    transaction.ImagePath = rdr["ImagePath"].ToString();


                    transactions.Add(transaction);
                }
            }
            return transactions;
        }

        public int GetMaxPrice(int auctionID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TransactionsContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetMaxProPriceByAuctionIDDa", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AuctionID", auctionID);

                con.Open();
                int maxPrice = 0;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read() && !reader.IsDBNull(reader.GetOrdinal("MaxProPrice")))
                    {
                        maxPrice = reader.GetInt32(reader.GetOrdinal("MaxProPrice"));
                    }
                }

                return maxPrice;
            }
        }

        public string GetImagePathByAuctionID(int auctionID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TransactionsContext"].ConnectionString;
            string imagePath = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("getimagebyid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AuctionID", auctionID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    imagePath = rdr["ImagePath"].ToString();
                }
            }

            return imagePath;
        }

    }
}








