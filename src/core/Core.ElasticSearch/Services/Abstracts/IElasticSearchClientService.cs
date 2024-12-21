namespace Core.ElasticSearch.Services.Abstracts;

public interface IElasticSearchClientService
{
    Task IndexDocumentAsync<T>(T document, string indexName) where T : class;
    Task<List<T>> SearchAsync<T>(string indexName) where T : class;
}