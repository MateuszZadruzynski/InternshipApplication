using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternshipApplicationClient.Interface
{
    public interface CompanyInterface
    {
        public Task<IEnumerable<CompanyDTO>> GetCompanies();
        public Task<CompanyDTO> GetCompany(int CompanyId);
        public Task<CompanyDTO> CheckNIP(string nip);
        public Task<CompanyDTO> UpdateCompany(int companyId, CompanyDTO studentDTO);
    }
}

