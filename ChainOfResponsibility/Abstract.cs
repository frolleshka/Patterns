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

    // некоторый контекст
    public class Context
    {
        public int ContextValue;
    }

    // некоторый элемент цепочки
    public class TestItem : IChainItem<Context>
    {
        void IChainItem<Context>.Handle(Context context, Action executeNext)
        {
            Console.WriteLine(context.ContextValue);
            context.ContextValue++;
            executeNext();
        }
    }

    /// <summary>
    /// Контракт который должен соблюдать каждый элемент цепочки
    /// </summary>
    public interface IChainItem<in TContext>
        where TContext : class
    {
        public void Handle(TContext context, Action executeNext);
    }

    /// <summary>
    /// Реализация цепочки
    /// Все просто, можно сделать асинхронную версию, суть не поменяется
    /// Храним обработчики в Chain(вызываем метод AddNext), как только нужно все обработать вызываем Handle.
    /// Можно несколько усложнить реализацию, добавить  результат обработки или добавить дополнительный враппинг на каждый хэндлер для обработки исключений и тд. В общем расширять можно как угодно. 
    /// </summary>
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
                if (current == null)
                {
                    return;
                }

                if (current.Next == null)
                {
                    current.Value.Handle(context, () => { });
                    return;
                }

                current = current.Next;
                current.Previous.Value.Handle(context, executor);
            };
            executor.Invoke();
        }
    }
}
