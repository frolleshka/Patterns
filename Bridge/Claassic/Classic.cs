using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Claassic
{

    public static class ClassicRunner
    {
        public static void Run()
        {
            var memoryTextContainer = new MemoryTextContainer();
            var textContainer = new FileTextContainer();

            memoryTextContainer.WritePage(5);

            var textWriter = new TextWriter(memoryTextContainer);
            textWriter.WriteFullText();
           

            var extendedTextWriter = new ExtendedTextWriter(textContainer);
            
            extendedTextWriter.WriteFullText();
            extendedTextWriter.WritePageRange(1, 3);
        }
    }

    public interface ITextContainer
    {
        string GetTextFromPage(int pageNum);
        string GetFullText();
    }

    public class MemoryTextContainer : ITextContainer
    {
        public string GetFullText()
        {
            return "Full text from MEMORY";
        }

        public string GetTextFromPage(int pageNum)
        {
            return $"Text from page[{pageNum}] from MEMORY";
        }

        // Можно применять например и так
        // только желательно чтобы либо реализация хранилась в абстракции либо наоборот
        // это только пример
        public void WritePage(int pageNumber)
        {
            var textWriter = new ExtendedTextWriter(this);
            textWriter.WritePageRange(pageNumber, pageNumber);
        }
    }

    public class FileTextContainer : ITextContainer
    {
        public string GetFullText()
        {
            return "Full text from FILE";
        }

        public string GetTextFromPage(int pageNum)
        {
            return $"Text from page[{pageNum}] from FILE";
        }
    }

    public class TextWriter
    {
        protected readonly ITextContainer _textContainer;

        public TextWriter(ITextContainer textContainer)
        {
            _textContainer = textContainer;
        }

        public void WriteFullText()
        {
            var fullText = _textContainer.GetFullText();
            Console.WriteLine("Start write text");
            Console.WriteLine(fullText);
            Console.WriteLine("End write text");
            Console.WriteLine("---");
        }
    }

    public class ExtendedTextWriter : TextWriter
    {
        public ExtendedTextWriter(ITextContainer textContainer) : base(textContainer)
        {
        }

        public void WritePageRange(int startPage, int endPage)
        {
            // тут еще должен быть код валидации startPage и endPage, ну или они должны быть в структуре нп
            Console.WriteLine("Start write text");
            for (int i = startPage; i <= endPage; i++)
            {
                Console.WriteLine(_textContainer.GetTextFromPage(i));
            }
            Console.WriteLine("End write text");
            Console.WriteLine("---");
        }
    }
}
