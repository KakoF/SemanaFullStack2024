using Financial.Api.Common.Api;
using Financial.Core.Handlers;
using Financial.Core.Models;
using Financial.Core.Requests.Transactions;
using Financial.Core.Responses;

namespace Financial.Api.Endpoints.Transactions
{
    public class DeleteTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
       => app.MapDelete("/{id}", HandleAsync)
           .WithName("Transactions: Delete")
           .WithSummary("Exclui uma transação")
           .WithDescription("Exclui uma transação")
           .WithOrder(3)
           .Produces<Response<Transaction>>();

        private static async Task<IResult> HandleAsync(
            ITransactionHandler handler,
            long id)
        {
            var request = new DeleteTransactionRequest
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var result = await handler.DeleteAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
