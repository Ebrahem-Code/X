namespace X.Application.Core.Search
{
    public interface ISearchService<T> where T : class
    {
        Task IndexDocumentAsync(T document);
        Task<IEnumerable<T>> SearchDocumentsAsync(string query);
    }
}