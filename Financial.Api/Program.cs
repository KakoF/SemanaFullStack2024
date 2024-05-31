using Financial.Api;
using Financial.Api.Common.Api;
using Financial.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCors(ApiConfiguration.CorsPolicyName);

//app.MapGet("/category", (GetCategoryByIdRequest request, ICategoryHandler handler) => handler.GetByIdAsync(request));
app.MapEndpoints();


app.Run();
