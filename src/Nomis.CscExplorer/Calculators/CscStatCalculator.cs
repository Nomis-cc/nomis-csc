using System.Numerics;

using Nomis.CscExplorer.Extensions;
using Nomis.CscExplorer.Interfaces.Models;
using Nomis.Utils.Extensions;

namespace Nomis.CscExplorer.Calculators
{
    /// <summary>
    /// Csc wallet stats calculator.
    /// </summary>
    internal sealed class CscStatCalculator
    {
        private readonly string _address;
        private readonly decimal _balance;
        private readonly IEnumerable<CscExplorerAccountTransactionRecord> _transactions;
        private readonly IEnumerable<CscExplorerAccountCetTransferRecord> _cetTransfers;
        private readonly IEnumerable<CscExplorerAccountCrc20TransferRecord> _crc20Transfers;
        private readonly IEnumerable<CscExplorerAccountCrc721TransferRecord> _crc721Transfers;

        public CscStatCalculator(
            string address,
            decimal balance,
            IEnumerable<CscExplorerAccountTransactionRecord> transactions,
            IEnumerable<CscExplorerAccountCetTransferRecord> cetTransfers,
            IEnumerable<CscExplorerAccountCrc20TransferRecord> crc20Transfers,
            IEnumerable<CscExplorerAccountCrc721TransferRecord> crc721Transfers)
        {
            _address = address;
            _balance = balance;
            _transactions = transactions;
            _cetTransfers = cetTransfers;
            _crc20Transfers = crc20Transfers;
            _crc721Transfers = crc721Transfers;
        }

        private int GetWalletAge()
        {
            var firstTransaction = _transactions.OrderBy(x => x.TimeStamp).First();
            return (int)((DateTime.UtcNow - firstTransaction.TimeStamp.ToString().ToDateTime()).TotalDays / 30);
        }

        private IEnumerable<double> GetTransactionsIntervals()
        {
            var result = new List<double>();
            DateTime? lastDateTime = null;
            foreach (var transaction in _transactions.OrderByDescending(x => x.TimeStamp))
            {
                var transactionDate = transaction.TimeStamp.ToString().ToDateTime();
                if (!lastDateTime.HasValue)
                {
                    lastDateTime = transactionDate;
                    continue;
                }

                var interval = Math.Abs((transactionDate - lastDateTime.Value).TotalHours);
                lastDateTime = transactionDate;
                result.Add(interval);
            }

            return result;
        }

        private BigInteger GetTokensSum(IEnumerable<CscExplorerAccountCrc721TransferRecord> tokenList)
        {
            var transactions = tokenList.Select(x => x.TxHash).ToHashSet();
            var result = new BigInteger();
            foreach (var st in _transactions.Where(x => transactions.Contains(x.TxHash)))
            {
                if (ulong.TryParse(st.Amount, out var value))
                {
                    result += value;
                }
            }

            return result;
        }

        public CscWalletStats GetStats()
        {
            if (!_transactions.Any())
            {
                return new()
                {
                    NoData = true
                };
            }

            var intervals = GetTransactionsIntervals().ToList();
            if (!intervals.Any())
            {
                return new()
                {
                    NoData = true
                };
            }

            var monthAgo = DateTime.Now.AddMonths(-1);

            var soldTokens = _crc721Transfers.Where(x => x.From?.Address?.Equals(_address, StringComparison.InvariantCultureIgnoreCase) == true).ToList();
            var soldSum = GetTokensSum(soldTokens);

            var soldTokensIds = soldTokens.Select(x => x.GetTokenUid());
            var buyTokens = _crc721Transfers.Where(x => x.To?.Address?.Equals(_address, StringComparison.InvariantCultureIgnoreCase) == true && soldTokensIds.Contains(x.GetTokenUid()));
            var buySum = GetTokensSum(buyTokens);

            var buyNotSoldTokens = _crc721Transfers.Where(x => x.To?.Address?.Equals(_address, StringComparison.InvariantCultureIgnoreCase) == true && !soldTokensIds.Contains(x.GetTokenUid()));
            var buyNotSoldSum = GetTokensSum(buyNotSoldTokens);

            var holdingTokens = _crc721Transfers.Count() - soldTokens.Count;
            var nftWorth = buySum == 0 ? 0 : (decimal)soldSum / (decimal)buySum * (decimal)buyNotSoldSum;
            var contractsCreated = _transactions.Count(x => x.Method?.Equals("Create Contract") == true && x.Status == 1);
            var totalTokens = _crc20Transfers.Select(x => x.TokenInfo?.Symbol).Distinct();

            return new()
            {
                Balance = _balance,
                WalletAge = GetWalletAge(),
                TotalTransactions = _transactions.Count(),
                MinTransactionTime = intervals.Min(),
                MaxTransactionTime = intervals.Max(),
                AverageTransactionTime = intervals.Average(),
                WalletTurnover = _transactions.Sum(x =>
                {
                    if (ulong.TryParse(x.Amount, out var value))
                    {
                        return (decimal)value;
                    }

                    return (decimal)0;
                }),
                LastMonthTransactions = _transactions.Count(x => x.TimeStamp.ToString().ToDateTime() > monthAgo),
                TimeFromLastTransaction = (int)((DateTime.UtcNow - _transactions.Last().TimeStamp.ToString().ToDateTime()).TotalDays / 30),
                NftHolding = holdingTokens,
                NftTrading = (decimal)(soldSum - buySum),
                NftWorth = nftWorth,
                DeployedContracts = contractsCreated,
                TokensHolding = totalTokens.Count()
            };
        }
    }
}