using ClassLibrary2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Spreadsheet;
using PP1.Models;
using System.Configuration;

namespace PP1.Controllers
{
    public class userController : Controller
    {
        private PropertyBuisLayer propertyBuisLayer;
        private string connectionString = ConfigurationManager.ConnectionStrings["UserContext"].ConnectionString;
        private UserBuislayer userContext;

        public userController()
        {
            propertyBuisLayer = new PropertyBuisLayer(connectionString);
            userContext = new UserBuislayer(connectionString);
        }

        UserContext db = new UserContext();
        User User = new User();

        public ActionResult Index()
        {
            List<User> users = userContext.Users.ToList();
            return View(users);
        }

        public ActionResult Details()
        {
            int userID = (int)Session["UserID"];

            List<User> users = userContext.Users.Where(u => u.UserID == userID).ToList();

            if (users.Count == 0)
            {
                return HttpNotFound();
            }
            else if (users.Count == 1)
            {
                User user = users[0];
                return View(user);
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        private string SaveUploadedFile(HttpPostedFileBase file)
        {
            // Get the file name
            string fileName = Path.GetFileName(file.FileName);

            // Set the destination folder path
            string folderPath = Server.MapPath("~/Users_Images");

            // Create the folder if it doesn't exist
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Set the file path
            string filePath = Path.Combine(folderPath, fileName);

            // Save the file to the destination folder
            file.SaveAs(filePath);

            // Return the file path
            return filePath;
        }

        private bool UserAlreadyExists(string username, string email)
        {
            using (var dbContext = new UserContext()) // Replace "UserContext" with the name of your actual DbContext class
            {
                // Check if the username or email already exists in the database
                bool usernameExists = dbContext.Users.Any(u => u.UserName == username);
                bool emailExists = dbContext.Users.Any(u => u.Email == email);

                return usernameExists || emailExists;
            }
        }

        public ActionResult MyProperties()
        {
            int userID = (int)Session["UserID"];
            IEnumerable<Property> properties = propertyBuisLayer.GetPropertiesByUserID(userID);

            return View(properties);
        }

        [HttpGet]
        public ActionResult MCreate(int proId)
        {
            Property_img propertyimg = new Property_img
            {
                Pro_id = proId
            };

            return View(propertyimg);
        }

        [HttpPost]
        public ActionResult MCreate(int proId, IEnumerable<HttpPostedFileBase> imageFiles)
        {
            if (ModelState.IsValid && imageFiles != null && imageFiles.Any(file => file.ContentLength > 0))
            {
                foreach (var imageFile in imageFiles)
                {
                    if (imageFile != null && imageFile.ContentLength > 0)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                        string imagePath = Path.Combine(Server.MapPath("~/Prop_Doc"), fileName);
                        string imageRelativePath = "~/Prop_Doc/" + fileName;

                        imageFile.SaveAs(imagePath);

                        Property_img propertyimg = new Property_img
                        {
                            Pro_id = proId,
                            img_name = fileName,
                            img_path = imageRelativePath
                        };

                        propertyBuisLayer.AddImage(propertyimg);
                    }
                }

                return RedirectToAction("MyProperties");
            }

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection, HttpPostedFileBase imageFile)
        {
            try
            {
                Session["Role_id"] = User.Role_id;
                string username = formCollection["UserName"];
                string email = formCollection["Email"];

                if (UserAlreadyExists(username, email))
                {
                    ViewBag.Notification = "The username or email already exists.";
                    return View();
                }

                User user = new User();
                user.Name = formCollection["Name"];
                user.UserID = Convert.ToInt16(formCollection["UserID"]);
                user.UserName = formCollection["UserName"];
                user.Email = formCollection["Email"];
                int phoneNumber;
                if (Int32.TryParse(formCollection["PhoneNumber"], out phoneNumber))
                {
                    user.PhoneNumber = phoneNumber;
                }
                else
                {
                    ModelState.AddModelError("", "Invalid phone number.");
                    return View();
                }
                user.Gender = formCollection["Gender"];
                user.Password = formCollection["Password"];
                user.Confirm_Password = formCollection["Confirm_Password"];
                user.Address = formCollection["Address"];
                user.Role_id = 1;

                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    string imagePath = SaveUploadedFile(imageFile);
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        user.image = imagePath;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error occurred while saving the uploaded image.");
                        return View();
                    }
                }
                else
                {
                    user.image = "~/Content/images/Default_Image.png";
                }

                userContext.AddUser(user);

                return RedirectToAction("Login", "user");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while saving the user. Please try again later.");
                return View();
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            User user = userContext.Users.FirstOrDefault(u => u.UserID == id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                userContext.UpdateUser(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {

            var checkLogin = db.Users.Where(x => x.UserName.Equals(user.UserName) && x.Password.Equals(user.Password)).FirstOrDefault();
            if (checkLogin != null)
            {
                Session["UserID"] = checkLogin.UserID;
                Session["UserName"] = checkLogin.UserName.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notification = "Invalid User Name or Password";
                return View("Login");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "user");
        }

        public ActionResult Delete(int id)
        {
            var tbUser = db.Users.SingleOrDefault(c => c.UserID == id);
            return View(tbUser);
        }
        [HttpPost]
        public ActionResult Delete(int id, User user)
        {
            try
            {
                var tbUser = db.Users.SingleOrDefault(c => c.UserID == id);
                db.Users.Remove(tbUser);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AdminCreate(FormCollection formCollection, HttpPostedFileBase imageFile)
        {
            try
            {
                Session["Role_id"] = User.Role_id;
                string username = formCollection["UserName"];
                string email = formCollection["Email"];

                if (UserAlreadyExists(username, email))
                {
                    ViewBag.Notification = "The username or email already exists.";
                    return View();
                }

                User user = new User();
                user.Name = formCollection["Name"];
                user.UserID = Convert.ToInt16(formCollection["UserID"]);
                user.UserName = formCollection["UserName"];
                user.Email = formCollection["Email"];
                int phoneNumber;
                if (Int32.TryParse(formCollection["PhoneNumber"], out phoneNumber))
                {
                    user.PhoneNumber = phoneNumber;
                }
                else
                {
                    ModelState.AddModelError("", "");
                    return View();
                }
                user.Gender = formCollection["Gender"];
                user.Password = formCollection["Password"];
                user.Confirm_Password = formCollection["Confirm_Password"];
                user.Address = formCollection["Address"];
                user.Role_id = 2;

                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    string imagePath = SaveUploadedFile(imageFile);
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        user.image = imagePath;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error occurred while saving the uploaded image.");
                        return View();
                    }
                }
                else
                {
                    user.image = "~/Content/images/Default_Image.png";
                }

                userContext.AddUser(user);

                return RedirectToAction("Login", "user");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while saving the user. Please try again later.");
                return View();
            }
        }

    }
}
