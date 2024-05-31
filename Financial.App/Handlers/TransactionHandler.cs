using Financial.Core.Handlers;
using Financial.Core.Models;
using Financial.Core.Requests.Transactions;
using Financial.Core.Responses;
using System.Net.Http;
using System.Net.Http.Json;

namespace Financial.App.Handlers
{
	public class TransactionHandler : ITransactionHandler
	{
		private readonly HttpClient _httpClient;
		public TransactionHandler(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient(AppConfiguration.HttpClientFinancial);
		}
		public async Task<Response<Transaction>> CreateAsync(CreateTransactionRequest request)
		{
			var result = await _httpClient.PostAsJsonAsync("v1/transactions", request);
			return await result.Content.ReadFromJsonAsync<Response<Transaction>>() ?? new Response<Transaction>(null, 400, "Falha ao criar transação");
		}

		public async Task<Response<Transaction>> DeleteAsync(DeleteTransactionRequest request)
		{
			var result = await _httpClient.DeleteAsync($"v1/transactions/{request.Id}");
			return await result.Content.ReadFromJsonAsync<Response<Transaction>>() ?? new Response<Transaction>(null, 400, "Falha ao deletar transação");
		}

		public async Task<Response<Transaction>> GetByIdAsync(GetTransactionByIdRequest request)
		{
			var result = await _httpClient.GetFromJsonAsync<Response<Transaction>>($"v1/transactions/{request.Id}");
			return result ?? new Response<Transaction>(null, 400, "Falha ao deletar transação");
		}

		public async Task<PagedResponse<List<Transaction>>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
		{
			var result = await _httpClient.GetFromJsonAsync<PagedResponse<List<Transaction>>>($"v1/transactions");
			return result ?? new PagedResponse<List<Transaction>>(null, 400, "Falha ao recuperar transações");
		}
		public async Task<Response<Transaction>> UpdateAsync(UpdateTransactionRequest request)
		{
			var result = await _httpClient.PutAsJsonAsync($"v1/transactions/{request.Id}", request);
			return await result.Content.ReadFromJsonAsync<Response<Transaction>>() ?? new Response<Transaction>(null, 400, "Falha ao deletar transação");
		}
	}
}
