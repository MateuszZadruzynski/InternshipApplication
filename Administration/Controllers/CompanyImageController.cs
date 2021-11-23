using Administration.Interface;
using AutoMapper;
using DataAcess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Controllers
{
    public class CompanyImageController : CompanyImageInterface
    {
        private readonly AppDbContext _database;
        private readonly IMapper _imapper;
        public CompanyImageController(AppDbContext database, IMapper mapper)
        {
            _imapper = mapper;
            _database = database;
        }

        public async Task<CompanyImageDTO> CreateImage(CompanyImageDTO imageDTO)
        {
            CompanyImage image = _imapper.Map<CompanyImageDTO, CompanyImage>(imageDTO);
            var add = await _database.images.AddAsync(image);
            await _database.SaveChangesAsync();
            return _imapper.Map<CompanyImage, CompanyImageDTO>(add.Entity);
        }

        public async Task<int> DeleteImageByCompanyId(int CompanyId)
        {
            var imageList = await _database.images.Where(x => x.CompanyId == CompanyId).ToListAsync();
            _database.images.RemoveRange(imageList);
            return await _database.SaveChangesAsync();
        }

        public async Task<int> DeleteImageByImageId(int ImageId)
        {
            var image = await _database.images.FindAsync(ImageId);
            _database.images.Remove(image);
            return await _database.SaveChangesAsync();
        }

        public async Task<int> DeleteImageByImageUrl(string imageUrl)
        {
            var Images = await _database.images.FirstOrDefaultAsync(x => x.URL.ToLower() == imageUrl.ToLower());
            _database.images.Remove(Images);
            return await _database.SaveChangesAsync();
        }


        public async Task<CompanyImageDTO> GetCompanyImage(int CompanyId)
        {
            return _imapper.Map<CompanyImage, CompanyImageDTO>(
            await _database.images.FirstOrDefaultAsync(x => x.CompanyId == CompanyId));
        }

        public async Task<IEnumerable<CompanyImageDTO>> GetAllImages()
        {
            try
            {
                IEnumerable<CompanyImageDTO> images =
                    _imapper.Map<IEnumerable<CompanyImage>, IEnumerable<CompanyImageDTO>>(_database.images);
                return images;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

    }
}
