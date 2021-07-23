using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;

namespace Proxy
{
    public static class WithDI
    {
        public static void Run()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<WorkerLoger>().AsSelf();
            builder.RegisterType<WorkerDependencyContainer>().AsSelf();
            builder.RegisterType<WorkerPermissionProxy>().As<IWorker>();

            var container = builder.Build();
            var worker = container.Resolve<IWorker>();
            for(int i = 0; i < 5; i++)
            {
                worker.Work(new WorkUser(i));
            }
        }
    }

    public class WorkerPermissionProxy : IWorker
    {
        private Lazy<Worker> worker;
        public WorkerPermissionProxy(WorkerDependencyContainer workerDependencyContainer)
        {
            worker = new Lazy<Worker>(() =>
            {
                return new Worker(workerDependencyContainer);
            });
        }

        public void Work(WorkUser workUser)
        {
            //это может делать еще один сервис, обязанность данного клвсса не пустить в Worker лишних юзеров
            if (workUser.UserId % 2 == 0)
            {
                worker.Value.Work(workUser);
            }
            else
            {
                Console.WriteLine($"User - {workUser} dont have permission for work");
            }
        }
    }

    public record WorkUser(int UserId);

    public interface IWorker
    {
        void Work(WorkUser workUser);
    }

    public class Worker : IWorker
    {
        private readonly WorkerDependencyContainer dependencyContainer;
        public Worker(WorkerDependencyContainer dependencyContainer)
        {
            Thread.Sleep(1000);
            this.dependencyContainer = dependencyContainer;
        }
        public void Work(WorkUser workUser)
        {
            dependencyContainer.WorkerLoger.LogCall(workUser);
        }
    }

    public class WorkerLoger
    {
        public void LogCall(WorkUser workUser)
        {
            Console.WriteLine($"WorkerLoger Log User - {workUser}");
        }
    }

    // можно использовать фабрику
    // но тут сделал зависимости через контейнер
    // тк добавить их надо будет только сюда, и переиспользовать в Worker + хотелось избежать использования др паттернов
    public class WorkerDependencyContainer
    {
        public readonly WorkerLoger WorkerLoger;
        public WorkerDependencyContainer(WorkerLoger workerLoger)
        {
            WorkerLoger = workerLoger;
        }
    }
}
