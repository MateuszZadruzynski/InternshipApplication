using Models;
using System.Threading.Tasks;
using Utils;

namespace InternshipApplicationClient.Interface
{
    public interface AuthenticationInterface
    {
        Task<Registration> SignUpStudent(StudentDTO student);
        Task<Registration> SignUpKeeper(KeeperDTO keeper);
        Task<Registration> SignUpCompany(CompanyDTO company);
        Task<SignInAuthentication> LoginStudent(AuthenticationUser authenticationUser);
        Task<SignInAuthentication> LoginKeeper(AuthenticationUser authenticationUser);
        Task<SignInAuthentication> LoginCompany(AuthenticationUser authenticationUser);
        Task Logout();
    }
}
