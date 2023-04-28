using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace RestSharpNetCoreTemplate.Helpers
{
    public class JsonHelpers
    {
        private static JObject json;
        public static JObject GetJsonObject(string filename)
        {
            try
            {
                json = JObject.Parse(File.ReadAllText(GeneralHelpers.GetCurrentSolutionFolderPath() + filename));

                return json;
            }
            catch (Exception)
            {
                throw new Exception("Json "+ filename + " não encontrado");
            }

        }

        public static string GetParameterAppSettings(string param)
        {
            try
            {
                JObject json = GetJsonObject("appsettings.json");
                return json[param].Value<string>();
            }
            catch (Exception)
            {
                throw new Exception("Parâmetro "+ param + " não encontrado no AppSettings");
            }

        }

        public static string GetParameterFromJsonFile(string filename, string param)
        {
            try
            {
                JObject json = GetJsonObject(filename);
                return json[param].Value<string>();
            }
            catch (Exception)
            {
                throw new Exception("Parâmetro "+ param + "não encontrado no "+filename);
            }

        }



    }
}
