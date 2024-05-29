using Scalar.AspNetCore;

namespace Financial.Api.Common.Api
{
    public static class AppExtension
    {
        public static void ConfigureDevEnvironment(this WebApplication app)
        {
            app.MapScalarApiReference();
            app.UseSwagger();
            app.UseSwaggerUI();
            // app.MapSwagger().RequireAuthorization();
        }
    }
}