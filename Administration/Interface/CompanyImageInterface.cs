using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Interface
{
    public interface CompanyImageInterface
    {
        public Task<CompanyImageDTO> CreateImage(CompanyImageDTO imageDTO);
        public Task<int> DeleteImageByImageId(int ImageId);
        public Task<int> DeleteImageByImageUrl(string imageUrl);
        public Task<int> DeleteImageByCompanyId(int CompanyId);
        public Task<CompanyImageDTO> GetCompanyImage(int CompanyId);
        public Task<IEnumerable<CompanyImageDTO>> GetAllImages();
    }
}
