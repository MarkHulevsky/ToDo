using Identity.Core.Grpc.Interfaces;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Users;

namespace Identity.Core.Validators;

public class UserValidator: IResourceOwnerPasswordValidator
{
    private readonly IUserClient _userClient;

    public UserValidator(IUserClient userClient)
    {
        _userClient = userClient;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        SignInResponse signInResult = await _userClient.SignInAsync(new SignInRequest
        {
            Email = context.UserName,
            Password = context.Password
        });

        if (!signInResult.IsSucceeded)
        {
            context.Result = new GrantValidationResult(TokenRequestErrors.UnauthorizedClient);
            return;
        }

        context.Result = new GrantValidationResult(
            subject: signInResult.Id,
            authenticationMethod: "custom");
    }
}