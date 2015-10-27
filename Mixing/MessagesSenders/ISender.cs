using Mixing.Messages;

namespace Mixing.MessagesSenders
{
    internal interface ISender<in T> where T: class 
    {
        void Send(T t);
    }
}