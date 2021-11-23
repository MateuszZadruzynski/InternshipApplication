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
    public class StudentControllerTest
    {
        [Fact]
        public void GetStudentByStudentId()
        {
            var fakeData = A.Dummy<StudentDTO>();
            var data = A.Fake<StudentInterface>();
            var user = A.Fake<UserManager<User>>();
            var singIn = A.Fake<SignInManager<User>>();
            var security = A.Fake<IOptions<Security>>();
            A.CallTo(() => data.GetStudent(fakeData.StudentId)).Returns(fakeData);
            var controller = new StudentController(user, singIn, data, security);

            var action = controller.GetStudent(fakeData.StudentId);

            var result = action.Result as OkObjectResult;
            var returnData = result.Value as StudentDTO;

            Assert.Same(fakeData, returnData);
        }

        [Fact]
        public void GetStudentByEmail()
        {
            var fakeData = A.Dummy<StudentDTO>();
            var data = A.Fake<StudentInterface>();
            var user = A.Fake<UserManager<User>>();
            var singIn = A.Fake<SignInManager<User>>();
            var security = A.Fake<IOptions<Security>>();

            var controller = new StudentController(user, singIn, data, security);

            var action = controller.GetStudentByEmail(fakeData.email);

            var result = action.Result as KeeperDTO;

            Assert.Null(result);
        }


        [Fact]
        public async Task UpdateStudent()
        {
            var fakeData = A.Dummy<StudentDTO>();
            var data = A.Fake<StudentInterface>();
            var user = A.Fake<UserManager<User>>();
            var singIn = A.Fake<SignInManager<User>>();
            var security = A.Fake<IOptions<Security>>();

            A.CallTo(() => data.UpdateStudent(fakeData.StudentId, fakeData)).Returns(fakeData);
            var controller = new StudentController(user, singIn, data, security);


            var action = await controller.UpdateStudent(fakeData.StudentId, fakeData);

            var result = action.Result as OkObjectResult;
            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public void checkIndexShouldBeNull()
        {
            var fakeData = A.Dummy<StudentDTO>();
            var data = A.Fake<StudentInterface>();
            var user = A.Fake<UserManager<User>>();
            var singIn = A.Fake<SignInManager<User>>();
            var security = A.Fake<IOptions<Security>>();

            A.CallTo(() => data.IsIndexUnique(fakeData.index, fakeData.StudentId)).Returns(fakeData);
            var controller = new StudentController(user, singIn, data, security);

            var action = controller.CheckIndex("1231231235");
            var result = action.Result as StudentDTO;

            Assert.NotEqual(fakeData, result);
        }
    }
}
