using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Deployment_App.Models
{
    public class DeploymentTable
    {
        public Int64 DEPLOYMENT_ID { get; set; }
        [Required(ErrorMessage = "Обязательно к заполнению")]
        public string TASK_ID_LIST { get; set; }

        [Required(ErrorMessage = "Обязательно к заполнению")]
        public string ISSUE { get; set; }

        [Required(ErrorMessage = "Обязательно к заполнению")]
        [DataType(DataType.DateTime)]
        public DateTime DATE_DEPLOY { get; set; }

        [Required(ErrorMessage = "Обязательно к заполнению")]
        public string DEPLOYER { get; set; }        
        public string TYPE_EXCEPTION { get; set; }       
        public string COMMENT_DEPLOY { get; set; }

        [Required(ErrorMessage = "Обязательно к заполнению")]
        [DataType(DataType.DateTime)]
        public DateTime MAIL_DATE { get; set; }

        [Required(ErrorMessage = "Обязательно к заполнению")]
        public string MAIL_SUBJECT { get; set; }

        [Required(ErrorMessage = "Обязательно к заполнению")]
        public string MAIL_SENDER { get; set; }
              
        public string DATA_REPORT { get; set; }
              
        public string GIT { get; set; }

        [Required(ErrorMessage = "Обязательно к заполнению")]
        [RegularExpression(@"[0-1]", ErrorMessage = "Некорректное значение")]
        public int IS_FIX { get; set; }

    }
}


