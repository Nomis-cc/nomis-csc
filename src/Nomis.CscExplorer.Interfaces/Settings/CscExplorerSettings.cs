using Nomis.Utils.Contracts.Common;

namespace Nomis.CscExplorer.Interfaces.Settings
{
    /// <summary>
    /// CscExplorer settings.
    /// </summary>
    public class CscExplorerSettings :
        ISettings
    {
        /// <summary>
        /// API key for CSC Explorer.
        /// </summary>
        public string? ApiKey { get; set; }

        /// <summary>
        /// CscExplorer API base URL.
        /// </summary>
        public string? ApiBaseUrl { get; set; }
    }
}