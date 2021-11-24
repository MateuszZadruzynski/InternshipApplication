using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataAcess.Data
{
    public class Keeper
    {
        public int KeeperId { get; set; }
        public degree degree { get; set; }
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

        [RegularExpression(@"^\s*\+?\s*([0-9][\s-]*){9}$", ErrorMessage = "Błędny numer telefonu.")]
        public string telephone { get; set; }
        public virtual KeeperAvatars KeeperAvatars { get; set; }
        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Proszę ponownie wpisać hasło.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasła nie są identyczne!")]
        public string ConfirmPassword { get; set; }
        public string? description { get; set; }
        public virtual ICollection<StudentKeeper> Students { get; set; }
        public int? CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }

    public enum degree
    {
        Engineer,
        Master,
        Doctoral,
        Professor
    }
}
