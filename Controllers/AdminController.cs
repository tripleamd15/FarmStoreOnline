using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FarmStore0906.Models;
using System.IO;
using System.Data.Entity.Validation;

namespace FarmStore0906.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            List<Fruit> allFruits = null;

            using (FarmstoreEntities dbContext=new FarmstoreEntities ())
            {
                allFruits = dbContext.Fruits.ToList();
            }
            return View(allFruits);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Fruit fruit)
        {
             using (FarmstoreEntities dbContext = new FarmstoreEntities())
            {
                
                dbContext.Fruits.Attach(fruit);
                dbContext.Entry(fruit).State = System.Data.Entity.EntityState.Added;
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index", new { pageNumber = 1 });  
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Fruit targetFruit = null;
            using (FarmstoreEntities dbContext = new FarmstoreEntities())
            {
                targetFruit = dbContext.Fruits.SingleOrDefault(m => m.Id == id);
            }
            return View(targetFruit);
        }

        [HttpPost]
        public ActionResult Edit(Fruit fruit)
        {

            using (FarmstoreEntities dbContext = new FarmstoreEntities())
            {
                if (this.Request.Files != null && this.Request.Files.Count > 0 &&  //4 for upload file
                    this.Request.Files[0].ContentLength > 0 &&
                    this.Request.Files[0].ContentLength < 1024 * 120)  //file size less than 120K
                {

                    string fileName = Path.GetFileName(this.Request.Files[0].FileName);
                    string pathOfWebsite = "~/Images/Fruits/" + fileName;
                    fruit.FruitImagePath = pathOfWebsite;
                    this.Request.Files[0].SaveAs(this.Server.MapPath(pathOfWebsite));

                }
               
                    dbContext.Fruits.Attach(fruit);
                    dbContext.Entry(fruit).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
               
            }
            return RedirectToAction("Index", new { pageNumber = 1 });  //4 for paging
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            Fruit fruit = null;
            using (FarmstoreEntities dbContext = new FarmstoreEntities())
            {
                fruit = dbContext.Fruits.SingleOrDefault(f => f.Id == id);
                dbContext.Fruits.Remove(fruit);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index", new { pageNumber = 1 });
        }
    }
}