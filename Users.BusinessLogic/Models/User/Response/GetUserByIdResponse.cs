using Common.Models;

namespace Users.BusinessLogic.Models.User.Response;

public class GetUserByIdResponse: BaseResponseModel
{
    public Guid Id { get; set; }

    public string Email { get; set; }
}