using RestSharp;
using RestSharpNetCoreTemplate.Bases;
using RestSharpNetCoreTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpNetCoreTemplate.Requests
{
    public class ReadFileRequest : RequestBase
    {

        public ReadFileRequest()
        {
            _urlBase = JsonHelpers.GetParameterAppSettings("URL_FILE");
            _resource = "Samsung_A510F_Galaxy_A5_(2016)_Manual_do_usuário.pdf";
            _method = Method.Get;
            SetupRequest();
        }

        public RestResponse GetFile()
        {
            RestResponse response = ExecuteRequest();

            return response;
        }
    }
}
