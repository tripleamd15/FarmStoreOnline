using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FarmStore0906.Models;

namespace FarmStore0906.Controllers
{
    public class NavController : Controller
    {
        // GET: Nav: Nav controller on Nov 12 2015 
        public PartialViewResult Menu(string category=null)
        {
            FarmstoreEntities dbContext = new FarmstoreEntities();
            
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = dbContext.Fruits.Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);       
        }

        
    }
}