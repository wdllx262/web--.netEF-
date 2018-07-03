using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
namespace lab6_2.Areas.admin.Controllers
{
    public class ShopController : Controller
    {
        // GET: admin/Shop

        [Authorize]
        public ActionResult Index(String search="",String order="")
        {
            lab6_2.Areas.admin.Models.shopEntities db = new Models.shopEntities();
            List<lab6_2.Areas.admin.Models.Product> lst = null;
            if (search == "")
            {
                switch (order)
                {
                    case "Id":
                        lst = db.Product.OrderBy(m => m.Id).ToList();
                        break;
                    case "Name":
                        lst = db.Product.OrderBy(m => m.Name).ToList();
                        break;
                    case "Price":
                        lst = db.Product.OrderBy(m => m.Price).ToList();
                        break;
                    default:
                        lst = db.Product.OrderBy(m => m.StockNum).ToList();
                        break;

                }
            }
            else
            {
                switch (order)
                {
                    case "Id":
                        lst = db.Product.Where(m => m.Name.Contains(search)).OrderBy(m => m.Id).ToList();
                        break;
                    case "Name":
                        lst = db.Product.Where(m => m.Name.Contains(search)).OrderBy(m => m.Name).ToList();
                        break;
                    case "Price":
                        lst = db.Product.Where(m => m.Name.Contains(search)).OrderBy(m => m.Price).ToList();
                        break;
                    default:
                        lst = db.Product.Where(m => m.Name.Contains(search)).OrderBy(m => m.StockNum).ToList();
                        break;

                }
            }
            ViewBag.lst = lst;
            ViewBag.search = search;
            return View();
        }
        [Authorize]

        public ActionResult Add()
        {
            return View();
        }
        public ActionResult AddSave(string Price, string ProductName, string Amount)
        {

            var file = Request.Files["pic"];
            String path =Server.MapPath("/upload/");
            String filepath = Guid.NewGuid().ToString() + ".jpg";
            file.SaveAs(path+filepath);
            lab6_2.Areas.admin.Models.Product item = new Models.Product();
            item.Name = ProductName;
            item.Price = Convert.ToInt32(Price);
            item.StockNum = Convert.ToInt32(Amount);
            item.Pic = filepath;
            lab6_2.Areas.admin.Models.shopEntities db =
                new lab6_2.Areas.admin.Models.shopEntities();
            db.Product.Add(item);
            db.SaveChanges();
            return RedirectToAction("/admin/Shop/Index");
        }
        public ActionResult Del(int id)
        {
            var product = new lab6_2.Areas.admin.Models.Product() { Id= id };
            lab6_2.Areas.admin.Models.shopEntities db = new Models.shopEntities();
            db.Product.Attach(product);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("/admin/Shop/Index");
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            lab6_2.Areas.admin.Models.shopEntities db = new Models.shopEntities();
            lab6_2.Areas.admin.Models.Product item = db.Product.Find(id);
            ViewBag.item = item;
            return View();
        }
        public ActionResult EditSave(decimal Price, string ProductName, int Amount,int ProductId)
        {

            lab6_2.Areas.admin.Models.shopEntities db = new Models.shopEntities();
            lab6_2.Areas.admin.Models.Product item = db.Product.Find(ProductId);
            item.Name = ProductName;
            item.Price = Price;
            item.StockNum = Amount;
            var file = Request.Files["pic"];
            if (file.ContentLength!=0)
            {
                String path = Server.MapPath("/upload/");
                String filepath = Guid.NewGuid().ToString() + ".jpg";
                System.IO.File.Delete(path+item.Pic);
                file.SaveAs(path + filepath);
                item.Pic = filepath;
            }
            db.SaveChanges();
            return RedirectToAction("/admin/Shop/Index");
        }
    }
}