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
            OrpalPhotoPortUtils.Base.ICryptograph cryptograph = OrpalPhotoPortUtils.CryptographCoClass.GetCryptograph();

            // Index
            MapperConfiguration mapConfig1 = new MapperConfiguration(cfg => cfg.CreateMap<UserDataContract, Models.User>()
                .ForMember("Role", opt => opt.MapFrom(udc => Code.CommnonCollections.Roles[udc.Role]))
                .ForMember("ActiveStatus", opt => opt.MapFrom(udc => Code.CommnonCollections.Statuses[udc.ActiveStatus]))
                .ForMember("Password", opt => opt.MapFrom(udc => cryptograph.Decode(udc.Password))));

            m_mapperIndex = mapConfig1.CreateMapper();

            // Edit
            MapperConfiguration mapConfig2 = new MapperConfiguration(cfg => cfg.CreateMap<UserDataContract, Models.User>()
                .ForMember("Role", opt => opt.MapFrom(udc => udc.Role.ToString()))
                .ForMember("ActiveStatus", opt => opt.MapFrom(udc => udc.ActiveStatus.ToString()))
                .ForMember("Password", opt => opt.MapFrom(udc => cryptograph.Decode(udc.Password))));

            m_mapperEdit = mapConfig2.CreateMapper();

            // Save
            MapperConfiguration mapConfig3 = new MapperConfiguration(cfg => cfg.CreateMap<Models.User, UserDataContract>()
                .ForMember("Role", opt => opt.MapFrom(model => Convert.ToInt32(model.Role)))
                .ForMember("ActiveStatus", opt => opt.MapFrom(model => Convert.ToInt32(model.ActiveStatus)))
                .ForMember("Password", opt => opt.MapFrom(udc => cryptograph.Encode(udc.Password))));

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
                    ViewBag.CollectionRoles = Code.CommnonCollections.GetCollection(Code.CommnonCollections.Roles, u.Role);
                    ViewBag.CollectionStatuses = Code.CommnonCollections.GetCollection(Code.CommnonCollections.Statuses, u.ActiveStatus);

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
            ViewBag.CollectionRoles = Code.CommnonCollections.GetCollection(Code.CommnonCollections.Roles, 0);
            ViewBag.CollectionStatuses = Code.CommnonCollections.GetCollection(Code.CommnonCollections.Statuses, 0);

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

            ViewBag.CollectionRoles = Code.CommnonCollections.GetCollection(Code.CommnonCollections.Roles, Convert.ToInt32(model.Role));
            ViewBag.CollectionStatuses = Code.CommnonCollections.GetCollection(Code.CommnonCollections.Statuses, Convert.ToInt32(model.ActiveStatus));

            return (bAdd) ? View("AddUser", model) : View("EditUser", model);
        }

        /// <summary>
        /// Remove user by specified id
        /// </summary>
        /// <param name="id">user's id</param>
        /// <returns></returns>
        public ActionResult RemoveUserAt(int? id)
        {
            if (id.HasValue)
            {
                using (var webDbService = new WebOrpalDbService.WebDbServiceClient())
                {
                    if (!webDbService.RemoveUserAt(id.Value))
                    {
                        ViewBag.errorMessage = $"Не удалось удалить пользователя c id = {id.Value}.";
                    }
                }
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Model validation
        /// </summary>
        /// <param name="fields">Fields for validation</param>
        /// <returns>True if validation is successful, False otherwise</returns>
        bool IsModelValid(string[] fields)
        {            
            return (fields.Count(x => !ModelState.IsValidField(x)) == 0);
        }
    }
}