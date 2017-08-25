using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrpalPhotoPort.Domain.DataContractMemebers;
using AutoMapper;

namespace WebOrpalPhotoPort.Controllers
{
    public class HomeController : Controller
    {
        IMapper m_mapperIndex;
        IMapper m_mapperEdit;
        IMapper m_mapperSave;

        public HomeController()
        {
            // Index
            MapperConfiguration mapConfig1 = new MapperConfiguration(cfg => cfg.CreateMap<UserDataContract, Models.User>()
                .ForMember("Role", opt => opt.MapFrom(udc => (udc.Role == 0) ? Properties.Resources.SimpleUser : Properties.Resources.Admin))
                .ForMember("ActiveStatus", opt => opt.MapFrom(udc => (udc.ActiveStatus == 0) ? Properties.Resources.Active : Properties.Resources.Banned)));

            m_mapperIndex = mapConfig1.CreateMapper();

            // Edit
            MapperConfiguration mapConfig2 = new MapperConfiguration(cfg => cfg.CreateMap<UserDataContract, Models.User>()
                .ForMember("Role", opt => opt.MapFrom(udc => udc.Role.ToString()))
                .ForMember("ActiveStatus", opt => opt.MapFrom(udc => udc.ActiveStatus.ToString())));

            m_mapperEdit = mapConfig2.CreateMapper();

            // Save
            MapperConfiguration mapConfig3 = new MapperConfiguration(cfg => cfg.CreateMap<Models.User, UserDataContract>()
                .ForMember("Role", opt => opt.MapFrom(model => Convert.ToInt32(model.Role)))
                .ForMember("ActiveStatus", opt => opt.MapFrom(model => Convert.ToInt32(model.ActiveStatus))));

            m_mapperSave = mapConfig3.CreateMapper();
        }

        /// <summary>
        /// View the main page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            IEnumerable<Models.User> model = null;

            using (var webDbService = new WebOrpalDbService.WebDbServiceClient())
            {
                var users = webDbService.GetUsers();
                model = m_mapperIndex.Map<List<Models.User>>(users);
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
                    ViewBag.CollectionRoles = Code.CommnonCollections.GetCollectionRoles(u.Role == 0, u.Role != 0);
                    ViewBag.CollectionStatuses = Code.CommnonCollections.GetCollectionStatuses(u.ActiveStatus == 0, u.ActiveStatus != 0);

                    Models.User model = m_mapperEdit.Map<Models.User>(u);

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
                    UserDataContract udc = m_mapperSave.Map<UserDataContract>(model);

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

            ViewBag.CollectionRoles = Code.CommnonCollections.GetCollectionRoles(model.Role == "0", model.Role != "0");
            ViewBag.CollectionStatuses = Code.CommnonCollections.GetCollectionStatuses(model.ActiveStatus == "0", model.ActiveStatus != "0");

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