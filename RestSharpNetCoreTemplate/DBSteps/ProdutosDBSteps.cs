using RestSharpNetCoreTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpNetCoreTemplate.DBSteps
{
    public class ProdutosDBSteps
    {
        public static List<string> RetornaProduto(string idProduto)
        {
            string queryFile = GeneralHelpers.GetProjectPath() + @"Queries\ConsultarProdutos.sql";

            string query = GeneralHelpers.ReplaceValueInFile(queryFile, "{idProduto}", idProduto);

            return DataBaseHelpers.GetQueryResult(query);
        }
    }
}
