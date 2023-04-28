using RestSharp;
using RestSharpNetCoreTemplate.Bases;
using RestSharpNetCoreTemplate.Helpers;

namespace RestSharpNetCoreTemplate.Requests
{
    public class UpdateUserRequest : RequestBase
    {

        public UpdateUserRequest()
        {
            _urlBase = JsonHelpers.GetParameterAppSettings("URL_REQRES");
            _resource = "/api/users/{id}";
            _method = Method.Put;
            SetupRequest();
    
        }
        public void SetParameter(string parameter)
        {
            _request.AddParameter("id", parameter, ParameterType.UrlSegment);
        }

        public void SetJsonBody(string name, string job)
        {
            _request.RequestFormat = DataFormat.Json;
            var jsonBody = new
            {
                name = name,
                job = job
            };
            _request.AddBody(jsonBody);
        }

        public RestResponse UpdateUser()
        {
            return ExecuteRequest();
        }
    }
}
