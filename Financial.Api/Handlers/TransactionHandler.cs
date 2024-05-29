using Financial.Api.Data;
using Financial.Core.Common;
using Financial.Core.Handlers;
using Financial.Core.Models;
using Financial.Core.Requests.Transactions;
using Financial.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Financial.Api.Handlers
{
    public class TransactionHandler : ITransactionHandler
    {
        private readonly AppDataContext _context;
        public TransactionHandler(AppDataContext context)
        {
            _context = context;
        }
        public async Task<Response<Transaction>> CreateAsync(CreateTransactionRequest request)
        {
            if (request is { Type: Core.Enums.eTransactionType.Withdraw, Amount: >= 0 })
                request.Amount *= -1;

            try
            {
                var transaction = new Transaction(request.Title, request.PaidOrReceivedAt,request.Type, request.Amount, request.CategoryId, request.UserId);

                await _context.Transactions.AddAsync(transaction);
                await _context.SaveChangesAsync();

                return new Response<Transaction>(transaction, 201, "Transação criada com sucesso!");
            }
            catch
            {
                return new Response<Transaction>(null, 400, "Não foi possível criar sua transação");
            }
        }

        public async Task<Response<Transaction>> UpdateAsync(UpdateTransactionRequest request)
        {
            if (request is { Type: Core.Enums.eTransactionType.Withdraw, Amount: >= 0 })
                request.Amount *= -1;

            try
            {
                var transaction = await _context
                    .Transactions
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction is null)
                    return new Response<Transaction>(null, 404, "Transação não encontrada");

                transaction.Update(request.CategoryId, request.Amount, request.Title, request.Type, request.PaidOrReceivedAt);


                _context.Transactions.Update(transaction);
                await _context.SaveChangesAsync();

                return new Response<Transaction>(transaction);
            }
            catch
            {
                return new Response<Transaction>(null, 500, "Não foi possível recuperar sua transação");
            }
        }

        public async Task<Response<Transaction>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await _context
                    .Transactions
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction is null)
                    return new Response<Transaction>(null, 404, "Transação não encontrada");

                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();

                return new Response<Transaction>(transaction);
            }
            catch
            {
                return new Response<Transaction>(null, 400, "Não foi possível recuperar sua transação");
            }
        }

        public async Task<Response<Transaction>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await _context
                    .Transactions
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return transaction is null
                    ? new Response<Transaction>(null, 404, "Transação não encontrada")
                    : new Response<Transaction>(transaction);
            }
            catch
            {
                return new Response<Transaction>(null, 400, "Não foi possível recuperar sua transação");
            }
        }

        public async Task<PagedResponse<List<Transaction>>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();
            }
            catch
            {
                return new PagedResponse<List<Transaction>>(null, 500,
                    "Não foi possível determinar a data de início ou término");
            }

            try
            {
                var query = _context
                    .Transactions
                    .AsNoTracking()
                    .Where(x =>
                        x.PaidOrReceivedAt >= request.StartDate &&
                        x.PaidOrReceivedAt <= request.EndDate &&
                        x.UserId == request.UserId)
                    .OrderBy(x => x.PaidOrReceivedAt);

                var transactions = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>>(
                    transactions,
                    count,
                    request.PageNumber,
                    request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Transaction>>(null, 400, "Não foi possível obter as transações");
            }
        }
    }
}
