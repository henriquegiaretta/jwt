using Jwt.Core.Contexts.AccountContext.Entities;
using Jwt.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using Jwt.Core.Contexts.AccountContext.ValueObjects;
using MediatR;

namespace Jwt.Core.Contexts.AccountContext.UseCases.Create;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;
    private readonly IService _service;

    public Handler(
        IRepository repository,
        IService service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<Response> Handle(
        Request request, 
        CancellationToken cancellationToken)
    {
        #region Validate

        try
        {
            var result = Specification.Ensure(request);

            if (!result.IsValid)
                return new Response("Requisição inválida", 400, result.Notifications);

        }
        catch (Exception e)
        {
            return new Response("Não foi possível validar sua requisição", 500);
        }
        
        #endregion
        
        #region Generate-Objects

        Email email;
        Password password;
        User user;

        try
        {
            email = new Email(request.Email);
            password = new Password(request.Password);
            user = new User(request.Name, email, password);

        }
        catch (Exception e)
        {
            return new Response(e.Message, 400);
        }


        #endregion
        
        #region User-Verification

        try
        {
            var exists = await _repository.AnyAsync(request.Email, cancellationToken);
            if (exists)
                return new Response("Este email já está em uso", 400);
        }
        catch (Exception e)
        {
            return new Response("Falha ao verificar o email cadastrado", 500);
        }
        

        #endregion
        
        #region Persists

        try
        {
            await _repository.SaveAsync(user, cancellationToken);
        }
        catch (Exception e)
        {
            return new Response("Falha ao persistir os dados", 500);
        }
        #endregion
        
        #region Send-Email

        try
        {
            await _service.SendVerificationEmailAsync(user, cancellationToken);
        }
        catch (Exception e)
        {
           //Do nothing
        }
        
        #endregion

        return new Response(
            "Conta criada com sucesso",
            new ResponseData(user.Id, user.Name, user.Email));
    }
}