using Administration.Interface;
using FakeItEasy;
using InternshipManagmentAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace InternshipApplicationTests.TestsAPI
{
    public class DiaryControllerTest
    {
        [Fact]
        public void GetDiarypById()
        {
            var fakeData = A.Dummy<DiaryDTO>();
            var data = A.Fake<DiaryInterface>();
            A.CallTo(() => data.GetDiary(fakeData.DiaryId)).Returns(fakeData);
            var controller = new DiaryController(data);

            var action = controller.GetDiary(fakeData.DiaryId);

            var result = action.Result as OkObjectResult;
            var returnData = result.Value as DiaryDTO;

            Assert.Same(fakeData, returnData);
        }

        [Fact]
        public void CreateDiary()
        {
            var fakeData = A.Dummy<DiaryDTO>();
            var data = A.Fake<DiaryInterface>();
            A.CallTo(() => data.CreateDiary(fakeData));
            var controller = new DiaryController(data);

            var action = controller.CreateDiary(fakeData);
            var result = action.Result as ObjectResult;

            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public void GetDiariesByStudentyId()
        {
            int DiaryId = 2;
            int number = 5;
            var fakeData = A.CollectionOfDummy<DiaryDTO>(number).AsEnumerable();
            var data = A.Fake<DiaryInterface>();
            A.CallTo(() => data.GetAllDiariesByStudent(DiaryId)).Returns(fakeData);
            var controller = new DiaryController(data);

            var action = controller.GetDiaries(DiaryId);

            var result = action.Result as OkObjectResult;
            var returnData = result.Value as IEnumerable<DiaryDTO>;

            Assert.Same(fakeData, returnData);
        }

        [Fact]
        public void DeleteDiary()
        {
            int DeleteInternshipId = int.MinValue;
            var fakeData = A.Dummy<DiaryDTO>();
            var data = A.Fake<DiaryInterface>();
            A.CallTo(() => data.DeleteDiary(DeleteInternshipId));
            var controller = new DiaryController(data);

            var action = controller.DeleteDiary(DeleteInternshipId);
            var result = action.Result as OkObjectResult;

            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public void UpdateDiary()
        {
            var fakeData = A.Dummy<DiaryDTO>();
            var data = A.Fake<DiaryInterface>();
            A.CallTo(() => data.UpdateDiary(fakeData.DiaryId, fakeData));
            var controller = new DiaryController(data);

            var action = controller.UpdateDiary(fakeData.DiaryId, fakeData);
            var result = action.Result as OkObjectResult;

            Assert.Equal("200", result.StatusCode.ToString());
        }
    }
}
