using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Nomis.CscExplorer.Interfaces.Models
{
    /// <summary>
    /// CSC Explorer transfers data.
    /// </summary>
    /// <typeparam name="TData">CSC Explorer transfer data.</typeparam>
    /// <typeparam name="TRecord">CSC Explorer transfer record.</typeparam>
    public interface ICscExplorerTransferList<TData, TRecord>
        where TRecord : ICscExplorerTransferRecord
        where TData : ICscExplorerTransferData<TRecord>
    {
        /// <summary>
        /// Transfers data.
        /// </summary>
        [JsonPropertyName("data")]
        [DataMember(EmitDefaultValue = true)]
        public TData? Data { get; set; }
    }
}