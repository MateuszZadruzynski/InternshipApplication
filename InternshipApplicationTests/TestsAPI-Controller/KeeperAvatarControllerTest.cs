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
    public class KeeperAvatarControllerTest
    {

        [Fact]
        public void CreateKeeprAvatar()
        {
            var fakeData = A.Dummy<KeeperAvatarsDTO>();
            var data = A.Fake<KeeperAvatarsInterface>();
            A.CallTo(() => data.CreateAvatar(fakeData));
            var controller = new KeeperAvatarController(data);

            var action = controller.CreateKeeperAvatar(fakeData);
            var result = action.Result as ObjectResult;

            Assert.Equal("200", result.StatusCode.ToString());
        }

        [Fact]
        public void GetKeeperAvatarByKeeperId()
        {
            var fakeData = A.Dummy<KeeperAvatarsDTO>();
            var data = A.Fake<KeeperAvatarsInterface>();
            A.CallTo(() => data.GetKeeperAvatar(fakeData.AvatarId)).Returns(Task.FromResult(fakeData));
            var controller = new KeeperAvatarController(data);

            var action = controller.GetKeeperAvatar(fakeData.AvatarId);
            var result = action.Result as OkObjectResult;
            var returnData = result.Value as KeeperAvatarsDTO;

            Assert.Same(fakeData, returnData);
        }

        [Fact]
        public void DeleteKeeperAvatarByKeeperId()
        {
            int DeletepId = int.MinValue;
            var fakeData = A.Dummy<KeeperAvatarsDTO>();
            var data = A.Fake<KeeperAvatarsInterface>();
            A.CallTo(() => data.DeleteAvatarByKeeperId(DeletepId));
            var controller = new KeeperAvatarController(data);

            var action = controller.DeleteAvatar(DeletepId);
            var result = action.Result as OkObjectResult;

            Assert.Equal("200", result.StatusCode.ToString());
        }
    }
}
