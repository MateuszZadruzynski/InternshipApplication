using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class AuthenticationUser
    {
        [Required(ErrorMessage = "Email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres email.")]
        public string Email{ get; set; }
        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
