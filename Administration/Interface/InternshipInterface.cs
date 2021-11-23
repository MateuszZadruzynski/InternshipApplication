using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Interface
{
    public interface InternshipInterface
    {
        public Task<InternshipDTO> CreateInternship(InternshipDTO internshipDTO);
        public Task<InternshipDTO> UpdateInternship(int InternshipID, InternshipDTO internshipDTO);
        public Task<InternshipDTO> GetInternship(int InternshipID);
        public Task<int> DeleteInternship(int InternshipID);
        public Task<int> DeleteInternshipByComapnyId(int CompanyId);
        public Task<IEnumerable<InternshipDTO>> GetAllInternships();
        public Task<IEnumerable<InternshipDTO>> GetAllCompanyInternships(int CompanyId);
    }
}