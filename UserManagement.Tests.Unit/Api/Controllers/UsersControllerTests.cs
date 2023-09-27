using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UsersManagement.Api.Controllers;
using UsersManagement.Core;
using UsersManagement.Domain.Models;
using Xunit;

namespace UsersManagement.Tests.Unit.Api.Controllers
{
    public class UsersControllerTests
    {

        //[Fact]
        //public async Task Sum_returns_summary_Async()
        //{
        //    // arrage
        //    int x = 1;
        //    int y = 2;
        //    var z = 3;
        //    UsersController controller = new UsersController(null);
        //    // Act
        //    var result = await controller.Sum(x, y);

        //    // Assert

        //    Assert.Equal(z, result);
        //}

        [Fact]
        public async Task DeleteUserAsync_return_200_notNull()
        {
            // arrage
            var mock = new Mock<IUserService>();
            int userId = 1;
            int StatusCode = 200;
            UsersController controller = new UsersController(mock.Object);

            // Act

            var result = await controller.DeleteUserAsync(userId);

            // Assert

            Assert.NotEqual(null,  result);
            Assert.Equal(StatusCode, ((OkObjectResult) result).StatusCode);
        }

        [Fact]
        public async Task GetUserAsync_returns_200_And_Data_IfUserFound()
        {
            // arrage
            var mock = new Mock<IUserService>();

            int userId = 1;
            int StatusCode = 200;
            UserModel model = new UserModel
            {
                Name = "1",
                Email = "xv@xcv",
                RoleId= 2,
                RoleName ="123",
            };
            UserModel mocModel = new UserModel
            {
                Name = "1",
                Email = "xv@xcv",
                RoleId = 2,
                RoleName = "123",
            };

            mock.Setup(a => a.GetUserAsync(It.IsAny<int>()))
             .ReturnsAsync(mocModel);

            // Act

            UsersController controller = new UsersController(mock.Object);

            var step1Result = await controller.GetUserAsync(userId);
            var step2result = (step1Result as ObjectResult);
            var result = step2result?.Value as UserModel;

            // Assert
            //result.Value
            Assert.NotNull(result);
            Assert.Equal(StatusCode, step2result.StatusCode);
            Assert.Equal(model.Name,result.Name);
            Assert.Equal(model.Email, result.Email);
            Assert.Equal(model.RoleId, result.RoleId);
            Assert.Equal(model.RoleName, result.RoleName);
        }
    }
}
