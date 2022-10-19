namespace Document.BusinessLogic.MessageQueueClients.Interfaces;

public interface IMessageQueueClient
{
    void Enqueue(byte[] message);
}