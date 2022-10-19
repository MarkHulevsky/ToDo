namespace Mail.BusinessLogic.Models;

public class SendViaEmailQueueMessageModel
{
    public Guid FileId { get; set; }

    public Guid UserId { get; set; }
}