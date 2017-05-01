using BaoZouRiBao.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace BaoZouRiBao.Storage
{
    public class RoamingStorage : IStorage
    {
        #region Property
        private ApplicationDataContainer Container
        {
            get
            {
                return ApplicationData.Current.RoamingSettings;
            }
        }
        #endregion
        
        /// <summary>
        /// Add item to RomaingSettings
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddItem(string key, object value)
        {
            Container.Values[key] = JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// Judge whether RomaingSettings has this item through the key given
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsItem(string key)
        {
            return Container.Values.ContainsKey(key);
        }

        /// <summary>
        /// Clear data in RomaingSettings
        /// </summary>
        public void FlushAll()
        {
            foreach (KeyValuePair<string, object> current in Container.Values)
            {
                RemoveItem(current.Key);
            }
        }

        /// <summary>
        /// Get item from RomaingSettings
        /// </summary>
        /// <typeparam name="T">Type that you saved</typeparam>
        /// <param name="key">Key</param>
        /// <returns>Value that you saved</returns>
        public T GetItem<T>(string key)
        {
            string value = Container.Values[key]?.ToString();
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> GetItems<T>(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <returns></returns>
        public long GetSize()
        {
            throw new NotImplementedException();
        }

        public bool RemoveItem(string key)
        {
            return Container.Values.Remove(key);
        }

        public void UpdateItem(string key, object value)
        {
            AddItem(key, value);
        }
    }
}
