using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Proxy
{
    public static class Classic
    {
        public static void Run()
        {
            var catalog = new CatalogCacheProxy();
            catalog.SetData(new[] {
                new CatalogItem { Value = Guid.NewGuid().ToString() },
                new CatalogItem { Value = Guid.NewGuid().ToString() },
            });
            
            // from file
            catalog.GetData();
            // from cache
            var data = catalog.GetData();

            catalog = new CatalogCacheProxy();
            // from cache
            data = catalog.GetData();
            data[0].Value = Guid.NewGuid().ToString();
            catalog.SetData(new[] { data[0] });
            // from file
            catalog.GetData();
            // from cache
            catalog.GetData();
        }

    }

    public interface ICatalog
    {
        IList<CatalogItem> GetData();
        void SetData(ICollection<CatalogItem> items);
    }

    public class CatalogItem
    {
        public Guid? Id { get; set; }
        public string Value { get; set; }
    }


    public class Catalog : ICatalog
    {
        private static string FileName = Path.Combine(Environment.CurrentDirectory, "CatalogItem.data");

        public IList<CatalogItem> GetData()
        {
            // Эмулируем задержку чтения
            Thread.Sleep(1000);
            Console.WriteLine("Read From File");
            if (!File.Exists(FileName))
            {
                return new CatalogItem[0];
            }

            var jsonString = File.ReadAllText(FileName);
            return JsonSerializer.Deserialize<List<CatalogItem>>(jsonString);
        }

        public void SetData(ICollection<CatalogItem> items)
        {
            foreach (var item in items.Where(s => !s.Id.HasValue))
            {
                    item.Id = Guid.NewGuid();
            }
            File.WriteAllText(FileName, JsonSerializer.Serialize(items));
        }
    }

    public class CatalogCacheProxy : ICatalog
    {
        private static IList<CatalogItem> cache;
        private readonly ICatalog catalog;

        public CatalogCacheProxy()
        {
            catalog = new Catalog();
        }

        public IList<CatalogItem> GetData()
        {
            if (cache == null)
            {
                cache = catalog.GetData();
                return cache;
            }
            else
            {
                Console.WriteLine("Read From Cache");
                return cache;
            }
        }

        public void SetData(ICollection<CatalogItem> items)
        {
            catalog.SetData(items);
            // Invalidate
            Console.WriteLine("Invalidate Cache");
            cache = null;
        }
    }
}
