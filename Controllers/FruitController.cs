using FarmStore0906.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmStore0906.Controllers
{
    public class FruitController : Controller
    {
        public ViewResult Introduction()
        {
            return View();
        }

        public ViewResult Contact()
        {
            return View();
        }
        
        // GET: Fruit
        public ActionResult Show(string category, int page=1)  //2 for paging
        {
            int fruitPerPage = 6;  
            using (FarmstoreEntities dbContext=new FarmstoreEntities ())
            {
                ProductListViewModel model = new ProductListViewModel
                {
                    AllFruits = dbContext.Fruits.Where(p=>category==null||p.Category==category).OrderBy(p => p.Id).
                    Skip(fruitPerPage * (page - 1)).Take(fruitPerPage).ToList(),

                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = fruitPerPage,
                        TotalItems = category == null ?   //take the page of current category
                           dbContext.Fruits.Count() :
                           dbContext.Fruits.Where(e => e.Category == category).Count()

                    },
                    CurrentCategory = category
                };
                
                return View(model);
            }
        }
       
        public ActionResult Search(/*string searchBy,*/string search,string category,int page=1)
        {
            int fruitPerPage = 6;
            using (FarmstoreEntities dbContext = new FarmstoreEntities())
            {
                double searchvalue = 0;
                bool isSearchNumber = double.TryParse(search, out searchvalue);
                if (isSearchNumber)
                {              
                    ProductListViewModel model = new ProductListViewModel
                    {
                        AllFruits = dbContext.Fruits.Where(
                           x => x.Price.ToString() == search).OrderBy(p => p.Id).
                        Skip(fruitPerPage * (page - 1)).Take(fruitPerPage).ToList(),
                        
                        PagingInfo = new PagingInfo
                        {
                            CurrentPage = page,
                            ItemsPerPage = 8,
                            TotalItems =
                               dbContext.Fruits.Where(x => x.Price == searchvalue).Count()

                        },
                        CurrentCategory = category
                    };
                   
                    return View(model);
                }
                else
                {                  
                    ProductListViewModel model = new ProductListViewModel
                    {
                        AllFruits = dbContext.Fruits.Where(
                                    x=>x.Name.StartsWith(search)||search==null).OrderBy(p => p.Id).
                        Skip(fruitPerPage * (page - 1)).Take(fruitPerPage).ToList(),

                        PagingInfo = new PagingInfo
                        {
                            CurrentPage = page,
                            ItemsPerPage = fruitPerPage,
                            TotalItems =dbContext.Fruits
                               .Where(x => x.Name.StartsWith(search) || search == null).Count()
                        },
                        CurrentCategory = category
                    };
                    
                    //allFruits = dbContext.GetFruitsOfPage(fruitPerPage, pageNumber).ToList();
                    //ViewBag.MaxPage = dbContext.Fruits.Count() / fruitPerPage + 1;
                    return View(model);
                }
            }
        }
    }
}