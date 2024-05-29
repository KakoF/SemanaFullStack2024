using Financial.Api.Common.Api;
using Financial.Api.Data;
using Financial.Api.Endpoints;
using Financial.Api.Handlers;
using Financial.Core.Handlers;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

//app.MapGet("/category", (GetCategoryByIdRequest request, ICategoryHandler handler) => handler.GetByIdAsync(request));
app.MapEndpoints();


app.Run();
