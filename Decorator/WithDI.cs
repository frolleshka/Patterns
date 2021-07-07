using Autofac;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    class WithDI
    {
        public static void Run()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventHandler>().As<IEventHandler<FirstEvent>>();
            builder.RegisterType<EventHandler>().As<IEventHandler<SecondEvent>>();

            // один из примеров регистрации декоратаров
            // в каждом нормальном Di должен быть подобный механизм
            // в Autofac есть несколько вариантов использования,
            // нп вызывать резодвить только по необходимости,
            // + есть некий контекст докоратаров
            // + можно настройить порядок вложенности (вданном примере сначалиа идут более поздно зареганые)
            builder.RegisterGenericDecorator(
                typeof(ExeptionHandlerDecorator<>),
                typeof(IEventHandler<>));

            builder.RegisterGenericDecorator(
                typeof(MetricHandlerDecorator<>),
                typeof(IEventHandler<>));
            var conteiner = builder.Build();

            Console.WriteLine("==FirstWorkerEntity==");
            var handlerFirst = conteiner.Resolve<IEventHandler<FirstEvent>>();
            handlerFirst.Handle(new FirstEvent("FirstWorkerEntity"));

            Console.WriteLine("==SecondEvent==");
            var handlerSecond = conteiner.Resolve<IEventHandler<SecondEvent>>();
            handlerSecond.Handle(new SecondEvent("SecondEvent"));
        }

    }

    // Общий контракт
    public interface IEventHandler<in T>
    {
        public void Handle(T workEntity);
    }

    public record FirstEvent(string Name);
    public record SecondEvent(string Name);

    // Основная имплементация которая будет декорироваться
    public class EventHandler : IEventHandler<FirstEvent>,
        IEventHandler<SecondEvent>
    {
        public void Handle(FirstEvent workEntity)
        {
            Console.WriteLine(workEntity);
        }

        public void Handle(SecondEvent workEntity)
        {
            if (DateTime.Now.Ticks % 2 == 0)
            {
                throw new Exception("DateTime.Now.Ticks % 2");
            }
            Console.WriteLine(workEntity);
        }
    }

    // Ниже несколько декораторов, которые решают каждый свою задачу
    public class ExeptionHandlerDecorator<T> : IEventHandler<T>
    {
        private IEventHandler<T> worker;

        public ExeptionHandlerDecorator(IEventHandler<T> worker)
        {
            this.worker = worker;
        }

        public void Handle(T workEntity)
        {
            Console.WriteLine($"Start ExeptionHandlerDecorator");
            
            try
            {
                worker.Handle(workEntity);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR - " + ex.Message);
            }
           
            Console.WriteLine($"Stop ExeptionHandlerDecorator");
        }
    }

    public class MetricHandlerDecorator<T> : IEventHandler<T>
    {
        private IEventHandler<T> worker;

        public MetricHandlerDecorator(IEventHandler<T> worker)
        {
            this.worker = worker;
        }

        public void Handle(T workEntity)
        {
            Console.WriteLine($"Start MetricHandlerDecorator");
            
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            worker.Handle(workEntity);
            stopWatch.Stop();
            Console.WriteLine(stopWatch.Elapsed.TotalMilliseconds);

            Console.WriteLine($"Stop MetricHandlerDecorator");
        }
    }
}
