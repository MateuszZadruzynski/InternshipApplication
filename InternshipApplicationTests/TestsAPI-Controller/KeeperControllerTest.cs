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
    public class KeeperControllerTest
    {
        [Fact]
        public void GetKeeperByKeeperId()
        {
            var fakeData = A.Dummy<KeeperDTO>();
            var data = A.Fake<KeeperInterface>();
            var user = A.Fake<UserManager<User>>();
            var singIn = A.Fake<SignInManager<User>>();
            var security = A.Fake<IOptions<Security>>();
            A.CallTo(() => data.GetKeeper(fakeData.KeeperId)).Returns(fakeData);
            var controller = new KeeperController(user, singIn, data, security);

            var action = controller.GetKeeper(fakeData.KeeperId);

            var result = action.Result as ObjectResult;
            var returnData = result.Value as KeeperDTO;

            Assert.Same(fakeData, returnData);
        }

        [Fact]
        public void GetKeeperByEmail()
        {
            var fakeData = A.Dummy<KeeperDTO>();
            var data = A.Fake<KeeperInterface>();
            var user = A.Fake<UserManager<User>>();
            var singIn = A.Fake<SignInManager<User>>();
            var security = A.Fake<IOptions<Security>>();
            var controller = new KeeperController(user, singIn, data, security);

            var action = controller.GetKeepertByEmail(fakeData.email);

            var result = action.Result as KeeperDTO;

            Assert.Null(result);
        }


        [Fact]
        public async Task UpdateKeeper()
        {
            var fakeData = A.Dummy<KeeperDTO>();
            var data = A.Fake<KeeperInterface>();
            var user = A.Fake<UserManager<User>>();
            var singIn = A.Fake<SignInManager<User>>();
            var security = A.Fake<IOptions<Security>>();
            A.CallTo(() => data.UpdateKeeper(fakeData.KeeperId,fakeData)).Returns(fakeData);
            var controller = new KeeperController(user, singIn, data, security);


            var action = await controller.UpdateKeeper(fakeData.KeeperId,fakeData);

            var result = action.Result as OkObjectResult;
            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public void GetAllCompanyKeepers()
        {
            int CompanyId = 2;
            int number = 5;
            var fakeData = A.CollectionOfDummy<KeeperDTO>(number).AsEnumerable();
            var data = A.Fake<KeeperInterface>();
            var user = A.Fake<UserManager<User>>();
            var singIn = A.Fake<SignInManager<User>>();
            var security = A.Fake<IOptions<Security>>();
            A.CallTo(() => data.GetAllCompanyKeepers(CompanyId)).Returns(fakeData);
            var controller = new KeeperController(user, singIn, data, security);

            var action = controller.GetCompanyKeepers(CompanyId);

            var result = action.Result as OkObjectResult;
            var returnData = result.Value as IEnumerable<KeeperDTO>;

            Assert.Same(fakeData, returnData);
        }

        [Fact]
        public void DeleteKeeper()
        {
            int DeleteKeeperId = int.MinValue;
            var fakeData = A.Dummy<KeeperDTO>();
            var data = A.Fake<KeeperInterface>();
            var user = A.Fake<UserManager<User>>();
            var singIn = A.Fake<SignInManager<User>>();
            var security = A.Fake<IOptions<Security>>();
            A.CallTo(() => data.DeleteKeeper(DeleteKeeperId));
            var controller = new KeeperController(user, singIn, data, security);

            var action = controller.DeleteKeeper(DeleteKeeperId);
            var result = action.Result as OkObjectResult;

            Assert.Equal("200", result.StatusCode.ToString());
        }
    }
}
