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
    public class CompanyImmageControllerTest
    {
        [Fact]
        public void GetAllCompanyImages()
        {
            int number = 5;
            var fakeData = A.CollectionOfDummy<CompanyImageDTO>(number);
            var data = A.Fake<CompanyImageInterface>();
            A.CallTo(() => data.GetAllImages()).Returns(fakeData);
            var controller = new CompanyImageController(data);

            var action = controller.GetAllCompanyImages();

            var result = action.Result as OkObjectResult;
            var returnData = result.Value as IEnumerable<CompanyImageDTO>;

            Assert.Same(fakeData, returnData);
        }

        [Fact]
        public void CreateCompanyImage()
        {
            var fakeData = A.Dummy<CompanyImageDTO>();
            var data = A.Fake<CompanyImageInterface>();
            A.CallTo(() => data.CreateImage(fakeData));
            var controller = new CompanyImageController(data);

            var action = controller.CreateCompanyImage(fakeData);
            var result = action.Result as ObjectResult;

            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public void GetCompanyImageByCompanyId()
        {
            var fakeData = A.Dummy<CompanyImageDTO>();
            var data = A.Fake<CompanyImageInterface>();
            A.CallTo(() => data.GetCompanyImage(fakeData.CompanyId)).Returns(Task.FromResult(fakeData));
            var controller = new CompanyImageController(data);

            var action = controller.GetCompanyImage(fakeData.CompanyId);

            var result = action.Result as OkObjectResult;
            var returnData = result.Value as CompanyImageDTO;

            Assert.Equal(fakeData, returnData);
        }

        [Fact]
        public void DeleteImageByImageId()
        {
            int DeletepId = int.MinValue;
            var fakeData = A.Dummy<CompanyImageDTO>();
            var data = A.Fake<CompanyImageInterface>();
            A.CallTo(() => data.DeleteImageByImageId(DeletepId));
            var controller = new CompanyImageController(data);

            var action = controller.DeleteImage(DeletepId);
            var result = action.Result as OkObjectResult;

            Assert.Equal("200", result.StatusCode.ToString());
        }
    }
}
