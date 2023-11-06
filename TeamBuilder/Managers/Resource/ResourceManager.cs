using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using TeamBuilder.Resources.Resx;

namespace TeamBuilder.Managers.Resource
{
    /// <summary>
    /// The resource manager.
    /// </summary>
    public class ResourceManager : IResourceManager
    {
        /// <summary>
        /// Gets the resource string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A string.</returns>
        public string GetResourceString(string key)
            => AppResources.ResourceManager.GetString(key);

        /// <summary>
        /// Gets the resource value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns>A <typeparamref name="T"></typeparamref></returns>
        public T GetResourceValue<T>(string key)
        {
            Application.Current.Resources.TryGetValue(key, out var resourceValue);
            return (T)resourceValue;
        }
    }
}
