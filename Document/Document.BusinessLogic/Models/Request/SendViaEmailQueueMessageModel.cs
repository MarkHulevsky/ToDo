namespace Document.BusinessLogic.Models.Request;

public class SendViaEmailQueueMessageModel
{
    public Guid FileId { get; set; }

    public Guid UserId { get; set; }
}