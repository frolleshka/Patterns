using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    public static class ClassicRunner
    {
        public static void Run()
        {
            // просто сообщение
            IMessageSender sender = new MessageSender();
            var message = "Message for send";
            sender.SendMessage(message);

            // сообщение с доп инфой
            sender = new MessageSenderAdditionalInfoDecorator(sender);
            sender.SendMessage(message);
            
            // сжимаем сообщение
            sender = new MessageSenderCompressorDecorator(sender);
            sender.SendMessage(message);

            // сжимаем сообщение с доп инфой
            sender = new MessageSender();
            sender = new MessageSenderCompressorDecorator(sender);
            sender = new MessageSenderAdditionalInfoDecorator(sender);
            sender.SendMessage(message);
        }
    }

    public interface IMessageSender
    {
        public void SendMessage(string message);
    }

    public class MessageSenderAdditionalInfoDecorator : IMessageSender
    {
        private readonly IMessageSender messageSender;

        public MessageSenderAdditionalInfoDecorator(IMessageSender messageSender)
        {
            this.messageSender = messageSender;
        }

        public void SendMessage(string message)
        {
            message = $"{DateTime.Now} : {message}";
            messageSender.SendMessage(message);
        }
    }

    public class MessageSenderCompressorDecorator : IMessageSender
    {
        private readonly IMessageSender messageSender;

        public MessageSenderCompressorDecorator(IMessageSender messageSender)
        {
            this.messageSender = messageSender;
        }

        public void SendMessage(string message)
        {
            var bytes = Encoding.Unicode.GetBytes(message);
            string compressString;
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);
                }
                compressString = Convert.ToBase64String(mso.ToArray());
            }
            messageSender.SendMessage(compressString);
        }
    }

    public class MessageSender : IMessageSender
    {
        public void SendMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
