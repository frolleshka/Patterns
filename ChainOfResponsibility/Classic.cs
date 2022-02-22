using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibility
{
    public static class Classic
    {
        public static void Run()
        {
            var document = new Document();
            var chainInstantion = CretaInstantion();
            
            chainInstantion.HandleDocument(document);
            document.Id = 10;
            chainInstantion.HandleDocument(document);
            document.Name = "Name";
            chainInstantion.HandleDocument(document);
            document.Data = "Data";
            chainInstantion.HandleDocument(document);

            try
            {
                CretaUncorrectInstantion();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            static IInstansion CretaUncorrectInstantion()
            {
                var first = new FirstInstansion();
                var second = new SecondInstansion();
                var third = new ThirdInstansion();

                first.AddNext(second);
                second.AddNext(third);
                third.AddNext(first);
                return first;
            }

            static IInstansion CretaInstantion()
            {
                var first = new FirstInstansion();
                var second = new SecondInstansion();
                var third = new ThirdInstansion();

                first.AddNext(second);
                second.AddNext(third);
                return first;
            }
        }
    }

    public enum InstantionType
    {
        First,
        Second,
        Third
    }


    // Сама цепочка, определил в базовом классе
    public abstract class BaseInstantion : IInstansion
    {
        public IInstansion NextInstantion { get; private set; }
        public abstract InstantionType InstansionType { get; }

        public void AddNext(IInstansion nextInstantion)
        {
            // тк это связный список и он определяется каждым элементом, то сделал проверку на зацикливание
            // можно было не открывать сл элемент через IInstansion, тогда можно было детектировать только в рантайме
            var currentNextInstantion = nextInstantion;
            while (currentNextInstantion != null)
            {
                // еслине будет инстанций как в этом примере, можно сравнивать по ссылке, или как угодно
                if (currentNextInstantion.InstansionType == InstansionType)
                {
                    throw new Exception("Loop in chain detected");
                }
                currentNextInstantion = currentNextInstantion.NextInstantion;
            }

            this.NextInstantion = nextInstantion;
        }

        private void MoveNext(Document document)
        {
            if (NextInstantion == null)
            {
                Console.WriteLine("End chain");
                return;
            }
            Console.WriteLine($"{this.GetType().Name} handled doc");
            document.CurrentInstantion = NextInstantion.InstansionType;
            NextInstantion.HandleDocument(document);
        }

        protected abstract void HandleDocumentInternal(Document document, Action MoveNext);

        public void HandleDocument(Document document)
        {
            // тк документ хранит состояние, берем только нужный обработчик
            if (document.CurrentInstantion != this.InstansionType)
            {
                NextInstantion?.HandleDocument(document);
                return;
            }
            HandleDocumentInternal(document, () => MoveNext(document));
        }
    }

    // каждый элемент цепи, можно сколь угодно добавлять и добавлять поведение.

    public class FirstInstansion : BaseInstantion
    {
        public override InstantionType InstansionType => InstantionType.First;
        protected override void HandleDocumentInternal(Document document, Action MoveNext)
        {
            if (document.Id == 0)
            {
                Console.WriteLine("Document.Id must be > 0");
                return;
            }
            MoveNext();
        }
    }

    public class SecondInstansion : BaseInstantion
    {
        public override InstantionType InstansionType => InstantionType.Second;

        protected override void HandleDocumentInternal(Document document, Action MoveNext)
        {
            if (string.IsNullOrEmpty(document.Name))
            {
                Console.WriteLine("document.Name must have string data");
                return;
            }
            MoveNext();
        }
    }

    public class ThirdInstansion : BaseInstantion
    {
        public override InstantionType InstansionType => InstantionType.Third;
        protected override void HandleDocumentInternal(Document document, Action MoveNext)
        {
            if (string.IsNullOrWhiteSpace(document.Data))
            { 
                Console.WriteLine("document.Data must have string data");
                return;
            }
            MoveNext();
        }
    }

    public interface IInstansion
    {
        IInstansion NextInstantion { get; }
        InstantionType InstansionType { get; }
        void HandleDocument(Document document);
    }

    // некоторый объект который гуляет по обработчикам
    // в данном случае даже хранит состояние до куда дошел)
    
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public InstantionType CurrentInstantion { get; set; } = InstantionType.First;
    }
}
