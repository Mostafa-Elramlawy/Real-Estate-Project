using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PP1.Models
{
    [Table("TB_user")]
    public class user
    {
        [Required(ErrorMessage = "This Field is Required")]
        public string Name { get; set; }
        public int UserID { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "This Field is Required")]
        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }

        public string Gender { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "This Field is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "This Field is Required")]
        [DataType(DataType.Password)]
        public string Confirm_Password { get; set; }

        public string Address { get; set; }
        public string image { get; set; }
        public int Role_id { get; set; }

    }

    public enum Gender
    {
        M,
        F
    }
}