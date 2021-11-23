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
    public class StudentAvatarControllerTest
    {
        [Fact]
        public void CreateKeeprAvatar()
        {
            var fakeData = A.Dummy<StudentAvatarsDTO>();
            var data = A.Fake<StudentAvatarsInterface>();
            A.CallTo(() => data.CreateAvatar(fakeData));
            var controller = new StudentAvatarController(data);

            var action = controller.CreateStudentAvatar(fakeData);
            var result = action.Result as ObjectResult;

            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public void GetStudentAvatarByStudentId()
        {
            var fakeData = A.Dummy<StudentAvatarsDTO>();
            var data = A.Fake<StudentAvatarsInterface>();
            A.CallTo(() => data.GetStudentAvatar(fakeData.AvatarId)).Returns(fakeData);
            var controller = new StudentAvatarController(data);

            var action = controller.GetStudentAvatar(fakeData.AvatarId);
            var result = action.Result as OkObjectResult;
            var returnData = result.Value as StudentAvatarsDTO;

            Assert.Equal(fakeData, returnData);
        }

        [Fact]
        public void DeleteStudentAvatarByStudentId()
        {
            int DeletepId = int.MinValue;
            var fakeData = A.Dummy<StudentAvatarsDTO>();
            var data = A.Fake<StudentAvatarsInterface>();
            A.CallTo(() => data.DeleteAvatarByStudentId(DeletepId));
            var controller = new StudentAvatarController(data);

            var action = controller.DeleteStudentAvatar(DeletepId);
            var result = action.Result as OkObjectResult;

            Assert.Equal("200", result.StatusCode.ToString());
        }
    }
}
