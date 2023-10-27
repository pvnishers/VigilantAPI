# Vigilant Core API

## 1. Introdução

Este projeto implementa um processo de web scraping para obter informações sobre notícias de procurados das APIs públicas do FBI e da Interpol. O objetivo é criar um banco de dados atualizado contendo os dados mais recentes sobre procurados internacionalmente para fins de análise.

O processo envolve fazer requests HTTP às APIs, parsear o conteúdo JSON retornado para extrair os detalhes de cada notícia, normalizar os dados em objetos .NET e armazená-los em um banco de dados Oracle SQL. Dados como nome, idade, localização, crimes e imagens são obtidos para cada procurado. Para as notícias da Interpol, mandados de prisão associados também são extraídos.

O código é estruturado em controllers, models, repositories e serviços para prover separação de responsabilidades. O repositório abstrai o acesso ao banco de dados. Scraping será feito periodicamente a cada 24 horas para manter os dados atualizados, e limites de requisição serão respeitados para não sobrecarregar os servidores.

No geral, este processo automatizado de coleta de dados de domínio público permitirá análises valiosas sobre procurados internacionais.

## 2. Bibliotecas e Dependências

### 2.1. Bibliotecas

- `Microsoft.AspNetCore.Mvc` - Fornece funcionalidades de API Web do ASP.NET Core MVC.
- `Newtonsoft.Json` - Usado para serialização e deserialização JSON.
- `Google.Cloud.Translation.V2` - Fornece tradução de textos via Google Cloud Translation API.

### 2.2. Dependências

- .NET 6 - Plataforma de desenvolvimento utilizada.
- Entity Framework Core - Mapeamento objeto-relacional e acesso ao banco de dados Oracle SQL.
- Oracle SQL - Banco de dados embutido utilizado para armazenar os dados extraídos.

## 3. Fluxo de Funcionamento

Os métodos que realizam a coleta de dados das APIs são executados todos os dias no horário da meia-noite via Google Cloud Scheduler.

### GetNotices (Interpol)

1. Cria um cliente HttpClient para fazer requisições web.
2. Faz um GET request para a API Red Notices da Interpol para obter a primeira página.
3. Deserializa a resposta JSON em objetos C# (RootObject e Notice).
4. Loop pelas notícias na resposta.
5. Para cada notice, faz outro request para obter os detalhes completos.
6. Deserializa a resposta em um objeto DetailNotice.
7. Para cada mandado de prisão, traduz a acusação usando o Google Translation API.
8. Cria um novo objeto NoticeModel normalizado.
9. Adiciona à lista de novos notices.
10. Após processar todos os notices, salva no banco de dados Oracle.

### GetWanted (FBI)

1. Cria um cliente HttpClient.
2. Faz um GET request para a API Wanted do FBI passando o número da página.
3. Deserializa a resposta JSON em objeto ResponseObject.
4. Extrai a lista de procurados (Items).
5. Loop pelos itens.
6. Cria um novo objeto WantedModel normalizado.
7. Verifica se já existe no banco de dados.
8. Se não existe, adiciona à lista de novos procurados.
9. Após processar todos os itens, salva no banco de dados Oracle.
10. Repete para todas as páginas até obter todos os registros.

### GetAllNotices e GetAllWanted (Filtros)

Para permitir a busca com filtros de procurados que foram salvos no banco de dados, os métodos `GetAllNotices` e `GetAllWanted` foram implementados. Estes métodos permitem a recuperação de listas paginadas de notícias e procurados com base em critérios específicos.

## 4. Estrutura de Dados

Os dados coletados das APIs são normalizados e armazenados nas seguintes tabelas do banco de dados Oracle:

### Tabela Notices

- Id (chave primária)
- DateOfBirth
- Nationalities
- EntityId
- Forename
- Name
- ArrestWarrants (relação 1:N)
- Sex
- ThumbnailUrl
- Url

Campos da tabela ArrestWarrants:

- Id
- NoticeModelId (chave estrangeira)
- Charge
- IssuingCountryId

### Tabela Wanted

- Id
- Uid
- Title
- Locations
- Sex
- Nationality
- Age_Min
- Age_Max
- Subjects
- Images
- Url
- Race
- Place_Of_Birth

Os campos nas tabelas correspondem aos campos extraídos das APIs e normalizados nos objetos C# NoticeModel e WantedModel antes de serem salvos no banco de dados Oracle. Isso inclui informações como nome, idade, localização, crimes, imagens, mandados de prisão e outros detalhes de cada procurado. Os relacionamentos entre tabelas também são mapeados no banco de dados.

## 5. Desafios e Considerações

Alguns desafios esperados neste processo de extração e manipulação de dados incluem:

- Dados incompletos ou ausentes - As APIs podem retornar dados incompletos ou nulos que precisam ser tratados, como datas de nascimento ou locais faltantes.
- Mudanças na estrutura dos dados - As APIs estão sujeitas a mudanças nos formatos JSON retornados, o que pode quebrar o parseamento. Os parsers precisam ser flexíveis.
- Limites de requisição - As APIs podem ter limites de requisições por dia/hora. Precisa-se respeitar esses limites e evitar sobrecarga.
- Tradução de textos - A tradução de mandados de prisão da Interpol pode falhar em alguns casos. Tratamento de erros necessário.
- Performance com grandes volumes - Otimizações precisam ser feitas para lidar com extração e parseamento de milhares de registros.
- Armazenamento e queries eficientes - Índices e chaves precisam ser projetados adequadamente no banco de dados.
- Legalidade e ética - A extração e uso desses dados públicos deve ser feita de forma legal e ética.
- Segurança - Precauções de segurança precisam ser tomadas ao lidar com dados sensíveis. Monitoramento, logging e tratamento de exceções serão importantes para lidar com esses desafios de forma robusta e confiável.

## 6. Conclusão

Em resumo, este projeto demonstra uma aplicação útil de técnicas de web scraping e processamento de dados para coletar conteúdo público e transformá-lo em informações relevantes.

A solução permite análises valiosas sobre procurados internacionais, como identificar tendências geográficas, tipos de crimes mais comuns e mudanças ao longo do tempo.

Acesse a solução em: [Link para a Solução](https://vigilant-react.web.app/)
