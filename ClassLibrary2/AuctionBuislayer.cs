using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics;
using System.Web.Mvc;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace ClassLibrary2
{
    public class AuctionBuislayer
    {
        public IEnumerable<Auction> Acutions
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["AuctionContext"].ConnectionString;
                List<Auction> auctions = new List<Auction>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetAuction", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Auction auction = new Auction();

                        auction.AuctionID = Convert.ToInt32(rdr["AuctionID"]);
                        auction.Bid_Name = rdr["Bid_Name"].ToString();
                        auction.Start_price = Convert.ToInt32(rdr["Start_price"]);
                        auction.Bid_start = Convert.ToDateTime(rdr["Bid_start"]);
                        auction.Bid_End = Convert.ToDateTime(rdr["Bid_End"]);
                        auction.lowest_bidding_price = Convert.ToInt32(rdr["lowest_bidding_price"]);
                        auction.Pro_id = Convert.ToInt32(rdr["Pro_id"]);
                        auction.AStatus_ID = Convert.ToInt32(rdr["AStatus_ID"]);

                        auctions.Add(auction);
                    }
                }
                return auctions;
            }
        }

        public void AddAuction(Auction auction)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AuctionContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddAuction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                //SqlParameter paramAuctionID = new SqlParameter();
                //paramAuctionID.ParameterName = "@AuctionID";
                //paramAuctionID.Value = auction.AuctionID;
                //cmd.Parameters.Add(paramAuctionID);


                SqlParameter paramBid_Name = new SqlParameter();
                paramBid_Name.ParameterName = "@Bid_Name";
                paramBid_Name.Value = auction.Bid_Name;
                cmd.Parameters.Add(paramBid_Name);


                SqlParameter paramStart_price = new SqlParameter();
                paramStart_price.ParameterName = "@Start_price";
                paramStart_price.Value = auction.Start_price;
                cmd.Parameters.Add(paramStart_price);


                SqlParameter paramBid_start = new SqlParameter();
                paramBid_start.ParameterName = "@Bid_start";
                paramBid_start.Value = auction.Bid_start;
                cmd.Parameters.Add(paramBid_start);

                SqlParameter paramBid_End = new SqlParameter();
                paramBid_End.ParameterName = "@Bid_End";
                paramBid_End.Value = auction.Bid_End;
                cmd.Parameters.Add(paramBid_End);


                SqlParameter paramlowest_bidding_price = new SqlParameter();
                paramlowest_bidding_price.ParameterName = "@lowest_bidding_price";
                paramlowest_bidding_price.Value = auction.lowest_bidding_price;
                cmd.Parameters.Add(paramlowest_bidding_price);


                SqlParameter paramPro_id = new SqlParameter();
                paramPro_id.ParameterName = "@Pro_id";
                paramPro_id.Value = auction.Pro_id;
                cmd.Parameters.Add(paramPro_id);


                SqlParameter paramAStatus_ID = new SqlParameter();
                paramAStatus_ID.ParameterName = "@AStatus_ID";
                paramAStatus_ID.Value = auction.AStatus_ID;
                cmd.Parameters.Add(paramAStatus_ID);


                con.Open();
                cmd.ExecuteNonQuery();

            }
        }

        public string GetAStatusName(int AStatus_ID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AuctionContext"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT AStatus_Name FROM AStatus WHERE AStatus_ID = @AStatus_ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AStatus_ID", AStatus_ID);
                connection.Open();
                string AStatus_Name = (string)command.ExecuteScalar();
                return AStatus_Name;
            }
        }

        public void SaveAuction(Auction auction)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AuctionContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spEidtAuction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramAuctionID = new SqlParameter();
                paramAuctionID.ParameterName = "@AuctionID";
                paramAuctionID.Value = auction.AuctionID;
                cmd.Parameters.Add(paramAuctionID);


                SqlParameter paramBid_Name = new SqlParameter();
                paramBid_Name.ParameterName = "@Bid_Name";
                paramBid_Name.Value = auction.Bid_Name;
                cmd.Parameters.Add(paramBid_Name);


                SqlParameter paramStart_price = new SqlParameter();
                paramStart_price.ParameterName = "@Start_price";
                paramStart_price.Value = auction.Start_price;
                cmd.Parameters.Add(paramStart_price);


                SqlParameter paramBid_start = new SqlParameter();
                paramBid_start.ParameterName = "@Bid_start";
                paramBid_start.Value = auction.Bid_start;
                cmd.Parameters.Add(paramBid_start);

                SqlParameter paramBid_End = new SqlParameter();
                paramBid_End.ParameterName = "@Bid_End";
                paramBid_End.Value = auction.Bid_End;
                cmd.Parameters.Add(paramBid_End);


                SqlParameter paramlowest_bidding_price = new SqlParameter();
                paramlowest_bidding_price.ParameterName = "@lowest_bidding_price";
                paramlowest_bidding_price.Value = auction.lowest_bidding_price;
                cmd.Parameters.Add(paramlowest_bidding_price);



                SqlParameter paramPro_id = new SqlParameter();
                paramPro_id.ParameterName = "@Pro_id";
                paramPro_id.Value = auction.Pro_id;
                cmd.Parameters.Add(paramPro_id);



                con.Open();
                cmd.ExecuteNonQuery();

            }
        }

        public void DeleteAuction(Auction auction)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AuctionContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteAuction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramAuctionID = new SqlParameter();
                paramAuctionID.ParameterName = "@AuctionID";
                paramAuctionID.Value = auction.AuctionID;
                cmd.Parameters.Add(paramAuctionID);
            }
        }

        public int GetLowestBiddingPriceById(int auctionId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AuctionContext"].ConnectionString;
            int lowestBiddingPrice = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetLowestBiddingPriceById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AuctionID", auctionId);

                con.Open();
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    lowestBiddingPrice = Convert.ToInt32(result);
                }
            }

            return lowestBiddingPrice;
        }

        public int GetStartPriceById(int auctionId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AuctionContext"].ConnectionString;
            int startPrice = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetStartPriceById", con); // Updated stored procedure name
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AuctionID", auctionId);

                con.Open();
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    startPrice = Convert.ToInt32(result);
                }
            }

            return startPrice;
        }

        public static class AuctionStatusHelper
        {
            public static string GetAStatusName(int aStatusId)
            {
                using (var connection = new SqlConnection("AuctionContext"))
                {
                    connection.Open();

                    using (var command = new SqlCommand("GetAuctionStatusName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AStatusId", aStatusId);

                        var result = command.ExecuteScalar();
                        return result?.ToString() ?? string.Empty;
                    }
                }
            }
        }

        public List<Property> GetPropertyList(string userName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AuctionContext"].ConnectionString;
            List<Property> properties = new List<Property>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Pro_id, Title FROM Property WHERE UserID = (SELECT UserID FROM TB_user WHERE UserName = @UserName)", con);

                cmd.Parameters.AddWithValue("@UserName", userName);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Property property = new Property();
                    property.Pro_id = Convert.ToInt32(rdr["Pro_id"]);
                    property.Title = rdr["Title"].ToString();
                    properties.Add(property);
                }
            }
            return properties;
        }

        public DateTime GetAuctionBidEnd(int auctionId)
        {
            DateTime bidEnd = DateTime.MinValue;
            string connectionString = ConfigurationManager.ConnectionStrings["AuctionContext"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("GetAuctionBidEnd", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AuctionID", auctionId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        bidEnd = reader.GetDateTime(0);
                    }
                }

                reader.Close();
            }

            return bidEnd;
        }

    }
}
