using MVCSepet.CustomTools;
using MVCSepet.DesignPatterns.SingletonPattern;
using MVCSepet.Models;
using MVCSepet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSepet.Controllers
{
    public class ShoppingController : Controller
    {
        NorthwindEntities _db;
        public ShoppingController()
        {
            _db = DBTool.DBInstance;
        }
        public ActionResult ProductList()
        {
            ShoppingVM svm = new ShoppingVM
            {
                Products = _db.Products.ToList()
            };
            return View(svm);
        }

        public ActionResult AddToCart(int id)
        {
            Cart cart = Session["scart"] == null ? new Cart() : Session["scart"] as Cart;

            Product eklenecekUrun = _db.Products.Find(id);

            CartItem ci = new CartItem();
            ci.ProductName = eklenecekUrun.ProductName;
            ci.ID = eklenecekUrun.ProductID;
            ci.UnitPrice = eklenecekUrun.UnitPrice;

            cart.SepeteEkle(ci);

            Session["scart"] = cart;

            TempData["mesaj"] = $"{ci.ProductName} sepete eklenmiştir";
            return RedirectToAction("ProductList");

        }
        public ActionResult SepetSayfasi()
        {
            if(Session["scart"] != null)
            {
                Cart c = Session["scart"] as Cart;
                return View(c);
            }
            ViewBag.SepetBos = "Sepetinizde ürün bulunmamaktadır";
            return View();
        }
        public ActionResult SepettenCikar(int id)
        {
            if(Session["scart"] != null)
            {
                Cart c = Session["scart"] as Cart;
                c.SepettenSil(id);
                if(c.Sepetim.Count== 0)
                {
                    Session.Remove("scart");
                    TempData["mesaj"] = "Sepetiniz bosaltılmıstır";
                    return RedirectToAction("ProductList");
                }
                return RedirectToAction("SepetSayfasi"); //direkt sepetinizde ürün bulunmamaktadır mesajına gider
            }
            return RedirectToAction("ProductList");
        }
    }
}