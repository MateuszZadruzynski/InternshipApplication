using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class SignInAuthentication
    {
        public bool isAuthenticationSuccessful { get; set; }
        public  string Token { get; set; }
        public StudentDTO studentDTO { get; set; }
        public KeeperDTO keeperDTO { get; set; }
        public CompanyDTO companyDTO { get; set; }
    }
}
