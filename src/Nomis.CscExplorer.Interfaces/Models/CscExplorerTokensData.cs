using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global
namespace Nomis.CscExplorer.Interfaces.Models
{
    /// <summary>
    /// CSC Explorer account tokens data.
    /// </summary>
    public class CscExplorerTokensData
    {
        /// <summary>
        /// CRC20 tokens list.
        /// </summary>
        [JsonPropertyName("crc20")]
        public List<CscExplorerTokensDataItem> Crc20List { get; set; } = new();

        /// <summary>
        /// CRC721 tokens list.
        /// </summary>
        [JsonPropertyName("crc721")]
        public List<CscExplorerTokensDataItem> Crc721List { get; set; } = new();
    }
}