using Administration.Interface;
using DataAcess.Data;
using FakeItEasy;
using InternshipManagmentAPI.Controllers;
using InternshipManagmentAPI.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace InternshipApplicationTests.TestsAPI
{
    public class StudentKeeperControllerTest
    {
        [Fact]
        public void GetStudentsByKeepertId()
        {
            int KeeperId = 2;
            int number = 4;
            var fakeData = A.CollectionOfDummy<StudentKeeperDTO>(number).AsEnumerable();
            var data = A.Fake<StudentKeeperInterface>();

            A.CallTo(() => data.GetAllStudentIdByKeeperId(KeeperId)).Returns(Task.FromResult(fakeData));
            var controller = new StudentKeeperController(data);

            var action = controller.GetStudents(KeeperId);

            var result = action.Result as OkObjectResult;
            var returnData = result.Value as IEnumerable<StudentKeeperDTO>;

            Assert.Same(fakeData, returnData);
        }

        [Fact]
        public void GetKeepersByStudentId()
        {
            int StudentId = 2;
            int number = 4;
            var fakeData = A.CollectionOfDummy<StudentKeeperDTO>(number).AsEnumerable();
            var data = A.Fake<StudentKeeperInterface>();

            A.CallTo(() => data.GetAllKeeperIdByStudentId(StudentId)).Returns(Task.FromResult(fakeData));
            var controller = new StudentKeeperController(data);

            var action = controller.GetKeepers(StudentId);

            var result = action.Result as OkObjectResult;
            var returnData = result.Value as IEnumerable<StudentKeeperDTO>;

            Assert.Same(fakeData, returnData);
        }

        [Fact]
        public void DeleteKeeperByKeeperId()
        {
            int DeleteStudentKeeperDTOId = int.MinValue;
            var fakeData = A.Dummy<StudentKeeperDTO>();
            var data = A.Fake<StudentKeeperInterface>();
            A.CallTo(() => data.DeleteKeeperByKeeperId(DeleteStudentKeeperDTOId));
            var controller = new StudentKeeperController(data);

            var action = controller.DeleteKeeper(DeleteStudentKeeperDTOId);
            var result = action.Result as OkObjectResult;

            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public void DeleteStudentrByStudentId()
        {
            int DeleteStudentKeeperDTOId = int.MinValue;
            var fakeData = A.Dummy<StudentKeeperDTO>();
            var data = A.Fake<StudentKeeperInterface>();
            A.CallTo(() => data.DeleteStudentByStudentId(DeleteStudentKeeperDTOId));
            var controller = new StudentKeeperController(data);

            var action = controller.DeleteStudent(DeleteStudentKeeperDTOId);
            var result = action.Result as OkObjectResult;

            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public void CreateStudentKeeper()
        {
            var fakeData = A.Dummy<StudentKeeperDTO>();
            var data = A.Fake<StudentKeeperInterface>();
            A.CallTo(() => data.CreateStudentKeeper(fakeData)).Returns(Task.FromResult(fakeData));
            var controller = new StudentKeeperController(data);

            var action = controller.CreateKeeperStudent(fakeData);
            var result = action.Result as ObjectResult;

            Assert.Equal("200", result.StatusCode.ToString());
        }
    }
}
