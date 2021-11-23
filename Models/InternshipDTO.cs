using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class InternshipDTO
    {
        [Key]
        public int InternshipID { get; set; }
        public int CompanyId { get; set; }
        [Required]
        [Range(0, 5000, ErrorMessage = "Pensja powinna zawierać się w przedziale od 0 do 5000zł.")]
        public int salary { get; set; }
        [RegularExpression(@"^.{3,}$", ErrorMessage = "Minimum 3 znaki.")]
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Maksymalna ilość znaków wynosi 30.")]
        public string worksite { get; set; }
   
        [Required]
        [RegularExpression("^(?:[01]?[0-9]|2[0-3]):[0-5][0-9]-(?:[01]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage ="Zły format (9:00-12:00)")]
        public string workTime { get; set; }
        public month month { get; set; }
        public string tasks { get; set; }
        public string description { get; set; }
        public virtual CompanyDTO Company { get; set; }
   
    }
    public enum month
    {
        [Display(Name = "Do Uzgodnienia")]
        Uzgodnienie,
        Styczeń,
        Luty,
        Marzec,
        Kwiecień,
        Maj,
        Czerwiec,
        Lipiec,
        Sierpeiń,
        Wrzesień,
        Październik,
        Listopad,
        Grudzień
    }
}
 
