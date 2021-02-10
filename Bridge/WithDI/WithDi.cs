using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.WithDI
{

    public static class DiRunner
    {
        public static void Run()
        {
            var kernel = Composition.Composite();

            var counterFactory = kernel.Get<RomoteCounterFactory>();

            IRemoteCounterModule channelRemoteModule = counterFactory
                .CreateCounterRemoteModule<ChannelCounterDeviceModule>();
            Console.WriteLine("channelRemoteModule");
            channelRemoteModule.AddValue(6);
            channelRemoteModule.DownValue(6);

            IRemoteCounterModule volumeRemoteModule = counterFactory
                .CreateCounterRemoteModule<VolumeCounterDeviceModule>();
            Console.WriteLine("volumeRemoteModule");
            volumeRemoteModule.AddValue(5);
            volumeRemoteModule.DownValue(5);

            var volumeRemote = kernel.Get<IRemoteVolumeModule>();
            Console.WriteLine("IRemoteVolumeModule");
            volumeRemote.AddValue(5);
            volumeRemote.DownValue(5);
            volumeRemote.SetMaxVolume();
            volumeRemote.Mute();
        }
    }

    public static class Composition
    {
        public static IKernel Composite()
        {
            var kernel = new StandardKernel();
            kernel.Bind<VolumeCounterDeviceModule>().ToSelf();
            kernel.Bind<ChannelCounterDeviceModule>().ToSelf();
            kernel.Bind<RomoteCounterFactory>().ToSelf();

            kernel.Bind<IRemoteVolumeModule>().To<RemoteVolumeModule>();

            return kernel;
        }
    }

    public interface ICounterDeviceModule 
    {
        public int CurrentValue { get; }
        public bool TryIncrement();
        public bool TryDecrement();
    }

    public class VolumeCounterDeviceModule : ICounterDeviceModule
    {
        private static int MaxValue = 3;

        private int _currentVolume;
        public int CurrentValue => _currentVolume;

        public bool TryDecrement()
        {
            Console.WriteLine("Decriment");
            var mathResult = _currentVolume - 1;

            if (mathResult < 1)
            {
                return false;
            }
            _currentVolume = mathResult;
            Console.WriteLine($"current value - {CurrentValue}");
            return true;
        }

        public bool TryIncrement()
        {
            Console.WriteLine("Increment");
            var mathResult = _currentVolume + 1;
            if (mathResult > MaxValue)
            {
                return false;
            }
            _currentVolume = mathResult;
            Console.WriteLine($"current value - {CurrentValue}");
            return true;
        }
    }

    public class ChannelCounterDeviceModule : ICounterDeviceModule
    {
        private static int MaxValue = 3;

        private int _currentChannel;
        public int CurrentValue => _currentChannel;

        public bool TryDecrement()
        {
            Console.WriteLine("Decrement");
            var mathResult = _currentChannel - 1;

            _currentChannel = mathResult < 1
                ? MaxValue
                : mathResult;

            Console.WriteLine($"current value - {CurrentValue}");
            return true;
        }

        public bool TryIncrement()
        {
            Console.WriteLine("Increment");
            var mathResult = _currentChannel + 1;

            _currentChannel = mathResult > MaxValue
               ? 1
               : mathResult;

            Console.WriteLine($"current value - {CurrentValue}");
            return true;
        }
    }

    public interface IRemoteCounterModule 
    {
        int AddValue(int valueCount);
        int DownValue(int valueCount);
    }

    public class RemoteCounterModule : IRemoteCounterModule
    {
        protected ICounterDeviceModule _volumeDeviceModule;

        public RemoteCounterModule(ICounterDeviceModule volumeDeviceModule)
        {
            _volumeDeviceModule = volumeDeviceModule;
        }

        public int AddValue(int valueCount)
        {
            Console.WriteLine($"AddValue {valueCount}");
            for (var i = 0; i < valueCount; i++)
            {
                if (!_volumeDeviceModule.TryIncrement())
                    break;
            }
            Console.WriteLine("===");
            return _volumeDeviceModule.CurrentValue;
        }

        public int DownValue(int valueCount)
        {
            Console.WriteLine($"DownValue {valueCount}");
            for (var i = 0; i < valueCount; i++)
            {
                if (!_volumeDeviceModule.TryDecrement())
                    break;
            }
            Console.WriteLine("===");
            return _volumeDeviceModule.CurrentValue;
        }
    }

    public interface IRemoteVolumeModule : IRemoteCounterModule
    {
        void Mute();
        void SetMaxVolume();
    }

    public class RemoteVolumeModule : RemoteCounterModule, IRemoteVolumeModule
    {
        public RemoteVolumeModule(VolumeCounterDeviceModule counterDeviceModule) 
            : base(counterDeviceModule)
        {
        }

        public void Mute()
        {
            Console.WriteLine("Mute");
            while (_volumeDeviceModule.TryDecrement());
            Console.WriteLine("====");
        }

        public void SetMaxVolume()
        {
            Console.WriteLine("SetMaxVolume");
            while (_volumeDeviceModule.TryIncrement());
            Console.WriteLine("====");
        }
    }

    // чтобы все работало красиво добавляем фабрику
    public class RomoteCounterFactory 
    {
        private readonly IKernel _kernel;

        public RomoteCounterFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IRemoteCounterModule CreateCounterRemoteModule<T>() where T : ICounterDeviceModule
        {
            var counter = _kernel.Get<T>();
            // TODO разобраться как в ninject сделать родительский скоп
            // либо забить на него и юзать норм DI
            return new RemoteCounterModule(counter);
        }
    }

}
