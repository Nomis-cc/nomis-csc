using Nomis.Web.Client.Common.Routes;

namespace Nomis.Web.Client.Csc.Routes
{
    /// <summary>
    /// CSC endpoints.
    /// </summary>
    public class CscEndpoints :
        BaseEndpoints
    {
        /// <summary>
        /// Initialize <see cref="CscEndpoints"/>.
        /// </summary>
        /// <param name="baseUrl">CSC API base URL.</param>
        public CscEndpoints(string baseUrl) 
            : base(baseUrl)
        {
        }

        /// <inheritdoc/>
        public override string Blockchain => "csc";
    }
}
