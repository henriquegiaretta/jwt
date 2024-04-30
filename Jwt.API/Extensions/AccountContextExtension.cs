using Jwt.Core.Contexts.AccountContext.UseCases.Create;
using Jwt.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using Jwt.Infra.Contexts.AccountContext.UseCases.Create;
using MediatR;

namespace Jwt.API.Extensions;

public static class AccountContextExtension
{
    public static void AddAccountContext(this WebApplicationBuilder builder)
    {
        #region Create

        builder.Services.AddTransient<IRepository, Repository>();
        builder.Services.AddTransient<IService, Service>();

        #endregion
    }

    public static void MapAccountEndpoints(this WebApplication app)
    {
        #region Create

        app.MapPost("api/v1/users", async (
            Request request,
            IRequestHandler<Request, Response> handler) =>
        {
            var result = await handler.Handle(request, new CancellationToken());

            return result.IsSucess
                ? Results.Created("", result)
                : Results.Json(result, statusCode: result.Status);
        });

        #endregion
    }
}