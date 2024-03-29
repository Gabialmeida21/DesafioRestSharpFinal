using RestSharp;
using RestSharpNetCoreTemplate.Bases;
using RestSharpNetCoreTemplate.Helpers;
using RestSharpNetCoreTemplate.Tests;

namespace RestSharpNetCoreTemplate.Requests
{
    public class CreateTokenRequest : RequestBase
    {

        public CreateTokenRequest()
        {
            _urlBase = JsonHelpers.GetParameterAppSettings("URL_REQRES");
            _resource = "/api/rest/users/me/token";
            _method = Method.Post;
            SetupRequest();
        }

        public void SetJsonBody()
        {
            var jsonBody = new
            {
                email = JsonHelpers.GetParameterAppSettings("AUTHENTICATOR_USER"),
                password = JsonHelpers.GetParameterAppSettings("AUTHENTICATOR_PASSWORD")
            };
            _request.AddBody(jsonBody);
        }
      
        public RestResponse Register()
        {
            return ExecuteRequest();
        }
    }
}
