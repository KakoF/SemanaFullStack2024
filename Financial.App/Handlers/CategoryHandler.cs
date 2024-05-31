using Financial.Core.Handlers;
using Financial.Core.Models;
using Financial.Core.Requests.Categories;
using Financial.Core.Responses;
using System.Net.Http.Json;

namespace Financial.App.Handlers
{
	public class CategoryHandler : ICategoryHandler
	{
		private readonly HttpClient _httpClient;
        public CategoryHandler(IHttpClientFactory httpClientFactory)
        {
			_httpClient = httpClientFactory.CreateClient(AppConfiguration.HttpClientFinancial);
		}
        public async Task<Response<Category>> CreateAsync(CreateCategoryRequest request)
		{
			var result = await _httpClient.PostAsJsonAsync("v1/categories", request);
			return await result.Content.ReadFromJsonAsync<Response<Category>>() ?? new Response<Category>(null, 400, "Falha ao criar categoria");
		}

		public async Task<Response<Category>> DeleteAsync(DeleteCategoryRequest request)
		{
			var result = await _httpClient.DeleteAsync($"v1/categories/{request.Id}");
			return await result.Content.ReadFromJsonAsync<Response<Category>>() ?? new Response<Category>(null, 400, "Falha ao deletar categoria");
		}

		public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoryRequest request)
		{
			var result = await _httpClient.GetFromJsonAsync<PagedResponse<List<Category>>>($"v1/categories");
			return result ?? new PagedResponse<List<Category>>(null, 400, "Falha ao recuperar categorias");
		}

		public async Task<Response<Category>> GetByIdAsync(GetCategoryByIdRequest request)
		{
			var result = await _httpClient.GetFromJsonAsync<Response<Category>>($"v1/categories/{request.Id}");
			return result ?? new Response<Category>(null, 400, "Falha ao recuperar categoria");
		}

		public async Task<Response<Category>> UpdateAsync(UpdateCategoryRequest request)
		{
			var result = await _httpClient.PutAsJsonAsync($"v1/categories/{request.Id}", request);
			return await result.Content.ReadFromJsonAsync<Response<Category>>() ?? new Response<Category>(null, 400, "Falha ao criar categoria");
		}
	}
}
