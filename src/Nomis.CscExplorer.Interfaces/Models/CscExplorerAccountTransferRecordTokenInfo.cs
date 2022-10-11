using System.Text.Json.Serialization;

namespace Nomis.CscExplorer.Interfaces.Models
{
    /// <summary>
    /// CSC Explorer transfer record token info.
    /// </summary>
    public class CscExplorerAccountTransferRecordTokenInfo
    {
        /// <summary>
        /// Contract.
        /// </summary>
        [JsonPropertyName("contract")]
        public string? Contract { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Symbol.
        /// </summary>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }
    }
}