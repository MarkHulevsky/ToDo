using AutoMapper;
using Document.DataAccess.Entities;
using Document.BusinessLogic.Models.Response;

namespace Document.BusinessLogic.MapperProfiles;

public class BlobFileProfile: Profile
{
    public BlobFileProfile()
    {
        CreateMap<BlobFile, GeneratePdfByDirectoryIdResponse>();
    }
}