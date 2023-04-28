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
    public class CreateUserTests : TestBase
    {

        CreateUserRequest postCreateUserRequest;

        [Test, Description("Criar novo usu�rio com sucesso")] // Descri��o do Teste
        public void CreateUserWithJsonObject() //Nome do Teste
        {
            //Arrange - Defini��o da massa de teste e resultado esperado
            string name = "UserXPTO";
            string job = "QA Engineer";
            HttpStatusCode expectedStatusCode = HttpStatusCode.Created;

            //Act - Constru��o e inst�ncia dos objetos
            postCreateUserRequest = new CreateUserRequest();
            postCreateUserRequest.SetJsonBodyWithObject(name, job);

            RestResponse response = postCreateUserRequest.CreateUser();//Executa a requisi��o

            //Asserts (Fluent Assertions) - Asser��es 
            response.StatusCode.Should().Be(expectedStatusCode);//Valida Status code

            using (new AssertionScope())//Soft Assert - Valida o grupo de asser��es - Concatena os erros caso houver
            {

                response.GetValueFromKey("name").Should().Be(name); // Valida se o atributo do json retornado � igual ao esperado
                response.GetValueFromKey("job").Should().Be(job); // Valida se o atributo do json retornado � igual ao esperado
                response.GetValueFromKey("id").Should().BeOfType(typeof(string)).And.NotBeNull(); // Valida se o atributo do json retornado � do tipo string e n�o � nulo
                response.Deserialize().Should().HaveCount(4); // Valida se o objeto possui 4 atributos
                response.Deserialize().Should().NotBeNull(); // Valida se o objeto n�o possui atributos nulos
            }
        }

        [Test, Description("Criar novo usu�rio")]
        public void CreateUserWithClassModel()
        {
            //Arrange
            string name = "UserXPTO";
            string job = "QA Engineer";
            HttpStatusCode expectedStatusCode = HttpStatusCode.Created;

            //Act
            postCreateUserRequest = new CreateUserRequest();
            postCreateUserRequest.SetJsonBodyWithModel(name, job);

            RestResponse response = postCreateUserRequest.CreateUser();

            //Assert
            response.StatusCode.Should().Be(expectedStatusCode);
            
        }

        [Test, Description("Criar novo usu�rio")]
        public void CreateUserWithJsonFile()
        {
            //Arrange
            string name = "UserXPTO";
            string job = "QA Engineer";
            HttpStatusCode expectedStatusCode = HttpStatusCode.Created;

            //ACT
            postCreateUserRequest = new CreateUserRequest();
            postCreateUserRequest.SetJsonBodyWithJsonFile(name, job);

            RestResponse response = postCreateUserRequest.CreateUser();

            //Assert
            response.StatusCode.Should().Be(expectedStatusCode);
            

        }
       
    }
}
