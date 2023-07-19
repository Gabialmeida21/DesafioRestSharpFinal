using FluentAssertions;
using NUnit.Framework;
using RestSharp;
using RestSharpNetCoreTemplate.Bases;
using RestSharpNetCoreTemplate.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpNetCoreTemplate.Tests
{
    [TestFixture]
    public class GetAllProjectsTests : TestBase
    {
        GetAllProjectsRequests listProjects;

        [Test, Description("Listar todos os projetos")]

        public void ListAllProjects() 
        {
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;

            listProjects = new GetAllProjectsRequests();
            RestResponse response = listProjects.ListProjects();

            response.StatusCode.Should().Be(expectedStatusCode);
        }
    }
}
