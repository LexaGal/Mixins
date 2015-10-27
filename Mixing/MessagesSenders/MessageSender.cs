using System;
using Mixing.Messages;
using Mixing.TargetTypeAttributes;

namespace Mixing.MessagesSenders
{
    [TargetType(typeof(Message), typeof(Message))]
    class MessageSender : ISender<Message>
    {
        public void Send(Message message)
        {
            Console.WriteLine("Message '{0}' is send by MessageSender", message.Id);
        }
    }
}