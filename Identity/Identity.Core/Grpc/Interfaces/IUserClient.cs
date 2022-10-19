using Users;

namespace Identity.Core.Grpc.Interfaces;

public interface IUserClient
{
    Task<SignInResponse> SignInAsync(SignInRequest request);
}