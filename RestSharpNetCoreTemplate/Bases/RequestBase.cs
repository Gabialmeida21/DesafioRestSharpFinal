using RestSharp;
using RestSharpNetCoreTemplate.Helpers;
using RestSharpNetCoreTemplate.Tests;
using System;
using System.Collections.Generic;
using System.IO;

namespace RestSharpNetCoreTemplate.Bases
{
    public class RequestBase
    {
        #region Parameters
        public static string _urlBase;
        public static string _resource;
        public static RestClient _client;
        public static RestRequest _request;
        public static Method _method;
        public static RestResponse _response;
        public static IDictionary<string,string> _parameters = new Dictionary<string, string>();
        #endregion

        #region Restsharp Actions

        public static void SetupRequest()
        {
            //Configurar o Client e a Request
            _client = new RestClient(_urlBase);
            _request = new RestRequest();
            _request.Resource = _resource;
            
        }

        public static RestResponse ExecuteRequest()
        {
            _request.AddHeader("Autorization", RegisterSteps._token);
            //Executar as Requisiçoes
            switch (_method)
            {
                case Method.Get:
                    _response = _client.ExecuteAsync(_request).Result;
                    break;
                case Method.Post:
                    _response = _client.ExecutePostAsync(_request).Result;
                    break;
                case Method.Put:
                    _response = _client.ExecutePutAsync(_request).Result;
                    break;
                case Method.Patch:
                    _response = _client.PatchAsync(_request).Result;
                    break;
                case Method.Delete:
                    _response = _client.DeleteAsync(_request).Result;
                    break;
                default:
                    throw new ArgumentException("Method " + _method.ToString() + " Not implemented.");

            }

            //Add Test info in Extent Report 
            ExtentReportHelpers.AddTestInfo();

            return _response;
        }

        public static void DownloadData(FileTypes fileType)
        {

            // Salva o arquivo na pasta do Extent Report
            byte[] file = _client.DownloadDataAsync(_request).Result;
            File.WriteAllBytes
                (ExtentReportHelpers._reportFolder + "\\" +
                _resource + "." + fileType, file);
        }


        public static void AddParameters()
        {
            
            foreach(var param in _parameters)
            {
                  _request.AddParameter(param.Key, param.Value);
            }
            
            
        }

        public static void RemoveParameter(string parameter)
        {
            _request.Parameters.RemoveParameter(parameter);
        }
        #endregion
    }
}
