## Template Testes Automatizados de API Net Core - RestSharp v107

## Importante

RestSharp v107 altera significativamente a estrutura básica da biblioteca e seu comportamento. É recomendado consultar a documentação da versão 107 para entender como migrar para a versão mais recente do [RestSharp v107](https://restsharp.dev/v107/#restsharp-v107 "RestSharp v107").

- Arquitetura Projeto
  - Biblioteca REST API client - [RestSharp v107](https://restsharp.dev/v107/#restsharp-v107 "RestSharp v107")
  - Linguagem	de Programação	- [CSharp](https://docs.microsoft.com/pt-br/dotnet/csharp/ "CSharp")
  - Orquestrador de testes - [NUnit 3.12](https://github.com/nunit/nunit "NUnit 3.13.3")
  - Asserções - [Fluent Assertions](https://fluentassertions.com/ "Fluent Assertions")
  - Relatório de testes automatizados - [ExtentReports 5.0.2](https://www.extentreports.com/docs/versions/5/net/index.html)
  - Framework de desenvolvimento - [.Net Core](https://dotnet.microsoft.com/download/dotnet-core/3.1)

## .Net Core e Visual Code
Estamos adotando o uso do Visual Code para o desenvolvimento de projetos CSharp que estão em framework .Net Core, para isso recomendamos a configuração da ferramenta e seus recursos conforme artigo explicativo: [clique aqui](https://medium.com/@saymowan/configurando-seu-vscode-para-desenvolver-projetos-de-testes-automatizados-netcore-nunit-476e73aa7b01).

## Arquitetura

**Premissas de uma boa arquitetura de automação de testes:**
*  Facilitar o desenvolvimento dos testes automatizados (reuso).
*  Facilitar a manutenção dos testes (refatoração).
*  Tornar o fluxo do teste o mais legível possível (fácil entendimento do que está sendo testado).

**Arquitetura padrão Base2** 

Estrutura das classes Request:

![alt text](https://i.imgur.com/gyu3Avr.png)

Estrutura das classes Tests:

![alt text](https://i.imgur.com/pAL2WvT.png)

Adicionando Headers:

`_request.AddHeader("Content-Type", "application/json")`

Adicionando Parâmetros (Default QueryString):

`_request.AddParameter(param.Key, param.Value);`

Adicionando Parâmetros por tipo (Sobrecarga):

`_request.AddParameter(param.Key, param.Value, ParameterType.QueryString);`

`_request.AddParameter(param.Key, param.Value, ParameterType.UrlSegment);`

Adicionando Body (String):

`const json = "{ data: { foo: \"bar\" } }";`

`_request.AddStringBody(json, ContentType.Json);`

Adicionando Body (Objeto):

`var param = new MyClass { IntData = 1, StringData = "test123" };`

`_request.AddJsonBody(param);`

Asserção Status Code:

`response.StatusCode.Should().Be(expectedStatusCode);`

Asserção de String:

`response.GetValueFromKey("data.email").Should().Contain("@")`

Asserção de Array:

`response.Deserialize()["data"].ForEach(x => x.SelectToken("email").ToString().Should().Contain("@"));`

**Arquitetura padrão Base2** 

Para facilitar o entendimento da arquitetura do projeto de testes automatizados, o template segue a padronização utilizada para Testes Web para facilitar o entendimento e proporcionar uma melhor organização.


  - Bases ("contem as bases para requisições REST e SOAP alem da base para os testes")
  - DBSteps ("Contem exemplo de uso de queries")
  - Helpers ("Contem metodos auxiliares para os projetos inclusive serializacao e deserializacao de jsons, entre outros")
  - Jsons ("Diretorio para armazenar os jsons utilizados nas requisições do projeto")
  - Queries ("Diretorio para armazenar as queries utilizadas no projeto")
  - Requests ("Diretorio para armazenar as requisições do projeto")
  - Tests ("Diretorio para armazenar os testes do projeto")
  - Xmls ("Diretorio para armazenar os xmls utilizados nas requisições do projeto")


# Padrões de escrita de código

O padrão adotado para escrita é o “CamelCase” onde uma palavra é separada da outra através de letras maiúsculas. Este padrão é adotado para o nome de pastas, classes, métodos, variáveis e arquivos em geral exceto constantes. Constantes devem ser escritas com todas suas letras em maiúsculo separando as palavras com “_”.

Ex: `PreencherUsuario(), nomeUsuario, LoginPage etc.`

**Padrões por tipo de componente**

* Pastas: começam sempre com letra maiúscula. Ex: `Pages, DataBaseSteps, Queries, Bases`
* Classes: começam sempre com letra maiúscula. Ex: `LoginRequest, LoginTests`
* Arquivos: começam sempre com letra maiúscula. Ex: `DataDrivenFile.csv`
* Métodos: começam sempre com letra maiúscula. Ex: `VerificarElementoXPTO()`
* Variáveis: começam sempre com letra minúscula. Ex: `botaoXPTO`
* Objetos: começam sempre com letra minúscula. Ex: `request`

No caso de palavras com uma letra, as duas devem ser escritas juntas de acordo com o padrão do tipo que será nomeado, ex:`retornaSeValorEOEsperado()`



**Padrões de escrita: Classes e Arquivos**

Nomes de classes e arquivos devem terminar com o tipo de conteúdo que representam, em inglês, ex:

```
LoginTests (classe de testes)
LoginTestData.csv (planilha de dados)
```

OBS: Atenção ao plural e singular! Se a classe contém um conjunto do tipo que representa, esta deve ser escrita no plural, caso seja uma entidade, deve ser escrita no singular.


**Padrões de escrita: Geral**

Nunca utilizar acentos, caracteres especiais e “ç” para denominar pastas, classes, métodos, variáveis, objetos e arquivos.

**Padrões de escrita: Objetos**

Nomes dos objetos devem ser exatamente os mesmos nomes de suas classes, iniciando com letra minúscula, ex:

```
LoginRequest (classe) loginRequest (objeto)
LoginFlows (classe) loginFlows (objeto)
```
