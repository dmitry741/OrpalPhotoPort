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
            var list = new List<Models.User>();

            Models.User user1 = new Models.User
            {
                id = 1,
                Name = "Орк-Правдерез",
                Email = "dmitrypavlov74@gmail.com",
                Login = "orc",
                Password = "12345",
                Role = 1            
            };

            Models.User user2 = new Models.User
            {
                id = 2,
                Name = "Паладин Света",
                Email = "sergey.suloev@gmail.com",
                Login = "паладин",
                Password = "12345",
                Role = 1
            };

            list.Add(user1);
            list.Add(user2);

            return View(list);
        }
    }
}