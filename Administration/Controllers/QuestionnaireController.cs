using Administration.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DataAcess.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Administration.Controllers
{
    public class QuestionnaireController : QuestionnaireInterface
    {
        private readonly AppDbContext _database;
        private readonly IMapper _imapper;
        public QuestionnaireController(AppDbContext database, IMapper mapper)
        {
            _imapper = mapper;
            _database = database;
        }
        public async Task<QuestionnaireModelDTO> CreateQuestionnaire(QuestionnaireModelDTO questionnaireModel)
        {
            QuestionnaireModel model = _imapper.Map<QuestionnaireModelDTO, QuestionnaireModel>(questionnaireModel);
            var addedDiary = await _database.QuestionnaireModels.AddAsync(model);
            await _database.SaveChangesAsync();
            return _imapper.Map<QuestionnaireModel, QuestionnaireModelDTO>(addedDiary.Entity);
        }

        public async Task<int> DeleteQuestionnaire(int QuestionnaireId)
        {
            var questionnaireData = await _database.QuestionnaireModels.FindAsync(QuestionnaireId);
            if (questionnaireData != null)
            {
                _database.QuestionnaireModels.Remove(questionnaireData);
                return await _database.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> DeleteQuestionnaireByStudentId(int StudentId)
        {
            var questionnaireData = await _database.QuestionnaireModels.Where(i => i.StudentId == StudentId).ToListAsync();
            _database.QuestionnaireModels.RemoveRange(questionnaireData);
            return await _database.SaveChangesAsync();
        }

        public async Task<IEnumerable<QuestionnaireModelDTO>> GetAllQuestionnaire()
        {
            try
            {
                IEnumerable<QuestionnaireModelDTO> questionnaireModelDTOs =
                    _imapper.Map<IEnumerable<QuestionnaireModel>, IEnumerable<QuestionnaireModelDTO>>(_database.QuestionnaireModels.Include(c => c.Student).Include(k => k.Keeper));
                return questionnaireModelDTOs;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

        public async Task<QuestionnaireModelDTO> GetQuestionnaire(int QuestionnaireId)
        {
            try
            {
                QuestionnaireModelDTO questionnaireData = _imapper.Map<QuestionnaireModel, QuestionnaireModelDTO>(
                    await _database.QuestionnaireModels.Include(c => c.Student).Include(k => k.Keeper).FirstOrDefaultAsync(x => x.QuestionnaireiD == QuestionnaireId));

                return questionnaireData;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

        public async Task<QuestionnaireModelDTO> GetQuestionnaireByKeeperAndStudent(int StudentId, int KeeperId)
        {
            try
            {
                QuestionnaireModelDTO questionnaireSK = _imapper.Map<QuestionnaireModel, QuestionnaireModelDTO>(
                    await _database.QuestionnaireModels.Include(c => c.Student).Include(k => k.Keeper).FirstOrDefaultAsync(x => x.StudentId == StudentId && x.KeeperId == KeeperId));

                return questionnaireSK;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

        public async Task<IEnumerable<QuestionnaireModelDTO>> GetQuestionnaireByStudentId(int StudentID)
        {

            try
            {
                IEnumerable<QuestionnaireModelDTO> questionnaireModelDTOs =
                    _imapper.Map<IEnumerable<QuestionnaireModel>, IEnumerable<QuestionnaireModelDTO>>( _database.QuestionnaireModels.Include(k =>k.Keeper).
                    Include(c => c.Student).Where(x => x.StudentId == StudentID));
                return questionnaireModelDTOs;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

        public async Task<QuestionnaireModelDTO> UpdateQuestionnaire(int QuestionnaireId, QuestionnaireModelDTO questionnaireModel)
        {
            try
            {
                if (QuestionnaireId == questionnaireModel.QuestionnaireiD)
                {
                    QuestionnaireModel questionnaireData = await _database.QuestionnaireModels.FindAsync(QuestionnaireId);
                    QuestionnaireModel questionnaire = _imapper.Map<QuestionnaireModelDTO, QuestionnaireModel>(questionnaireModel, questionnaireData);
                    var questionnaireUpdate = _database.QuestionnaireModels.Update(questionnaire);
                    await _database.SaveChangesAsync();
                    return _imapper.Map<QuestionnaireModel, QuestionnaireModelDTO>(questionnaireUpdate.Entity);
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
