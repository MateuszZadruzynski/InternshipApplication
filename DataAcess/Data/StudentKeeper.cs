using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Data
{
    public class StudentKeeper
    {
        [Key]
        public int StudentKeeperId { get; set; }
        public int KeeperId { get; set; }
        public int StudentId { get; set; }
        public string Email { get; set; }
        public virtual Student Student { get; set; }
        public virtual Keeper Keeper { get; set; }
    }
}
