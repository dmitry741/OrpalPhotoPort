using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebOrpalPhotoPort.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// View the main page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = new List<Models.User>();

            using (var webDbService = new WebOrpalDbService.WebDbServiceClient())
            {
                var users = webDbService.GetUsers();

                // маппинг WebOrpalDbService.UserDataContract на Models.User
                foreach (WebOrpalDbService.UserDataContract udc in users)
                {
                    model.Add(new Models.User
                    {
                        id = udc.id,
                        Name = udc.Name,
                        Email = udc.Email,
                        Login = udc.Login,
                        Password = udc.Password,
                        Role = (udc.Role == 0) ? "Пользователь" : "Админ",
                        RegDateTime = udc.RegDateTime.ToLongDateString(),
                        IsDeleted = udc.IsDeleted
                    }
                    );
                }
            }

            return View(model);
        }

        /// <summary>
        /// edit user
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        public ActionResult EditUser(int? id)
        {
            ActionResult actionResult;

            using (var webDbService = new WebOrpalDbService.WebDbServiceClient())
            {
                var users = webDbService.GetUsers();
                var u = users.FirstOrDefault(x => x.id == id);

                if (u != null)
                {
                    ViewBag.curUser = u.Name;
                    actionResult = View();
                }
                else
                {
                    TempData["ErrorDesc"] = "Ошибка! Пользователь не найден.";
                    actionResult = RedirectToAction("ErrorPage");
                }
            }

            return actionResult;
        }

        public ActionResult ErrorPage()
        {
            ViewBag.ErrorDesc = TempData["ErrorDesc"];
            return View();
        }

        public ActionResult AddUser()
        {
            ViewBag.CollectionStatuses = Code.CommnonCollections.CollectionStatuses;
            ViewBag.CollectionRoles = Code.CommnonCollections.CollectionRoles;

            return View();
        }

        [HttpPost]
        public ActionResult SaveUser(Models.User model)
        {
            model.RegDateTime = DateTime.Now.ToShortDateString();

            string[] fields = new string[] { "Name", "Login", "Password", "Email" };

            if (IsModelValid(fields))
            {
                using (var webDbService = new WebOrpalDbService.WebDbServiceClient())
                {
                    //  mapping model on UserDataContract
                    OrpalPhotoPort.Domain.DataContractMemebers.UserDataContract udc = new OrpalPhotoPort.Domain.DataContractMemebers.UserDataContract
                    {
                        // TODO:
                    };

                }

                return RedirectToAction("Index");
            }

            return View("AddUser", model);
        }

        bool IsModelValid(string[] fields)
        {            
            bool result = true;

            foreach (string f in fields)
            {
                if (!ModelState.IsValidField(f))
                {
                    result = false;
                    break;
                }                
            }

            return result;
        }
    }
}