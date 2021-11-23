using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipApplicationClient.Interface
{
    public interface QuestionnaireInterface
    {
        public Task<IEnumerable<QuestionnaireModelDTO>> GetQuestionnaireByStudentId(int StudentId);
        public Task<QuestionnaireModelDTO> UpdateQuestionnaire(int QuestionnaireId, QuestionnaireModelDTO questionnaireModelDTO);
        public Task<QuestionnaireModelDTO> CreateQuestionnaire(QuestionnaireModelDTO questionnaireModelDTO);
        public Task<QuestionnaireModelDTO> GetQuestionnaireByKeeperIdAndStudentId(int StudentId, int KeeperId);
        public Task<int> DeleteQuestionnaire(int QuestionnaireId);
    }
}
