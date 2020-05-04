using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Deployment_App.Models
{
    public class LoginModel
    { 
        [Required(ErrorMessage = "Необходимо ввести логин")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Необходимо ввести пароль")]
        public string Password { get; set; }
        
    }
}