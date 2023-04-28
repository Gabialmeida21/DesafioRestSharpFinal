using NUnit.Framework;
using RestSharp;
using RestSharpNetCoreTemplate.Bases;
using RestSharpNetCoreTemplate.Helpers;
using RestSharpNetCoreTemplate.Requests;
using RestSharpNetCoreTemplate.Steps;
using System.Net;

namespace RestSharpNetCoreTemplate.Tests
{    
    [TestFixture]
    public class ReadFileTests : TestBase
    {

        ReadFileRequest fileRequest;

        [Test]
        public void ReadAndDownloadFile()
        {
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;

            fileRequest = new ReadFileRequest();

            RestResponse response = fileRequest.GetFile();
            ReadFileSteps.DownloadPdfFile();

            Assert.AreEqual(expectedStatusCode, response.StatusCode);
        }

        
        
    }
}
