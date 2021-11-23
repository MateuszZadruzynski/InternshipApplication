using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DiaryDTO
    {
        public int DiaryId { get; set; }
        public int StudentId { get; set; }
        [Required]
        public string title { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Required]
        public string summary { get; set; }
        [Required]
        public string description { get; set; }
        public string conclustion { get; set; }
        public virtual StudentDTO Student{ get; set; }
    }
}
