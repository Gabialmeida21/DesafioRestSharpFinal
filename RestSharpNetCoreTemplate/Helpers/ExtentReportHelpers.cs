using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using RestSharpNetCoreTemplate.Bases;
using RestSharpNetCoreTemplate.Tests;

namespace RestSharpNetCoreTemplate.Helpers
{
    public class ExtentReportHelpers
    {
        public static ExtentReports extent = null;
        public static ExtentTest test;
        public static string _reportFolder;
        public static void CreateReport()
        {
            if (extent == null)
            {
                var htmlReporter = new ExtentHtmlReporter(GenerateNewReportFolder());
                extent = new ExtentReports();
                extent.AttachReporter(htmlReporter);
            }
        }

        public static void AddTest()
        {
            test = extent.CreateTest(NunitHelpers._nunitTestName, NunitHelpers._nunitTestDescription)
                .AssignCategory(NunitHelpers._nunitTestCategory);

        }
        public static void AddTestInfo()
        {
            if (test != null)
            {
                if (RegisterSteps._authIsEnabled)
                {
                    var nodeauth = test.CreateNode("🔑 request - " + RegisterSteps._authMethod + " - " + RegisterSteps._authResource);
                    nodeauth.Log(Status.Info, "<b>URL: </b><pre>" + RegisterSteps._authBaseUrl + "</pre>");
                    nodeauth.Log(Status.Info, "<b>RESOURCE: </b><pre>" + RegisterSteps._authResource + "</pre>");
                    nodeauth.Log(Status.Info, "<b>METHOD: </b><pre>" + RegisterSteps._authMethod + "</pre>");
                    nodeauth.Log(Status.Info, "<b>PARAMETERS: </b>\n" + RegisterSteps._authJsonBody);
                    nodeauth.Log(Status.Info, "<b>STATUS CODE: </b>" + "\n<pre>" + (int)RegisterSteps._authResponseBody.StatusCode + " - " +
                   RegisterSteps._authResponseBody.StatusCode + "</pre>");
                    nodeauth.Log(Status.Info, "<b>RESPONSE BODY: </b>" + "\n<pre>" + RegisterSteps._authResponseBody.GetPrettyResponse() + "</pre>");
                }

                var node = test.CreateNode("&#128640; Request - " + RequestBase._method + " - " + RequestBase._resource);
                node.Log(Status.Info, "<b>URL: </b><pre>" + RequestBase._urlBase + "</pre>");
                node.Log(Status.Info, "<b>RESOURCE: </b><pre>" + RequestBase._resource + "</pre>");
                node.Log(Status.Info, "<b>METHOD: </b><pre>" + RequestBase._method + "</pre>");
                node.Log(Status.Info, "<b>PARAMETERS: </b>\n" + RequestBase._request.Parameters.GetParametersFromRequest());
                node.Log(Status.Info, "<b>STATUS CODE: </b>" + "\n<pre>" + (int)RequestBase._response.StatusCode + " - " +
                    RequestBase._response.StatusCode + "</pre>");
                node.Log(Status.Info, "<b>RESPONSE BODY: </b>" + RequestBase._response.GetPrettyResponse());


            }

        }

        public static void AddTestResult()
        {
            switch (NunitHelpers._nunitTestResultStatus)
            {
                case TestStatus.Inconclusive:
                    break;
                case TestStatus.Skipped:
                    test.Log(Status.Skip, "Test Result: " + Status.Skip + "<pre>" + "Message: " + NunitHelpers._nunitTestResultMessage + "</pre>" + "<pre>" + "Stack Trace: " + NunitHelpers._nunitTestResultStacktrace + "</pre>");
                    break;
                case TestStatus.Passed:
                    test.Log(Status.Pass, "<b>Test Result: " + Status.Pass + "</b>");
                    break;
                case TestStatus.Warning:
                    test.Log(Status.Warning, "Test Result: " + Status.Warning + "<pre>" + "Message: " + NunitHelpers._nunitTestResultMessage + "</pre>" + "<pre>" + "Stack Trace: " + NunitHelpers._nunitTestResultStacktrace + "</pre>");
                    break;
                case TestStatus.Failed:
                    Exception e = new Exception(NunitHelpers._nunitTestResultStacktrace);
                    test.Log(Status.Fail, "Test Result: " + Status.Fail + "<pre>" + "Message: " + NunitHelpers._nunitTestResultMessage + "</pre>" + "<pre>" + "Stack Trace: " + e + "</pre>");
                    test.Fail(e);
                    break;
            }

        }

        public static void GenerateReport()
        {
            extent.Flush();
        }

        private static string GenerateNewReportFolder()
        {
            string extentReportPath = GeneralHelpers.GetCurrentSolutionFolderPath() + "Reports\\";
            string extentReportFileName = JsonHelpers.GetParameterAppSettings("REPORT_NAME");
            string extentReportFolderName = extentReportFileName + GeneralHelpers.GetDateAsString("dd-MM-yyyy_HH-mm");

            _reportFolder = extentReportPath +
                extentReportFolderName + "\\";
            return _reportFolder;
        }

    }
}
