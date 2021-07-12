using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flyweight
{
    public class Classic
    {
        public static void Run()
        {
            var factory = new ColorFactory();
            var display = new Display(1920, 1080, factory);
            factory.UseCache = true;
            var painter = new Painter();
            var sizeBeforePaint = Process.GetCurrentProcess().PrivateMemorySize64;
            Console.WriteLine($"Usage before paint - { sizeBeforePaint }");
            paint();
            var sizeAfterFlyweightPaint = Process.GetCurrentProcess().PrivateMemorySize64;
            Console.WriteLine($"Usage after paint with Flyweight - {sizeAfterFlyweightPaint}, diff {sizeAfterFlyweightPaint - sizeBeforePaint}");
            
            display.Clear();
            GC.Collect();

            factory.UseCache = false;
            paint();
            var sizeAfterPaint = Process.GetCurrentProcess().PrivateMemorySize64;
            Console.WriteLine($"Usage after paint without Flyweight - { Process.GetCurrentProcess().PrivateMemorySize64}, diff {sizeAfterFlyweightPaint - sizeAfterPaint}, ");

            // Рисуем одно и то же для чистоты эксперемента, хотя тут все и ежу понятно)
            void paint()
            {
                painter.DrowSquare(display: display,
                    startPoint: new Point { X = 0, Y = 0 },
                    width: 800,
                    color: new ColorDto { Red = 124, Gray = 101, Blue = 123 });
                painter.DrowRectangle(display: display,
                    startPoint: new Point { X = 1000, Y = 0 },
                    width: 900,
                    height: 1000,
                    color: new ColorDto { Red = 121, Gray = 11, Blue = 124 });
            }
        }
    }

    public class Painter
    {
        public void DrowSquare(Display display, Point startPoint, int width, ColorDto color)
        {
            DrowRectangle(display, startPoint, width, width, color);
        }

        public void DrowRectangle(Display display, Point startPoint, int width, int height, ColorDto color)
        {
            var point = new Point();
            for (int i = 0; i < width; i++)
            {
                point.X = i + startPoint.X;
                for (int j = 0; j < height; j++)
                {
                    point.Y = j + startPoint.Y;
                    display.SetPixelColor(point, color);
                }
            }
        }
    }

    public class Display
    {
        public int Width => pixels.GetLength(0);
        public int Height => pixels.GetLength(1);

        private Color[,] pixels;
        private ColorFactory colorFacrtory;

        public Display(int width, int height, ColorFactory colorFacrtory)
        {
            pixels = new Color[width, height];
            this.colorFacrtory = colorFacrtory;
        }

        public void Clear()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    pixels[i, j] = null;
                }
            }
        }

        public void SetPixelColor(Point point, ColorDto ColorDto)
        {
            pixels[point.X, point.Y] = colorFacrtory.GetColor(ColorDto);
        }
    }

    public struct Point { public int X; public int Y; };

    // На самом деле можно передавать и классом, но тут с виду подходит структура
    // для потребителя дисплея бует все логично, пиксель - структура
    // но мы экономим память через ColorFactory, это никому знать не обязательно
    public struct ColorDto { public byte Red; public byte Gray; public byte Blue; };

    public record Color(byte Red, byte Gray, byte Blue);

    // Пример может не совсем корректный, но попадает под паттерн
    // Просто храним одинаковые цвета в словаре до востребования
    public class ColorFactory
    {
        private Dictionary<int, Color> colors = new Dictionary<int, Color>();
        public bool UseCache { get; set; }

        public Color GetColor(ColorDto colorDto)
        {
            // для сравнения
            if (!UseCache)
            {
                return new Color(colorDto.Red, colorDto.Gray, colorDto.Blue);
            }

            var hash = colorDto.GetHashCode();
            if (!colors.TryGetValue(hash, out var color))
            {
                color = new Color(colorDto.Red, colorDto.Gray, colorDto.Blue);
                colors[hash] = color;
            }
            return color;
        }
    }
}
