namespace TeamBuilder.Services.Storage
{
    public interface ISecureStorageService
    {
        public interface ISecureStorage
        {
            /// <summary>
            /// Save the <see cref="Task"/>.
            /// </summary>
            /// <param name="key">The key.</param>
            /// <param name="value">The value.</param>
            /// <returns>A Task.</returns>
            Task Save(string key, string value);

            /// <summary>
            /// Save the <see cref="Task"/>.
            /// </summary>
            /// <param name="key">The key.</param>
            /// <param name="obj">The obj.</param>
            /// <returns>A Task.</returns>
            Task Save(string key, object obj);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="key">The key.</param>
            /// <returns><![CDATA[Task<string>]]></returns>
            Task<string> Load(string key);

            /// <summary>
            /// Load a value from Secure Storage and Deserialize to type (T)
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="key"></param>
            /// <returns></returns>
            Task<T> Load<T>(string key);

            /// <summary>
            /// Clear all.
            /// </summary>
            /// <returns>A Task.</returns>
            Task ClearAll();

            /// <summary>
            /// 
            /// </summary>
            /// <param name="key">The key.</param>
            /// <returns>A bool.</returns>
            bool Remove(string key);
        }
    }
}
