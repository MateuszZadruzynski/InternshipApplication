using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Administration.Interface
{
    public interface QuestionnaireInterface
    {
        public Task<QuestionnaireModelDTO> CreateQuestionnaire(QuestionnaireModelDTO questionnaireModel);
        public Task<QuestionnaireModelDTO> UpdateQuestionnaire(int QuestionnaireId, QuestionnaireModelDTO questionnaireModel);
        public Task<QuestionnaireModelDTO> GetQuestionnaire(int QuestionnaireId);
        public Task<QuestionnaireModelDTO> GetQuestionnaireByKeeperAndStudent(int StudentId, int KeeperId);
        public Task<IEnumerable<QuestionnaireModelDTO>> GetAllQuestionnaire();
        public Task<IEnumerable<QuestionnaireModelDTO>> GetQuestionnaireByStudentId(int StudentId);
        public Task<int> DeleteQuestionnaire(int QuestionnaireId);
        public Task<int> DeleteQuestionnaireByStudentId(int StudentId);
    }
}
