using Mail.BusinessLogic.HttpClients.Models.User.Response;

namespace Mail.BusinessLogic.HttpClients.Interfaces;

public interface IUserHttpClient
{
    Task<GetUserByIdResponse?> GetByIdAsync(Guid userId);
}