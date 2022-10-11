using Microsoft.Extensions.Options;
using Nomis.CscExplorer.Interfaces.Models;
using Nomis.Utils.Wrapper;
using Nomis.Web.Client.Common.Extensions;
using Nomis.Web.Client.Common.Settings;
using Nomis.Web.Client.Csc.Routes;

namespace Nomis.Web.Client.Csc.Managers
{
    /// <inheritdoc cref="ICscManager" />
    public class CscManager :
        ICscManager
    {
        private readonly HttpClient _httpClient;
        private readonly CscEndpoints _endpoints;

        /// <summary>
        /// Initialize <see cref="CscManager"/>.
        /// </summary>
        /// <param name="webApiSettings"><see cref="WebApiSettings"/>.</param>
        public CscManager(
            IOptions<WebApiSettings> webApiSettings)
        {
            _httpClient = new()
            {
                BaseAddress = new(webApiSettings.Value?.ApiBaseUrl ?? throw new ArgumentNullException(nameof(webApiSettings.Value.ApiBaseUrl)))
            };
            _endpoints = new(webApiSettings.Value?.ApiBaseUrl ?? throw new ArgumentNullException(nameof(webApiSettings.Value.ApiBaseUrl)));
        }

        /// <inheritdoc />
        public async Task<IResult<CscWalletScore>> GetWalletScoreAsync(string address)
        {
            var response = await _httpClient.GetAsync(_endpoints.GetWalletScore(address));
            return await response.ToResultAsync<CscWalletScore>();
        }
    }
}