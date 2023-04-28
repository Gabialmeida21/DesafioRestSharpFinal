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

        [Test, Description("Criar novo usuário com sucesso")] // Descrição do Teste
        public void CreateUserWithJsonObject() //Nome do Teste
        {
            //Arrange - Definição da massa de teste e resultado esperado
            string name = "UserXPTO";
            string job = "QA Engineer";
            HttpStatusCode expectedStatusCode = HttpStatusCode.Created;

            //Act - Construção e instância dos objetos
            postCreateUserRequest = new CreateUserRequest();
            postCreateUserRequest.SetJsonBodyWithObject(name, job);

            RestResponse response = postCreateUserRequest.CreateUser();//Executa a requisição

            //Asserts (Fluent Assertions) - Asserções 
            response.StatusCode.Should().Be(expectedStatusCode);//Valida Status code

            using (new AssertionScope())//Soft Assert - Valida o grupo de asserções - Concatena os erros caso houver
            {

                response.GetValueFromKey("name").Should().Be(name); // Valida se o atributo do json retornado é igual ao esperado
                response.GetValueFromKey("job").Should().Be(job); // Valida se o atributo do json retornado é igual ao esperado
                response.GetValueFromKey("id").Should().BeOfType(typeof(string)).And.NotBeNull(); // Valida se o atributo do json retornado é do tipo string e não é nulo
                response.Deserialize().Should().HaveCount(4); // Valida se o objeto possui 4 atributos
                response.Deserialize().Should().NotBeNull(); // Valida se o objeto não possui atributos nulos
            }
        }

        [Test, Description("Criar novo usuário")]
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

        [Test, Description("Criar novo usuário")]
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
