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
    public class StudentController : StudentInterface
    {
        private readonly AppDbContext _database;
        private readonly IMapper _imapper;
        public StudentController(AppDbContext database, IMapper mapper)
        {
            _imapper = mapper;
            _database = database;
        }
        public async Task<StudentDTO> CreateStudent(StudentDTO studentDTO)
        {
            Student student = _imapper.Map<StudentDTO, Student>(studentDTO);
            var addedStudents = await _database.students.AddAsync(student);
            await _database.SaveChangesAsync();
            return _imapper.Map<Student, StudentDTO>(addedStudents.Entity);
        }

        public async Task<int> DeleteStudent(int Id)
        {
            var studentData = await _database.students.FindAsync(Id);
            if (studentData != null)
            {
                var images = await _database.studentAvatars.Where(i => i.StudentId == Id).ToListAsync();
                var dairy = await _database.diaries.Where(i => i.StudentId == Id).ToListAsync();
                _database.images.RemoveRange();
                _database.diaries.RemoveRange();
                _database.students.Remove(studentData);
                return await _database.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<StudentDTO> FindStudentByLastName(string lastName)
        {
            try
            {
                StudentDTO studentDTO = _imapper.Map<Student, StudentDTO>(
                    await _database.students.FirstOrDefaultAsync(x => x.lastName.ToLower() == lastName));

                return studentDTO;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

        public async Task<StudentDTO> FindStudentByName(string name)
        {
            try
            {
                StudentDTO studentDTO = _imapper.Map<Student, StudentDTO>(
                    await _database.students.FirstOrDefaultAsync(x => x.name.ToLower() == name));

                return studentDTO;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

        public async Task<IEnumerable<StudentDTO>> GetAllStudents()
        {
            try
            {
                IEnumerable<StudentDTO> studentDTOs =
                    _imapper.Map<IEnumerable<Student>, IEnumerable<StudentDTO>>(_database.students.Include( a => a.StudentAvatars).Include(c => c.Keeper));
                return studentDTOs;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

        public async Task<StudentDTO> GetStudent(int StudentId)
        {
            try
            {
                StudentDTO studentDTO = _imapper.Map<Student, StudentDTO>(
                    await _database.students.Include(x => x.StudentAvatars).Include(c => c.Keeper).FirstOrDefaultAsync(x => x.StudentId == StudentId));

                return studentDTO;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

        public async Task<StudentDTO> GetStudentByEmail(string email)
        {
            try
            {
                StudentDTO studentDTO = _imapper.Map<Student, StudentDTO>(
                    await _database.students.FirstOrDefaultAsync(x => x.email == email));

                return studentDTO;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

        public async Task<StudentDTO> GetStudentByIndex(string index)
        {
            try
            {
                StudentDTO studentDTO = _imapper.Map<Student, StudentDTO>(
                    await _database.students.FirstOrDefaultAsync(x => x.index == index));

                return studentDTO;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

        public async Task<StudentDTO> IsIndexUnique(string index, int StudentId = 0)
        {
            try
            {
                if (StudentId == 0)
                {
                    StudentDTO student = _imapper.Map<Student, StudentDTO>(
                    await _database.students.FirstOrDefaultAsync(x => x.index == index));
                    return student;
                }
                else
                {
                    StudentDTO student = _imapper.Map<Student, StudentDTO>(
                    await _database.students.FirstOrDefaultAsync(x => x.index == index && x.StudentId != StudentId));
                    return student;
                }
            }
            catch (Exception error)
            {
                return null;
            }
        }
    

        public async Task<StudentDTO> UpdateStudent(int StudentId, StudentDTO studentDTO)
        {
            try
            {
                if (StudentId == studentDTO.StudentId)
                {
                    Student studentData = await _database.students.FindAsync(StudentId);
                    Student student = _imapper.Map<StudentDTO, Student>(studentDTO, studentData);
                    var updatedStudent = _database.students.Update(student);
                    await _database.SaveChangesAsync();
                    return _imapper.Map<Student, StudentDTO>(updatedStudent.Entity);
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
