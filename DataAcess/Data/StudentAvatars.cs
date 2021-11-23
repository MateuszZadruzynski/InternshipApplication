using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Data
{
    public class StudentAvatars
    {
        [Key]
        public int AvatarId { get; set; }
        public int StudentId { get; set; }
        public string URL { get; set; }
        public virtual Student Student { get; set; }
    }
}
