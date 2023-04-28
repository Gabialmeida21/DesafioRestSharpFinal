using FluentAssertions;
using NUnit.Framework;
using RestSharp;
using RestSharpNetCoreTemplate.Bases;
using RestSharpNetCoreTemplate.Helpers;
using RestSharpNetCoreTemplate.Requests;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RestSharpNetCoreTemplate.Tests
{
    public class SoapTests : TestBase
    {
        SoapRequest soapRequest;
        [Test,Description("Teste Soap")]
        public void SoapAddTwoNumbers()
        {
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            int valor1 = 10;
            int valor2 = 15;

            soapRequest = new SoapRequest();
            RestResponse response = soapRequest.AddTwoNumbers(valor1, valor2);

            //Asserts (Fluent Assertions)
            response.StatusCode.Should().Be(expectedStatusCode);

            response.GetValueFromXMLKey("AddResult").Should().Be("25");
        }
    }
}
