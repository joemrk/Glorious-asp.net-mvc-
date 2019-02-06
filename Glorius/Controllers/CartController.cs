using Glorius.Models;
using Glorius.Models.Data;
using Glorius.Models.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace Glorius.Controllers
{
    public class CartController : Controller
    {
        [HttpGet]
        public ViewResult Cart()
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

            return View(new CartVM
            {
                Cart = GetCart(),
            });
        }

        [HttpPost]
        public ActionResult Add(int id, int a, int amount = 1)
        {
            if (amount > 0)
            {
                using (Db db = new Db())
                {
                    ProductDTO product = db.Products.Find(id);
                    GetCart().Add(product, amount);
                }
                if (a == 1)
                {
                    //return RedirectToAction("Cart", "Cart");
                    return Redirect("/cart");
                }
                else
                {
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            else
            {
                return Redirect("/cart");
            }            
        }

        public void Edit(int id, int amount)
        {
            if (amount > 0)
            {
                using (Db db = new Db())
                {
                    ProductDTO product = db.Products.Find(id);
                    GetCart().Edit(product, amount);
                }
            }
        }

        public ActionResult Del(int id)
        {
            GetCart().Del(id);
            return Redirect("/cart");
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

        [HttpPost]
        public ActionResult Buy(string name, string num, string email, string city, string post, string adres, string note)
        {
            #region  shitCode, FIXED /////////////////////////////////////////
            int conrtrol = 0;

            List<string> model = new List<string>();

            model.Add(name);
            model.Add(num);
            model.Add(email);
            model.Add(city);
            model.Add(post);
            model.Add(adres);
            model.Add(note);

            foreach (string item in model)
            {
                bool result = !(item.Contains("{") ||
                                item.Contains("}") ||
                                item.Contains("<") ||
                                item.Contains(">") ||
                                item.Contains("&") ||
                                item.Contains("#") ||
                                item.Contains("$"));
                if (!result)
                {
                    conrtrol += 1;
                }
            }
            #endregion
            if (conrtrol == 0)
            {
                string str = null;
                foreach (var item in GetCart().Lines)
                {
                    str = str + item.Product.ProductCode + " - " + item.Product.Name + " = " + item.Quantity + " шт" + "\n";
                }
                str = str + "Итог: " + GetCart().TotalSum() + "грн";
                MailAddress from = new MailAddress("glorius.order@gmail.com", "Glorius new order");
                MailAddress to = new MailAddress("porka23@i.ua");

                using (MailMessage message = new MailMessage(from, to))
                using (SmtpClient smtp = new SmtpClient())
                {
                    message.Subject = "Новый заказ";
                    message.Body = str + "\n" + "\n" + name + "\n" + "\n" + "380" + num + "\n" + "\n" + email + "\n" + "\n" + city + "\n" + "\n" + post + "\n" + "\n" + adres + "\n" + "\n" + note;

                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(from.Address, "gloriusorder7878");

                    smtp.Send(message);
                }
                GetCart().Clear();

                return RedirectToAction("buyPost", "Home");
            } else{

                ModelState.AddModelError("", "Введен запрещенный символ");
                return Redirect("/cart");
            }
        }

        [HttpGet]
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
    }
}