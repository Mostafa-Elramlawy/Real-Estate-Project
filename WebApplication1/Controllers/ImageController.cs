using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary2;

namespace PP1.Controllers
{
    public class ImageController : Controller
    {
        private PropertyBuisLayer propertyBuisLayer;

        public ImageController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PropertyContext"].ConnectionString;
            propertyBuisLayer = new PropertyBuisLayer(connectionString);
        }

        public ActionResult Index(int propertyId = 0)
        {
            IEnumerable<Property_img> propertyImages = propertyBuisLayer.GetImage(propertyId);
            return View(propertyImages);
        }

        public ActionResult Create(int proId)
        {
            ViewBag.ProId = proId;

            Property_img propertyImage = new Property_img
            {
                Pro_id = proId
            };

            return View(propertyImage);
        }

        [HttpPost]
        public ActionResult Create(Property_img propertyimg, IEnumerable<HttpPostedFileBase> imageFiles)
        {
            if (ModelState.IsValid && imageFiles != null && imageFiles.Any(file => file.ContentLength > 0))
            {
                foreach (var imageFile in imageFiles)
                {
                    if (imageFile != null && imageFile.ContentLength > 0)
                    {
                        // Generate a unique file name for the image
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                        string imagePath = Path.Combine(Server.MapPath("~/Prop_Doc"), fileName);
                        string imageRelativePath = "~/Prop_Doc/" + fileName;

                        // Save the uploaded image file to the specified folder
                        imageFile.SaveAs(imagePath);

                        propertyimg.img_name = fileName;
                        propertyimg.img_path = imageRelativePath;

                        // Pass the Pro_id and the image details to the PropertyBuisLayer
                        propertyBuisLayer.AddImage(propertyimg);
                    }
                }

                return RedirectToAction("Index", "property");
            }

            return View(propertyimg);
        }
    }
}
