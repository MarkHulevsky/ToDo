using Common.Models;

namespace Document.BusinessLogic.Models.Response;

public class GeneratePdfByDirectoryIdResponse: BaseResponseModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}