using System.Globalization;
using System.Net;

using Nethereum.Util;
using Nomis.CscExplorer.Calculators;
using Nomis.CscExplorer.Interfaces;
using Nomis.CscExplorer.Interfaces.Models;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Exceptions;
using Nomis.Utils.Wrapper;

namespace Nomis.CscExplorer
{
    /// <inheritdoc cref="ICscExplorerService"/>
    internal sealed class CscExplorerService :
        ICscExplorerService,
        ITransientService
    {
        /// <summary>
        /// Initialize <see cref="CscExplorerService"/>.
        /// </summary>
        /// <param name="client"><see cref="ICscExplorerClient"/>.</param>
        public CscExplorerService(
            ICscExplorerClient client)
        {
            Client = client;
        }

        /// <inheritdoc/>
        public ICscExplorerClient Client { get; }

        /// <inheritdoc/>
        public async Task<Result<CscWalletScore>> GetWalletStatsAsync(string address)
        {
            if (!new AddressUtil().IsValidAddressLength(address) || !new AddressUtil().IsValidEthereumAddressHexFormat(address))
            {
                throw new CustomException("Invalid address", statusCode: HttpStatusCode.BadRequest);
            }

            var balance = (await Client.GetBalanceAsync(address)).Data?.Balance;
            var transactions = (await Client.GetTransactionsAsync<CscExplorerAccountTransactions, CscExplorerAccountTransactionData, CscExplorerAccountTransactionRecord>(address)).ToList();
            var cetTransfers = (await Client.GetTransactionsAsync<CscExplorerAccountCetTransfers, CscExplorerAccountCetTransferData, CscExplorerAccountCetTransferRecord>(address)).ToList();
            var crc20Transfers = (await Client.GetTransactionsAsync<CscExplorerAccountCrc20Transfers, CscExplorerAccountCrc20TransferData, CscExplorerAccountCrc20TransferRecord>(address)).ToList();
            var crc721Transfers = (await Client.GetTransactionsAsync<CscExplorerAccountCrc721Transfers, CscExplorerAccountCrc721TransferData, CscExplorerAccountCrc721TransferRecord>(address)).ToList();

            var walletStats = new CscStatCalculator(
                    address,
                    decimal.TryParse(balance, NumberStyles.AllowDecimalPoint, new NumberFormatInfo { CurrencyDecimalSeparator = "." }, out var balanceValue) ? balanceValue : 0,
                    transactions,
                    cetTransfers,
                    crc20Transfers,
                    crc721Transfers)
                .GetStats();

            return await Result<CscWalletScore>.SuccessAsync(new()
            {
                Stats = walletStats,
                Score = walletStats.GetScore()
            }, "Got CSC wallet score.");
        }
    }
}