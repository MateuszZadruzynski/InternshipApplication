using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataAcess.Data
{
    public class Student
    {
        public int StudentId { get; set; }
        [RegularExpression(@"^.{3,}$")]
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string name { get; set; }
        [RegularExpression(@"^.{3,}$")]
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string lastName { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [RegularExpression(@"^\s*\+?\s*([0-9][\s-]*){9}$")]
        public string telephone { get; set; }
        [Required]
        [RegularExpression(@"[0-9]{6}$")]
        public string index { get; set; }

        public string major { get; set; }
        [RegularExpression(@"^.{3,}$")]
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string street { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string streetNumber { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string homeNumber { get; set; }
        [RegularExpression(@"^.{3,}$")]
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string city { get; set; }
        [RegularExpression(@"^[0-9]{2}-[0-9]{3}$", ErrorMessage = "Błędny kod pocztowy.")]
        [Required]
        [StringLength(10, MinimumLength = 5)]
        public string postCode { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Proszę ponownie wpisać hasło.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasła nie są identyczne!")]
        public string ConfirmPassword { get; set; }

        public virtual StudentAvatars StudentAvatars { get; set; }
        public virtual ICollection<Diary> Diary { get; set; }
        public virtual ICollection<StudentKeeper> Keeper { get; set; }
    }
}
