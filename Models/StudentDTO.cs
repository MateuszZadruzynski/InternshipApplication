using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StudentDTO
    {
        public int StudentId { get; set; }
        [RegularExpression(@"^.{3,}$", ErrorMessage = "Imie jest za którkie.")]
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

        [RegularExpression(@"^(\+[0-9]{9})$", ErrorMessage = "Błędny numer telefonu.")]
        public string telephone { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{6}$", ErrorMessage = "Indeks musi składać się z 6 znaków.")]
        public string index { get; set; }
        public string major { get; set; }

        [RegularExpression(@"^.{3,}$", ErrorMessage = "Minimum 3 znaki.")]
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nazwa ulicy jest za długa.")]
        public string street { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Numer ulicy jest błędny.")]
        public string streetNumber { get; set; }
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Numer domu jest błędny.")]
        public string homeNumber { get; set; }
        [RegularExpression(@"^.{3,}$", ErrorMessage = "Nazwa miasta jest za krótka.")]
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Nazwa miasta jest za długa.")]
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
        public virtual StudentAvatarsDTO StudentAvatars{ get; set; }
        public virtual ICollection<DiaryDTO> Diary { get; set; }
        public List<string> UrlList { get; set; }
        public virtual ICollection<StudentKeeperDTO> Keeper { get; set; }
    }

}