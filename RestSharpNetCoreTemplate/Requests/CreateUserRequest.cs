using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpNetCoreTemplate.Bases;
using RestSharpNetCoreTemplate.Helpers;
using RestSharpNetCoreTemplate.Model;

namespace RestSharpNetCoreTemplate.Requests
{
    public class CreateUserRequest : RequestBase
    {

        public CreateUserRequest()
        {
            _urlBase = JsonHelpers.GetParameterAppSettings("URL_REQRES"); //Chamando a url no appsenttings
            _resource = "/api/users"; //endpoint
            _method = Method.Post; //tipo do metodo 
            SetupRequest();
            _request.AddHeader("Content-Type", "application/json"); //
    
        }

        public void SetJsonBodyWithObject(string name, string job)
        {
            _request.RequestFormat = DataFormat.Json;
            var jsonBody = new
            {
                name = name,
                job = job
            };
            _request.AddBody(jsonBody);
        }

        public void SetJsonBodyWithModel(string name, string job)
        {
            _request.RequestFormat = DataFormat.Json;

            var createUser = new CreateUserModel
            {
                name = name,
                job = job
            };

            _request.AddBody(createUser);
        }
        public void SetJsonBodyWithJsonFile(string name, string job)
        {
            JObject data = GeneralHelpers.ReadJsonContent("Jsons/CreateUser.json");
            data["name"] = name;
            data["job"] = job;

            _request.AddStringBody(data.ToString(), DataFormat.Json);
        }

        public RestResponse CreateUser()
        {
            return ExecuteRequest();
        }
    }
}
