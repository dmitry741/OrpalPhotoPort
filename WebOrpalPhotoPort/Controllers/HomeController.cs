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
                        ActiveStatus = (udc.ActiveStatus == 0) ? "Активный" : "Заблокирован"
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
                        RegDateTime = u.RegDateTime,
                        Role = u.Role.ToString(),
                        ActiveStatus = u.ActiveStatus.ToString()
                    };

                    ViewBag.CollectionRoles = Code.CommnonCollections.GetCollectionRoles(u.Role == 0, u.Role != 0);
                    ViewBag.CollectionStatuses = Code.CommnonCollections.GetCollectionStatuses(u.ActiveStatus == 0, u.ActiveStatus != 0);

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
        /// Add user
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
                    };

                    int r;

                    // role
                    int.TryParse(model.Role, out r);
                    udc.Role = r;

                    // status
                    int.TryParse(model.ActiveStatus, out r);
                    udc.ActiveStatus = r;

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
                        udc.RegDateTime = model.RegDateTime;

                        if (!webDbService.EditUser(udc))
                        {
                            ViewBag.errorEditMessage = $"Не удалось сохранить профайл для {model}. Попробуйте сделать это чуть позже.";
                            return View("EditUser", model);
                        }
                    }
                }

                return RedirectToAction("Index");
            }

            if (bAdd)
            {
                // TODO: for adding of user
            }
            else
            {
                ViewBag.CollectionRoles = Code.CommnonCollections.GetCollectionRoles(model.Role == "0", model.Role != "0");
                ViewBag.CollectionStatuses = Code.CommnonCollections.GetCollectionStatuses(model.ActiveStatus == "0", model.ActiveStatus != "0");
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