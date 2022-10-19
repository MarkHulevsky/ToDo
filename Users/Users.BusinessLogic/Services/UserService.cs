using AutoMapper;
using Common.Models;
using Microsoft.AspNetCore.Identity;
using Users.BusinessLogic.Constants;
using Users.BusinessLogic.Models.User.Response;
using Users.BusinessLogic.Services.Interfaces;
using Users.DataAccess.Entities;

namespace Users.BusinessLogic.Services;

public class UserService: IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel> GetByIdAsync(Guid userId)
    {
        User user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return new BaseResponseModel
            {
                Message = UserConstants.USER_NOT_FOUND_ERROR
            };
        }

        var response = _mapper.Map<GetUserByIdResponse>(user);

        response.IsSucceeded = true;

        return response;
    }
}