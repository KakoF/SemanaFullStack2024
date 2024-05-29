using Financial.Api.Common.Api;
using Financial.Core.Handlers;
using Financial.Core.Models;
using Financial.Core.Requests.Categories;
using Financial.Core.Responses;

namespace Financial.Api.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Categories: Update")
            .WithSummary("Atualiza uma categoria")
            .WithDescription("Atualiza uma categoria")
            .WithOrder(2)
            .Produces<Response<Category>>();

        private static async Task<IResult> HandleAsync(
            ICategoryHandler handler,
            UpdateCategoryRequest request,
            long id)
        {
            request.UserId = ApiConfiguration.UserId;
            request.Id = id;

            var result = await handler.UpdateAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
