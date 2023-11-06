using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamBuilder.Services.Network
{
    /// <summary>
    /// The network service.
    /// </summary>
    public class NetworkService : INetworkService
    {
        /// <summary>
        /// Has network.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool HasNetwork() => Connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}
