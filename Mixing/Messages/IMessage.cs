using System.IO;

namespace Mixing.Messages
{
    public interface IMessage
    {
        Stream Read();
        void Append(Stream stream);
        void Close();
    }
}