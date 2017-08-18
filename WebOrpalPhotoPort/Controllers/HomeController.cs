using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrpalPhotoPort.Domain.DataContractMemebers;

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

                // mapping UserDataContract on Models.User
                foreach (UserDataContract udc in users)
                {
                    model.Add(new Models.User
                    {
                        id = udc.id,
                        Name = udc.Name,
                        Email = udc.Email,
                        Login = udc.Login,
                        Password = udc.Password,
                        Role = (udc.Role == 0) ? "Пользователь" : "Админ",
                        RegDateTime = udc.RegDateTime,
                        IsDeleted = udc.IsDeleted
                    }
                    );
                }
            }

            return View(model);
        }

        /// <summary>
        /// Edit user
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

                    Models.User model = new Models.User
                    {
                        id = u.id,
                        Name = u.Name,
                        Email = u.Email,
                        Login = u.Login,
                        Password = u.Password,
                        Role = (u.Role == 0) ? "Пользователь" : "Админ",
                        RegDateTime = u.RegDateTime,
                        IsDeleted = u.IsDeleted
                    };

                    actionResult = View(model);
                }
                else
                {
                    TempData["ErrorDesc"] = "Ошибка! Пользователь не найден.";
                    actionResult = RedirectToAction("ErrorPage");
                }
            }

            return actionResult;
        }

        /// <summary>
        /// View common error page
        /// </summary>
        /// <returns></returns>
        public ActionResult ErrorPage()
        {
            ViewBag.ErrorDesc = TempData["ErrorDesc"];
            return View();
        }

        /// <summary>
        /// add user
        /// </summary>
        /// <returns></returns>
        public ActionResult AddUser()
        {
            ViewBag.CollectionStatuses = Code.CommnonCollections.CollectionStatuses;
            ViewBag.CollectionRoles = Code.CommnonCollections.CollectionRoles;

            return View();
        }

        /// <summary>
        /// Save user in database via WebOrpalDbService.WebDbServiceClient
        /// </summary>
        /// <param name="model">User model</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveUser(Models.User model)
        {
            string[] fields = new string[] { "Name", "Login", "Password", "Email" };
            bool bAdd = (model.id == 0);

            if (IsModelValid(fields))
            {
                using (var webDbService = new WebOrpalDbService.WebDbServiceClient())
                {
                    //  mapping Models.User on UserDataContract
                    UserDataContract udc = new UserDataContract
                    {
                        id = model.id,
                        Name = model.Name,
                        Email = model.Email,
                        Login = model.Login,
                        Password = model.Password,                        
                        Role = (model.Role == "Админ") ? 1 : 0,
                        IsDeleted = model.IsDeleted
                    };

                    if (bAdd)
                    {
                        udc.RegDateTime = DateTime.Now;

                        if (!webDbService.AddUser(udc))
                        {
                            ViewBag.errorEditMessage = $"Не удалось добавить пользователя {model}. Попробуйте сделать это чуть позже.";
                            return View("AddUser", model);
                        }
                    }
                    else // edit
                    {
                        if (!webDbService.EditUser(udc))
                        {
                            ViewBag.errorEditMessage = $"Не удалось сохранить профайл для {model}. Попробуйте сделать это чуть позже.";
                            return View("EditUser", model);
                        }
                    }
                }

                return RedirectToAction("Index");
            }

            return (bAdd) ? View("AddUser", model) : View("EditUser", model);
        }

        /// <summary>
        /// Model validation
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
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