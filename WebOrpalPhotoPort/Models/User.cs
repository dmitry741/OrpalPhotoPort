using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebOrpalPhotoPort.Models
{
    public class User
    {
        public int id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Обязательное поле")]
        [StringLength(250)]
        [DisplayName("Имя")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Обязательное поле")]
        [StringLength(100)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Обязательное поле")]
        [StringLength(50)]
        [DisplayName("Логин")]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Обязательное поле")]
        [StringLength(16)]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Обязательное поле")]
        [StringLength(100)]
        [DisplayName("Роль")]
        public string Role { get; set; }

        [DisplayName("Дата регистрации")]
        public string RegDateTime { get; set; }

        [DisplayName("Статус")]
        public string IsDeleted { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}