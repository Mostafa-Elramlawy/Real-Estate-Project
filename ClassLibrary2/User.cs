using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class User
    {
        [Required(ErrorMessage = "This Field is Required")]
        public string Name { get; set; }

        public int UserID { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }

        public string Gender { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [DataType(DataType.Password)]
        public string Confirm_Password { get; set; }

        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [DataType(DataType.ImageUrl)]
        public string image { get; set; }

        public int Role_id { get; set; }
    }
}