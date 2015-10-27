using System;
using Mixing.Messages;
using Mixing.TargetTypeAttributes;

namespace Mixing.MessagesSenders
{
    [TargetType(typeof(InviteMessage), typeof(Message))]
    [TargetType(typeof(Message), typeof(Message))]
    class InviteMessageSender : ISender<Message>
    {
        public void Send(Message message)
        {
            Console.WriteLine("InviteMessage '{0}' is send by InviteMessageSender", message.Id);
        }
    }
}