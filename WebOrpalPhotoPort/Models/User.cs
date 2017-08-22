using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace WebOrpalPhotoPort.Models
{
    public class User
    {
        public int id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Обязательное поле")]
        [StringLength(250)]
        [DisplayName("Имя")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Обязательное поле. Формат: name@example.com")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Обязательное поле")]
        [StringLength(50)]
        [DisplayName("Логин")]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Обязательное поле")]
        [MinLength(6)]
        [MaxLength(16)]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(100)]
        [DisplayName("Роль")]
        public string Role
        {
            get
            {
                if (CollectionRoles == null || CollectionRoles.Count() == 0)
                {
                    CollectionRoles = Code.CommnonCollections.CollectionRoles;
                }

                var item = CollectionRoles.FirstOrDefault(x => x.Selected);
                return item.Text;
            }

            set
            {
                string newrole = value;
                CollectionRoles = Code.CommnonCollections.GetCollectionRoles(newrole == "Пользователь", newrole != "Пользователь");
            }
        }

        public IEnumerable<SelectListItem> CollectionRoles { get; set; }

        [DisplayName("Дата регистрации")]
        public DateTime RegDateTime { get; set; }

        [DisplayName("Статус")]
        public bool IsDeleted
        {
            get
            {
                if (CollectionStatuses == null || CollectionStatuses.Count() == 0)
                {
                    CollectionStatuses = Code.CommnonCollections.CollectionStatuses; ;
                }

                var item = CollectionStatuses.FirstOrDefault(x => x.Selected);
                return (item.Value == "1");
            }
            set
            {
                CollectionStatuses = Code.CommnonCollections.GetCollectionStatuses(!value, value);
            }
        }

        public string CurStatus
        {
            get
            {
                var item = CollectionStatuses.FirstOrDefault(x => x.Selected);
                return item.Text;
            }
        }

        public IEnumerable<SelectListItem> CollectionStatuses { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}