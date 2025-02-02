using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary2;

namespace YourNamespace.Controllers
{
    public class CMController : Controller
    {
        private CraftsMenBusinessLayer craftsMenBusinessLayer;
        private string connectionString;

        public CMController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["CraftsMen12Context"].ConnectionString;
            craftsMenBusinessLayer = new CraftsMenBusinessLayer(connectionString);
        }

        public ActionResult Index()
        {
            IEnumerable<CraftsMen2> craftsmen = craftsMenBusinessLayer.CraftsMens;

            return View(craftsmen);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CraftsMen2 craftsman)
        {
            craftsMenBusinessLayer.AddCraftsMen(craftsman);
            IEnumerable<CraftsMen2> craftsmen = craftsMenBusinessLayer.CraftsMens;

            return View("Index", craftsmen);
        }


        public ActionResult Edit(int id)
        {
            var craftsman = craftsMenBusinessLayer.CraftsMens.FirstOrDefault(c => c.CM_ID == id);
            if (craftsman == null)
            {
                return HttpNotFound();
            }

            return View(craftsman);
        }

        [HttpPost]
        public ActionResult Edit(CraftsMen2 craftsman)
        {
            if (ModelState.IsValid)
            {
                craftsMenBusinessLayer.UpdateCraftsMen(craftsman);
                return RedirectToAction("Index", "Admin");
            }

            return View(craftsman);
        }

        public ActionResult Delete(int id)
        {
            var craftsman = craftsMenBusinessLayer.CraftsMens.FirstOrDefault(c => c.CM_ID == id);
            if (craftsman == null)
            {
                return HttpNotFound();
            }

            return View(craftsman);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            craftsMenBusinessLayer.DeleteCraftsMen(id);
            return RedirectToAction("Index", "Admin");
        }
    }
}
