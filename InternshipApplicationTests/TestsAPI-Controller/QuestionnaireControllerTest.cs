using Administration.Interface;
using FakeItEasy;
using InternshipManagmentAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Models;
using Xunit;
using DataAcess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using InternshipManagmentAPI.Utilities;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace InternshipApplicationTests.TestsAPI
{
    public class QuestionnaireControllerTest
    {
        [Fact]
        public void GetQuestionnaireByStudentAndKeeperId()
        {
            var fakeData = A.Dummy<QuestionnaireModelDTO>();
            var data = A.Fake<QuestionnaireInterface>();
            A.CallTo(() => data.GetQuestionnaireByKeeperAndStudent(fakeData.StudentId, fakeData.KeeperId)).Returns(fakeData);
            var controller = new QuestionnaireController(data);

            var action = controller.GetQuestionnaireByStudentAndKeeperId(fakeData.StudentId, fakeData.KeeperId);

            var result = action.Result as OkObjectResult;
            var returnData = result.Value as QuestionnaireModelDTO;

            Assert.Equal(fakeData, returnData);
        }

        [Fact]
        public void CreateQuestionnaire()
        {
            var fakeData = A.Dummy<QuestionnaireModelDTO>();
            var data = A.Fake<QuestionnaireInterface>();
            A.CallTo(() => data.CreateQuestionnaire(fakeData));
            var controller = new QuestionnaireController(data);

            var action = controller.CreateQuestionnaire(fakeData);
            var result = action.Result as ObjectResult;

            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public void GetQuestionnaireByStudentByStudentyId()
        {
            int StudentId = 2;
            int number = 5;
            var fakeData = A.CollectionOfDummy<QuestionnaireModelDTO>(number).AsEnumerable();
            var data = A.Fake<QuestionnaireInterface>();
            A.CallTo(() => data.GetQuestionnaireByStudentId(StudentId)).Returns(fakeData);
            var controller = new QuestionnaireController(data);

            var action = controller.GetQuestionnaireByStudent(StudentId);

            var result = action.Result as OkObjectResult;
            var returnData = result.Value as IEnumerable<QuestionnaireModelDTO>;

            Assert.Same(fakeData, returnData);
        }

        [Fact]
        public void DeleteQuestionnaire()
        {
            int DeleteQuestionnaireId = int.MinValue;
            var fakeData = A.Dummy<QuestionnaireModelDTO>();
            var data = A.Fake<QuestionnaireInterface>();
            A.CallTo(() => data.DeleteQuestionnaire(DeleteQuestionnaireId));
            var controller = new QuestionnaireController(data);

            var action = controller.DeleteQuestionnaire(DeleteQuestionnaireId);
            var result = action.Result as OkObjectResult;

            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public void UpdateQuestionnairey()
        {
            var fakeData = A.Dummy<QuestionnaireModelDTO>();
            var data = A.Fake<QuestionnaireInterface>();
            A.CallTo(() => data.UpdateQuestionnaire(fakeData.QuestionnaireiD,fakeData));
            var controller = new QuestionnaireController(data);

            var action = controller.UpdateQuestionnaire(fakeData.QuestionnaireiD, fakeData);
            var result = action.Result as OkObjectResult;

            Assert.Equal("200", result.StatusCode.ToString());
        }
    }
}
