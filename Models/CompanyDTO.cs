using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class CompanyDTO
    {
        public int CompanyId { get; set; }
        [RegularExpression(@"^.{3,}$", ErrorMessage = "Wymagane są minimum 3 znaki!")]
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nazwa firmy jest zbyt długa.")]
        public string name { get; set; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = "NIP musi składać się z 10 znaków.")]
        [Required]
        [StringLength(10)]
        public string nip { get; set; }
        [RegularExpression(@"^.{2,}$", ErrorMessage = "Wymagane są minimum 2 znaki!")]
        [Required]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Nazwa jest zbyt długa.")]
        public string type { get; set; }
        [RegularExpression(@"^.{3,}$", ErrorMessage = "Wymagane są minimum 3 znaki!")]
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nazwa jest zbyt długa.")]
        public string street { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Nazwa jest zbyt długa.")]
        public string streetNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Błędny adres email.")]
        public string email { get; set; }

        [RegularExpression(@"^.{3,}$", ErrorMessage = "Wymagane jest minimum 5 znaków!")]
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Nazwa jest zbyt długa.")]
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
        public virtual CompanyImageDTO CompanyImages { get; set; }
        public List<string> UrlList { get; set; }
        public virtual ICollection<KeeperDTO> Keepers { get; set; }
        public virtual ICollection<InternshipDTO> InternshipsOfferts { get; set; }
    }
}
