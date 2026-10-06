namespace Griffin.EventStoreDB.Events;

public interface IProjection
{
    void When(object @event);
}