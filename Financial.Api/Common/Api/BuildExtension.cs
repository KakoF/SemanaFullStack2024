﻿using Financial.Api.Handlers;
using Financial.Core.Handlers;
using Financial.Core;
using Microsoft.EntityFrameworkCore;
using System;
using Financial.Api.Data;

namespace Financial.Api.Common.Api
{
    public static class BuildExtension
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            ApiConfiguration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
            Configuration.BackendUrl = builder.Configuration.GetValue<string>("Clients:BackendUrl") ?? string.Empty;
            Configuration.FrontendUrl = builder.Configuration.GetValue<string>("Clients:FrontendUrl") ?? string.Empty;
        }

        public static void AddDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.CustomSchemaIds(n => n.FullName);
            });

        }

        public static void AddDataContexts(this WebApplicationBuilder builder)
        {
            builder
                .Services
                .AddDbContext<AppDataContext>(
                    x =>
                    {
                        x.UseSqlServer(ApiConfiguration.ConnectionString);
                    });

        }

        public static void AddCrossOrigin(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(
                options => options.AddPolicy(
                    ApiConfiguration.CorsPolicyName,
                    policy => policy
                        .WithOrigins([
                            Configuration.BackendUrl,
                            Configuration.FrontendUrl
                        ])
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                ));
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder
                .Services
                .AddScoped<ICategoryHandler, CategoryHandler>();

            builder
                .Services
                .AddScoped<ITransactionHandler, TransactionHandler>();
        }
    }
}