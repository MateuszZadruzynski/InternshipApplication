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
    public class CompanyControllerTest
    {
        [Fact]
        public void GetCompanyById()
        {
            var fakeData = A.Dummy<CompanyDTO>();
            var data = A.Fake<CompanyInterface>();
            var user = A.Fake<UserManager<User>>();
            var singIn = A.Fake<SignInManager<User>>();
            var security = A.Fake<IOptions<Security>>();

            A.CallTo(() => data.GetCompany(fakeData.CompanyId)).Returns(fakeData);
            var controller = new CompanyController(user,singIn,data,security);

            var action = controller.GetCompany(fakeData.CompanyId);

            var result = action.Result as OkObjectResult;
            var returnData = result.Value as CompanyDTO;

            Assert.Same(fakeData, returnData);
        }

        [Fact]
        public void GetAllCompanies()
        {
            int number = 5;
            var fakeData = A.CollectionOfDummy<CompanyDTO>(number).AsEnumerable();
            var data = A.Fake<CompanyInterface>();
            var user = A.Fake<UserManager<User>>();
            var singIn = A.Fake<SignInManager<User>>();
            var security = A.Fake<IOptions<Security>>();

            A.CallTo(() => data.GetAllCompanies()).Returns(Task.FromResult(fakeData));
            var controller = new CompanyController(user, singIn, data, security);

            var action = controller.GetCompanies();

            var result = action.Result as OkObjectResult;
            var returnData = result.Value as IEnumerable<CompanyDTO>;

            Assert.Equal(number, returnData.Count());
        }

        [Fact]
        public void checkNipShouldBeNull()
        {
            var fakeData = A.Dummy<CompanyDTO>();
            var data = A.Fake<CompanyInterface>();
            var user = A.Fake<UserManager<User>>();
            var singIn = A.Fake<SignInManager<User>>();
            var security = A.Fake<IOptions<Security>>();

            A.CallTo(() => data.IsNipUnique(fakeData.nip, fakeData.CompanyId)).Returns(Task.FromResult(fakeData));
            var controller = new CompanyController(user, singIn, data, security);

            var action = controller.CheckNIP("1231231235");
            var result = action.Result as CompanyDTO;

            Assert.NotEqual(fakeData, result);
        }

        [Fact]
        public async Task UpdateCompanyAsync()
        {
            var fakeData = A.Dummy<CompanyDTO>();
            var data = A.Fake<CompanyInterface>();
            var user = A.Fake<UserManager<User>>();
            var singIn = A.Fake<SignInManager<User>>();
            var security = A.Fake<IOptions<Security>>();

            A.CallTo(() => data.UpdateCompany(fakeData.CompanyId, fakeData));
            var controller = new CompanyController(user, singIn, data, security);

            var action = await controller.UpdateCompany(fakeData.CompanyId, fakeData);
            var result = action.Result as OkObjectResult;
            Assert.Equal("200", result.StatusCode.ToString()) ;
        }
    }
}
