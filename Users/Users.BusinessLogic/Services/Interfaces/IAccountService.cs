using Common.Models;
using Users.BusinessLogic.Models.Account.Request;

namespace Users.BusinessLogic.Services.Interfaces;

public interface IAccountService
{
    Task<BaseResponseModel> CreateAsync(CreateUserRequest request);
}