using Common.Models;

namespace Users.BusinessLogic.Services.Interfaces;

public interface IUserService
{
    Task<BaseResponseModel> GetByIdAsync(Guid userId);
}