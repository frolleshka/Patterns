using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter.WithDI
{
    public static class DiRunner
    {
        public static void Run()
        {
            var kernel = Composition.Composite();
            var billPaymentAutonate = kernel.Get<IBillPaymentAutomat>();
            billPaymentAutonate.Pay(new Bill(Nominal:30));
        }
    }

    public static class Composition
    {
        public static IKernel Composite()
        {
            var kernel = new StandardKernel();
            kernel.Bind<PaymentAutomat>().ToSelf();
            kernel.Bind<IBillPaymentAutomat>().To<BillPaymentAutomat>();
            return kernel;
        }
    }

    public class PaymentAutomat 
    {
        public void ThrowJetons(JetonPacket jetons)
        {
            Console.WriteLine($"You throw {jetons.JetonCount} jetons");
        }
    }

    public class JetonPacket
    {
        public int JetonCount { get; set; }
    }

    public record Bill (int Nominal);

    public interface IBillPaymentAutomat
    {
        void Pay(Bill bill);
    }

    public class BillPaymentAutomat : IBillPaymentAutomat
    {
        private readonly PaymentAutomat _paymentAutomat;

        public BillPaymentAutomat(PaymentAutomat paymentAutomat)
        {
            _paymentAutomat = paymentAutomat;
        }

        public void Pay(Bill bill)
        {
            // тут можно юзать сервис конвертации в зависимсти от типа валюты,
            // но это совсем другая история
            var cource = new Random().Next(1, 5);
            Console.WriteLine($"Your cource 1/{cource}");
            var jetonCount = bill.Nominal * cource;
            _paymentAutomat.ThrowJetons(new JetonPacket { JetonCount = jetonCount });
        }
    }
}
