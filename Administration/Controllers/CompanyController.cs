using Administration.Interface;
using AutoMapper;
using DataAcess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Controllers
{
    public class CompanyController : CompanyInterface
    {
        private readonly AppDbContext _database;
        private readonly IMapper _imapper;
        public CompanyController(AppDbContext database, IMapper mapper)
        {
            _imapper = mapper;
            _database = database;
        }
        public async Task<CompanyDTO> CreateCompany(CompanyDTO companyDTO)
        {
            Company company = _imapper.Map<CompanyDTO, Company>(companyDTO);
            var addedCompanys = await _database.companies.AddAsync(company);
            await _database.SaveChangesAsync();
            return _imapper.Map<Company, CompanyDTO>(addedCompanys.Entity);
        }

        public async Task<int> DeleteCompany(int CompanyId)
        {
            var companyData = await _database.companies.FindAsync(CompanyId);
            if(companyData !=null)
            {
                var images = await _database.images.Where(i => i.CompanyId == CompanyId).ToListAsync();
                var internships = await _database.internships.Where(i => i.CompanyId == CompanyId).ToListAsync();
                _database.internships.RemoveRange();
                _database.images.RemoveRange();
                _database.companies.Remove(companyData);
                return await _database.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<CompanyDTO> FindCompanyByName(string name)
        {
            try
            {
                CompanyDTO companyDTO = _imapper.Map<Company, CompanyDTO>(
                    await _database.companies.FirstOrDefaultAsync(x => x.name.ToLower() == name.ToLower()));

                return companyDTO;
            }
            catch (Exception error)
            {
                return null;
            }
        }

        public async Task<IEnumerable<CompanyDTO>> GetAllCompanies()
        {
            try
            {
                IEnumerable<CompanyDTO> companyDTOs = 
                    _imapper.Map<IEnumerable<Company>, IEnumerable<CompanyDTO>> 
                    (_database.companies.Include( x => x.CompanyImages).Include(i =>i.InternshipsOfferts));

                return companyDTOs;
            }
            catch(Exception error)
            {
                return null;
            }
        }

        public async Task<CompanyDTO> GetCompany(int CompanyId)
        {
            try
            {
                CompanyDTO companyDTO = _imapper.Map<Company,CompanyDTO>(
                    await _database.companies.Include(x => x.CompanyImages).Include(i => i.InternshipsOfferts).FirstOrDefaultAsync(x => x.CompanyId == CompanyId));

                return companyDTO;
            }
            catch(Exception error)
            {
                return null; 
            }
        }

        public async Task<CompanyDTO> UpdateCompany(int CompanyId, CompanyDTO companysDTO)
        {
            try
            {

                if (CompanyId == companysDTO.CompanyId)
                {
                    Company companyData = await _database.companies.FindAsync(CompanyId);
                    Company company = _imapper.Map<CompanyDTO, Company>(companysDTO, companyData);
                    var updatedCompany = _database.companies.Update(company);
                    await _database.SaveChangesAsync();
                    return _imapper.Map<Company, CompanyDTO>(updatedCompany.Entity);
                }
                else
                {
                    return null;
                }
            }
            catch(Exception error)
            {
                return null;
            }
        }
        public async Task<CompanyDTO> IsNipUnique(string nip, int CompanyId = 0)
        {
            try
            {
                if (CompanyId == 0)
                {
                    CompanyDTO company = _imapper.Map<Company, CompanyDTO>(
                    await _database.companies.FirstOrDefaultAsync(x => x.nip == nip));
                    return company;
                }
                else
                {
                    CompanyDTO company = _imapper.Map<Company, CompanyDTO>(
                    await _database.companies.FirstOrDefaultAsync(x => x.nip == nip && x.CompanyId != CompanyId));
                    return company;
                }
            }
            catch (Exception error)
            {
                return null;
            }
        }

        public async Task<CompanyDTO> GetCompanyByEmail(string email)
        {
            try
            {
                CompanyDTO companyDTO = _imapper.Map<Company, CompanyDTO>(
                    await _database.companies.FirstOrDefaultAsync(x => x.email == email));

                return companyDTO;
            }
            catch (Exception eror)
            {
                return null;
            }
        }
    }
}
