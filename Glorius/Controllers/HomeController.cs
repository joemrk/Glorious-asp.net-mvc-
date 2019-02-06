using Glorius.Models;
using Glorius.Models.Data;
using Glorius.Models.ViewModels;
using Glorius.Models.ViewModels.Shop;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Glorius.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(); // зробити головану стор1нку з слайдером, популярними 1 новими
        }

        [HttpGet]
        public ActionResult Products(int? page, int? catId, int? matId, int? secId)
        {
            CartLong();

            List<ProductVM> productsList;
            List<CategoryVM> categoriesList;

            var pageNumber = page ?? 1;

            using (Db db = new Db())
            {
                productsList = db.Products.ToArray()
                    .Where(x => (catId == null || catId == 0 || x.CategoryId == catId)
                             && (matId == null || matId == 0 || x.MaterialId == matId)
                             && (secId == null || secId == 0 || x.SectionId == secId))
                    .Select(x => new ProductVM(x))
                    .ToList();

                categoriesList = db.Categories.ToArray()
                    .Where(x => (secId == null || secId == 0 || x.SectionId == secId))
                    .Select(x => new CategoryVM(x))
                    .ToList();

                ViewBag.Categories = new SelectList(categoriesList, "Id", "Name");
                ViewBag.Materials = new SelectList(db.Materials.ToList(), "Id", "Name");
                ViewBag.Sections = new SelectList(db.Sections.ToList(), "Id", "Name");

                ViewBag.SelectedSec = secId.ToString();
                ViewBag.SelectedCat = catId.ToString();
                ViewBag.SelectedMat = matId.ToString();
            }
            var onePageOfProducts = productsList.ToPagedList(pageNumber, 20);
            ViewBag.OnePageOfProducts = onePageOfProducts;

            return View(productsList);
        }

        [HttpGet]
        [Route("product/{pc}")]
        public ActionResult GetProduct(string pc)
        {
            CartLong();
            //00 00 0000
            int id = Convert.ToInt32(pc.Substring(4));

            ProductVM product;
            using (Db db = new Db())
            {
                ProductDTO dto = db.Products.Find(id);
                product = new ProductVM(dto);
            }

            List<GalleryVM> gallery;
            List<int> ids = new List<int>();
            using (Db db = new Db())
            {
                gallery = db.ProductGallery
                            .ToArray()
                            .Where(x => x.ProductId == id)
                            .Select(x => new GalleryVM(x))
                            .ToList();
            }

            foreach (var item in gallery)
            {
                ids.Add(item.Id);
            }
            ViewBag.Ids = ids;

            return View(product);
        }

        public ActionResult Search(int? page, string str)
        {
            List<ProductVM> res;
            var pageNumber = page ?? 1;

            var strCheck = !(str.Contains('{') ||
                           str.Contains('}') ||
                           str.Contains('<') ||
                           str.Contains('>') ||
                           str.Contains('&') ||
                           str.Contains('#') ||
                           str.Contains('$'));

            if (strCheck)
            {
                using (Db db = new Db())
                {
                    res = db.Products
                            .ToArray()
                            .Where(x => x.Name.Contains(str) || x.ProductCode.Contains(str))
                            .Select(x => new ProductVM(x))
                            .ToList();
                }

                var onePageOfProducts = res.ToPagedList(pageNumber, 16);
                ViewBag.OnePageOfProducts = onePageOfProducts;
                return PartialView(res);
            }
            else return View();
        }

        void CartLong()
        {
            int counter = 0;
            var vr = new CartVM
            {
                Cart = GetCart(),
            };

            foreach (var item in vr.Cart.Lines)
            {
                counter += item.Quantity;
            }
            ViewBag.CartLong = counter.ToString();
        }

        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }

        //for preview image
        public FileContentResult GetPreview(int id)
        {
            using (Db db = new Db())
            {
                ProductDTO img = db.Products.Find(id);

                if (img != null)
                {
                    return File(img.Img, img.ImgType);
                }
                else
                {
                    return null;
                }
            }
        }

        //for gallery image
        public FileContentResult GetGallery(int id)
        {
            using (Db db = new Db())
            {
                GalleryDTO img = db.ProductGallery.Find(id);

                if (img != null)
                {
                    return File(img.Img, img.ImgType);
                }
                else
                {
                    return null;
                }
            }
        }


        public JsonResult CheckString(string str)
        {
            var result = !(str.Contains('{') ||
                           str.Contains('}') ||
                           str.Contains('<') ||
                           str.Contains('>') ||
                           str.Contains('&') ||
                           str.Contains('#') ||
                           str.Contains('$'));

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult buyPost()
        {
            CartLong();
            return View();
        }
        public ActionResult Agreement()
        {
            CartLong();
            return View();
        }

        public ActionResult Sketch()
        {
            CartLong();
            return View();
        }
        public ActionResult Contacts()
        {
            CartLong();
            return View();
        }

        public ActionResult payments()
        {
            CartLong();
            return View();
        }
        public ActionResult About()
        {
            using (Db db = new Db())
            {
                ViewBag.ProductCounter = db.Products.Count();
                ViewBag.OrderCounter = 23;
            }

            CartLong();
            return View();
        }
        public ActionResult Book()
        {
            CartLong();
            return View();
        }
        public ActionResult Warranty()
        {
            CartLong();
            return View();
        }

        public ActionResult Terms()
        {
            CartLong();
            return View();
        }
    }
}