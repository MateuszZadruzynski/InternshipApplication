using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Data
{
    public class Internship
    {
        [Key]
        public int InternshipID { get; set; }
        [Required]
        [Range(0, 5000)]
        public int CompanyId { get; set; }
        public int salary { get; set; }
        [RegularExpression(@"^.{3,}$")]
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string worksite { get; set; }
        [Required]
        [RegularExpression("^(?:[01]?[0-9]|2[0-3]):[0-5][0-9]$")]
        public string workTime{ get; set; }
        [Required]
        public month month { get; set; }
        public string tasks { get; set; }
        public string description { get; set; }
        public virtual Company Company { get; set; }
    }
    public enum month
    {
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }

}
