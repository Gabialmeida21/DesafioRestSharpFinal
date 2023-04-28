using RestSharp;
using RestSharpNetCoreTemplate.Bases;
using RestSharpNetCoreTemplate.Helpers;
using RestSharpNetCoreTemplate.Tests;

namespace RestSharpNetCoreTemplate.Requests
{
    public class ReadUsersWithAutorizationRequest : RequestBase
    {

        public ReadUsersWithAutorizationRequest()
        {
            _urlBase = JsonHelpers.GetParameterAppSettings("URL_REQRES");
            _resource = "/api/users";
            _method = Method.Get;
            SetupRequest();
        
        }
        public void SetParameter(string parameter)
        {
            _request.AddParameter("id", parameter, ParameterType.QueryString);
        } 
      
        public RestResponse ListUser()
        {
            return ExecuteRequest();
        }

    }
}
