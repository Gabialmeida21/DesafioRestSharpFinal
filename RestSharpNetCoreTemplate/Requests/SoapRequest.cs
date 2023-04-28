using RestSharp;
using RestSharpNetCoreTemplate.Bases;
using RestSharpNetCoreTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace RestSharpNetCoreTemplate.Requests
{
    public class SoapRequest : RequestBase
    {
        public SoapRequest()
        {
            _urlBase = JsonHelpers.GetParameterAppSettings("URL_SOAP");
            _resource = "/calculator.asmx";
            _method = Method.Post;
            SetupRequest();
            _request.AddHeader("Accept-Encoding", "gzip,deflate");
            _request.AddHeader("Content-Type", "text/xml;charset=UTF-8");
            

        }
        public static void SetXmlBody(int value1, int value2)
        {

            string content = GeneralHelpers.ReadFileContent("Xml\\AddTwoNumbers.xml"); // Load the xml file content 
            content = content.Replace("$valor1", value1.ToString()).Replace("$valor2", value2.ToString());

            _request.AddStringBody(content, DataFormat.Xml);

        }

        public RestResponse AddTwoNumbers(int value1, int value2)
        {
            SetXmlBody(value1, value2);

            return ExecuteRequest();
        }

    }
}
