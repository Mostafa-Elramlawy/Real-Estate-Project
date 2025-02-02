using ClassLibrary2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourNamespace.Controllers;

namespace PP1.Controllers
{
    public class AdminController : Controller
    {
        private readonly AuctionBuislayer auctionBuislayer;
        private CityBuisLayer cityBuisLayer;
        private DistrictBuisLayer districtBuisLayer;
        private P_TypeBuisLayer pTypeBuisLayer;
        private GovernmentBuisLayer governmentBuisLayer;
        private StatusBuisLayer statusBuisLayer;
        private UserBuislayer userBuislayer;
        private PropertyBuisLayer propertyBuisLayer; // Add this line

        public AdminController()
        {
            cityBuisLayer = new CityBuisLayer();
            districtBuisLayer = new DistrictBuisLayer();
            pTypeBuisLayer = new P_TypeBuisLayer();
            governmentBuisLayer = new GovernmentBuisLayer();
            statusBuisLayer = new StatusBuisLayer();
            auctionBuislayer = new AuctionBuislayer();
            propertyBuisLayer = new PropertyBuisLayer(ConfigurationManager.ConnectionStrings["PropertyContext"].ConnectionString);
            userBuislayer = new UserBuislayer(ConfigurationManager.ConnectionStrings["PropertyContext"].ConnectionString); 
        }

        // GET: Admin
        public ActionResult Index()
        {
            IEnumerable<User> users = userBuislayer.Users;
            ViewBag.Users = users;
            ViewBag.UserCount = users.Count();

            IEnumerable<Property> properties = propertyBuisLayer.Properties;
            ViewBag.Properties = properties;
            ViewBag.PropertyCount = properties.Count();

            AuctionBuislayer auctionBuislayer = new AuctionBuislayer();
            IEnumerable<Auction> auctions = auctionBuislayer.Acutions;
            ViewBag.Auctions = auctions;
            ViewBag.AuctionCount = auctions.Count();

            var cmController = new CMController();
            cmController.ControllerContext = ControllerContext;
            var indexResult = cmController.Index();
            var craftsmen = (IEnumerable<CraftsMen2>)((ViewResult)indexResult).Model;
            ViewBag.Craftsmen = craftsmen;
            ViewBag.CraftsmenCount = craftsmen.Count();

            return View();
        }
        // GET: Admin/CreateCity
        [HttpGet]
        public ActionResult CreateCity()
        {
            List<City> cities = cityBuisLayer.GetCities();
            ViewBag.Cities = cities;
            return View();
        }

        // POST: Admin/CreateCity
        [HttpPost]
        public ActionResult CreateCity(City city)
        {
            if (ModelState.IsValid)
            {
                cityBuisLayer.SaveCity(city);
                return RedirectToAction("Index");
            }

            List<City> cities = cityBuisLayer.GetCities();
            ViewBag.Cities = cities;
            return View(city);
        }


        // GET: Admin/CreateDistrict
        [HttpGet]
        public ActionResult CreateDistrict()
        {
            List<District> districts = districtBuisLayer.GetDistricts();
            ViewBag.Districts = districts;
            return View();
        }

        // POST: Admin/CreateDistrict
        [HttpPost]
        public ActionResult CreateDistrict(District district)
        {
            if (ModelState.IsValid)
            {
                districtBuisLayer.SaveDistrict(district);
                return RedirectToAction("Index");
            }

            List<District> districts = districtBuisLayer.GetDistricts();
            ViewBag.Districts = districts;
            return View(district);
        }

        // GET: Admin/CreateP_Type
        [HttpGet]
        public ActionResult CreateP_Type()
        {
            List<P_Type> pTypes = pTypeBuisLayer.GetP_Types();
            ViewBag.P_Types = pTypes;
            return View();
        }

        // POST: Admin/CreateP_Type
        [HttpPost]
        public ActionResult CreateP_Type(P_Type pType)
        {
            if (ModelState.IsValid)
            {
                pTypeBuisLayer.SaveP_Type(pType);
                return RedirectToAction("Index");
            }

            List<P_Type> pTypes = pTypeBuisLayer.GetP_Types();
            ViewBag.P_Types = pTypes;
            return View(pType);
        }

        // GET: Admin/CreateGovernment
        [HttpGet]
        public ActionResult CreateGovernment()
        {
            List<Government> governments = governmentBuisLayer.GetGovernments();
            ViewBag.Governments = governments;
            return View();
        }

        // POST: Admin/CreateGovernment
        [HttpPost]
        public ActionResult CreateGovernment(Government government)
        {
            if (ModelState.IsValid)
            {
                governmentBuisLayer.SaveGovernment(government);
                return RedirectToAction("Index");
            }

            List<Government> governments = governmentBuisLayer.GetGovernments();
            ViewBag.Governments = governments;
            return View(government);
        }

        // GET: Admin/CreateStatus
        [HttpGet]
        public ActionResult CreateStatus()
        {
            List<Status> statuses = statusBuisLayer.GetStatuses();
            ViewBag.Statuses = statuses;
            return View();
        }

        // POST: Admin/CreateStatus
        [HttpPost]
        public ActionResult CreateStatus(Status status)
        {
            if (ModelState.IsValid)
            {
                statusBuisLayer.SaveStatus(status);
                return RedirectToAction("Index");
            }

            List<Status> statuses = statusBuisLayer.GetStatuses();
            ViewBag.Statuses = statuses;
            return View(status);
        }

        public ActionResult AIndex()
        {
            // Retrieve all auctions from the database
            var auctions = auctionBuislayer.Acutions;
            var auctionViewModels = auctions.Select(a => new AuctionViewModel
            {
                AuctionID = a.AuctionID,
                Bid_Name = a.Bid_Name,
                Start_price = a.Start_price,
                Bid_start = a.Bid_start,
                Bid_End = a.Bid_End,
                lowest_bidding_price = a.lowest_bidding_price,
                Pro_id = a.Pro_id,
                AStatus_ID = a.AStatus_ID,
                AStatus_name = auctionBuislayer.GetAStatusName(a.AStatus_ID),
                Property = null // Set the Property as needed
            });

            // Pass the auctionViewModels to the view
            return View(auctionViewModels);
        }

        [HttpPost]
        public ActionResult Approve(int auctionId)
        {
            // Retrieve the auction by ID from the database
            var auction = auctionBuislayer.Acutions.FirstOrDefault(a => a.AuctionID == auctionId);

            if (auction != null)
            {
                // Update the AStatus_name to "Approved"
                auction.AStatus_ID = 2;

                // Save the updated auction
                auctionBuislayer.SaveAuction(auction);
            }

            // Redirect back to the admin index
            return RedirectToAction("AIndex");
        }

    }
}
