using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Data
{
    public class CompanyImage
    {
        [Key]
        public int ImageId{ get; set; }
        public int CompanyId { get; set; }
        public string URL { get; set; }  
        public virtual Company Company { get; set; }
    }
}
