using NUnit.Framework;
using RestSharpNetCoreTemplate.Helpers;
using RestSharpNetCoreTemplate.Requests;
using RestSharpNetCoreTemplate.Tests;

namespace RestSharpNetCoreTemplate.Bases
{
    public class TestBase
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            //Criar instancia do Relatorio .html
            ExtentReportHelpers.CreateReport();

            //Setar Token/Autorizador
            RegisterSteps.SetTokenHeader();

        }

        [SetUp]
        public void SetUp()
        {
            //Setar os parametros do teste no relatorio
            NunitHelpers.SetTestParameters();

            //Adicionar novo teste no relatorio
            ExtentReportHelpers.AddTest();

        }

        [TearDown]
        public void TearDown()
        {
            //Setar resultado do teste
            NunitHelpers.SetTestResult();

            //Adicionar resultado do teste no relatório
            ExtentReportHelpers.AddTestResult();

        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            //Gerar relatório no diretorio
            ExtentReportHelpers.GenerateReport();
        }
    }
}
