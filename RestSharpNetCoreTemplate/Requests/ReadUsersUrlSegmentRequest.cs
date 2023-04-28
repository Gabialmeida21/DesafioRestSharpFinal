using RestSharp;
using RestSharpNetCoreTemplate.Bases;
using RestSharpNetCoreTemplate.Helpers;

namespace RestSharpNetCoreTemplate.Requests
{
    public class ReadUsersUrlSegmentRequest : RequestBase
    {

        public ReadUsersUrlSegmentRequest() //Construtor da Request do Teste
        {
            _urlBase = JsonHelpers.GetParameterAppSettings("URL_REQRES"); //Setar a URL base
            _resource = "/api/users/{id}"; //Setar a Rota
            _method = Method.Get; //Setar o Método
            SetupRequest(); // Construir a Requisição
        }
        public void SetParameter(string parameter)
        {
            _request.AddParameter("id", parameter, ParameterType.UrlSegment); //Setar o parâmetro - Segmento
        } 
        public RestResponse ListUser()
        {
            return ExecuteRequest(); //Executar a requisição e retornar a resposta
        }

    }
}
