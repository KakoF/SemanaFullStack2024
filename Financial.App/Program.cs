using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Financial.App;
using MudBlazor.Services;
using Financial.Core;
using Financial.Core.Handlers;
using Financial.App.Handlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient(AppConfiguration.HttpClientFinancial,
	opt =>
	{
		opt.BaseAddress = new Uri(Configuration.BackendUrl);
	});

builder.Services.AddScoped<ICategoryHandler, CategoryHandler>();
builder.Services.AddScoped<ITransactionHandler, TransactionHandler>();

await builder.Build().RunAsync();
