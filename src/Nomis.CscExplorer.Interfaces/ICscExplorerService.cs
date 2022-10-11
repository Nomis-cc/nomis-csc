using Nomis.CscExplorer.Interfaces.Models;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.CscExplorer.Interfaces
{
    /// <summary>
    /// CSC Explorer service.
    /// </summary>
    public interface ICscExplorerService :
        IInfrastructureService
    {
        /// <summary>
        /// Client for interacting with CSC Explorer API.
        /// </summary>
        public ICscExplorerClient Client { get; }

        /// <summary>
        /// Get CSC wallet stats by address.
        /// </summary>
        /// <param name="address">CSC wallet address.</param>
        /// <returns>Returns <see cref="CscWalletScore"/> result.</returns>
        public Task<Result<CscWalletScore>> GetWalletStatsAsync(string address);
    }
}