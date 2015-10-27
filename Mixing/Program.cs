using System;
using System.Text;
using System.Threading.Tasks;
using Mixing.Messages;
using Mixing.Mixin;

namespace Mixing
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                Message message = new Message("Hi, Message!");
                InviteMessage inviteMessage = new InviteMessage("Hi, InviteMessage");

                message.Send();
                inviteMessage.Send();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}