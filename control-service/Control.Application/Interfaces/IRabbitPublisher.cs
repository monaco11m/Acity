namespace Control.Application.Interfaces
{
    public interface IRabbitPublisher
    {
        Task PublishAsync(object message);
    }
}