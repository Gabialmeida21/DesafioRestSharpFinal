using System.Net;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using RestSharp;
using RestSharpNetCoreTemplate.Bases;
using RestSharpNetCoreTemplate.Helpers;
using RestSharpNetCoreTemplate.Requests;

namespace RestSharpNetCoreTemplate.Tests
{
    [TestFixture]
    public class UpdateUserTests : TestBase
    {

        UpdateUserRequest updateUserRequest;

        [Test, Description("Alterar usuário")]
        public void UpdateUser()
        {
            //Arrange
            string name = "UserXPTO";
            string job = "QA Engineer";
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            string userId = "2";

            //Act
            updateUserRequest = new UpdateUserRequest();
            updateUserRequest.SetParameter(userId);
            updateUserRequest.SetJsonBody(name, job);

            RestResponse response = updateUserRequest.UpdateUser();

            //Assert
            response.StatusCode.Should().Be(expectedStatusCode);
            using (new AssertionScope())//Soft Assert
            {
                response.GetValueFromKey("name").Should().Be(name);
                response.GetValueFromKey("job").Should().Be(job);
                response.GetValueFromKey("updatedAt").Should().NotBeNull();
            }
        }

    }
}
