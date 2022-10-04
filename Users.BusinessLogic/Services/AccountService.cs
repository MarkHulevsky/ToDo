using Common.Models;
using Microsoft.AspNetCore.Identity;
using Users.BusinessLogic.Constants;
using Users.BusinessLogic.Models.Account.Request;
using Users.BusinessLogic.Services.Interfaces;
using Users.DataAccess.Entities;

namespace Users.BusinessLogic.Services;

public class AccountService: IAccountService
{
    private readonly UserManager<User> _userManager;

    public AccountService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<BaseResponseModel> CreateAsync(CreateUserRequest request)
    {
        User user = await _userManager.FindByEmailAsync(request.Email);

        if (user is not null)
        {
            return new BaseResponseModel
            {
                Message = AccountConstants.USER_ALREADY_EXISTS_ERROR
            };
        }

        user = new User
        {
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            UserName = request.Email,
            NormalizedUserName = request.UserName
        };

        IdentityResult createUserResult = await _userManager.CreateAsync(user, request.Password);

        if (!createUserResult.Succeeded)
        {
            return new BaseResponseModel
            {
                Message = string.Concat(createUserResult.Errors.SelectMany(error => error.Description))
            };
        }

        return new BaseResponseModel
        {
            IsSucceeded = true,
            Message = AccountConstants.USER_CREATED_SUCCESSFULLY
        };
    }


}