using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StudentKeeperDTO
    {
        public int StudentKeeperId { get; set; }
        public int KeeperId { get; set; }
        public int StudentId { get; set; }
        public string Email { get; set; }
    }
}
