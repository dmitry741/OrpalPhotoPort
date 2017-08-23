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

        [DisplayName("Дата регистрации")]
        public DateTime RegDateTime { get; set; }

        [DisplayName("Роль")]
        public string Role { get; set; }

        [DisplayName("Статус")]
        public string ActiveStatus { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}