using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Glorius.Models.Data;
using Glorius.Models.ViewModels;
using Newtonsoft.Json.Linq;

namespace Glorius.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            bool valid = Validator(model.Log) && Validator(model.Pass);

            using (Db db = new Db())
            {
                string sacretKey;

                LoginDTO loginModel = db.Logins.Find(11);
                sacretKey = loginModel.Log.ToString();

                var response = Request["g-recaptcha-response"];
                var client = new WebClient();
                var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?sacret={0}&response={1}", sacretKey, response));
                var obj = JObject.Parse(result);
                var status = (bool)obj.SelectToken("success");

                if (valid /*&& status*/)
                {
                    LoginVM vm;

                    LoginDTO dto = db.Logins.Find(9);
                    vm = new LoginVM(dto);

                    if (model.Log.ToString() == vm.Log.ToString() && model.Pass.ToString() == vm.Pass.ToString())
                    {
                        TempData["user"] = model.Log.ToString();

                        FormsAuthentication.SetAuthCookie(model.Log.ToString(), true);
                        return Redirect("/admin/shop/products/");
                    }
                    else
                    {
                        TempData["SM"] = "Неправильный логин или пароль";
                        return RedirectToAction("Login", "Account");
                    }
                }
                else
                {
                    TempData["SM"] = "Введен запрещенный символ";
                    return View();
                }
            }
        }

        public bool Validator(string str)
        {
            return !(str.Contains('{') ||
                     str.Contains('}') ||
                     str.Contains('<') ||
                     str.Contains('>') ||
                     str.Contains('&') ||
                     str.Contains('#') ||
                     str.Contains('$'));
        }

        public ActionResult Exit()
        {
            FormsAuthentication.SignOut();
            TempData["user"] = null;
            return Redirect("/account/login");
        }
    }
}
