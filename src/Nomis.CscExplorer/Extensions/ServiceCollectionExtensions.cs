using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.Blockchain.Abstractions.Settings;
using Nomis.CscExplorer.Interfaces;
using Nomis.CscExplorer.Interfaces.Settings;
using Nomis.Utils.Extensions;

namespace Nomis.CscExplorer.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add CscExplorer service.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddCscExplorerService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSettings<CscExplorerSettings>(configuration);
            var settings = configuration.GetSettings<ApiVisibilitySettings>();
            if (settings.CscAPIEnabled)
            {
                return services
                    .AddTransient<ICscExplorerClient, CscExplorerClient>()
                    .AddTransientInfrastructureService<ICscExplorerService, CscExplorerService>();
            }
            else
            {
                return services;
            }
        }
    }
}