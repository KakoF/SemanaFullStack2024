using Financial.Api.Common.Api;
using Financial.Core.Handlers;
using Financial.Core.Requests.Transactions;
using Financial.Core.Responses;
using Financial.Core;
using Microsoft.AspNetCore.Mvc;
using Financial.Core.Models;

namespace Financial.Api.Endpoints.Transactions
{
    public class GetTransactionsByPeriodEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Transactions: Get All")
            .WithSummary("Recupera todas as transações")
            .WithDescription("Recupera todas as transações")
            .WithOrder(5)
            .Produces<PagedResponse<List<Transaction>>>();

        private static async Task<IResult> HandleAsync(
            ITransactionHandler handler,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetTransactionsByPeriodRequest
            {
                UserId = ApiConfiguration.UserId,
                PageNumber = pageNumber,
                PageSize = pageSize,
                StartDate = startDate,
                EndDate = endDate
            };

            var result = await handler.GetByPeriodAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
