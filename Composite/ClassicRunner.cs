using Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    public static class ClassicRunner
    {
        public static void Run()
        {
            var forearm = new Forearm() { Description = "Forearm" };
            var palm = new Palm() { Description = "Palm" };
            
            forearm.AddChildren(palm);
            for (int i = 0; i < 5; i++)
            {
                palm.AddChildren(CreateFinger(i));
            }

            Console.Write(forearm.GetStruct());

            Finger CreateFinger(int fingerIndex)
            {
                var finger = new Finger() { Description = $"Finger_{fingerIndex}" };
                for(int i = 0; i < 3; i++)
                {
                    finger.AddChildren(new Falange()
                    {
                        Description = $"Falange_{i} for {finger.Description}"
                    });
                }
                return finger;
            }
        }
    }

    // Общий интерфейс для всех абстракций которые будут учавствовать в комановке
    public interface IPathOfBody
    {
        string Description { get; init; }
    }

    // Контейнер. Содержит в себе дочерние элементы, так же может реализовывать общий интерфейс,
    // может делегировать имплементацию дочерним элементам.
    public abstract class CompositePathOfBody : IPathOfBody
    {
        private List<IPathOfBody> pathOfBodies = new List<IPathOfBody>();

        public CompositePathOfBody AddChildren(IPathOfBody pathOfBody)
        {
            pathOfBodies.Add(pathOfBody);
            return this;
        }

        public abstract string Description { get; init; }

        // Обходим компановку и формируем строку. 
        // Тут все примитивно. В реале вариантов может быть массса,
        // лучше это дело вынести отсюда, тк это ответственность не компоновщика.
        public string GetStruct()
        {
            var result = $"{Description} -> [ ";
            
            foreach(var pathOfBody in pathOfBodies)
            {
                if (pathOfBody is CompositePathOfBody composite)
                {
                    result += $"{composite.GetStruct()}; ";
                }
                else
                {
                    result += $"{pathOfBody.Description}--";
                }
            }
            result += " ]";

            return result;
        }
    }

    // Простой компонент - "лист"
    public class Falange : IPathOfBody
    {
        public string Description { get; init; }
    }

    // Компонетны контейнеры - ветви
    public class Finger : CompositePathOfBody
    {
        public override string Description { get; init; }
    }

    public class Forearm : CompositePathOfBody
    {
        public override string Description { get; init; }
    }

    public class Palm : CompositePathOfBody
    {
        public override string Description { get; init; }
    }
}
