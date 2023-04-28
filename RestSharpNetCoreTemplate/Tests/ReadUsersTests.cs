using FluentAssertions;
using FluentAssertions.Execution;
using FluentValidation;
using NUnit.Framework;
using RestSharp;
using RestSharpNetCoreTemplate.Bases;
using RestSharpNetCoreTemplate.Helpers;
using RestSharpNetCoreTemplate.Requests;
using System.Net;

namespace RestSharpNetCoreTemplate.Tests
{
    [TestFixture]
    public class ReadUsersTests : TestBase
    {

        ReadUsersQueryStringRequest listUsersQueryStringRequest;
        ReadUsersUrlSegmentRequest listUsersUrlSegmentRequest;


        [Test, Description("Listar todos usuários")]
        public void ReadAllUsers()
        {
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;

            listUsersQueryStringRequest = new ReadUsersQueryStringRequest();
            RestResponse response = listUsersQueryStringRequest.ListUser();


            //Asserts 
            response.StatusCode.Should().Be(expectedStatusCode);//Validação status code
            using (new AssertionScope())//Soft Assert 
            {
               
                response.GetValueFromKey("data[0].id").ToString().Should().NotBeEmpty(); // Validação do id em todos objetos - NotEmpty
                
                response.Deserialize()["data"].ForEach(x => x.SelectToken("id").ToString().Should().NotBeEmpty());
                response.Deserialize()["data"].ForEach(x => x.SelectToken("email").ToString().Should().Contain("@"));
                response.Deserialize()["data"].ForEach(x => x.SelectToken("first_name").ToString().Should().NotBeEmpty());
                response.Deserialize()["data"].ForEach(x => x.SelectToken("last_name").ToString().Should().NotBeEmpty());
                response.Deserialize()["data"].ForEach(x => x.SelectToken("avatar").ToString().Should().Contain("https://"));

                response.Deserialize().Should().HaveCount(6);
                response.Deserialize()["data"].Should().HaveCount(6);
            }
        }

        [Test, Description("Listar usuário por ID")]
        public void ReadUserByIdWithQueryString()
        {
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            string userId = "2";
            
            listUsersQueryStringRequest = new ReadUsersQueryStringRequest();
            listUsersQueryStringRequest.SetParameter(userId);
           
            RestResponse response = listUsersQueryStringRequest.ListUser();

            //Asserts 
            response.StatusCode.Should().Be(expectedStatusCode);//Valida status code

            using (new AssertionScope()) //SoftAssert
            {
                response.GetValueFromKey("data.id").Should().Be(userId);//Valida se o ID retornado é igual ao informado de parametro
                response.GetValueFromKey("data.email").Should().Contain("@"); //Valida se o Email contém "@"
                response.GetValueFromKey("data.first_name").Should().BeOfType(typeof(string)).And.NotBeNull();// Valida e o first_name é do tipo string e não é nulo
                response.GetValueFromKey("data.avatar").Should().BeOfType(typeof(string)).And.NotBeNull();
                response.GetValueFromKey("support.url").Should().BeOfType(typeof(string)).And.Contain("https://"); //Valida se a url contém "https://"

                response.Deserialize().Should().HaveCount(2); //Conta atributos do objeto principal
                response.Deserialize()["data"].Should().HaveCount(5); //Conta atributos do objeto "data"
                response.Deserialize()["support"].Should().HaveCount(2); //Conta atributos do objeto "support"
            }
        }

        [Test, Description("Listar usuário por ID")]
        public void ReadUserByIdWithUrlSegment()
        {   //Arrange - Definição da massa de teste e resultado esperado    
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            string userId = "2";

            //Act -  Construção e instancia dos objetos
            listUsersUrlSegmentRequest = new ReadUsersUrlSegmentRequest();
            listUsersUrlSegmentRequest.SetParameter(userId);
            RestResponse response = listUsersUrlSegmentRequest.ListUser();//Execução da requisição

            //Asserts (Fluent Assertions) - Asserções
            response.StatusCode.Should().Be(expectedStatusCode);//Valida status code
            using (new AssertionScope())//Soft Assert - Grupo de asserções - Execução sem interrupção
            {
                response.GetValueFromKey("data.id").Should().Be(userId); // Validação de atributo - Equals 
                response.GetValueFromKey("data.email").Should().Contain("@");// Validação de atributo - Contanis

                response.GetValueFromKey("data.first_name").Should().BeOfType(typeof(string)).And.NotBeNull(); //Validação objeto.atributo - Tipo e Not Null
                response.GetValueFromKey("data.avatar").Should().BeOfType(typeof(string)).And.NotBeNull(); //Validação objeto.atributo - Tipo e Not Null
                response.GetValueFromKey("support.url").Should().BeOfType(typeof(string)).And.NotBeNull(); //Validação objeto.atributo - Tipo e Not Null

                response.Deserialize().Should().HaveCount(2); //Validação quantidade de atributos no objeto default
                response.Deserialize()["data"].Should().HaveCount(5); //Validação quantidade de atributos no objeto "data"
                response.Deserialize()["support"].Should().HaveCount(2); //Validação quantidade de atributos no objeto "support"
            }
        }

        [Test, Description("Listar usuário por ID")]
        public void ReadUserByIdSetParameters()
        {
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            RequestBase._parameters.Add("id","2");

            listUsersQueryStringRequest = new ReadUsersQueryStringRequest();
            RequestBase.AddParameters(); 

            RestResponse response = listUsersQueryStringRequest.ListUser();

            //Asserts 
            response.StatusCode.Should().Be(expectedStatusCode);//Valida status code

            using (new AssertionScope()) //SoftAssert
            {
                response.GetValueFromKey("data.id").Should().Be("2");//Valida se o ID retornado é igual ao informado de parametro
               
            }
        }


    }
}
