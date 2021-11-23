using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Interface
{
    public interface CompanyInterface
    {
        public Task<CompanyDTO> CreateCompany(CompanyDTO companyDTO);
        public Task<CompanyDTO> UpdateCompany(int CompanyId,CompanyDTO companyDTO);
        public Task<CompanyDTO> GetCompany(int CompanyIdd);
        public Task<int> DeleteCompany(int CompanyId);
        public Task<CompanyDTO> GetCompanyByEmail(string email);
        public Task<IEnumerable<CompanyDTO>> GetAllCompanies();
        public Task<CompanyDTO> FindCompanyByName(string name);
        public Task<CompanyDTO> IsNipUnique(string nip, int CompanyId = 0);
    }
}
