
using RestSharp;
using RestSharpNetCoreTemplate.Bases;
using RestSharpNetCoreTemplate.Helpers;
using RestSharpNetCoreTemplate.Requests;

namespace RestSharpNetCoreTemplate.Tests
{
    public class RegisterSteps : RequestBase
    {
        static RegisterRequest registerRequest;
        public static string _authBaseUrl;
        public static string _authResource;
        public static Method _authMethod;
        public static object _authJsonBody;
        public static RestResponse _authResponseBody;
        public static bool _authIsEnabled = false;
        public static string _token = "";
        private static string GetToken()
        {
            registerRequest = new RegisterRequest();
            registerRequest.SetJsonBody();
            RestResponse response = registerRequest.Register();
            return response.GetValueFromKey("token");
        }

        public static void SetTokenHeader()
        {
            _authIsEnabled = true;
            _token = GetToken();

            _authBaseUrl = _urlBase;
            _authResource = _resource;
            _authMethod = _method;
            _authJsonBody = _request.Parameters.GetParametersFromRequest();
            _authResponseBody = _response;
        }
    }
}
