using System.Net.Http;
using System.Threading.Tasks;

namespace InternshipApplicationClient.Interface
{
    public interface UploadInterface
    {
        public Task<string> UploadImage(MultipartFormDataContent multipartFormDataContent);
        public Task<int> DeleteImage(string name);
    }
}
