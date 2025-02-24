using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nest;
using X.Application.Core.Search;

namespace X.Infrastructure.ExternalServices.Search
{
    public class SearchService<T> : ISearchService<T> where T : class
    {
        private readonly IElasticClient _elasticClient;
        private readonly ILogger<SearchService<T>> _logger;

        public SearchService(IElasticClient elasticClient, ILogger<SearchService<T>> logger)
        {
            _elasticClient = elasticClient;
            _logger = logger;
        }

        public async Task IndexDocumentAsync(T document)
        {
            var response = await _elasticClient.IndexDocumentAsync(document);
            if (!response.IsValid)
            {
                _logger.LogError("Failed to index document: {Error}", response.OriginalException.Message);
                throw new Exception("Failed to index document", response.OriginalException);
            }
        }

        public async Task<IEnumerable<T>> SearchDocumentsAsync(string query)
        {
            var response = await _elasticClient.SearchAsync<T>(s => s
                .Query(q => q
                    .QueryString(qs => qs
                        .Query(query)
                    )
                )
            );

            if (!response.IsValid)
            {
                _logger.LogError("Failed to search documents: {Error}", response.OriginalException.Message);
                throw new Exception("Failed to search documents", response.OriginalException);
            }

            return response.Documents;
        }
    }
}