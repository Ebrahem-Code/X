using Microsoft.AspNetCore.Mvc;
using X.Application.Core.Search;
using X.Domain.Orders;

namespace X.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService<Order> _searchService;

        public SearchController(ISearchService<Order> searchService)
        {
            _searchService = searchService;
        }

        [HttpPost("index")]
        public async Task<IActionResult> IndexDocument([FromBody] Order order)
        {
            await _searchService.IndexDocumentAsync(order);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> SearchDocuments([FromQuery] string query)
        {
            var results = await _searchService.SearchDocumentsAsync(query);
            return Ok(results);
        }
    }
}