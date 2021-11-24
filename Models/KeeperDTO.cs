using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class KeeperDTO
    {
        public int KeeperId { get; set; }
        public degree degree { get; set; }
        [RegularExpression(@"^.{3,}$", ErrorMessage = "Minimum 3 znaki.")]
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Imie jest za długie.")]
        public string name { get; set; }
        [RegularExpression(@"^.{3,}$", ErrorMessage = "Nazwisko jest za krótkie.")]
        [Required]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Nazwisko jest za długie.")]
        public string lastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Błędny adres email.")]
        public string email { get; set; }
        [RegularExpression(@"^\s*\+?\s*([0-9][\s-]*){9}$", ErrorMessage = "Błędny numer telefonu.")]
        public string telephone { get; set; }

        public virtual KeeperAvatarsDTO KeeperAvatars { get; set; }
        public List<string> UrlList { get; set; }
        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Proszę ponownie wpisać hasło.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasła nie są identyczne!")]
        public string ConfirmPassword { get; set; }
        public string? description { get; set; }
        public List<string> EmailList { get; set; }
        public virtual ICollection<StudentKeeperDTO> Students { get; set; }
        public int? CompanyId { get; set; }
        public virtual CompanyDTO Company { get; set; }
    }
    public enum degree
    {
        Inżynier,
        Licencjat,
        Magister,
        Doktor,
        Profesor
    }
}
