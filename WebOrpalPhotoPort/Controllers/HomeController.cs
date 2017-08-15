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
                        IsDeleted = (udc.IsDeleted) ? "Заблокирован" : "Активен"
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
            ActionResult ar;

            using (var webDbService = new WebOrpalDbService.WebDbServiceClient())
            {
                var users = webDbService.GetUsers();
                var u = users.FirstOrDefault(x => x.id == id);

                if (u != null)
                {
                    ViewBag.curUser = u.Name;
                    ar = View();
                }
                else
                {
                    TempData["ErrorDesc"] = "Ошибка! Пользователь не найден.";
                    ar = RedirectToAction("ErrorPage");
                }
            }

            return ar;
        }

        public ActionResult ErrorPage()
        {
            ViewBag.ErrorDesc = TempData["ErrorDesc"];
            return View();
        }

        public ActionResult AddUser(Models.User user)
        {
            // @Html.DropDownList("Special", ViewBag.Users as SelectList, new { name = "Special", value = "@c.Id" })
            //SelectList statuses = new SelectList(new string[] { "Активный", "Заблокирован" }, "StatusId", " Status", "0");
            //ViewBag.Statuses = statuses;

            //List<SelectListItem> items = new List<SelectListItem>();
            //items.Add(new SelectListItem { Text = "Активный", Value = "0", Selected = true });
            //items.Add(new SelectListItem { Text = "Заблокирован", Value = "1" });

            //ViewBag.Statuses = items;

            return View(user);
        }

        [HttpPost]
        public ActionResult SaveUser(Models.User user)
        {
            if (ModelState.IsValid)
            {
                // TODO:
                return RedirectToAction("Index");
            }

            user.Name = "Qwerty";

            return RedirectToAction("AddUser");
        }
    }
}