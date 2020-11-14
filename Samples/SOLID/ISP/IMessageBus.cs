using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.SOLID.ISP
{
public interface IMessageBus 
    : IPublisher, ISubscriber
{ }

public interface IPublisher
{
    void Publish(object message);
}

public interface ISubscriber
{
    void Subscribe(string topic, Action<object> messageHandler);
}
}
