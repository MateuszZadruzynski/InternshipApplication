using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataAcess.Data
{
    public class Diary
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
        public virtual Student Student { get; set; }
    }
}
