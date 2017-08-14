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

            var webDbService = new WebOrpalDbService.WebDbServiceClient();
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

            return View(model);
        }
    }
}