using RestSharp;
using RestSharpNetCoreTemplate.Bases;
using RestSharpNetCoreTemplate.Helpers;

namespace RestSharpNetCoreTemplate.Requests
{
    public class ReadUsersQueryStringRequest : RequestBase
    {

        public ReadUsersQueryStringRequest()
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
