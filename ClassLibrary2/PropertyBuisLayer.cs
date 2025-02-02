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
using System.IO;
using System.Web.UI.WebControls;
using System.Web;

namespace ClassLibrary2
{
    public class PropertyBuisLayer
    {
        private string connectionString;

        public PropertyBuisLayer(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<Property> Properties
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["PropertyContext"].ConnectionString;
                List<Property> properties = new List<Property>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllProperty", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Property property = new Property();
                        property.Pro_id = Convert.ToInt32(rdr["Pro_id"]);
                        property.Price = Convert.ToInt32(rdr["Price"]);
                        property.Title = rdr["Title"].ToString();
                        property.Description = rdr["Description"].ToString();
                        property.NumberOfBedRooms = rdr["NumberOfBedRooms"].ToString();
                        property.NumberOfBathRooms = rdr["NumberOfBathRooms"].ToString();
                        property.NumOfLivingRoom = rdr["NumOfLivingRoom"].ToString();
                        property.Building_Num = Convert.ToInt32(rdr["Building_Num"]);
                        property.City_id = Convert.ToInt16(rdr["City_id"]);
                        property.Government_id = Convert.ToInt16(rdr["Government_id"]);
                        property.Floor_Num = Convert.ToInt32(rdr["Floor_Num"]);
                        property.size = Convert.ToInt32(rdr["size"]);
                        property.garage_num = Convert.ToInt32(rdr["garage_num"]);
                        property.P3D_path = rdr["P3D_path"].ToString(); 
                        property.ImagePath = rdr["ImagePath"].ToString();
                        
                        property.Floor_Num = Convert.ToInt32(rdr["Floor_Num"]);
                        property.District_ID = Convert.ToInt16(rdr["District_ID"]);
                        property.Status_ID = Convert.ToInt16(rdr["Status_ID"]);
                        property.P_Type_ID = Convert.ToInt16(rdr["P_Type_ID"]);
                        property.UserID = Convert.ToInt32(rdr["UserID"]);
                        properties.Add(property);
                    }
                }
                return properties;
            }
        }

        public void AddProperty(Property property, HttpPostedFileBase imageFile)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PropertyContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddProperty", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Price", property.Price);
                cmd.Parameters.AddWithValue("@Title", property.Title);
                cmd.Parameters.AddWithValue("@Description", property.Description);
                cmd.Parameters.AddWithValue("@NumberOfBedRooms", property.NumberOfBedRooms);
                cmd.Parameters.AddWithValue("@NumberOfBathRooms", property.NumberOfBathRooms);
                cmd.Parameters.AddWithValue("@NumOfLivingRoom", property.NumOfLivingRoom);
                cmd.Parameters.AddWithValue("@size", property.size);
                cmd.Parameters.AddWithValue("@garage_num", property.garage_num);
                cmd.Parameters.AddWithValue("@Floor_Num", property.Floor_Num);
                cmd.Parameters.AddWithValue("@Building_Num", property.Building_Num);
                cmd.Parameters.AddWithValue("@District_ID", property.District_ID);
                cmd.Parameters.AddWithValue("@UserID", property.UserID);
                cmd.Parameters.AddWithValue("@Status_ID", property.Status_ID);
                cmd.Parameters.AddWithValue("@P_Type_ID", property.P_Type_ID);
                cmd.Parameters.AddWithValue("@City_id", property.City_id);
                cmd.Parameters.AddWithValue("@Government_id", property.Government_id);
                cmd.Parameters.AddWithValue("@P3D_path", property.P3D_path);

                string fileName = null;
                string imageRelativePath = null;

                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    fileName = Path.GetFileName(imageFile.FileName);
                    string imagePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Prop_Doc"), fileName);
                    imageRelativePath = "~/Prop_Doc/" + fileName;
                    imageFile.SaveAs(imagePath);
                    cmd.Parameters.AddWithValue("@ImagePath", imageRelativePath);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ImagePath", DBNull.Value);
                }

                con.Open();
                int proId = (int)cmd.ExecuteScalar();
            }
        }

        public void UpdateProperty(Property property, HttpPostedFileBase imageFile)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PropertyContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateProperty", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Pro_id", property.Pro_id);
                cmd.Parameters.AddWithValue("@Price", property.Price);
                cmd.Parameters.AddWithValue("@Title", property.Title);
                cmd.Parameters.AddWithValue("@Description", property.Description);
                cmd.Parameters.AddWithValue("@NumberOfBedRooms", property.NumberOfBedRooms);
                cmd.Parameters.AddWithValue("@NumberOfBathRooms", property.NumberOfBathRooms);
                cmd.Parameters.AddWithValue("@NumOfLivingRoom", property.NumOfLivingRoom);
                cmd.Parameters.AddWithValue("@size", property.size);
                cmd.Parameters.AddWithValue("@garage_num", property.garage_num);
                cmd.Parameters.AddWithValue("@Floor_Num", property.Floor_Num);
                cmd.Parameters.AddWithValue("@Building_Num", property.Building_Num);
                cmd.Parameters.AddWithValue("@District_ID", property.District_ID);
                cmd.Parameters.AddWithValue("@UserID", property.UserID);
                cmd.Parameters.AddWithValue("@Status_ID", property.Status_ID);
                cmd.Parameters.AddWithValue("@P_Type_ID", property.P_Type_ID);
                cmd.Parameters.AddWithValue("@City_id", property.City_id);
                cmd.Parameters.AddWithValue("@Government_id", property.Government_id);
                cmd.Parameters.AddWithValue("@P3D_path", property.P3D_path);

                string fileName = null;
                string imageRelativePath = null;

                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    fileName = Path.GetFileName(imageFile.FileName);
                    string imagePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Prop_Doc"), fileName);
                    imageRelativePath = "~/Prop_Doc/" + fileName;
                    imageFile.SaveAs(imagePath);
                    cmd.Parameters.AddWithValue("@ImagePath", imageRelativePath);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ImagePath", DBNull.Value);
                }

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProperty(int propertyId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PropertyContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteProperty", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Pro_id", propertyId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Property> GetPropertiesByUserID(int userID)
        {
            return Properties.Where(p => p.UserID == userID);
        }


        /// //////////////////////////////////////////////////////
        public List<City> GetCities()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PropertyContext"].ConnectionString;
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
        public string GetCityName(int cityId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PropertyContext"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT City_name FROM City WHERE City_id = @CityId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CityId", cityId);
                connection.Open();
                string cityName = (string)command.ExecuteScalar();
                return cityName;
            }
        }

        /// //////////////////////////////////////////////////////
        public List<District> GetDis()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PropertyContext"].ConnectionString;
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
        public string GetDisName(int DisId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PropertyContext"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT District_name FROM District WHERE District_ID = @District_ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@District_ID", DisId);
                connection.Open();
                string districtName = (string)command.ExecuteScalar();
                return districtName;
            }
        }

        /// //////////////////////////////////////////////////////
        public List<Status> GetStates()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PropertyContext"].ConnectionString;
            List<Status> statuses = new List<Status>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Status_ID, Status_Name FROM Status", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Status status = new Status();
                    status.Status_ID = Convert.ToInt32(rdr["Status_ID"]);
                    status.Status_Name = rdr["Status_Name"].ToString();
                    statuses.Add(status);
                }
            }
            return statuses;
        }
        public string GetStateName(int StateId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PropertyContext"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Status_Name FROM Status WHERE Status_ID = @Status_ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Status_ID", StateId);
                connection.Open();
                string stateName = (string)command.ExecuteScalar();
                return stateName;
            }
        }

        /// //////////////////////////////////////////////////////
        public List<Government> GetGovernment()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PropertyContext"].ConnectionString;
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
        public string GetGovernmentName(int GovernmentId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PropertyContext"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Government_name FROM Government WHERE Government_id = @Government_id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Government_id", GovernmentId);
                connection.Open();
                string GovName = (string)command.ExecuteScalar();
                return GovName;
            }
        }

        /// //////////////////////////////////////////////////////
        public List<P_Type> GetP_Type()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PropertyContext"].ConnectionString;
            List<P_Type> p_Types = new List<P_Type>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT P_Type_ID, P_Type_Name FROM P_Type", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    P_Type government = new P_Type();
                    government.P_Type_ID = Convert.ToInt32(rdr["P_Type_ID"]);
                    government.P_Type_Name = rdr["P_Type_Name"].ToString();
                    p_Types.Add(government);
                }
            }
            return p_Types;
        }
        public string GetP_TypeName(int P_TypeId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PropertyContext"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT P_Type_Name FROM P_Type WHERE P_Type_ID = @P_Type_ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@P_Type_ID", P_TypeId);
                connection.Open();
                string P_TypeName = (string)command.ExecuteScalar();
                return P_TypeName;
            }
        }

        /// //////////////////////////////////////////////////////
        public IEnumerable<Property_img> GetImages(int propertyId = 0)
        {
            List<Property_img> propertyImages = new List<Property_img>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetImages", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Pro_id", propertyId);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Property_img propertyImage = new Property_img();
                    propertyImage.img_id = Convert.ToInt32(rdr["img_id"]);
                    propertyImage.img_name = rdr["img_name"].ToString();
                    propertyImage.img_path = rdr["img_path"].ToString();
                    propertyImage.Pro_id = Convert.ToInt32(rdr["Pro_id"]);

                    propertyImages.Add(propertyImage);
                }
            }

            return propertyImages;
        }
        public void AddImage(Property_img propertyImage)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddImage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@img_name", propertyImage.img_name);
                cmd.Parameters.AddWithValue("@img_path", propertyImage.img_path);
                cmd.Parameters.AddWithValue("@Pro_id", propertyImage.Pro_id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public IEnumerable<Property_img> GetImage(int propertyId = 0)
        {
            List<Property_img> propertyImages = new List<Property_img>();

            using (var context = new PropertyContext(connectionString))
            {
                if (propertyId != 0)
                {
                    propertyImages = context.PropertyImages.Where(img => img.Pro_id == propertyId).ToList();
                }
                else
                {
                    propertyImages = context.PropertyImages.ToList();
                }
            }
            return propertyImages;
        }

    }
}

