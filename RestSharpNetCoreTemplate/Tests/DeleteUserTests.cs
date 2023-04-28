using System.Net;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;
using RestSharpNetCoreTemplate.Bases;
using RestSharpNetCoreTemplate.Requests;

namespace RestSharpNetCoreTemplate.Tests
{
    [TestFixture]
    public class DeleteUserTests : TestBase
    {

        DeleteUserRequest deleteUserRequest;

        [Test, Description("Excluir usuário")]
        public void DeleteUser()
        {
            //Arrange
            HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent;
            string userId = "2";

            //Act
            deleteUserRequest = new DeleteUserRequest();
            deleteUserRequest.SetParameter(userId);

            RestResponse response = deleteUserRequest.DeleteUser();

            //Assert
            response.StatusCode.Should().Be(expectedStatusCode);
            response.Content.Should().BeEmpty();


           
        }

    }
}
