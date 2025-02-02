using ClassLibrary2;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PP1.Controllers
{
    public class AuctionController : Controller
    {
         public ActionResult Index(int page = 1, int pageSize = 20)
        {
            int userId = (int)Session["UserID"];
            ViewBag.CurrentUserID = userId;
            // Create an instance of AuctionBuislayer
            AuctionBuislayer auctionBusinessLayer = new AuctionBuislayer();

            IEnumerable<Auction> auctions = auctionBusinessLayer.Acutions.OrderByDescending(a => a.AuctionID);
            Dictionary<int, int> auctionMaxPrices = new Dictionary<int, int>();
            TransactionsBuislayer transactionsBuislayer = new TransactionsBuislayer();
            foreach (Auction auction in auctions)
            {
                // Get the highest price for the auction
                int maxPrice = transactionsBuislayer.GetMaxPrice(auction.AuctionID);

                // Add the auction ID and its corresponding highest price to the dictionary
                auctionMaxPrices.Add(auction.AuctionID, maxPrice);
            }

            // Pass the auctionMaxPrices dictionary to the view
            ViewBag.AuctionMaxPrices = auctionMaxPrices;

            // Perform pagination
            int totalItems = auctions.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            auctions = auctions.Skip((page - 1) * pageSize).Take(pageSize);

            List<AuctionViewModel> auctionViewModels = new List<AuctionViewModel>();
            PropertyBuisLayer propertyBuisLayer = new PropertyBuisLayer(ConfigurationManager.ConnectionStrings["PropertyContext"].ConnectionString);
            foreach (Auction auction in auctions)
            {
                // Retrieve the property image path for the auction
                Property property = propertyBuisLayer.Properties.FirstOrDefault(p => p.Pro_id == auction.Pro_id);
                string imagePath = property?.ImagePath ?? string.Empty;

                // Create the AuctionViewModel instance
                AuctionViewModel auctionViewModel = new AuctionViewModel
                {
                    AuctionID = auction.AuctionID,
                    Bid_Name = auction.Bid_Name,
                    Start_price = auction.Start_price,
                    Bid_start = auction.Bid_start,
                    Bid_End = auction.Bid_End,
                    lowest_bidding_price = auction.lowest_bidding_price,
                    Pro_id = auction.Pro_id,
                    AStatus_ID = auction.AStatus_ID,
                    AStatus_name = auctionBusinessLayer.GetAStatusName(auction.AStatus_ID), // Method to retrieve the auction status name based on the ID
                    ImagePath = imagePath,
                    UserID = auction.UserID,
                    Property = property
                };

                auctionViewModels.Add(auctionViewModel);
            }

            // Pass additional data to the view
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            // Pass the auctionViewModels to the view
            return View(auctionViewModels);
        }

        // GET: Auction/Create
        public ActionResult Create()
        {
            AuctionViewModel auctionViewModel = new AuctionViewModel();

            AuctionBuislayer auctionBuislayer = new AuctionBuislayer();
            string userName = Session["UserName"].ToString();
            auctionViewModel.PropertyList = auctionBuislayer.GetPropertyList(userName);

            return View(auctionViewModel);
        }

        // POST: Auction/Create
        [HttpPost]
        public ActionResult Create(AuctionViewModel AuctionViewModel)
        {
            if (AuctionViewModel.Bid_start < DateTime.Now)
            {
                ModelState.AddModelError("Bid_start", "Bid start date must be greater than or equal to the current date.");
            }

            if (AuctionViewModel.Bid_End <= AuctionViewModel.Bid_start)
            {
                ModelState.AddModelError("Bid_End", "Bid end date must be greater than the bid start date.");
            }

            if (ModelState.IsValid)
            {
                AuctionBuislayer auctionBuislayer = new AuctionBuislayer();

                Auction auction = new Auction
                {
                    Bid_Name = AuctionViewModel.Bid_Name,
                    Start_price = AuctionViewModel.Start_price,
                    Bid_start = AuctionViewModel.Bid_start,
                    Bid_End = AuctionViewModel.Bid_End,
                    lowest_bidding_price = AuctionViewModel.lowest_bidding_price,
                    AStatus_ID = 1,
                    Pro_id = AuctionViewModel.Pro_id,
                };

                auctionBuislayer.AddAuction(auction);
                return RedirectToAction("Index");
            }
            ViewBag.ErrorMessage = "Please enter the correct auction details.";



            // If the model state is invalid, return to the create view with validation errors
            AuctionViewModel.PropertyList = new AuctionBuislayer().GetPropertyList(Session["UserName"].ToString());

            return View(AuctionViewModel);
        }
    }
}

