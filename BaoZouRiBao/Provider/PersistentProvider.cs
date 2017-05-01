using BaoZouRiBao.Interfaces;
using BaoZouRiBao.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Provider
{
    public class PersistentProvider : IProvider, IStorage
    {
        private IStorage _storage;
        private StorageType _storageType;

        public PersistentProvider(StorageType storageType)
        {
            _storageType = storageType;
            Initialize();
        }

        public void Initialize()
        {
            if (_storageType == StorageType.Local)
                _storage = new LocalStorage();
            else
                _storage = new RoamingStorage();
        }


        /// <summary>
        /// Add item to Storage
        /// </summary>
        /// <param name="key">
        /// If the type of storage is sqlite,
        /// should be the format of "Type/TypeID"
        /// Eg:If you have a type of "Person" and the id is 123
        /// so the format will like "Person/123"
        /// </param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void AddItem(string key, object value)
        {
            _storage.AddItem(key, value);
        }

        /// <summary>
        /// Update item in Storage
        /// </summary>
        /// <param name="key">
        /// If the type of storage is sqlite,
        /// should be the format of "Type/TypeID"
        /// Eg:If you have a type of "Person" and the id is 123
        /// so the format will like "Person/123"
        /// </param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void UpdateItem(string key, object value)
        {
            _storage.UpdateItem(key, value);
        }

        /// <summary>
        /// Judge whether Storage has this item through the key given
        /// </summary>
        /// <param name="key">
        /// If the type of storage is sqlite,
        /// should be the format of "Type/TypeID"
        /// Eg:If you have a type of "Person" and the id is 123
        /// so the format will like "Person/123"
        /// </param>
        /// <returns>true for yes and false for no</returns>
        public bool ContainItem(string key)
        {
            return _storage.ContainsItem(key);
        }

        /// <summary>
        /// Clear data in Storage
        /// </summary>
        public void Flush(StorageType type)
        {
            _storage.FlushAll();
        }

        /// <summary>
        /// Get item from Storage
        /// </summary>
        /// <typeparam name="T">Type that you saved</typeparam>
        /// <param name="key">
        /// If the type of storage is sqlite,
        /// should be the format of "Type/TypeID"
        /// Eg:If you have a type of "Person" and the id is 123
        /// so the format will like "Person/123"
        /// </param>
        /// <returns>Value that you saved</returns>
        public T GetItem<T>(string key)
        {
            return _storage.GetItem<T>(key);
        }

        /// <summary>
        /// Get items from Storage
        /// </summary>
        /// <typeparam name="T">Type that you saved</typeparam>
        /// <param name="key">
        /// If the type of storage is sqlite,
        /// should be the format of "Type/TypeID"
        /// Eg:If you have a type of "Person" and the id is 123
        /// so the format will like "Person/123"
        /// </param>
        /// <returns></returns>
        public List<T> GetItems<T>(string key)
        {
            return _storage.GetItems<T>(key);
        }

        /// <summary>
        /// Remove item in Storage
        /// </summary>
        /// <typeparam name="T">Type that you saved</typeparam>
        /// <param name="key">
        /// If the type of storage is sqlite,
        /// should be the format of "Type/TypeID"
        /// Eg:If you have a type of "Person" and the id is 123
        /// so the format will like "Person/123"
        /// </param>
        /// <returns>Value that you saved</returns>
        public bool RemoveItem(string key)
        {
            return _storage.RemoveItem(key);
        }

        /// <summary>
        /// Get the size of local file
        /// </summary>
        /// <returns></returns>
        public long GetSize()
        {
            return _storage.GetSize();
        }

        public bool ContainsItem(string key)
        {
            return _storage.ContainsItem(key);
        }

        public void FlushAll()
        {
            _storage.FlushAll();
        }
    }
}
