using Nomis.CscExplorer.Interfaces.Models;

namespace Nomis.CscExplorer.Interfaces
{
    /// <summary>
    /// CscExplorer client.
    /// </summary>
    public interface ICscExplorerClient
    {
        /// <summary>
        /// Get the account balance.
        /// </summary>
        /// <param name="address">Account address.</param>
        /// <returns>Returns <see cref="CscExplorerAccount"/>.</returns>
        Task<CscExplorerAccount> GetBalanceAsync(string address);

        /// <summary>
        /// Get the account tokens.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        Task<CscExplorerAccountTokens> GetTokensAsync(string address);

        /// <summary>
        /// Get list of specific transactions of the given account.
        /// </summary>
        /// <typeparam name="TResult">The type of returned response.</typeparam>
        /// <typeparam name="TData">The type of transfer data.</typeparam>
        /// <typeparam name="TRecord">The type of transfer record.</typeparam>
        /// <param name="address">Account address.</param>
        /// <returns>Returns list of specific transactions of the given account.</returns>
        Task<IEnumerable<TRecord>> GetTransactionsAsync<TResult, TData, TRecord>(string address)
            where TResult : ICscExplorerTransferList<TData, TRecord>
            where TData : ICscExplorerTransferData<TRecord>
            where TRecord : ICscExplorerTransferRecord;
    }
}