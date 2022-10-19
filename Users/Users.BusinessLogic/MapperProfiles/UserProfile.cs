using AutoMapper;
using Users.BusinessLogic.Models.User.Response;
using Users.DataAccess.Entities;

namespace Users.BusinessLogic.MapperProfiles;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<User, GetUserByIdResponse>();
    }
}