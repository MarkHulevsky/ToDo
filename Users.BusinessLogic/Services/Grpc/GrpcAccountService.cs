using Grpc.Core;
using Microsoft.AspNetCore.Identity;
using Users.DataAccess.Entities;

namespace Users.BusinessLogic.Services.Grpc;

public class GrpcAccountService: GrpcUser.GrpcUserBase
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public GrpcAccountService(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public override async Task<SignInResponse> SignIn(SignInRequest request, ServerCallContext context)
    {
        User user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return new SignInResponse();
        }

        SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

        if (!signInResult.Succeeded)
        {
            return new SignInResponse();
        }

        return new SignInResponse
        {
            IsSucceeded = true,
            Id = user.Id.ToString(),
            Email = user.Email
        };
    }
}