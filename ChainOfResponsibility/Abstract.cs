using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibility
{
    public static class Abstract
    {
        public static void Run()
        {
            Console.WriteLine("");
            Console.WriteLine("AbstractRun");
            var context = new Context();
            var item = new TestItem();
            var cor = new CoR<Context>();
            cor.AddNext(item);
            cor.AddNext(item);
            cor.AddNext(item);

            cor.Handle(context);
            Console.WriteLine(context.ContextValue);
        }
    }

    public class Context
    {
        public int ContextValue;
    }

    public class TestItem : IChainItem<Context>
    {
        void IChainItem<Context>.Handle(Context context, Action executeNext)
        {
            context.ContextValue++;
            executeNext();
        }
    }

    public interface IChainItem<in TContext>
        where TContext : class
    {
        public void Handle(TContext context, Action executeNext);
    }

    // некоторый пример общей абстракции, надо описать

    public class CoR <TContext>
        where TContext : class
    {
        private readonly LinkedList<IChainItem<TContext>> Chain = new LinkedList<IChainItem<TContext>>();
        public CoR<TContext> AddNext(IChainItem<TContext> chainItem)
        {
            Chain.AddLast(chainItem);
            return this;
        }

        public void Handle(TContext context)
        {
            var current = Chain.First;
            Action executor = null;
            executor = () =>
            {
                current = current.Next;
                if (current == null)
                {
                    return;
                }
                current.Previous.Value.Handle(context, executor);
            };
            executor.Invoke();
        }
    }
}
