using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace RestSharpNetCoreTemplate.Helpers
{
    public class NunitHelpers
    {

        public static string _nunitTestName;
        public static string _nunitTestDescription;
        public static string _nunitTestCategory;
        public static TestStatus _nunitTestResultStatus;
        public static string _nunitTestResultMessage;
        public static string _nunitTestResultStacktrace;

        public static void SetTestParameters()
        {
            try
            {
                _nunitTestName = TestContext.CurrentContext.Test.MethodName;
                _nunitTestDescription = (string)TestContext.CurrentContext.Test.Properties.Get("Description");
                _nunitTestCategory = TestContext.CurrentContext.Test.ClassName.Split(".")[2];//Adicionar if para verificar se existe a categoria @Category()
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao definir os parâmetros de testes do NUnit: " + ex.Message);
            }

        }

        public static void SetTestResult()
        {
            try
            {
                _nunitTestResultStatus = TestContext.CurrentContext.Result.Outcome.Status;
                _nunitTestResultMessage = string.IsNullOrEmpty(TestContext.CurrentContext.Result.Message) ? "" : string.Format("{0}", TestContext.CurrentContext.Result.Message);
                _nunitTestResultStacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? "" : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao definir os parâmetros do resultado dos testes do NUnit: " + ex.Message);
            }


        }
    }
}
