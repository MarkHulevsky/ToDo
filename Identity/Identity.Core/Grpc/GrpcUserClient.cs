using Grpc.Net.Client;
using Identity.Core.Grpc.Interfaces;
using Microsoft.Extensions.Configuration;
using Users;

namespace Identity.Core.Grpc;

public class GrpcUserClient: IUserClient
{
    private readonly IConfiguration _configuration;

    public GrpcUserClient(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<SignInResponse> SignInAsync(SignInRequest request)
    {
        GrpcChannel channel = GrpcChannel.ForAddress(_configuration["GrpcUsersUrl"]);

        var client = new GrpcUser.GrpcUserClient(channel);

        SignInResponse signInResponse = await client.SignInAsync(request);

        return signInResponse;
    }
}