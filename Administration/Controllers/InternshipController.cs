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
    public class InternshipController : InternshipInterface
    {

        private readonly AppDbContext _database;
        private readonly IMapper _imapper;
        public InternshipController(AppDbContext database, IMapper mapper)
        {
            _imapper = mapper;
            _database = database;
        }

        public async Task<InternshipDTO> CreateInternship(InternshipDTO internshipDTO)
        {
            Internship internship = _imapper.Map<InternshipDTO, Internship>(internshipDTO);
            var addedInternship = await _database.internships.AddAsync(internship);
            await _database.SaveChangesAsync();
            return _imapper.Map<Internship, InternshipDTO>(addedInternship.Entity);
        }

        public async Task<int> DeleteInternship(int InternshipID)
        {
            var internshipData = await _database.internships.FindAsync(InternshipID);
            if (internshipData != null)
            {
                _database.internships.Remove(internshipData);
                return await _database.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> DeleteInternshipByComapnyId(int CompanyId)
        {
            var internshipList = await _database.internships.Where(i => i.CompanyId == CompanyId).ToListAsync();
            _database.internships.RemoveRange(internshipList);
            return await _database.SaveChangesAsync();
        }

        public async Task<IEnumerable<InternshipDTO>> GetAllCompanyInternships(int CompanyId)
        {
            try
            {
                IEnumerable<InternshipDTO> internshipDTOs =
                    _imapper.Map<IEnumerable<Internship>, IEnumerable<InternshipDTO>>(_database.internships.Include(c => c.Company).Where(x => x.CompanyId == CompanyId));
                return internshipDTOs;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

        public async Task<IEnumerable<InternshipDTO>> GetAllInternships()
        {
            try
            {
                IEnumerable<InternshipDTO> internshipDTOs =
                    _imapper.Map<IEnumerable<Internship>, IEnumerable<InternshipDTO>>(_database.internships.Include(c => c.Company));
                return internshipDTOs;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

        public async Task<InternshipDTO> GetInternship(int InternshipID)
        {
            try
            {
                InternshipDTO internshipDTO = _imapper.Map<Internship, InternshipDTO>(
                    await _database.internships.Include(c => c.Company).FirstOrDefaultAsync(x => x.InternshipID == InternshipID));

                return internshipDTO;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

        public async Task<InternshipDTO> UpdateInternship(int InternshipID, InternshipDTO internshipDTO)
        {
            try
            {
                if (InternshipID == internshipDTO.InternshipID)
                {
                    Internship internshipData = await _database.internships.FindAsync(InternshipID);
                    Internship internship = _imapper.Map<InternshipDTO, Internship>(internshipDTO, internshipData);
                    var updatedInternship = _database.internships.Update(internship);
                    await _database.SaveChangesAsync();
                    return _imapper.Map<Internship, InternshipDTO>(updatedInternship.Entity);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception eror)
            {
                return null;
            }
        }
    }
}
