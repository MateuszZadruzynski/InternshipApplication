using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CompanyImageDTO
    {
        public int ImageId { get; set; }
        public int CompanyId { get; set; }
        public string URL { get; set; }

    }
}
