using RestSharp;
using RestSharpNetCoreTemplate.Bases;
using RestSharpNetCoreTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpNetCoreTemplate.Requests
{
    public class GetAllProjectsRequests : RequestBase
    {
        public GetAllProjectsRequests() 
        {
            _urlBase = JsonHelpers.GetParameterAppSettings("URL_MANTIS");
            _resource = "/api/rest/projects/";
            _method = Method.Get;
            SetupRequest();
        }

        public RestResponse ListProjects()
        {
            return ExecuteRequest();
        }
    }
}
