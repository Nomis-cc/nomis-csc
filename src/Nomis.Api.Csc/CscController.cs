using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nomis.Api.Csc.Abstractions;
using Nomis.CscExplorer.Interfaces;
using Nomis.CscExplorer.Interfaces.Models;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.Csc
{
    /// <summary>
    /// A controller to aggregate all CSC-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Csc.")]
    internal sealed partial class CscController :
        CscBaseController
    {
        private readonly ILogger<CscController> _logger;
        private readonly ICscExplorerService _cscExplorerService;

        /// <summary>
        /// Initialize <see cref="CscController"/>.
        /// </summary>
        /// <param name="cscExplorerService"><see cref="ICscExplorerService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public CscController(
            ICscExplorerService cscExplorerService,
            ILogger<CscController> logger)
        {
            _cscExplorerService = cscExplorerService ?? throw new ArgumentNullException(nameof(cscExplorerService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get Nomis Score for given wallet address.
        /// </summary>
        /// <param name="address" example="0x1c1BDADD6b167f4A60dfECcC525534Bf0f5BF323">CSC wallet address to get Nomis Score.</param>
        /// <returns>An NomisScore value and corresponding statistical data.</returns>
        /// <remarks>
        /// Sample request:
        ///     GET /api/v1/csc/wallet/0x1c1BDADD6b167f4A60dfECcC525534Bf0f5BF323/score
        /// </remarks>
        /// <response code="200">Returns Nomis Score and stats.</response>
        /// <response code="400">Address not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("wallet/{address}/score", Name = "GetCscWalletScore")]
        [AllowAnonymous]
        [SwaggerOperation(
            OperationId = "GetCscWalletScore",
            Tags = new[] { CscTag })]
        [ProducesResponseType(typeof(Result<CscWalletScore>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetCscWalletScoreAsync(
            [Required(ErrorMessage = "Wallet address should be set")] string address)
        {
            var result = await _cscExplorerService.GetWalletStatsAsync(address);
            return Ok(result);
        }
    }
}