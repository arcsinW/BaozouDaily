using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Interfaces
{
    internal interface IStorage
    {
        void AddItem(string key, object value);

        void UpdateItem(string key, object value);

        bool ContainsItem(string key);

        void FlushAll();

        T GetItem<T>(string key);

        List<T> GetItems<T>(string key);

        bool RemoveItem(string key);

        long GetSize();

    }
}
