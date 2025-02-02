using ClassLibrary2;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Configuration;

namespace PP1.Controllers
{
    public class propertyController : Controller
    {
        private PropertyBuisLayer propertyBuisLayer;

        public propertyController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PropertyContext"].ConnectionString;
            propertyBuisLayer = new PropertyBuisLayer(connectionString);
        }

        // GET: property
        public ActionResult Index(int page = 1)
        {
            int pageSize = 10; // Number of properties per page

            IEnumerable<Property> properties = propertyBuisLayer.Properties;
            List<P_Type> propertyTypes = propertyBuisLayer.GetP_Type();

            // Calculate the total number of properties
            int totalProperties = properties.Count();

            // Calculate the total number of pages
            int totalPages = (int)Math.Ceiling((double)totalProperties / pageSize);

            // Retrieve the properties for the current page
            List<Property> propertiesForPage = properties.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            List<PropertyViewModel> propertyViewModels = new List<PropertyViewModel>();

            foreach (Property property in propertiesForPage)
            {
                PropertyViewModel propertyViewModel = new PropertyViewModel
                {
                    Property = property,
                    City_name = propertyBuisLayer.GetCityName(property.City_id),
                    Status_Name = propertyBuisLayer.GetStateName(property.Status_ID),
                    Government_name = propertyBuisLayer.GetGovernmentName(property.Government_id),
                    P_Type_Name = propertyBuisLayer.GetP_TypeName(property.P_Type_ID),
                    District_name = propertyBuisLayer.GetDisName(property.District_ID),
                    Cities = propertyBuisLayer.GetCities(),
                    Districts = propertyBuisLayer.GetDis(),
                    Statuses = propertyBuisLayer.GetStates(),
                    Governments = propertyBuisLayer.GetGovernment(),
                    P_Types = propertyTypes,
                    P3D_path = property.P3D_path,
                    PropertyImage = propertyBuisLayer.GetImages(property.Pro_id).FirstOrDefault()
                };

                propertyViewModels.Add(propertyViewModel);
            }

            ViewBag.TotalProperties = totalProperties;
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(propertyViewModels);
        }

        // GET: 
        public ActionResult Details(int id)
        {
            Property property = propertyBuisLayer.Properties.SingleOrDefault(u => u.Pro_id == id);
            List<Property_img> propertyImages = propertyBuisLayer.GetImages(id).ToList();

            PropertyViewModel propertyViewModel = new PropertyViewModel
            {
                Property = property,
                PropertyImages = propertyImages,
                P3D_path = property.P3D_path
            };

            return View(propertyViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            PropertyViewModel propertyViewModel = new PropertyViewModel();
            propertyViewModel.Cities = propertyBuisLayer.GetCities();
            propertyViewModel.Districts = propertyBuisLayer.GetDis();
            propertyViewModel.Statuses = propertyBuisLayer.GetStates();
            propertyViewModel.Governments = propertyBuisLayer.GetGovernment();
            propertyViewModel.P_Types = propertyBuisLayer.GetP_Type();
            int userId = (int)Session["UserID"];
            ViewBag.UserID = userId;
            return View(propertyViewModel);
        }

        [HttpPost]
        public ActionResult Create(PropertyViewModel propertyViewModel, HttpPostedFileBase imageFile)
        {
            Property property = new Property
            {
                // Set the property fields from the view model
                Pro_id = propertyViewModel.Property.Pro_id,
                Price = propertyViewModel.Property.Price,
                Title = propertyViewModel.Property.Title,
                Description = propertyViewModel.Property.Description,
                NumberOfBedRooms = propertyViewModel.Property.NumberOfBedRooms,
                NumberOfBathRooms = propertyViewModel.Property.NumberOfBathRooms,
                NumOfLivingRoom = propertyViewModel.Property.NumOfLivingRoom,
                size = propertyViewModel.Property.size,
                garage_num = propertyViewModel.Property.garage_num,
                Floor_Num = propertyViewModel.Property.Floor_Num,
                Building_Num = propertyViewModel.Property.Building_Num,
                UserID = propertyViewModel.Property.UserID,
                P3D_path = propertyViewModel.P3D_path,
                // Set the IDs from the view model
                City_id = propertyViewModel.City_id,
                Government_id = propertyViewModel.Government_id,
                P_Type_ID = propertyViewModel.P_Type_ID,
                Status_ID = propertyViewModel.Status_ID,
                District_ID = propertyViewModel.District_ID
            };

            // Add the Property object to the database
            propertyBuisLayer.AddProperty(property, imageFile);

            // Get the city name for the selected city_id
            string cityName = propertyBuisLayer.GetCityName(property.City_id);

            // Set the city name in the propertyViewModel
            propertyViewModel.City_name = cityName;
            propertyViewModel.Cities = propertyBuisLayer.GetCities();
            propertyViewModel.Districts = propertyBuisLayer.GetDis();
            propertyViewModel.Statuses = propertyBuisLayer.GetStates();
            propertyViewModel.Governments = propertyBuisLayer.GetGovernment();
            propertyViewModel.P_Types = propertyBuisLayer.GetP_Type();
            propertyViewModel.P3D_path = property.P3D_path;

            // Redirect to the Create action of the ImageController
            return RedirectToAction("Index", "property");
        }

        public ActionResult Delete(int id)
        {
            Property property = propertyBuisLayer.Properties.SingleOrDefault(u => u.Pro_id == id);
            Property_img propertyImage = propertyBuisLayer.GetImages(id).FirstOrDefault();

            PropertyViewModel propertyViewModel = new PropertyViewModel
            {
                Property = property,
                PropertyImage = propertyImage,
                P3D_path = property.P3D_path 
            };

            return View(propertyViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            // Delete the Property from the database
            propertyBuisLayer.DeleteProperty(id);

            return RedirectToAction("Index");
        }

    }
}
