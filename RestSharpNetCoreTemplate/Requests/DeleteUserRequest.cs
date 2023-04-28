using RestSharp;
using RestSharpNetCoreTemplate.Bases;
using RestSharpNetCoreTemplate.Helpers;

namespace RestSharpNetCoreTemplate.Requests
{
    public class DeleteUserRequest : RequestBase
    {

        public DeleteUserRequest()
        {
            _urlBase = JsonHelpers.GetParameterAppSettings("URL_REQRES");
            _resource = "/api/users/{id}";
            _method = Method.Delete;
            SetupRequest();
        }
        public void SetParameter(string parameter)
        {
            _request.AddParameter("id", parameter, ParameterType.UrlSegment);
        }

        public RestResponse DeleteUser()
        {
            return ExecuteRequest();
        }
    }
}
