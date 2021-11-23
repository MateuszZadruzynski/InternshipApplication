using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternshipApplicationClient.Interface
{
    public interface InternshipInterface
    {
        public Task<IEnumerable<InternshipDTO>> GetInternships();
        public Task<InternshipDTO> GetInternship(int internshipId);
        public Task<IEnumerable<InternshipDTO>> GetInternshipsByCompanyId(int CompanyId);
        public Task<InternshipDTO> UpdateInternship(int internshipId, InternshipDTO internshipDTO);
        public Task<InternshipDTO> CreateInternship(InternshipDTO internshipDTO);
        public Task<int> DeleteInternship(int internshipId);
    }
}
