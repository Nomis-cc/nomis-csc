using System.Text.Json.Serialization;

namespace Nomis.CscExplorer.Interfaces.Models
{
    /// <summary>
    /// CSC Explorer transfer record.
    /// </summary>
    public interface ICscExplorerTransferRecord
    {
        /// <summary>
        /// From.
        /// </summary>
        [JsonPropertyName("from")]
        public CscExplorerAccountAddress? From { get; set; }

        /// <summary>
        /// Height.
        /// </summary>
        [JsonPropertyName("height")]
        public long Height { get; set; }

        /// <summary>
        /// Timestamp.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public long TimeStamp { get; set; }

        /// <summary>
        /// To.
        /// </summary>
        [JsonPropertyName("to")]
        public CscExplorerAccountAddress? To { get; set; }

        /// <summary>
        /// Transaction hash.
        /// </summary>
        [JsonPropertyName("tx_hash")]
        public string? TxHash { get; set; }
    }
}