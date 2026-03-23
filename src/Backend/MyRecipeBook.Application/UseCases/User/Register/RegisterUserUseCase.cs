using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communications.Requests;
using MyRecipeBook.Communications.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase
{

    private readonly IUser _user;

    public RegisterUserUseCase(IUser user)
    {
        _user = user;
    }
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {


        // Criptografia da senha
        var cryptoPassword = new PasswordEncripter();
        var autoMapper = new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper();


        Validate(request);

        var user = autoMapper.Map<Domain.Entities.User>(request);


        user.Password = cryptoPassword.Encrypt(request.Password);

        await _user.Add(user);

        return new ResponseRegisteredUserJson
        {
            Name = request.Name
        };

    }


    private void Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessage);
        }

    }

}
