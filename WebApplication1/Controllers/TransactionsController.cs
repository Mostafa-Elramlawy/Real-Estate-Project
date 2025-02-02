using ClassLibrary2;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PP1.Controllers
{

    public class TransactionsController : Controller
    {
        public ActionResult Index()

        {
            TransactionsBuislayer transactionsBuislayer = new TransactionsBuislayer();
            List<Transactions> transactions = transactionsBuislayer.Transactions.ToList();
            return View(transactions);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            AuctionBuislayer auctionBuislayer = new AuctionBuislayer();
            Auction auction = new Auction();
            int lowestBiddingPrice = auctionBuislayer.GetLowestBiddingPriceById(id); // Retrieve the lowest bidding price

            TransactionsBuislayer transactionsBuislayer = new TransactionsBuislayer();

            int userId = (int)Session["UserID"];
            ViewBag.UserID = userId;
            Property property = transactionsBuislayer.GetPropertyByAuctionID(id); // Retrieve the property details
            if (property == null)
            {
                // Handle the case when the property is not found
                // For example, you can redirect to an error page or return an appropriate view
                return RedirectToAction("NotFound");
            }
            DateTime bidEnd = auctionBuislayer.GetAuctionBidEnd(id); // Retrieve the Bid_End value of the auction
            TimeSpan timeRemaining = bidEnd - DateTime.Now;
            TransactionsViewModel transactionsViewModel = new TransactionsViewModel
            {
                Property = property,
                HighestPrice = transactionsBuislayer.GetMaxPrice(id),
                TimeRemaining = timeRemaining,
                lowest_bidding_price = lowestBiddingPrice,
                ImagePath = transactionsBuislayer.GetImagePathByAuctionID(id),


                // Other properties initialization
            };

            return View(transactionsViewModel);

        }

        [HttpPost]
        public ActionResult Create(int id, TransactionsViewModel TransactionsViewModel)
        {
            TransactionsBuislayer transactionsBuislayer = new TransactionsBuislayer();
            AuctionBuislayer auctionBuislayer = new AuctionBuislayer();

            int maxPrice = transactionsBuislayer.GetMaxPrice(id);
            int lowestBiddingPrice = auctionBuislayer.GetLowestBiddingPriceById(id);
            int startprice = auctionBuislayer.GetStartPriceById(id);

            Property property = transactionsBuislayer.GetPropertyByAuctionID(id);
            if (property == null)
            {
                // Handle the case when the property is not found
                // For example, you can redirect to an error page or return an appropriate view
                return RedirectToAction("NotFound");
            }

            TransactionsViewModel.Property = property;

            if (TransactionsViewModel.transactions.Pro_Price < startprice + lowestBiddingPrice)
            {
                ModelState.AddModelError("TransactionsViewModel.transactions.Pro_Price", "Bidding Price must be greater than or equal to the sum of the start price and lowest bidding price.");
            }

            if (TransactionsViewModel.transactions.Pro_Price < maxPrice + lowestBiddingPrice)
            {
                ModelState.AddModelError("TransactionsViewModel.transactions.Pro_Price", "Bidding Price must be greater than or equal to the sum of the lowest bidding price and Highest Price.");
            }

            if (ModelState.IsValid)
            {
                Transactions transactions = new Transactions
                {
                    Pro_Price = TransactionsViewModel.transactions.Pro_Price,
                    Transaction_date = DateTime.Now,
                    AuctionID = id,
                    UserID = (int)Session["UserID"]
                };

                transactionsBuislayer.AddTransaction(transactions);

                return RedirectToAction("Index", "Auction");
            }

            // Retrieve the UserID from TempData
            int userId = (int)Session["UserID"];
            ViewBag.UserID = userId;

            return View(TransactionsViewModel);
        }

        public ActionResult result(int id)
        {

            TransactionsBuislayer transactionsBuislayer = new TransactionsBuislayer();
            List<Transactions> transactions = transactionsBuislayer.Transactions1(id).ToList();
            return View(transactions);
        }

    }

}




