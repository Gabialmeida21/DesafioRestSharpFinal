using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serializers;
using RestSharpNetCoreTemplate.Bases;

namespace RestSharpNetCoreTemplate.Helpers
{
    public static class GeneralHelpers
    {

        public static JToken Deserialize(this RestResponse response)
        {


            if (response.Content.HasValue())
            {

                try
                {
                    return JToken.Parse(response.Content);//Retorna Json Deserializado
                }
                catch
                {
                    throw new Exception("Não foi possível deserializar o objeto " + response.Content);
                }
            }
            return response.Content; //Retorna response vazio
        }

        public static JToken GetPrettyResponse(this RestResponse response)
        {


            if (response.Content.HasValue())
            {

                if (response.ContentType == "application/json")
                {
                    try
                    {
                        return "<pre>" + response.Deserialize() + "</pre>";//Deserializar Json
                    }
                    catch
                    {
                        throw new Exception("Não foi possível deserializar o objeto " + response.Content);
                    }
                }
                else if (response.ContentType == "text/xml")
                {

                    return "<textarea>" + response.Content.PrettyXml() + "</textarea>";
                }
                else if (response.ContentType == "application/pdf")
                {
                    return GenerateStringFromFileDownloaded(response);
                }
            }
            return response.Content;
        }


        public static JToken GenerateStringFromFileDownloaded(RestResponse response)
        {
            JToken result = null;
            if (response.ContentType == "application/pdf")
            {
                result = RequestBase._resource +
                    " - " + "<a href='" +
                    ExtentReportHelpers._reportFolder + "\\" + RequestBase._resource + ".pdf"
                    + "'target='_blank'> Click to open the file</a>";
            }
            return result;
        }
        public static string GetParametersFromRequest(this ParametersCollection parameters)
        {
            string values = null;
            if (parameters != null)
            {

                if (parameters.Count > 0)
                {
                    foreach (var key in parameters)
                    {
                        if (key.Type.Equals(ParameterType.RequestBody))
                        {
                            if (RequestBase._response.ContentType == "text/xml")
                            {

                                values = values + "<b>" + key.Type + "</b><textarea>\n"
                            + JsonConvert.SerializeObject(key.Value, Newtonsoft.Json.Formatting.Indented) + "</textarea>\n";
                            }
                            else
                            {
                                values = values + "<b>" + key.Type + "</b><pre>\n"
                                + JsonConvert.SerializeObject(key.Value, Newtonsoft.Json.Formatting.Indented) + "</pre>\n";
                            }
                        }
                        else
                        {
                            values = values + "<b><pre>" + key.Type + "</b>" + ": "
                            + key.Name + " = " + key.Value + "</pre>\n";
                        }

                    }
                }
                else values = "<pre>Vazio</pre>";
            }


            return values;
        }

        public static string GetValueFromKey(this RestResponse response, string attribKey)
        {

            try
            {
                JToken json = response.Deserialize();
                return (string)json.SelectToken(attribKey);
            }
            catch (Exception)
            {

                throw new Exception("Não foi possível converter o objeto " + attribKey + " para String");
            }

        }

        public static string GetValueFromXMLKey(this RestResponse response, string elementKey)
        {

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(response.Content);

            //Display all the book titles.
            XmlNodeList elemList = doc.GetElementsByTagName(elementKey);

            if (elemList[0].InnerXml.HasValue())
            {
                return elemList[0].InnerXml;
            }
            else
            {
                throw new Exception("Não foi possível encontrar o elemento " + elementKey + " no XML");
            }

        }


        public static string PrettyXml(this string xml)
        {
            var stringBuilder = new StringBuilder();

            var element = XElement.Parse(xml);

            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            settings.NewLineOnAttributes = true;

            using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
            {
                element.Save(xmlWriter);
            }

            return stringBuilder.ToString();
        }
        public static bool HasValue(this string valor)// Testar se é nulo também
        {
            if (valor != string.Empty || valor != null)
            {
                return true;
            }
            return false;

        }

        public static string ReadFileContent(string filename)
        {
            return File.ReadAllText(GeneralHelpers.GetCurrentSolutionFolderPath() + filename);
        }
        public static JObject ReadJsonContent(string filename)
        {
            return JObject.Parse(File.ReadAllText(GeneralHelpers.GetCurrentSolutionFolderPath() + filename));
        }

        public static string FormatJson(string str)
        {
            string INDENT_STRING = "    ";
            var indent = 0;
            var quoted = false;
            var sb = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                switch (ch)
                {
                    case '{':
                    case '[':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, ++indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case '}':
                    case ']':
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, --indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        sb.Append(ch);
                        break;
                    case '"':
                        sb.Append(ch);
                        bool escaped = false;
                        var index = i;
                        while (index > 0 && str[--index] == '\\')
                            escaped = !escaped;
                        if (!escaped)
                            quoted = !quoted;
                        break;
                    case ',':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case ':':
                        sb.Append(ch);
                        if (!quoted)
                            sb.Append(" ");
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }
            return sb.ToString();
        }

        public static void EnsureDirectoryExists(string fullReportFilePath)
        {
            var directory = Path.GetDirectoryName(fullReportFilePath);
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        }

        public static string GetStringWithRandomNumbers(int size)
        {
            Random random = new Random();

            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, size)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetProjectPath()
        {
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;

            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));

            return new Uri(actualPath).LocalPath;
        }

        public static bool VerificaSeStringEstaContidaNaLista(List<string> lista, string p_string)
        {
            foreach (string item in lista)
            {
                if (item.Equals(p_string))
                {
                    return true;
                }
            }
            return false;
        }

        public static int RetornaNumeroDeObjetosDoJson(JArray json)
        {
            return json.Count;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetMethodNameByLevel(int level)
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(level);
            return sf.GetMethod().Name;
        }

        public static bool IsAJsonArray(string json)
        {
            if (json.StartsWith("["))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static T GetValue<T>(this JToken jToken, string key,
                           T defaultValue = default(T))
        {
            T returnValue = defaultValue;

            if (jToken[key] != null)
            {
                object data = null;
                string sData = jToken[key].ToString();

                Type type = typeof(T);

                if (type is double)
                    data = double.Parse(sData);
                else if (type is string)
                    data = sData;

                if (null == data && type.IsValueType)
                    throw new ArgumentException("Cannot parse type \"" +
                        type.FullName + "\" from value \"" + sData + "\"");

                returnValue = (T)Convert.ChangeType(data,
                    type, CultureInfo.InvariantCulture);
            }

            return returnValue;
        }

        public static string GetValuesFromList(this ParametersCollection collection)
        {
            IDictionary<string, string> values = new Dictionary<string, string>();
            if (collection.Count > 0)
            {
                foreach (var item in collection)
                {
                    values.Add(item.Name.ToString(), item.Value.ToString());
                }
                string json1 = JsonConvert.SerializeObject(collection);
                string json2 = JsonConvert.SerializeObject(values);
            }
            return "-";
        }
        public static bool HasValue(this List<string> list)
        {
            if (list != null || list.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static string GetCurrentSolutionFolderPath()
        {
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;

            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));

            return new Uri(actualPath).LocalPath;
        }

        public static string GetDateAsString(String format)
        {
            return DateTime.Now.ToString(format);//"dd-MM-yyyy_HH-mm"
        }

        public static string ReplaceValueInFile(string file, string currentValue, string newValue)
        {
            string text = File.ReadAllText(file);
            text = text.Replace(currentValue, newValue);
            return text;
        }

    }

    static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action)
        {
            foreach (var i in ie)
            {
                action(i);
            }
        }
    }


}
