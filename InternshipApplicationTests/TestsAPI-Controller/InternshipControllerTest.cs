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
    public class InternshipControllerTest
    {
        [Fact]
        public void GetInternships()
        {
            int number = 5;
            var fakeData = A.CollectionOfDummy<InternshipDTO>(number).AsEnumerable();
            var data = A.Fake<InternshipInterface>();
            A.CallTo(() => data.GetAllInternships()).Returns(Task.FromResult(fakeData));
            var controller = new InternshipController(data);


            var action = controller.GetCompanies();

            var result = action.Result as OkObjectResult;
            var returnData = result.Value as IEnumerable<InternshipDTO>;

            Assert.Equal(number, returnData.Count());
        }

        [Fact]
        public void GetInternshipById()
        {
            var fakeData = A.Dummy<InternshipDTO>();
            var data = A.Fake<InternshipInterface>();
            A.CallTo(() => data.GetInternship(fakeData.InternshipID)).Returns(fakeData);
            var controller = new InternshipController(data);

            var action = controller.GetInternship(fakeData.InternshipID);

            var result = action.Result as OkObjectResult;
            var returnData = result.Value as InternshipDTO;

            Assert.Same(fakeData, returnData);
        }

        [Fact]
        public void CreateInternship()
        {
            var fakeData = A.Dummy<InternshipDTO>();
            var data = A.Fake<InternshipInterface>();
            A.CallTo(() => data.CreateInternship(fakeData));
            var controller = new InternshipController(data);

            var action = controller.CreateInternship(fakeData);
            var result = action.Result as ObjectResult;

            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public void GetInternshipByCompanyId()
        {
            int CompanyId = 2;
            int number = 5;
            var fakeData = A.CollectionOfDummy<InternshipDTO>(number).AsEnumerable();
            var data = A.Fake<InternshipInterface>();
            A.CallTo(() => data.GetAllCompanyInternships(CompanyId)).Returns(fakeData);
            var controller = new InternshipController(data);

            var action = controller.GetInternshipByCompanyId(CompanyId);

            var result = action.Result as OkObjectResult;
            var returnData = result.Value as IEnumerable<InternshipDTO>;

            Assert.Same(fakeData, returnData); 
        }

        [Fact]
        public void DeleteInternships()
        {
            int DeleteInternshipId = int.MinValue;
            var fakeData = A.Dummy<InternshipDTO>();
            var data = A.Fake<InternshipInterface>();
            A.CallTo(() => data.DeleteInternship(DeleteInternshipId));
            var controller = new InternshipController(data);

            var action = controller.DeleteInternship(DeleteInternshipId);
            var result = action.Result as OkObjectResult;

            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public void UpdateInternship()
        {
            var fakeData = A.Dummy<InternshipDTO>();
            var data = A.Fake<InternshipInterface>();
            A.CallTo(() => data.UpdateInternship(fakeData.InternshipID, fakeData));
            var controller = new InternshipController(data);

            var action = controller.UpdateInternship(fakeData.InternshipID, fakeData);
            var result = action.Result as OkObjectResult;

            Assert.Equal("200", result.StatusCode.ToString());
        }
    }
}
