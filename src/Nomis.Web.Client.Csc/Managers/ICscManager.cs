using Nomis.CscExplorer.Interfaces.Models;
using Nomis.Utils.Wrapper;
using Nomis.Web.Client.Common.Managers;

namespace Nomis.Web.Client.Csc.Managers
{
    /// <summary>
    /// CSC manager.
    /// </summary>
    public interface ICscManager :
        IManager
    {
        /// <summary>
        /// Get CSC wallet score.
        /// </summary>
        /// <param name="address">Wallet address.</param>
        /// <returns>Returns result of <see cref="CscWalletScore"/>.</returns>
        Task<IResult<CscWalletScore>> GetWalletScoreAsync(string address);
    }
}