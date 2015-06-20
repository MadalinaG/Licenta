//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartQuizZApp.Models
{
    public partial class User
    {
        [ScaffoldColumn(false)]
        public int UserID { get; set; }

        [DisplayName("FirstName")]
        [Required(ErrorMessage = "First Name is required")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50, MinimumLength = 4)]
        public string FirstName { get; set; }

        [DisplayName("LastName")]
        [Required(ErrorMessage = "Last Name is required")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50, MinimumLength = 4)]
        public string LastName { get; set; }

        [DisplayName("UserName")]
        [Required(ErrorMessage = "User Name is required")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50, MinimumLength = 4)]
        public string UserName { get; set; }

        public System.DateTime StartDate { get; set; }

        [DisplayName("EmailAdress")]
        [Required(ErrorMessage = "EmailAdress is required")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(150, MinimumLength = 4)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "The Email field is required.")]
        public string EmailAdress { get; set; }

        public string Photo { get; set; }
        
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        [DisplayName("Password")]
        public string Password { get; set; }

        public string Language { get; set; }

        public string Culture { get; set; }

        public string PhoneNumber { get; set; }

        public Nullable<int> Administrator { get; set; }
    }
}
