using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternshipApplicationClient.Interface
{
    public interface CompanyImageInterface
    {
        public Task<CompanyImageDTO> CreateCompanyImage(CompanyImageDTO companyImageDTO);
        public Task<int> DeleteCompanyImageByCompanyId(int compayId);
        public Task<CompanyImageDTO> GetCompanyImages(int companyId);
        public Task<IEnumerable<CompanyImageDTO>> GetAllCompanyImages();
    }
}
