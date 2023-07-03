using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class CustomHireDateAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        public new string ErrorMessage { get; set; }

        public override bool IsValid(object value)
        {
            DateTime dateTime = Convert.ToDateTime(value);

            return dateTime <= DateTime.Now;
        }
    }
}