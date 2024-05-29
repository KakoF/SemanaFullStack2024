using Financial.Api.Common.Api;
using Financial.Core.Handlers;
using Financial.Core.Models;
using Financial.Core.Requests.Categories;
using Financial.Core.Responses;
using Microsoft.Extensions.Configuration;

namespace Financial.Api.Endpoints.Categories
{
    public class CreateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
             => app.MapPost("/", HandleAsync)
            .WithName("Categories: Create")
            .WithSummary("Cria uma nova categoria")
            .WithDescription("Cria uma nova categoria")
            .WithOrder(1)
            .Produces<Response<Category>>();

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, CreateCategoryRequest request)
        {
            request.UserId = ApiConfiguration.UserId;
            var response = await handler.CreateAsync(request);
            return response.IsSuccess
                ? TypedResults.Created($"v1/categories/{response.Data?.Id}", response)
                : TypedResults.BadRequest(response);
        }
    }
}
