using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataAcess.Data
{
    public class Company
    {   
        public int CompanyId { get; set; }
        [RegularExpression(@"^.{3,}$")]
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string name { get; set; }
        [RegularExpression(@"^\d{10}$")]
        [Required]
        [StringLength(10)]
        public string nip { get; set; }
        [RegularExpression(@"^.{2,}$")]
        [Required]
        [StringLength(15, MinimumLength = 2)]
        public string type { get; set; }

        [RegularExpression(@"^.{3,}$")]
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string street { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string streetNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Błędny adres email.")]
        public string email { get; set; }

        [RegularExpression(@"^.{3,}$")]
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string city { get; set; }
        [RegularExpression(@"^[0-9]{2}-[0-9]{3}$", ErrorMessage = "Błędny kod pocztowy.")]
        [Required]
        [StringLength(10, MinimumLength = 5)]
        public string postCode { get; set; }
        public string description { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Proszę ponownie wpisać hasło.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasła nie są identyczne!")]
        public string ConfirmPassword { get; set; }
        public virtual CompanyImage CompanyImages { get; set; }
        public virtual ICollection<Keeper> Keepers { get; set; }
        public virtual ICollection<Internship> InternshipsOfferts { get; set; }
    }
}
