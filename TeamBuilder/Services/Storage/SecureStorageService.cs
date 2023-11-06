using System.Text.Json;
using MauiStorage = Microsoft.Maui.Storage;

namespace TeamBuilder.Services.Storage
{
    public class SecureStorageService : ISecureStorageService
    {
        private readonly MauiStorage.ISecureStorage secureStorage;

        public SecureStorageService(MauiStorage.ISecureStorage secureStorage)
        {
            this.secureStorage = secureStorage;
        }
        public async Task Save(string key, string value)
        {
            try
            {
                await secureStorage.SetAsync(key, value);
                return;
            }
            catch (Exception ex)
            {
                var error = ex;
            }
        }

        public async Task Save(string key, object obj)
        {
            if (!string.IsNullOrEmpty(key) && (obj != null))
            {
                var json = JsonSerializer.Serialize(obj);
                await secureStorage.SetAsync(key, json);
            }
            return;
        }

        public async Task<string> Load(string key)
        {
            string keyValue = await secureStorage.GetAsync(key);

            if (!string.IsNullOrEmpty(keyValue))
            {
                //TODO
            }
            return keyValue;
        }

        public async Task<T> Load<T>(string key)
        {
            string keyValue = await secureStorage.GetAsync(key);
            T TResult = default(T);

            if (!string.IsNullOrEmpty(keyValue))
            {
                TResult = JsonSerializer.Deserialize<T>(keyValue);
            }
            return TResult;
        }

        public bool Remove(string key)
        {
            bool itemRemoved = secureStorage.Remove(key);
            return itemRemoved;
        }

        public async Task ClearAll()
        {
            secureStorage.RemoveAll();
            return;
        }
    }
}
