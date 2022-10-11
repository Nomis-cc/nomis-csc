using System.Text.Json.Serialization;

namespace Nomis.CscExplorer.Interfaces.Models
{
    /// <summary>
    /// CSC Explorer account data.
    /// </summary>
    public class CscExplorerAccountData
    {
        /// <summary>
        /// Address.
        /// </summary>
        [JsonPropertyName("address")]
        public CscExplorerAccountAddress? Address { get; set; }

        /// <summary>
        /// Balance.
        /// </summary>
        [JsonPropertyName("balance")]
        public string? Balance { get; set; }
    }
}