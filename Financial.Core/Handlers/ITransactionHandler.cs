using Financial.Core.Models;
using Financial.Core.Requests.Transactions;
using Financial.Core.Responses;

namespace Financial.Core.Handlers
{
    public interface ITransactionHandler
    {
        Task<Response<Transaction>> CreateAsync(CreateTransactionRequest request);
        Task<Response<Transaction>> UpdateAsync(UpdateTransactionRequest request);
        Task<Response<Transaction>> DeleteAsync(DeleteTransactionRequest request);
        Task<Response<Transaction>> GetByIdAsync(GetTransactionByIdRequest request);
        Task<PagedResponse<List<Transaction>>> GetByPeriodAsync(GetTransactionsByPeriodRequest request);
    }
}