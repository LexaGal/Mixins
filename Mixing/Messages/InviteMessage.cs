using System;
using System.Text;

namespace Mixing.Messages
{
    public class InviteMessage : Message
    {
        public override string Id
        {
            get { return string.Format("InviteMessage: {0}", Guid.NewGuid()); }
        }

        public override void Dispose()
        {
            //clean my res.
        }

        public InviteMessage(string info) : base(info)
        {
        }
    }
}