using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter.Classic
{
    public static class ClassicRunner
    {
        public static void Run()
        {
            var adapteeLogger = new AdapteeLogger();
            var fooAdapter = new ObjectLoggerAdapter(adapteeLogger);
            var stringAdapter = new MessageLoggerAdapter(adapteeLogger);

            var range = Enumerable.Range(0, 5);
            var fooCollection = range.Select(s => new Foo(Guid.NewGuid()));
            fooAdapter.Log(fooCollection);

            var messageCollection =
                range.Select(s => "Messsage - " + Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
            stringAdapter.Log(messageCollection);
        }
    }

    public record Foo ( Guid Id );

    public class AdapteeLogger
    {
        public void LogMessage(StringBuilder stringBuilder)
        {
            Console.WriteLine($"LogDate : {DateTime.Now}");
            Console.WriteLine(stringBuilder);
            Console.WriteLine($"************************");
        }
    }

    public interface ILoggerAdapter<in T>
    {
        void Log(IEnumerable<T> messages);
    }

    // Это дело лучше всего обобщить до конкретного типа
    // ObjectLoggerAdapter<T> : ILoggerAdapter<T>
    public class ObjectLoggerAdapter : ILoggerAdapter<object>
    {
        private readonly AdapteeLogger _adapteeLogger;

        public ObjectLoggerAdapter(AdapteeLogger adapteeLogger)
        {
            _adapteeLogger = adapteeLogger;
        }

        public void Log(IEnumerable<object> objects)
        {
            var sb = new StringBuilder();
            foreach (var @object in objects)
            {
                sb.AppendLine($"objectType - {@object.GetType().Name}");
                sb.AppendLine(@object.ToString());
                sb.AppendLine("++++");
            }
            _adapteeLogger.LogMessage(sb);
        }
    }

    public class MessageLoggerAdapter : ILoggerAdapter<string>
    {
        private readonly AdapteeLogger _adapteeLogger;

        public MessageLoggerAdapter(AdapteeLogger adapteeLogger)
        {
            _adapteeLogger = adapteeLogger;
        }

        public void Log(IEnumerable<string> messages)
        {
            var sb = new StringBuilder();
            foreach(var message in messages)
            {
                sb.AppendLine(message);
                sb.AppendLine("=====");
            }
            _adapteeLogger.LogMessage(sb);
        }
    }
}
