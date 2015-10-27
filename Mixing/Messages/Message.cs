using System;
using System.IO;
using System.Text;

namespace Mixing.Messages
{
    public class Message : IDisposable, IMessage
    {
        public Message(string info)
        {
            Info = new StringBuilder(info);
        }

        public virtual StringBuilder Info { get; private set; }

        public virtual string Id
        {
            get { return string.Format("Message: {0}", Guid.NewGuid()); }
        }

        public virtual Stream Read()
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(string.Format("{0}: {1}", Id, Info)));
        }

        public virtual void Append(Stream stream)
        {
            Info.Append(new StreamReader(stream).ReadToEnd());
        }

        public virtual void Close()
        {
            Dispose();
        }

        public virtual void Dispose()
        {
            //clean my res.
        }
    }
}